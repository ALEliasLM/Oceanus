using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Compass : MonoBehaviour
{
    public GameObject Param;
    
    
    private const int Tamanho = 500;
    private const int MeioTamanho = 250;

    public RectTransform North;
    public RectTransform South;
    public RectTransform Weast;
    public RectTransform East;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var forward = new Vector2(Param.transform.forward.x, Param.transform.forward.z);
        var NorthAngle = -Vector2.SignedAngle(forward, Vector2.up);
        var SouthAngle = -Vector2.SignedAngle(forward, Vector2.down);
        var WeastAngle = -Vector2.SignedAngle(forward, Vector2.left);
        var EastAngle = -Vector2.SignedAngle(forward, Vector2.right);
        if (Input.GetKey(KeyCode.Space))
        {
            print(NorthAngle);
            print(SouthAngle);
            print(WeastAngle);
            print(EastAngle);
        }

        North.localPosition = InterpolatePosition(NorthAngle);
        South.localPosition = InterpolatePosition(SouthAngle);
        Weast.localPosition = InterpolatePosition(WeastAngle);
        East.localPosition = InterpolatePosition(EastAngle);

        North.localScale = InterpolateScale(NorthAngle)*2;
        South.localScale = InterpolateScale(SouthAngle)*2;
        Weast.localScale = InterpolateScale(WeastAngle)*2;
        East.localScale = InterpolateScale(EastAngle)*2;



        //Se esse angulo precisa estar no 0, entao ele tem o minimo de profundidade e o minimo de distancia lateral
    }

    float AngleToCompassPos(float angle)
    {
        return(MeioTamanho / 180f) * angle;
    }

    public Vector3 InterpolatePosition(float angle)
    {
        var result = Vector3.zero;
        if (angle >= 0)
        {
            if (angle <= 90)
            {
                var pointA = new Vector3(0, 0, -250);
                var pointB = new Vector3(250, 0, -250);
                var pointC = new Vector3(250, 0, 0);
                var point = angle / 90;

                var AB = Vector3.Lerp(pointA, pointB, point);
                var BC = Vector3.Lerp(pointB, pointC, point);
                return result = Vector3.Lerp(AB, BC, point);

            }
            if (angle <= 180)
            {
                var pointA = new Vector3(250, 0, 0);
                var pointB = new Vector3(250, 0, +250);
                var pointC = new Vector3(0, 0, 250);
                var point = (angle - 90) / 90;

                var AB = Vector3.Lerp(pointA, pointB, point);
                var BC = Vector3.Lerp(pointB, pointC, point);
                return result = Vector3.Lerp(AB, BC, point);
            }
        }
        
        if (angle <= -90)
        {
            var pointA = new Vector3(-250, 0, 0);
            var pointB = new Vector3(-250, 0, +250);
            var pointC = new Vector3(0, 0, 250);
            var point = (angle +90)/ -90;
            
            var AB = Vector3.Lerp(pointA, pointB, point);
            var BC = Vector3.Lerp(pointB, pointC, point);
            return result = Vector3.Lerp(AB, BC, point);
        }
        else
        {
            var pointA = new Vector3(0, 0, -250);
            var pointB = new Vector3(-250, 0, -250);
            var pointC = new Vector3(-250, 0, 0);
            var point = angle/ -90;
            
            var AB = Vector3.Lerp(pointA, pointB, point);
            var BC = Vector3.Lerp(pointB, pointC, point);
            return result = Vector3.Lerp(AB, BC, point);
        }
    }

    public Vector3 InterpolateScale(float angle)
    {
        var value = 0f;
        if (Mathf.Abs(angle) <= 120)
        {
            value = 1 - Mathf.Abs(angle)/120f;
        }

        var final = new Vector3(1.5f, 1.5f, 1.5f);
        return Vector3.Lerp(Vector3.zero, final, value);
    }
}
