using UnityEngine;
using PhysicsUtilities;

public class TrapMovement : MonoBehaviour
{
    [SerializeField] Transform[] satellites;
    [SerializeField] [Range(36f, 180f)] float[] angularSpeeds;
    [SerializeField] [Range(1f, 10f)] float[] radiuses;

    float[] angles;

    void Awake()
    {
        angles = new float[satellites.GetLength(0)];
    }

    void Update()
    {
        for (int i = 0; i < satellites.GetLength(0); i++)
            PhysicalMotions.UniformCircular2D(transform, satellites[i], radiuses[i], angularSpeeds[i], ref angles[i]);
    }
}