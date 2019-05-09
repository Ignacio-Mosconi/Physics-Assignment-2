using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Boundary
{
    Top, Bottom, Left, Right
}

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] SpriteRenderer road;

    static ObstacleManager instance;

    float[] roadBoundaries;
    float[] leftBoundBoundaries;
    float[] rightBoundBoundaries;

    void Awake()
    {
        if (Instance != this)
            Destroy(gameObject);
    }

    public ObstacleManager Instance
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
}