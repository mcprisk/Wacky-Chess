using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantMover : MonoBehaviour, Mover
{
    public void MoveTo(Transform transform, Vector3 targetPosition)
    {
        transform.position = targetPosition;
    }
}
