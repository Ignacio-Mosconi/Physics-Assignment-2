using UnityEngine;

public class Car : Obstacle
{
    void Awake()
    {
        speed = ObstacleManager.Instance.GetRandomCarSpeed();
    }

    public override void Respawn()
    {
        float minHorSpawn;
        float maxHorSpawn;
        float minVerSpawn;
        float maxVerSpawn;

        minHorSpawn = ObstacleManager.Instance.GetRoadBound(Boundary.Left);
        maxHorSpawn = ObstacleManager.Instance.GetRoadBound(Boundary.Right);

        minVerSpawn = ObstacleManager.Instance.GetViewBound(Boundary.Top);
        maxVerSpawn = minVerSpawn * 2f;

        Vector3 spawnPosition = Vector3.zero;

        spawnPosition.x = Random.Range(minHorSpawn, maxHorSpawn);
        spawnPosition.y = Random.Range(minVerSpawn, maxVerSpawn);
        
        transform.position = spawnPosition;
        speed = ObstacleManager.Instance.GetRandomCarSpeed();
    }
}