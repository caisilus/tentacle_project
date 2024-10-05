using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class InverseKinematics
{
    public static void UpdateAngles(Vector3 targetPosition, RoboticArm arm, float learningRate = 0.5f)
    {
        float deltaAngle = 0.1f;

        for (int i = 0; i < arm.AnglesInDegrees.Length; i++)
        {
            Vector3 initialTipPosition = InitialTipPosition(arm);
            float initialDistanceToTarget = Vector3.Distance(initialTipPosition, targetPosition);
            Vector3 perurbedTipPosition = PerturbedTipPosition(arm, i, deltaAngle);
            float perturbedDistanceToTarget = Vector3.Distance(perurbedTipPosition, targetPosition);
            float differential = (perturbedDistanceToTarget - initialDistanceToTarget) / deltaAngle;

            arm.AnglesInDegrees[i] = Mathf.Clamp(arm.AnglesInDegrees[i] - differential * learningRate, -90f, 90f);
        }
    }

    private static Vector3 InitialTipPosition(RoboticArm arm)
    {
        return ForwardKinematicsAlgorithm.CalculateTipPosition(arm.BasePosition, arm.Lengthes, arm.Axis, arm.AnglesInDegrees);
    }

    private static Vector3 PerturbedTipPosition(RoboticArm arm, int index, float deltaAngle = 0.001f)
    {
        float[] angles = arm.AnglesInDegrees;
        angles[index] += deltaAngle;

        return ForwardKinematicsAlgorithm.CalculateTipPosition(arm.BasePosition, arm.Lengthes, arm.Axis, angles);
    }
}
