using UnityEngine;
using PhysicsUtilities;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class Obstacle : MonoBehaviour
{
    float currentSpeed;
    protected SpriteRenderer spriteRenderer;
    protected float speed;

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public abstract void Respawn();

    public void Move()
    {
        PhysicalMotions.Linear(transform, -transform.up, currentSpeed);
    }

    public void UpdateCurrentSpeed(float addedSpeed)
    {
        currentSpeed = speed + addedSpeed;
    }

    public float Width
    {
        get { return spriteRenderer.bounds.size.x; }
    }

    public float Height
    {
        get { return spriteRenderer.bounds.size.y; }
    }
}