using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
using UnityEngine;

public class PathMovement : MonoBehaviour
{
    [SerializeField] private Transform _attachedPath;

    private Transform[] _points;

    public IReadOnlyCollection<Transform> Points { get => _points; }

    private void Awake()
    {
        _points = DefinePointsOfPath();
    }

    public bool TryGetPoint(int indexPoint, out Transform point)
    {
        bool hasPoint = false;
        point = null;

        if (indexPoint < _points.Length)
        {
            hasPoint = true;
            point = _points[indexPoint];
        }

        return hasPoint;
    }

    private Transform[] DefinePointsOfPath()
    {
        Transform[] points = new Transform[_attachedPath.childCount];

        for (int i = 0; i < _attachedPath.childCount; i++)
        {
            points[i] = _attachedPath.GetChild(i);
        }

        return points;
    }
}
