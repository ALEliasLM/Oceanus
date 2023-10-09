using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class ScannerGoBRRR : MonoBehaviour
{
    // Start is called before the first frame update

    public InputActionProperty input;
    [SerializeField] private MeshCollider _collider;
    [SerializeField] private MeshRenderer _mesh;
    [SerializeField] private GameObject _intersection;
    private Animator anim;
    private bool played = false;
    private List<LifeForm> lifeFormsOnScan = new();

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (input.action.ReadValue<float>() > .6f || Input.GetKey(KeyCode.Space))
        {
            _collider.enabled = true;
            _mesh.enabled = true;
            _intersection.SetActive(true);
            if (!played)
            {
                played = true;
                anim.enabled = true;
                anim.Play("New Animation",1,0);
            }
        }
        else
        {
            played = false;
            _collider.enabled = false;
            _mesh.enabled = false;
            _intersection.SetActive(false);
            anim.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent<LifeForm>(out var has);
        if (has  != null && !(has.alreadyScanned))
        {
            has.onScan = true;
        }
        
        
    }

    private void OnTriggerExit(Collider other)
    {
        other.TryGetComponent<LifeForm>(out var has);
        if (has  != null)
        {
            has.onScan = false;
        }
    }
    
}
