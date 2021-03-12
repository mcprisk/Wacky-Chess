using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantMover : MonoBehaviour, Mover
{
    public void MoveTo(Transform transform, Vector3 targetPosition, Quaternion targetRotation)
    {
        transform.position = targetPosition;
        transform.rotation = targetRotation;
    }
}
