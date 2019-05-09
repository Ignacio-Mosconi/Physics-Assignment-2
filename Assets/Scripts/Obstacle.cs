using UnityEngine;
using PhysicsUtilities;

public abstract class Obstacle : MonoBehaviour
{
    [SerializeField] float speed;

    public abstract void Respawn();

    public void Move()
    {
        PhysicalMotions.Linear(transform, transform.forward, speed);
    }
}