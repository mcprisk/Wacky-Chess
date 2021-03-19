using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlip : MonoBehaviour
{
    [SerializeField] Camera mainCamera;

    public void SetupCamera(TeamColor team)
    {
        if (team == TeamColor.Black)
            FlipCamera();
    }

    private void FlipCamera()
    {
        Vector3 currentRotation = mainCamera.transform.rotation.eulerAngles;
        currentRotation.x += 180;
        mainCamera.transform.rotation = Quaternion.Euler(currentRotation);
    }
}
