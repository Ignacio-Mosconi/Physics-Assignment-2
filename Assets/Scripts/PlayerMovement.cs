﻿using System.Collections.Generic;
using UnityEngine;
using PhysicsUtilities;

[RequireComponent(typeof(BoundingBox))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] [Range(1f, 5f)] float movementSpeed = 5f;
    [SerializeField] [Range(2f, 6f)] float jumpSpeed = 6f;
    [SerializeField] [Range(30f, 60f)] float jumpAngle = 45f;
    [SerializeField] [Range(10f, 30f)] float gravity = 20f;
    [SerializeField] LayerMask trapLayer;
    
    Coroutine jumpingRoutine;
    Vector3 initialPosition;

    void Awake()
    {
        BoundingBox boundingBox = GetComponent<BoundingBox>();
        boundingBox.OnTrigger.AddListener(OnCollisionTriggered);

        initialPosition = transform.position;
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
            if (transform.position.y < initialPosition.y)
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
        transform.position = new Vector3(transform.position.x, initialPosition.y, transform.position.z);
    }

    void OnCollisionTriggered(CustomCollider2D collider)
    {
        if (LayerMask.GetMask(LayerMask.LayerToName(collider.gameObject.layer)) == trapLayer)
        {
            if (jumpingRoutine != null)
                Land();
            transform.position = initialPosition;
        }
    }
}