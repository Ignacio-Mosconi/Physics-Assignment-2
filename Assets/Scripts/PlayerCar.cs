﻿using UnityEngine;
using PhysicsUtilities;

[RequireComponent(typeof(BoundingBox))]
public class PlayerCar : MonoBehaviour
{
    [SerializeField] [Range(2f, 3f)] float wheelAcceleration = 2f;
    [SerializeField] [Range(30f, 45f)] float maxWheelSpeed = 30f;
    [SerializeField] [Range(0.5f, 1.5f)] float wheelRadius = 0.5f;
    [SerializeField] [Range(0.5f, 1f)] float frictionAcceleration = 0.5f;

    BoundingBox boundingBox;
    Vector3 initialPosition;
    float[] wheelsAccels = { 0f, 0f };
    float[] wheelsSpeeds = { 0f, 0f };

    void Awake()
    {
        boundingBox = GetComponent<BoundingBox>();
        boundingBox.OnTrigger.AddListener(OnTriggerCollisionDetected);
    }

    void Start()
    {
        initialPosition = transform.position;
        wheelsAccels[0] = -frictionAcceleration;
        wheelsAccels[1] = -frictionAcceleration;
    }

    void Update()
    {
        if (Input.GetButtonDown("Left Acceleration") || Input.GetButtonUp("Left Acceleration"))
        {
            int leftInput = (int)Input.GetAxisRaw("Left Acceleration");
            
            switch (leftInput)
            {
                case -1:
                    wheelsAccels[0] = -wheelAcceleration;
                    break;
                case 1:
                    wheelsAccels[0] = wheelAcceleration;
                    break;
                default:
                    if (wheelsAccels[0] > 0f)
                        wheelsAccels[0] = -frictionAcceleration;
                    else
                        wheelsAccels[0] = frictionAcceleration;
                break;
            }
        }

        if (Input.GetButtonDown("Right Acceleration") || Input.GetButtonUp("Right Acceleration"))
        {
            int rightInput = (int)Input.GetAxisRaw("Right Acceleration");
            
            switch (rightInput)
            {
                case -1:
                    wheelsAccels[1] = -wheelAcceleration;
                    break;
                case 1:
                    wheelsAccels[1] = wheelAcceleration;
                    break;
                default:
                    if (wheelsAccels[1] > 0f)
                        wheelsAccels[1] = -frictionAcceleration;
                    else
                        wheelsAccels[1] = frictionAcceleration;
                break;
            }
        }

        float minLeftWheelSpeed = (wheelsAccels[0] == -frictionAcceleration) ? 0f : -maxWheelSpeed;
        float minRightWheelSpeed = (wheelsAccels[1] == -frictionAcceleration) ? 0f : -maxWheelSpeed;
        float maxLeftWheelSpeed = (wheelsAccels[0] == frictionAcceleration) ? 0f : maxWheelSpeed;
        float maxRightWheelSpeed = (wheelsAccels[1] == frictionAcceleration) ? 0f : maxWheelSpeed;

        PhysicalMotions.ConstantAccelerationCircular2D(wheelRadius, wheelsAccels[0], ref wheelsSpeeds[0], 
                                                        minLeftWheelSpeed, maxLeftWheelSpeed);
        PhysicalMotions.ConstantAccelerationCircular2D(wheelRadius, wheelsAccels[1], ref wheelsSpeeds[1], 
                                                        minRightWheelSpeed, maxRightWheelSpeed);

        float carSpeedLeft = wheelRadius * wheelsSpeeds[0];
        float carSpeedRight = wheelRadius * wheelsSpeeds[1];

        Vector3 carDirLeft = Mathf.Sign(carSpeedLeft) * transform.up - transform.right;
        Vector3 carDirRight = Mathf.Sign(carSpeedRight) * transform.up + transform.right;

        PhysicalMotions.Linear(transform, carDirLeft, Mathf.Abs(carSpeedLeft));
        PhysicalMotions.Linear(transform, carDirRight, Mathf.Abs(carSpeedRight));

        ClampPosition();
    }

    void ClampPosition()
    {
        ObstacleManager obstacleManager = ObstacleManager.Instance;

        float halfBBWidth = boundingBox.Width * 0.5f;
        float halfBBHeight = boundingBox.Height * 0.5f;

        float leftBound = obstacleManager.GetRoadBound(Boundary.Left) + halfBBWidth;
        float rightBound = obstacleManager.GetRoadBound(Boundary.Right) - halfBBWidth;
        float topBound = obstacleManager.GetRoadBound(Boundary.Top) - halfBBHeight;
        float bottomBound = obstacleManager.GetRoadBound(Boundary.Bottom) + halfBBHeight;

        float newPosX = Mathf.Clamp(transform.position.x, leftBound, rightBound);
        float newPosY = Mathf.Clamp(transform.position.y, bottomBound, topBound);

        transform.position = new Vector3(newPosX, newPosY, transform.position.z);            
    }

    void Respawn()
    {
        transform.position = initialPosition;
        wheelsAccels[0] = -frictionAcceleration;
        wheelsAccels[1] = -frictionAcceleration;
        wheelsSpeeds[0] = 0f;
        wheelsSpeeds[1] = 0f;
    }

    void OnTriggerCollisionDetected(CustomCollider2D collider)
    {
        LayerMask colliderLayerMask = LayerMask.GetMask(LayerMask.LayerToName(collider.gameObject.layer));
        
        if (colliderLayerMask == ObstacleManager.Instance.ObstaclesLayerMask)
        {
            Respawn();
            ObstacleManager.Instance.RespawnObstacle(collider.transform);
        }
    }
}