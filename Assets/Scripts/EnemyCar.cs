using UnityEngine;
using PhysicsUtilities;

[RequireComponent(typeof(BoundingBox))]
public class EnemyCar : Obstacle
{
    BoundingBox boundingBox;

    void Awake()
    {
        boundingBox = GetComponent<BoundingBox>();
        boundingBox.OnTrigger.AddListener(OnTriggerCollisionDetected);
    }

    void Start()
    {
        speed = ObstacleManager.Instance.GetRandomCarSpeed();
    }

    public override void Respawn()
    {
        float halfBBWidth = boundingBox.Width * 0.5f;
        float halfBBHeight = boundingBox.Height * 0.5f;

        float minHorSpawn = ObstacleManager.Instance.GetRoadBound(Boundary.Left) + halfBBWidth;
        float maxHorSpawn = ObstacleManager.Instance.GetRoadBound(Boundary.Right) - halfBBWidth;
        float minVerSpawn = ObstacleManager.Instance.GetViewBound(Boundary.Top) + halfBBHeight;
        float maxVerSpawn = minVerSpawn * 2f;

        Vector3 spawnPosition = Vector3.zero;

        spawnPosition.x = Random.Range(minHorSpawn, maxHorSpawn);
        spawnPosition.y = Random.Range(minVerSpawn, maxVerSpawn);
        
        transform.position = spawnPosition;
        speed = ObstacleManager.Instance.GetRandomCarSpeed();
    }

    void OnTriggerCollisionDetected(CustomCollider2D collider)
    {
        LayerMask colliderLayerMask = LayerMask.GetMask(LayerMask.LayerToName(collider.gameObject.layer));

        if (colliderLayerMask == ObstacleManager.Instance.PlayerLayerMask)
            Respawn();
    }
}