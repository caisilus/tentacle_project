using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private RoboticArm roboticArm;

    [SerializeField]
    private Target target;

    [SerializeField]
    private bool forwardKinematics;

    public bool ForwardKinematics { get => forwardKinematics; }
    public bool ReverseKinematics { get => !forwardKinematics; }

    void Update()
    {
        if (forwardKinematics)
        {
            Vector3[] jointPositions = ForwardKinematicsAlgorithm.CalculateJointPositions(this.roboticArm.Segments);
            target.SetPosition(jointPositions.Last());
        }
    }
}
