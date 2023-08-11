using UnityEngine;

public class TrajectoryRenderer : MonoBehaviour
{
    [SerializeField] private float trajectoryLength;
    [SerializeField] private NodePool pool;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask colorLayer;

    private ColorEnum currentColor;
    private Ray ray;


    public void RenderTrajectory(Vector2 ballPosition, Vector2 direction, float ballRadius, ColorEnum ballColor)
    {
        pool.ResetNodes();
        Physics2D.queriesStartInColliders = false;
        ray = new Ray(ballPosition, direction);
        var remainingLength = trajectoryLength;
        currentColor = ballColor;
        while (remainingLength > 0)
        {
            var hit = Physics2D.CircleCast(ray.origin, ballRadius, ray.direction, remainingLength, wallLayer);

            if (hit)
            {
                var nextPoint = hit.point - -hit.normal * ballRadius;
                if (hit.collider.TryGetComponent<Goal>(out var goal) || hit.normal.IsEdge())
                {
                    pool.ActivateNode(nextPoint, ray.origin, currentColor, currentColor, true);
                    break;
                }

                if (!hit.collider.TryGetComponent(out ColorManager manager))
                    manager = Physics2D.Raycast(nextPoint, -hit.normal, 1f, colorLayer).collider
                        .GetComponent<ColorManager>();


                remainingLength -= Vector2.Distance(ray.origin, nextPoint);
                pool.ActivateNode(nextPoint, ray.origin, currentColor, manager.Color);
                currentColor = manager.Color;
                var reflect = Vector2.Reflect(ray.direction, hit.normal);
                ray = new Ray(nextPoint, reflect);
            }
            else
            {
                pool.ActivateNode(ray.origin + ray.direction * remainingLength, ray.origin, currentColor, currentColor);
                remainingLength = 0;
            }
        }

        Physics2D.queriesStartInColliders = true;
    }

    public void DeleteTrajectory() => pool.ResetNodes();
}