using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class MoveSpaceShip : MonoBehaviour
{
    // Start is called before the first frame update


    [SerializeField] private InputActionProperty right_analog;
    [SerializeField] private InputActionProperty left_analog;
    
    
    [SerializeField] private const float MAX_SPEED = 100;
    [SerializeField] private const float ACCELERATION_TICK = 50;
    private Rigidbody rb;
    
    private Vector3 desiredVel, desiredRot;

    private Coroutine actualCoroutine;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 rightInput = right_analog.action.ReadValue<Vector2>();
        Vector2 leftInput = left_analog.action.ReadValue<Vector2>();
        float accelerationValue = ACCELERATION_TICK * Time.deltaTime;

        bool inputed = false;

        desiredVel += new Vector3(rightInput.x * accelerationValue, 0, rightInput.y * accelerationValue);
        desiredRot += new Vector3(leftInput.y * accelerationValue, leftInput.x * accelerationValue, 0);
        
        if (Input.GetKey(KeyCode.W))
        {
            if (actualCoroutine != null)
            {
                StopCoroutine(actualCoroutine);
            }

            inputed = true;
            desiredVel = rb.velocity + transform.forward * accelerationValue;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (actualCoroutine != null)
            {
                StopCoroutine(actualCoroutine);
            }
            inputed = true;
            desiredVel = rb.velocity - transform.forward * accelerationValue;
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            inputed = true;
            desiredRot = rb.rotation.eulerAngles + new Vector3(0,Time.deltaTime * 10,0);
        }
        else if (Input.GetKey(KeyCode.A))
        {
           
            inputed = true;
            desiredRot = rb.rotation.eulerAngles - new Vector3(0,Time.deltaTime * 10,0);
        }
        
        if(inputed)
            rb.MoveRotation(quaternion.Euler(desiredRot));
        
        else
        {
            if (actualCoroutine != null)
                actualCoroutine = StartCoroutine(StopShip());
        }
    }

    Vector3 ClampVelocity(Vector3 vel)
    {
        var x = Mathf.Clamp(vel.x, -MAX_SPEED, MAX_SPEED);
        var y = Mathf.Clamp(vel.y, -MAX_SPEED, MAX_SPEED);
        var z = Mathf.Clamp(vel.z, -MAX_SPEED, MAX_SPEED);
        return new Vector3(x, y, z);
    }

    IEnumerator StopShip()
    {
        float timer = 0;
        while (timer < 1)
        {
            timer += Time.deltaTime * 2;
            timer = Mathf.Clamp(timer, 0, 1);
            rb.velocity = Vector3.Lerp(desiredVel, Vector3.zero, timer);
            yield return null;
        }

        desiredVel = Vector3.zero;
    }
}
