using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Boundary
{
    Top, Bottom, Left, Right
}

public class ObstacleManager : MonoBehaviour
{
    static ObstacleManager instance;

    [SerializeField] SpriteRenderer road = default;
    [SerializeField] [Range(1f, 3f)] float treesSpeed = 2f;
    [SerializeField] [Range(1f, 2.5f)] float minCarsSpeed = 1.5f;
    [SerializeField] [Range(2.5f, 5f)] float maxCarsSpeed = 3f;
    [SerializeField] LayerMask obstaclesLayerMask = default;
    [SerializeField] LayerMask playerLayerMask = default;

    Dictionary<Boundary, float> roadBoundaries = new Dictionary<Boundary, float>();
    Dictionary<Boundary, float> viewBoundaries = new Dictionary<Boundary, float>();
    Obstacle[] obstacles;

    void Awake()
    {
        if (Instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        Camera cam = Camera.main;

        Vector3 leftLowerCorner = new Vector3(0f, 0f, 0f);
        Vector3 rightUpperCorner = new Vector3(1f, 1f, 0f);
        float leftRoadBound = -road.bounds.size.x * 0.5f;
        float rightRoadBound = road.bounds.size.x * 0.5f;

        roadBoundaries.Add(Boundary.Left, leftRoadBound);
        roadBoundaries.Add(Boundary.Right, rightRoadBound);
        roadBoundaries.Add(Boundary.Top, cam.ViewportToWorldPoint(rightUpperCorner).y);
        roadBoundaries.Add(Boundary.Bottom, cam.ViewportToWorldPoint(leftLowerCorner).y);
        
        viewBoundaries.Add(Boundary.Left, cam.ViewportToWorldPoint(leftLowerCorner).x);
        viewBoundaries.Add(Boundary.Right, cam.ViewportToWorldPoint(rightUpperCorner).x);
        viewBoundaries.Add(Boundary.Top, cam.ViewportToWorldPoint(rightUpperCorner).y);
        viewBoundaries.Add(Boundary.Bottom, cam.ViewportToWorldPoint(leftLowerCorner).y);

        obstacles = GetComponentsInChildren<Obstacle>();
    }

    void Update()
    {
        foreach (Obstacle obstacle in obstacles)
        {
            obstacle.Move();
            if (obstacle.transform.position.y < viewBoundaries[Boundary.Bottom])
                obstacle.Respawn();
        }
    }

    public void RespawnObstacle(Transform obstacleTransform)
    {
        Obstacle obstacle = obstacleTransform.GetComponent<Obstacle>();
        if (obstacle)
            obstacle.Respawn();
    }

    public float GetViewBound(Boundary boundary)
    {
        return viewBoundaries[boundary];
    }

    public float GetRoadBound(Boundary boundary)
    {
        return roadBoundaries[boundary];
    }

    public float GetRandomCarSpeed()
    {
        return (UnityEngine.Random.Range(minCarsSpeed, maxCarsSpeed));
    }

    public float TreesSpeed
    {
        get { return treesSpeed; }
    }

    public static ObstacleManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<ObstacleManager>();
                if (!instance)
                {
                    GameObject gameObj = new GameObject("Obstacle Manager");
                    instance = gameObj.AddComponent<ObstacleManager>();
                }
            }

            return instance;   
        }
    }

    public LayerMask ObstaclesLayerMask
    {
        get { return obstaclesLayerMask; }
    }

    public LayerMask PlayerLayerMask
    {
        get { return playerLayerMask; }
    }
}