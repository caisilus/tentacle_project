using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ForwardKinematicsAlgorithm
{
    public static Vector3[] CalculateJointPositions(ArmSegment[] segments)
    {
        Vector3 basePosition = segments.First().GlobalPosition;
        float[] lengthes = segments.Select(x => x.Length).ToArray();

        Vector3[] points = InitialJointPositions(basePosition, lengthes);

        Vector3[] axis = ComputeAngles(segments);

        Quaternion angle = Quaternion.identity;

        for (int i = 1; i < points.Length; i++)
        {
            var diff = Vector3.up * lengthes[i - 1];

            angle = Quaternion.AngleAxis(segments[i - 1].AngleInDegrees, axis[i - 1]) * angle;
            diff = angle * diff;

            points[i] = points[i - 1] + diff;
        }

        return points;
    }

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

    private static Vector3[] ComputeAngles(ArmSegment[] segments)
    {
        Vector3[] axis = segments.Select(segment => segment.Axis).ToArray();
        Quaternion angle = Quaternion.identity;

        for (int i = 1; i < axis.Length; i++)
        {
            angle = Quaternion.AngleAxis(segments[i - 1].AngleInDegrees, axis[i - 1]) * angle;
            axis[i] = angle * axis[i];
        }

        return axis;
    }
}
