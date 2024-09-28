using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Target : MonoBehaviour
{
   public void SetPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
    }
}
