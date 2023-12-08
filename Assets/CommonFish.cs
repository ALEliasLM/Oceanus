using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CommonFish : LifeForm
{
    public float swimSpeed = 2.0f;
    public float rotationSpeed = 2.0f;

    //o que eu quero que tenha? Waypoints para ser alvo em determinados momentos, uma forma aleatoria de se mexer pelos pontos
    public bool randomizeStart = false;
    public List<Vector3> waypoints; //o primeiro deve ser um canto que o peixe trata como casa ou abrigo, o segundo um lugar para se alimentar e o terceiro eh aleatorio
    public List<Vector3> lastwaypoints; //o primeiro deve ser um canto que o peixe trata como casa ou abrigo, o segundo um lugar para se alimentar e o terceiro eh aleatorio

    public Vector3 targetLocation;

    public AStarMesh mesh;

    private Rigidbody rb;

    bool grabbed = false;
    public bool inPanic = false;
    bool onRoute = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lastwaypoints = new();
        if (randomizeStart)
        {
            Vector3 start = mesh.Points[Random.Range(0, mesh.Points.Count - 1)];
            while (start.x < 0) start = mesh.Points[Random.Range(0, mesh.Points.Count - 1)];
            transform.position = start;
        }
        else
        {
            transform.position = waypoints[0];
        }
        targetLocation = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();

        if (grabbed) return;
        if (rb.angularVelocity.magnitude > 1f)
        {
            rb.angularVelocity = Vector3.Lerp(rb.angularVelocity, Vector3.zero, Time.deltaTime * rotationSpeed);
            return;
        }
        if (inPanic)
        {
            StartCoroutine(FollowPath());
            inPanic = false;
            return;
        }else if (onRoute)
        {
            
            return ;
        }
        else
        {
            //print("Navegando pela mesha");
            NavigateThroughMesh();
        }

       
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    private void NavigateThroughMesh()
    {
        if (Vector3.Distance(targetLocation, transform.position) <= .1f)
        {
            rb.velocity = Vector3.zero;
            lastwaypoints.Add(targetLocation);
            //nao temos pra onde ir, devemos pegar uma localizacao aleatoria
            targetLocation = mesh.GetNewRandomDirection(targetLocation);
        }
        else
        {
            rb.velocity = (targetLocation - transform.position).normalized * swimSpeed * Time.deltaTime;
            rb.rotation = Quaternion.Lerp(rb.rotation, Quaternion.LookRotation(targetLocation - transform.position), Time.deltaTime * rotationSpeed);
        }
    }

    private IEnumerator FollowPath()
    {
        onRoute = true;
        var path = mesh.FindPath(targetLocation, waypoints[0]);
        var current = 0;
        while(current < path.Count)
        {
            //print("OnRoute");
            var currentPoint = path[current];
            if(Vector3.Distance(currentPoint, transform.position) <= .25f)
            {
                //print("vou para o ponto: " + (current + 1));
                rb.velocity = Vector3.zero;
                current++;
            }
            else
            {
                rb.velocity = (currentPoint - transform.position).normalized * swimSpeed * Time.deltaTime * 5;
                rb.rotation = Quaternion.Lerp(rb.rotation, Quaternion.LookRotation(currentPoint - transform.position), Time.deltaTime * rotationSpeed * 5) ;
            }
            yield return null;
        }
        onRoute = false;
        inPanic = false;
        targetLocation = path[current - 1];
        print(targetLocation.ToString());
    }
}