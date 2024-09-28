using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleNode : MonoBehaviour
{
    [SerializeField]
    private bool xAxis;
    [SerializeField]
    private bool zAxis => !xAxis;
    
    [Range(-180, 180)]
    public float AngleInDegrees;
    
    [SerializeField]
    private Transform Tip;

    public Vector3 GlobalPosition { get => transform.position; }
    public float Length { get => (Tip.position - transform.position).magnitude; }
    public Vector3 Axis { get => xAxis ? new Vector3(1, 0, 0) : new Vector3(0, 0, 1); }

    private void Update()
    {
        transform.localRotation = Quaternion.AngleAxis(AngleInDegrees, Axis);
    }
}
