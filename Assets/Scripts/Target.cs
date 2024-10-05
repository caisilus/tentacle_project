using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Target : MonoBehaviour
{
    public Vector3 Position
    {
        get => transform.position;
        set
        {
            transform.position = value;
        }
    }
}
