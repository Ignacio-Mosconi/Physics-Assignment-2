using UnityEngine;
using PhysicsUtilities;

public abstract class Obstacle : MonoBehaviour
{
    protected float speed;

    public abstract void Respawn();

    public void Move()
    {
        PhysicalMotions.Linear(transform, -transform.up, speed);
    }
}