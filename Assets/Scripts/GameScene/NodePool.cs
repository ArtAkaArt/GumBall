using UnityEngine;

public class NodePool : ObjectPool
{
    [SerializeField] private TrajectoryNode nodePrefab;
    private void Awake() => Initialize(nodePrefab.gameObject);

    public void ActivateNode(Vector2 startPosition, Vector2 lineEndPosition, ColorEnum lineColor, ColorEnum ballColor,
        bool isDestroyed = false)
    {
        var getResult = TryGetObject(out var node);
        if (!getResult)
            return;

        node.SetActive(true);
        var nodeScript = node.GetComponent<TrajectoryNode>();
        nodeScript.SetPositions(startPosition, lineEndPosition, lineColor, ballColor);
        if (isDestroyed)
            nodeScript.RenderDestroyedSprite();
    }
}