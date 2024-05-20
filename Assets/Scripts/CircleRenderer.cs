using System;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class CircleRenderer : MonoBehaviour
{
    public int Points = 36;
    private LineRenderer _lineRenderer;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public void CalculateCircle(float radius)
    {
        _lineRenderer.positionCount = Points;
        _lineRenderer.loop = true;
        for (int i = 0; i < Points; i++)
        {
            float degree =  360f / Points * i * Mathf.Deg2Rad;
            _lineRenderer.SetPosition(i, transform.position + new Vector3(radius * Mathf.Cos(degree), radius * Mathf.Sin(degree)));
        }
    }

    public void ShowCircle()
    {
        _lineRenderer.enabled = true;
    }

    public void HideCircle()
    {
        _lineRenderer.enabled = false;
    }
}