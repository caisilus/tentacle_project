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
    [SerializeField]
    private bool inverseKinematics;

    [SerializeField]
    private float distanceEps = 0.1f;

    [SerializeField]
    private float iterationsLimit = 1000;

    private int iterations = 0;

    private Vector3 armTipPosition;


    private void FixedUpdate()
    {
        armTipPosition = ForwardKinematicsAlgorithm.CalculateTipPosition(
            roboticArm.BasePosition,
            roboticArm.Lengthes,
            roboticArm.Axis,
            roboticArm.AnglesInDegrees
        );

        if (forwardKinematics)
        {
            ForwardKinematicsStep();
            return;
        } 
        
        if (inverseKinematics) 
        {
            InverseKinematicsStep();
            return;
        }

        ResetIterations();
    }

    private void ForwardKinematicsStep()
    {
        target.Position = armTipPosition;
    }

    private void InverseKinematicsStep()
    {
        if (iterations > iterationsLimit || Vector3.Distance(armTipPosition, target.Position) < distanceEps)
        {
            return;
        }

        InverseKinematics.UpdateAngles(target.Position, roboticArm, 10f);
        iterations++;
    }

    private void ResetIterations()
    {
        iterations = 0;
    }
}
