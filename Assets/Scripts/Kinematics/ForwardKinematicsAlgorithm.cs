using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ForwardKinematicsAlgorithm
{
    public static Vector3[] CalculateJointPositions(Vector3 basePosition, float[] lengthes, Vector3[] axis, float[] anglesInDegrees)
    {
        Vector3[] points = InitialJointPositions(basePosition, lengthes);

        Vector3[] rotatedAxis = ComputeAngles(axis, anglesInDegrees);

        Quaternion angle = Quaternion.identity;

        for (int i = 1; i < points.Length; i++)
        {
            var diff = Vector3.up * lengthes[i - 1];

            angle = Quaternion.AngleAxis(anglesInDegrees[i - 1], rotatedAxis[i - 1]) * angle;
            diff = angle * diff;

            points[i] = points[i - 1] + diff;
        }

        return points;
    }

    public static Vector3 CalculateTipPosition(Vector3 basePosition, float[] lengthes, Vector3[] axis, float[] anglesInDegrees) => CalculateJointPositions(basePosition, lengthes, axis, anglesInDegrees).Last();

    private static Vector3[] InitialJointPositions(Vector3 basePosition, float[] lengthes)
    {
        Vector3[] points = Enumerable.Repeat(new Vector3(0, 0, 0), lengthes.Length + 1).ToArray();
        points[0] = basePosition;
        for (int i = 1; i < points.Length; i++)
        {
            points[i] = points[i - 1] + Vector3.up * lengthes[i - 1];
        }

        return points;
    }

    private static Vector3[] ComputeAngles(Vector3[] axis, float[] angles)
    {
        Quaternion angle = Quaternion.identity;

        for (int i = 1; i < axis.Length; i++)
        {
            angle = Quaternion.AngleAxis(angles[i - 1], axis[i - 1]) * angle;
            axis[i] = angle * axis[i];
        }

        return axis;
    }
}
