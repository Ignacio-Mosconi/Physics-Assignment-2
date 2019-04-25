using System.Collections.Generic;
using UnityEngine;
using PhysicsUtilities;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float acceleration = 1f;
    [SerializeField] float maxSpeed = 7f;

    Coroutine obliqueMotionRoutine;
    float horAcceleration = 0f;
    float verAcceleration = 0f;
    float initialHorSpeed = 0f;
    float initialVerSpeed = 0f;

    void Update()
    {
        // Linear Motion Test

        // Vector3 direction;
        // float horInput = Input.GetAxisRaw("Horizontal");
        // float verInput = Input.GetAxisRaw("Vertical");

        // direction = new Vector3(horInput, verInput, 0f);

        // PhysicalMovements.LinearMotion(transform, direction, movementSpeed);

        // Linear Motion Test

        // if (Input.GetAxisRaw("Horizontal") > 0f && horAcceleration != acceleration)
        //     horAcceleration = acceleration;
        // if (Input.GetAxisRaw("Horizontal") < 0f && horAcceleration != -acceleration)
        //     horAcceleration = -acceleration;
        // if (Input.GetAxisRaw("Vertical") > 0f && verAcceleration != acceleration)
        //     verAcceleration = acceleration;
        // if (Input.GetAxisRaw("Vertical") < 0f && verAcceleration != -acceleration)
        //     verAcceleration = -acceleration;

        // if (Input.GetKey(KeyCode.S))
        // {
        //     horAcceleration = 0f;
        //     verAcceleration = 0f;
        //     initialHorSpeed = 0f;
        //     initialVerSpeed = 0f;
        // }

        // PhysicalMovements.ConstantAccelerationMotion(transform, AccelerationAxis.Horizontal, ref initialHorSpeed, horAcceleration, maxSpeed);
        // PhysicalMovements.ConstantAccelerationMotion(transform, AccelerationAxis.Vertical, ref initialVerSpeed, verAcceleration, maxSpeed);

        // Oblique Motion Test

        if (Input.GetButtonDown("Fire1") && obliqueMotionRoutine == null)
            obliqueMotionRoutine = StartCoroutine(PhysicalMovements.PerformObliqueMotion2D(transform, 
                                                                                            Random.Range(10f, 15f), 
                                                                                            Random.Range(0f, 180f), 
                                                                                            Physics.gravity.y));
        
        if (Input.GetKey(KeyCode.R))
        {
            if (obliqueMotionRoutine != null)
            {
                StopCoroutine(obliqueMotionRoutine);
                obliqueMotionRoutine = null;
            }
            
            transform.position = Vector3.zero;
        }
    }
}