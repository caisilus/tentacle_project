using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmSegment : MonoBehaviour
{
    [SerializeField]
    private bool xAxis;
    [SerializeField]
    private bool zAxis => !xAxis;

    [SerializeField]
    private Transform Tip;

    private float angleInDegrees;

    public Vector3 GlobalPosition { get => transform.position; }
    public float Length { get => (Tip.position - transform.position).magnitude; }
    public Vector3 Axis { get => xAxis ? new Vector3(1, 0, 0) : new Vector3(0, 0, 1); }

    public float AngleInDegrees { 
        get => angleInDegrees;
        set
        {
            if (value < -180 || value > 180) return;

            angleInDegrees = value;
            transform.localRotation = Quaternion.AngleAxis(AngleInDegrees, Axis);
        }
    }
}
