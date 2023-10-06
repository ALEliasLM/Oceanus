using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class MoveSpaceShip : MonoBehaviour
{
    // Start is called before the first frame update


    [SerializeField] private InputActionProperty right_analog;
    [SerializeField] private InputActionProperty left_analog;
    [SerializeField] private InputActionProperty right_analog_PRESS;
    [SerializeField] private InputActionProperty left_analog_PRESS;

    [SerializeField] private float angularSpeed = 5;
    [SerializeField] private float velocitySpeed = 300;


    private Rigidbody rb;
    
    

    public Coroutine velocityCoroutine, angularVelocityCoroutine;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void Update()
    {
        Vector2 rightInput = right_analog.action.ReadValue<Vector2>();
        Vector2 leftInput = left_analog.action.ReadValue<Vector2>();


        Vector3 desiredVel;
        desiredVel = transform.forward * leftInput.y * velocitySpeed;
        desiredVel += transform.right * leftInput.x * velocitySpeed;

        Quaternion rotationDelta = Quaternion.Euler(rightInput.y * angularSpeed, rightInput.x * angularSpeed, 0);
        Quaternion desiredRot = rb.rotation * rotationDelta;

      /*  Vector3 euler = desiredRot.eulerAngles;
        euler.z = 0;
        //(0 -> 40) (320 -> 359)
        
        desiredRot = Quaternion.Euler(euler);*/
        if(leftInput != Vector2.zero)
            rb.velocity = Vector3.Lerp(rb.velocity, desiredVel, Time.deltaTime);



        if(rightInput != Vector2.zero) {
            var lerped = Quaternion.Lerp(rb.rotation, desiredRot, Time.deltaTime);
            var euler = lerped.eulerAngles;
            euler.z = 0;
            if(euler.x > 180)
            {
                euler.x = Mathf.Clamp(euler.x, 320,359.999f);
            }
            else
            {
                euler.x = Mathf.Clamp(euler.x, 0, 40);
            }
        
            lerped = Quaternion.Euler(euler);

            print(lerped.eulerAngles);
            rb.MoveRotation(lerped);
        }

    } 

   
}
