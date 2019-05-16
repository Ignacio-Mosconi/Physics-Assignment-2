using UnityEngine;

public class Tree : Obstacle
{
    void Awake()
    {
        speed = ObstacleManager.Instance.TreesSpeed;
    }

    public override void Respawn()
    {
        float minHorSpawn;
        float maxHorSpawn;
        float minVerSpawn;
        float maxVerSpawn;
        
        bool spawnToTheLeft = (Random.Range(0, 2) == 0) ? true : false;

        if (spawnToTheLeft)
        {
            minHorSpawn = ObstacleManager.Instance.GetViewBound(Boundary.Left);
            maxHorSpawn = ObstacleManager.Instance.GetRoadBound(Boundary.Left);
        }
        else
        {
            minHorSpawn = ObstacleManager.Instance.GetRoadBound(Boundary.Right);
            maxHorSpawn = ObstacleManager.Instance.GetViewBound(Boundary.Right);
        }

        minVerSpawn = ObstacleManager.Instance.GetViewBound(Boundary.Top);
        maxVerSpawn = minVerSpawn * 2f;

        Vector3 spawnPosition = Vector3.zero;

        spawnPosition.x = Random.Range(minHorSpawn, maxHorSpawn);
        spawnPosition.y = Random.Range(minVerSpawn, maxVerSpawn);
        
        transform.position = spawnPosition;
    }
}