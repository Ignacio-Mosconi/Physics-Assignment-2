using System.Collections.Generic;
using UnityEngine;
using PhysicsUtilities;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] [Range(1f, 5f)] float movementSpeed = 5f;
    [SerializeField] [Range(2f, 6f)] float jumpSpeed = 6f;
    [SerializeField] [Range(30f, 60f)] float jumpAngle = 45f;
    [SerializeField] [Range(10f, 30f)] float gravity = 20f;
    
    Coroutine jumpingRoutine;
    float groundedY;

    void Awake()
    {
        groundedY = transform.position.y;
    }

    void Update()
    {
        if (jumpingRoutine == null)
        {
            float movement = Input.GetAxisRaw("Horizontal");
            Vector3 movementDir = new Vector3(movement, 0f, 0f);

            Move(movementDir);
            if (Input.GetButtonDown("Jump")) 
                Jump(movementDir);
        }
        else
            if (transform.position.y < groundedY)
                Land();
    }

    void Move(Vector3 dir)
    {
        PhysicalMotions.Linear(transform, dir, movementSpeed);
    }

    void Jump(Vector3 dir)
    {
        float angle;

        if (dir.x == 0f)
            angle = 90f;
        else
        {
            if (dir.x > 0f)
                angle = jumpAngle;
            else
                angle = 180f - jumpAngle;
        }

        jumpingRoutine = StartCoroutine(PhysicalMotions.PerformObliqueShot2D(transform, jumpSpeed, angle, -gravity));
    }

    void Land()
    {
        StopCoroutine(jumpingRoutine);
        jumpingRoutine = null;
        transform.position = new Vector3(transform.position.x, groundedY, transform.position.z);
    }
}