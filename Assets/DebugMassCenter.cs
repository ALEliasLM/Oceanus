using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugMassCenter : MonoBehaviour
{

    public List<Rigidbody> rigidbodies;
    public List<TextMeshProUGUI> texts;
    public GameObject centroDeMassaPrefab;

    List<GameObject> centroids = new();

    private void Start()
    {
        for(int i = 0; i < rigidbodies.Count; i++)
        {
            centroids.Add(Instantiate(centroDeMassaPrefab, rigidbodies[i].centerOfMass, Quaternion.identity));
        }
    }

    void Update()
    {
        for (int i = 0; i < rigidbodies.Count; i++)
        {
            centroids[i].transform.position = rigidbodies[i].worldCenterOfMass;
            texts[i].text = rigidbodies[i].centerOfMass.ToString();

        }
    }
    
}
