using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    [SerializeField]
    private TentacleNode[] nodes;

    private Vector3 basePosition;
    private Vector3[] points;

    private float[] lengthes;

    // DEBUG
    private List<GameObject> debugPool = new List<GameObject>();

    private void Start()
    {
        basePosition = nodes.First().GlobalPosition;
        lengthes = nodes.Select(x => x.Length).ToArray();
        points = Enumerable.Repeat(new Vector3(0, 0, 0), nodes.Length + 1).ToArray();

        for (int i = 0; i < points.Length; i++)
        {
            var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);

            debugPool.Add(sphere);
        }

        ResetPoints();
    }

    private void ResetPoints()
    {
        points[0] = basePosition;
        for (int i = 1; i < points.Length; i++)
        {
            points[i] = points[i - 1] + Vector3.up * lengthes[i - 1];
        }
    }

    private void Update()
    {
        UpdateTarget();
    }

    private void UpdateTarget()
    {
        ResetPoints();

        Vector3[] axis = nodes.Select(node => node.Axis).ToArray();
        for (int i = 1; i < axis.Length; i++)
        {
            Quaternion angle = Quaternion.identity;
            for (int j = 0; j < i; j++)
            {
                angle = Quaternion.AngleAxis(nodes[j].AngleInDegrees, axis[j]) * angle;
            }
            axis[i] = angle * axis[i];
        }

        for (int i = 1; i < points.Length; i++)
        {
            var diff = Vector3.up * lengthes[i - 1];

            Quaternion angle = Quaternion.identity;
            for (int j = 0; j < i; j++)
            {
                angle = Quaternion.AngleAxis(nodes[j].AngleInDegrees, axis[j]) * angle;
            }
            diff = angle * diff;

            points[i] = points[i - 1] + diff;
        }

        //DebugWithSperes();

        Vector3 topPosition = points.Last();
        transform.position = topPosition;
    }

    private void LogVector(Vector3 vector)
    {
        Debug.Log($"x: {vector.x}, y: {vector.y}, z: {vector.z}");
    }

    private void DebugWithSperes()
    {
        for (int i = 0; i < points.Length; i++)
        {
            debugPool[i].transform.position = points[i];
        }
    }

    private void _RotateAxis()
    {
        Vector3[] axis = nodes.Select(node => node.Axis).ToArray();
        for (int i = 1; i < axis.Length; i++)
        {
            Quaternion angle = Quaternion.identity;
            for (int j = 0; j < i; j++)
            {
                angle = angle * Quaternion.AngleAxis(nodes[j].AngleInDegrees, axis[j]);
                Debug.Log(angle);
            }
            axis[i] = angle * axis[i];
        }
    }
}
