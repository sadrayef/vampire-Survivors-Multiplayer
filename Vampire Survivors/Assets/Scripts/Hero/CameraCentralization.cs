using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    //-------------------------------------------------------------

    public Transform target;
    public Vector3 offset;

    //-------------------------------------------------------------

    void Update()
    {
        transform.position = target.position + offset;
    }
}
