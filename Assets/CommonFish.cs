using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CommonFish : LifeForm
{
    public float swimSpeed = 2.0f;
    public float rotationSpeed = 2.0f;

    [SerializeField] Transform biomaArea;
    [SerializeField] Vector3 targetPosition;

    private Rigidbody rb;
    void Start()
    {
        SetRandomTarget();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        
        MoveTowardsTarget();

        var rot = transform.localEulerAngles;
        rot.z = 0;
        transform.localEulerAngles = rot;
    }
    
    private void MoveTowardsTarget()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetPosition - transform.position), rotationSpeed * Time.deltaTime);
        rb.velocity = transform.forward * swimSpeed;

        // Verificar se o peixe está perto do destino
        if (Vector3.Distance(transform.position, targetPosition) < 1.0f)
        {
            SetRandomTarget();
        }
    }

    private void SetRandomTarget()
    {
        // Definir um novo destino aleatório dentro da área do bioma
        float randomX = Random.Range(biomaArea.position.x - biomaArea.localScale.x / 2 +12, biomaArea.position.x + biomaArea.localScale.x / 2 - 12);
        float randomY = Random.Range(biomaArea.position.y - biomaArea.localScale.y / 2 +12, biomaArea.position.y + biomaArea.localScale.y / 2 - 12);
        float randomZ = Random.Range(biomaArea.position.z - biomaArea.localScale.z / 2 +12, biomaArea.position.z + biomaArea.localScale.z / 2 - 12);
        
        //print(randomX+" "+ randomY+" "+ randomZ);
        targetPosition = new Vector3(randomX, randomY, randomZ);
    }

    private void OnCollisionEnter(Collision other)
    {
        SetRandomTarget();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "RAM")
        {
            targetPosition = transform.position + other.transform.forward * 3;
            targetPosition.y = transform.position.y + Random.Range(-4,4);
            this.swimSpeed *= 5;
            this.rotationSpeed *= 5;
        }
    }
    
    
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "RAM")
        {
            this.swimSpeed /= 5;
            this.rotationSpeed /= 5;
        }
    }
}
