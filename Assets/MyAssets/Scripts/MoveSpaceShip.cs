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
    [SerializeField] private bool right_analog_Hold;

    [SerializeField] private bool left_analog_Hold;

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

        Vector2 rightInput = right_analog_Hold ? right_analog.action.ReadValue<Vector2>() : Vector2.zero;
        Vector2 leftInput = left_analog_Hold ? left_analog.action.ReadValue<Vector2>() : Vector2.zero;

        Vector3 desiredVel;
        desiredVel = transform.forward * leftInput.y * velocitySpeed;
        desiredVel += transform.right * leftInput.x * velocitySpeed;

        transform.position += desiredVel * Time.deltaTime;

        Quaternion rotationDelta = Quaternion.Euler(rightInput.y * angularSpeed, rightInput.x * angularSpeed, 0);
        Quaternion desiredRot = transform.rotation * rotationDelta;

        Vector3 euler = desiredRot.eulerAngles;
        euler.z = 0;
        if (euler.x > 180)
        {
            euler.x = Mathf.Clamp(euler.x, 320, 359.999f);
        }
        else
        {
            euler.x = Mathf.Clamp(euler.x, 0, 40);
        }

        desiredRot = Quaternion.Euler(euler);

        transform.rotation = desiredRot;
    }


    public void LeftController(bool b) { left_analog_Hold = b; }
    public void RightController(bool b) { right_analog_Hold = b; }
   
}
