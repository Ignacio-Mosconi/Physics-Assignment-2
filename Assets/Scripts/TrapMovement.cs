using UnityEngine;
using PhysicsUtilities;

public class TrapMovement : MonoBehaviour
{
    [SerializeField] Transform[] satellites;
    [SerializeField] [Range(36f, 180f)] float[] angularSpeeds;
    [SerializeField] [Range(1f, 10f)] float[] radiuses;
    [SerializeField] [Range(1f, 20f)] float[] accelerations;
    [SerializeField] [Range(180f, 720f)] float[] maxSpeeds;

    float[] angles;

    void Awake()
    {
        angles = new float[satellites.GetLength(0)];
    }

    void Update()
    {
        for (int i = 0; i < satellites.GetLength(0); i++)
            PhysicalMotions.ConstantAccelerationCircular2D(transform, satellites[i], radiuses[i], accelerations[i], 
                                                            ref angularSpeeds[i], ref angles[i], maxSpeeds[i]);
    }
}