using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RoboticArm : MonoBehaviour
{
    [SerializeField]
    private ArmSegment[] segments;

    [SerializeField]
    private float[] angles;

    public ArmSegment[] Segments { get => segments; }

    void Update()
    {
        UpdateAngles();
    }

    private void UpdateAngles()
    {
        for (int i = 0; i < segments.Length; i++)
        {
            segments[i].AngleInDegrees = angles[i];
        }
    }
}
