using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CommonFish : LifeForm
{
    public float swimSpeed = 2.0f;
    public float rotationSpeed = 2.0f;

    public Transform biomaArea;
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
        rb.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetPosition - transform.position), rotationSpeed * Time.deltaTime);
        var distance = targetPosition - transform.position;
        rb.velocity = distance.normalized * swimSpeed;

        // Verificar se o peixe está perto do destino
        if (Vector3.Distance(transform.position, targetPosition) < 1.0f)
        {
            SetRandomTarget();
        }
    }

    void SetRandomTarget()
    {
        // Definir um novo destino aleatório dentro da área do bioma
        float randomX = Random.Range(biomaArea.position.x - biomaArea.localScale.x / 2 , biomaArea.position.x + biomaArea.localScale.x / 2 );
        float randomY = Random.Range(biomaArea.position.y - biomaArea.localScale.y / 2 , biomaArea.position.y + biomaArea.localScale.y / 2);
        float randomZ = Random.Range(biomaArea.position.z - biomaArea.localScale.z / 2 , biomaArea.position.z + biomaArea.localScale.z / 2);
        
        //print(randomX+" "+ randomY+" "+ randomZ);
        targetPosition = new Vector3(randomX, randomY, randomZ);
    }

    private void OnCollisionEnter(Collision other)
    {
        
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        SetRandomTarget();
        MoveTowardsTarget();
    }

}