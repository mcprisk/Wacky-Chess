using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private Transform bottomLeftSquareTransform;
    [SerializeField] private float squareSize;

    internal Vector3 CalculatePositionFromCoords(Vector3Int coords)
    {
        return bottomLeftSquareTransform.position + new Vector3(
            coords.x * squareSize,
            0f,
            coords.z * squareSize);
    }
}
