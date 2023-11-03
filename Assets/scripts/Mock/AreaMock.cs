using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AreaMock : MonoBehaviour
{
    [SerializeField] private int _x1;

    public void SetX1(int x)
    {
        _x1 = x;
    }

    [SerializeField] private int _x2;

    public void SetX2(int x)
    {
        _x2 = x;
    }

    [SerializeField] private int _x3;

    public void SetX3(int x)
    {
        _x3 = x;
    }

    [SerializeField] private int _x4;

    public void SetX4(int x)
    {
        _x4 = x;
    }

    [SerializeField] private int _x5;

    public void SetX5(int x)
    {
        _x5 = x;
    }

    [SerializeField] private int _x6;

    public void SetX6(int x)
    {
        _x6 = x;
    }

    [SerializeField] private int _y1;

    public void Sety1(int y)
    {
        _y1 = y;
    }

    [SerializeField] private int _y2;

    public void Sety2(int y)
    {
        _y2 = y;
    }

    [SerializeField]  private int _y3;

    public void Sety3(int y)
    {
        _y3 = y;
    }

    [SerializeField]  private int _y4;

    public void Sety4(int y)
    {
        _y4 = y;
    }

    [SerializeField]  private int _y5;

    public void Sety5(int y)
    {
        
        Debug.Log("set y5");
        Debug.Log(y);
        _y5 = y;
    }

    [SerializeField]  private int _y6;

    public void Sety6(string y)
    {
        Debug.Log(y);
        //_y6 = y;
    }

    public void Paint()
    {
        Vector3 vec1 = new Vector3(_x1, _y1, 0); 
        Vector3 vec2 = new Vector3(_x2, _y2, 0);
        Vector3 vec3 = new Vector3(_x3, _y3, 0);
        Vector3 vec4 = new Vector3(_x4, _y4, 0);
        Vector3 vec5 = new Vector3(_x5, _y5, 0);
        Vector3 vec6 = new Vector3(_x6, _y6, 0);
        GoalManager.instance.EncloseArea(new Vector3[]{vec1,vec2,vec3,vec4,vec5,vec6});
    }
}