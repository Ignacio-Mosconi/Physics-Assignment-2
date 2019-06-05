using UnityEngine;

public class TreeObstacle : Obstacle
{
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        speed = ObstacleManager.Instance.TreesSpeed;
    }

    public override void Respawn()
    {
        float minHorSpawn;
        float maxHorSpawn;
        float minVerSpawn;
        float maxVerSpawn;

        float halfSRWidth = spriteRenderer.bounds.extents.x;
        float halfSRHeight = spriteRenderer.bounds.extents.y;
        
        bool spawnToTheLeft = (Random.Range(0, 2) == 0) ? true : false;

        if (spawnToTheLeft)
        {
            minHorSpawn = ObstacleManager.Instance.GetViewBound(Boundary.Left) + halfSRWidth;
            maxHorSpawn = ObstacleManager.Instance.GetRoadBound(Boundary.Left) - halfSRWidth;
        }
        else
        {
            minHorSpawn = ObstacleManager.Instance.GetRoadBound(Boundary.Right) + halfSRWidth;
            maxHorSpawn = ObstacleManager.Instance.GetViewBound(Boundary.Right) - halfSRWidth;
        }

        minVerSpawn = ObstacleManager.Instance.GetViewBound(Boundary.Top) + halfSRHeight;
        maxVerSpawn = minVerSpawn * 2f;

        Vector3 spawnPosition = Vector3.zero;

        spawnPosition.x = Random.Range(minHorSpawn, maxHorSpawn);
        spawnPosition.y = Random.Range(minVerSpawn, maxVerSpawn);
        
        transform.position = spawnPosition;
    }
}