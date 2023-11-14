using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class HingeActionManager : MonoBehaviour
{
    public GameObject hingeObject;
    public float AngleOffset = 2f;

    public bool grabbed = false;
    HingeJoint joint;
    float min, max;

    public UnityEvent OnMinValue;
    public UnityEvent onBetweenValues;
    public UnityEvent OnMaxValue;

    public FishInventory inventory;


    // Update is called once per frame

    private void Start()
    {
        joint = hingeObject.GetComponent<HingeJoint>();
        min = joint.limits.min;
        max = joint.limits.max;
    }
    void Update()
    {
        if (!grabbed) return;
        if (joint.angle <= min + AngleOffset)
        {
            OnMinValue?.Invoke();
        }
        else if(joint.angle >= max - AngleOffset)
        {
            OnMaxValue?.Invoke();
        }
        else
        {
            onBetweenValues?.Invoke();
        }
    }

    public void SetGrabbed (bool grabbed)
    {
        this.grabbed = grabbed;
    }
}

