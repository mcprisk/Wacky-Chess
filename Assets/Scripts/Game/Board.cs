using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private Transform XZTransform;
    [SerializeField] private Transform YZTransform;
    [SerializeField] private Transform XYTransform;
    [SerializeField] private float squareSize;

    private const int flip_distance = 32;

    internal Vector3 CalculatePositionFromCoords(Vector3Int coords)
    {
        // -Y FACE
        if (coords.y == 0 && coords.x != 0 && coords.z != 0)
        {
            return XZTransform.position + new Vector3(
                (coords.x - 1) * squareSize,
                0f,
                (coords.z - 1) * squareSize);
        }

        // +Y FACE

        // -X FACE
        else if (coords.x == 0 && coords.y != 0 && coords.z != 0)
        {
            return YZTransform.position + new Vector3(
                0f,
                (coords.y - 1) * squareSize,
                (coords.z - 1) * squareSize);
        }

        // +X FACE

        // -Z FACE
        else if (coords.z == 0 && coords.x != 0 && coords.y != 0)
        {
            return XYTransform.position + new Vector3(
                (coords.x - 1) * squareSize,
                (coords.y - 1) * squareSize,
                0f);
        }

        // +Z FACE

        else
        {
            Debug.LogError("Invalid Piece Location");
            return new Vector3Int(-1, -1, -1);
        }        
    }

    internal Quaternion CalculateRotationFromCoords(Vector3Int coords)
    {
        // -Y FACE
        if (coords.y == 0 && coords.x != 0 && coords.z != 0)
        {
            return Quaternion.Euler(0,90,0);
        }

        // +Y FACE

        // -X FACE
        else if (coords.x == 0 && coords.y != 0 && coords.z != 0)
        {
            return Quaternion.Euler(0, 0, 270);
        }

        // +X FACE

        // -Z FACE
        else if (coords.z == 0 && coords.x != 0 && coords.y != 0)
        {
            return Quaternion.Euler(90, 0, 0);
        }

        // +Z FACE

        else
        {
            Debug.LogError("Invalid Piece Location");
            return new Quaternion();
        }
    }
}
