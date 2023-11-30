using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

[ExecuteInEditMode]
public class AStarMesh : MonoBehaviour
{
    public GameObject DebugObject;
    public int Height = 3;
    public int Width = 10, Depth = 10;
    public List<Vector3> Points;
    public bool visualize = true;
    public float radious = 1;

    [Button]
    public void Bake()
    {
        Points = new List<Vector3>();
        while(transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
        for (int i = 0; i < Height; i++)
        {
            GeneratePoints(Points, visualize, i, radious);
        }
        print(Points.Count);
    }

    public Vector3 start, end;
    [Button]

    public void MostrarCaminho()
    {
        Console.Clear();
        print(Points.Count);
        foreach(var v in FindPath(start, end))
        {
            print(v);
        }
    }


    public void GeneratePoints(List<Vector3> Points, bool visualize, int Height,  float test_Radious = 1f)
    {
        for (int i = 0; i < Width; i++)
        {
            for (int ii = 0; ii < Depth; ii++)
            {
                var pos = new Vector3(ii, Height, i) + transform.position;

                var go = visualize ? Instantiate(DebugObject, pos, Quaternion.identity, transform) : null;
                if (go != null)
                    go.name = pos.ToString();

                var col = Physics.OverlapSphere(pos, test_Radious);
                if (col.Length > 0)
                {
                    if (visualize)
                        DestroyImmediate(go);

                    Points.Add(-Vector3.one);
                }
                else
                {
                    Points.Add(pos);
                }

            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
/*
    public List<Vector3> FindPath(Vector3 start, Vector3 end)
    {
        if (!IsValidPoint(start) || !IsValidPoint(end))
        {
            Debug.LogError("Ponto de início ou destino inválido");
            return null;
        }

        // Inicializa a lista de pontos abertos e fechados
        List<Vector3> openList = new List<Vector3>();
        HashSet<Vector3> closedSet = new HashSet<Vector3>();

        // Dicionário para armazenar os custos G e F de cada ponto
        Dictionary<Vector3, float> gCost = new Dictionary<Vector3, float>();
        Dictionary<Vector3, float> fCost = new Dictionary<Vector3, float>();

        // Inicializa os custos G e F do ponto de início
        gCost[start] = 0;
        fCost[start] = Heuristic(start, end);

        openList.Add(start);

        while (openList.Count > 0)
        {
            Vector3 current = GetLowestFCost(openList, fCost);

            if (current == end)
            {
                return ReconstructPath(gCost, end);
            }

            openList.Remove(current);
            closedSet.Add(current);

            foreach (Vector3 neighbor in GetNeighbors(current, points))
            {
                if (closedSet.Contains(neighbor) || !IsValidPoint(points, neighbor))
                {
                    continue;
                }

                float tentativeGCost = gCost[current] + Vector3.Distance(current, neighbor);

                if (!openList.Contains(neighbor) || tentativeGCost < gCost[neighbor])
                {
                    gCost[neighbor] = tentativeGCost;
                    fCost[neighbor] = gCost[neighbor] + Heuristic(neighbor, end);

                    if (!openList.Contains(neighbor))
                    {
                        openList.Add(neighbor);
                    }
                }
            }
        }

        return null; // Caminho não encontrado
    }
*/
    public List<Vector3> FindPath(Vector3 start, Vector3 end)
    {
        // Verifica se o start e o end são válidos
        if (!IsValidPoint( start) || !IsValidPoint( end))
        {
            Debug.LogError("Ponto de início ou destino inválido");
            return null;
        }

        // Inicializa a lista de pontos abertos e fechados
        List<Vector3> openList = new List<Vector3>();
        HashSet<Vector3> closedSet = new HashSet<Vector3>();

        // Dicionário para armazenar os custos G e F de cada ponto
        Dictionary<Vector3, float> gCost = new Dictionary<Vector3, float>();
        Dictionary<Vector3, float> fCost = new Dictionary<Vector3, float>();

        // Inicializa os custos G e F do ponto de início
        gCost[start] = 0;
        fCost[start] = Heuristic(start, end);

        openList.Add(start);

        while (openList.Count > 0)
        {
            Vector3 current = GetLowestFCost(openList, fCost);

            if (current == end)
            {
                return ReconstructPath(gCost, end);
            }

            openList.Remove(current);
            closedSet.Add(current);

            foreach (Vector3 neighbor in GetNeighbors(current))
            {
                if (closedSet.Contains(neighbor) || !IsValidPoint(neighbor))
                {
                    continue;
                }

                float tentativeGCost = gCost[current] + Vector3.Distance(current, neighbor);

                if (!openList.Contains(neighbor) || tentativeGCost < gCost[neighbor])
                {
                    gCost[neighbor] = tentativeGCost;
                    fCost[neighbor] = gCost[neighbor] + Heuristic(neighbor, end);

                    if (!openList.Contains(neighbor))
                    {
                        openList.Add(neighbor);
                    }
                }
            }
        }

        print("Nao achei essa porra");
        return null; // Caminho não encontrado
    }

    private  float Heuristic(Vector3 a, Vector3 b)
    {
        return Vector3.Distance(a, b);
    }

    private  Vector3 GetLowestFCost(List<Vector3> openList, Dictionary<Vector3, float> fCost)
    {
        float lowestCost = float.MaxValue;
        Vector3 lowestPoint = Vector3.zero;

        foreach (Vector3 point in openList)
        {
            if (fCost.ContainsKey(point) && fCost[point] < lowestCost)
            {
                lowestCost = fCost[point];
                lowestPoint = point;
            }
        }

        return lowestPoint;
    }

    private List<Vector3> GetNeighbors(Vector3 point)
    {
        List<Vector3> neighbors = new List<Vector3>();
        List<float> points = new List<float>();

        //Direções cardeais
        if (point.x + 1 < Width) points.Add((point.x + 1) + point.z * Width + point.y * (Width * Depth));
        if (point.x - 1 >= 0) points.Add((point.x - 1) + point.z * Width + point.y * (Width * Depth));
        if (point.z + 1 < Depth) points.Add(point.x + (point.z + 1) * Width + point.y * (Width * Depth));
        if (point.z - 1 >= 0) points.Add(point.x + (point.z - 1) * Width + point.y * (Width * Depth));

        //Subir nas direções cardeais apenas
        if (point.y + 1 < Height && point.x + 1 < Width) points.Add((point.x + 1) + point.z * Width + (point.y + 1) * (Width * Depth));
        if (point.y + 1 < Height && point.x - 1 >= 0) points.Add((point.x - 1) + point.z * Width + (point.y + 1) * (Width * Depth));
        if (point.y + 1 < Height && point.z + 1 < Depth) points.Add(point.x + (point.z + 1) * Width + (point.y + 1) * (Width * Depth));
        if (point.y + 1 < Height && point.z - 1 >= 0) points.Add(point.x + (point.z - 1) * Width + (point.y + 1) * (Width * Depth));

        //Descer nas direcoes cardeais apenas
        if (point.y -1 >= 0 && point.x + 1 < Width) points.Add((point.x + 1) + point.z * Width + (point.y - 1) * (Width * Depth));
        if (point.y - 1 >= 0 && point.x - 1 >= 0) points.Add((point.x - 1) + point.z * Width + (point.y - 1) * (Width * Depth));
        if (point.y - 1 >= 0 && point.z + 1 < Depth) points.Add(point.x + (point.z + 1) * Width + (point.y - 1) * (Width * Depth));
        if (point.y - 1 >= 0 && point.z - 1 >= 0) points.Add(point.x + (point.z - 1) * Width + (point.y - 1) * (Width * Depth));


        //Diagonal na mesma altura
        if (point.x + 1 < Width && point.z + 1 < Depth) points.Add((point.x + 1) + (point.z + 1) * Width + point.y * (Width * Depth));
        if (point.x - 1 >= 0 && point.z + 1 < Depth) points.Add((point.x - 1) + (point.z + 1) * Width + point.y * (Width * Depth));
        if (point.x + 1 < Width && point.z - 1 >= 0) points.Add((point.x + 1) + (point.z - 1) * Width + point.y * (Width * Depth));
        if (point.x - 1 >= 0 && point.z - 1 >= 0) points.Add((point.x - 1) + (point.z - 1) * Width + point.y * (Width * Depth));

        foreach(float i in points)
        {
            if ( (int)i < Points.Count && (int) i >= 0  && Points[(int)i] != -Vector3.one) neighbors.Add(Points[(int)i]);
        }
        
        

        return neighbors;
    }

    private  bool IsValidPoint(Vector3 point)
    {
        return point.x >= 0 && point.y >= 0 && point.z >= 0;
    }

    private  List<Vector3> ReconstructPath(Dictionary<Vector3, float> gCost, Vector3 end)
    {
        List<Vector3> path = new List<Vector3>();
        path.Add(end);

        while (gCost.ContainsKey(end) && gCost[end] > 0)
        {
            Vector3 parent = GetLowestGCostNeighbor(end, gCost);
            path.Add(parent);
            end = parent;
        }

        path.Reverse();
        return path;
    }

    private Vector3 GetLowestGCostNeighbor(Vector3 point, Dictionary<Vector3, float> gCost)
    {
        float lowestCost = float.MaxValue;
        Vector3 lowestPoint = Vector3.zero;

        foreach (Vector3 neighbor in GetNeighbors(point))
        {
            if (gCost.ContainsKey(neighbor) && gCost[neighbor] < lowestCost)
            {
                lowestCost = gCost[neighbor];
                lowestPoint = neighbor;
            }
        }

        return lowestPoint;
    }


    public Vector3 GetNewRandomDirection(Vector3 pos)
    {
        var neighbors = GetNeighbors(pos);
        if (neighbors.Count == 0) print(pos);
        return neighbors[Random.Range(0, neighbors.Count-1)];
    }
}

