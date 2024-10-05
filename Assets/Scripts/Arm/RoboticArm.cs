using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RoboticArm : MonoBehaviour
{
    [SerializeField]
    private ArmSegment[] segments;

    public float[] AnglesInDegrees;

    public ArmSegment[] Segments { get => segments; }


    public float[] Lengthes { get => segments.Select(segment => segment.Length).ToArray(); }

    public Vector3[] Axis { get => segments.Select(segment => segment.Axis).ToArray(); }

    public Vector3 BasePosition { get => segments.First().GlobalPosition; }

    void Update()
    {
        UpdateAngles();
    }

    private void UpdateAngles()
    {
        for (int i = 0; i < segments.Length; i++)
        {
            segments[i].AngleInDegrees = AnglesInDegrees[i];
        }
    }
}
