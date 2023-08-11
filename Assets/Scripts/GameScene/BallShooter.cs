using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(TrajectoryRenderer))]
public class BallShooter : MonoBehaviour
{
    [SerializeField] private MenuHandler handler;
    [SerializeField] private AudioHub hub;
    [SerializeField] private Ball ballPrefab;
    [SerializeField] private float speed;
    [SerializeField] Transform startingPosition;
    [SerializeField] private GameObject spawnPoints;
    [SerializeField] private float maxAvailableAngle = 65;

    private Transform currentPosition;
    private TrajectoryRenderer trajectoryRenderer;
    private ColorEnum ballColor;
    private Ball ball;
    private Camera cam;
    private Rigidbody2D ballRb;
    private CircleCollider2D ballCollider;
    private float ballRadius;
    private List<GameObject> points;
    private Vector2 lastValidDirection;

    void Start()
    {
        cam = Camera.main;
        points = spawnPoints.GetComponentsInChildren<Transform>()
            .Select(x => x.gameObject)
            .ToList();
        trajectoryRenderer = GetComponent<TrajectoryRenderer>();
        SetShootPoints();
        InitBall();
    }

    public void SetShootPoints()
    {
        points.ForEach(x => x.SetActive(true));
        currentPosition = startingPosition;
        currentPosition.gameObject.SetActive(false);
    }

    public void InitBall()
    {
        if (ball != null)
            Destroy(ball.gameObject);
        ball = Instantiate(ballPrefab, currentPosition.position, Quaternion.identity);
        ball.InitComponents(handler, hub);
        ballRb = ball.GetComponent<Rigidbody2D>();
        ballCollider = ball.GetComponent<CircleCollider2D>();
        ballCollider.isTrigger = true;
        ballRadius = ballCollider.radius;
        ballColor = ball.GetComponent<ColorManager>().Color;
    }

    private void Update()
    {
        if (LevelManager.IsShooted || LevelManager.IsPaused) 
            return;

        var clickPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        var direction = clickPoint - ball.transform.position;
        direction = ((Vector2)direction).normalized;

        var aimAngle = Vector2.Angle(Vector2.right, direction);
        var isAimCorrect = aimAngle <= maxAvailableAngle;

        if (isAimCorrect)
            lastValidDirection = direction;

        if ((clickPoint - ball.transform.position).x > 0 && isAimCorrect)
        {
            points.ForEach(x => x.layer = 2);
            trajectoryRenderer.RenderTrajectory(ball.transform.position, direction, ballRadius, ballColor);
            points.ForEach(x => x.layer = 0);
        }

        if (!Input.GetMouseButtonDown(0) || clickPoint.x < ball.transform.position.x - ballRadius)
            return;

        var rayHit = Physics2D.Raycast(clickPoint, Vector2.zero);
        if (rayHit)
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if (rayHit.collider.CompareTag("Spawn Point"))
            {
                ChangeBallPosition(rayHit.transform);
                trajectoryRenderer.DeleteTrajectory();
                return;
            }

            if (rayHit.collider.TryGetComponent<Ball>(out var ballPosition))
                return;

            if (clickPoint.x <= ball.transform.position.x)
                return;
        }
        trajectoryRenderer.DeleteTrajectory();
        spawnPoints.SetActive(false);
        ShootBall();
    }

    private void ShootBall()
    {
        ball.Direction = lastValidDirection;
        ball.Speed = speed;
        ballRb.AddForce(lastValidDirection * speed, ForceMode2D.Impulse);
        LevelManager.IsShooted = true;
    }

    private void ChangeBallPosition(Transform newPosition)
    {
        newPosition.gameObject.SetActive(false);
        ball.transform.position = newPosition.position;
        currentPosition.gameObject.SetActive(true);
        currentPosition = newPosition;
    }
}