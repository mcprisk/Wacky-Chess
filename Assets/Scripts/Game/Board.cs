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

    private const float flip_distance = 32;

    internal Vector3 CalculatePositionFromCoords(Vector3Int coords)
    {
        // If 2 zeros or sevens, then coords are on a 45
        // If 1 zero or seven, then coords are on a wall
        // If 0 or 3 zeros or sevens, then coords are invalid
        int zeroOrMax = 0;
        for (int i = 0; i < 3; ++i)
        {
            zeroOrMax += (coords[i] == 0 || coords[i] == 7) ? 1 : 0;
        }

        // 45's

        if (zeroOrMax == 2)
        {
            if (coords.z == 0 && coords.y == 0)
            {
                return new Vector3(
                    6 + squareSize * (coords.x - 1),
                    2,
                    -30);
            }
            else if (coords.x == 0 && coords.y == 0)
            {
                return new Vector3(
                    2,
                    2,
                    -26 + squareSize * (coords.z - 1));
            }
            else if (coords.x == 0 && coords.z == 0)
            {
                return new Vector3(
                    2,
                    6 + squareSize * (coords.y - 1),
                    -30);
            }
            else if (coords.x == 7 && coords.z == 7)
            {
                return new Vector3(
                    30,
                    6 + squareSize * (coords.y - 1),
                    -2);
            }
            else if (coords.y == 7 && coords.z == 7)
            {
                return new Vector3(
                    6 + squareSize * (coords.x - 1),
                    30,
                    -2);
            }
            else if (coords.x == 7 && coords.y == 7)
            {
                return new Vector3(
                    30,
                    30,
                    -26 + squareSize * (coords.z - 1));
            }
            else if (coords.x == 7 && coords.y == 0)
            {
                return new Vector3(
                    30,
                    2,
                    -26 + squareSize * (coords.z - 1));
            }
            else if (coords.z == 7 && coords.y == 0)
            {
                return new Vector3(
                    6 + squareSize * (coords.x - 1),
                    2,
                    -2);
            }
            else if (coords.x == 0 && coords.y == 7)
            {
                return new Vector3(
                    2,
                    30,
                    -26 + squareSize * (coords.z - 1));
            }
            else if (coords.z == 0 && coords.y == 7)
            {
                return new Vector3(
                    6 + squareSize * (coords.x - 1),
                    30,
                    -30);
            }
            else if (coords.x == 0 && coords.z == 7)
            {
                return new Vector3(
                    2,
                    6 + squareSize * (coords.y - 1),
                    -2);
            }
            else if (coords.x == 7 && coords.z == 0)
            {
                return new Vector3(
                    30,
                    6 + squareSize * (coords.y - 1),
                    -30);
            }
        }

        // WALLS

        else if (zeroOrMax == 1)
        {
            // -Y FACE
            if (coords.y == 0)
            {
                return XZTransform.position + new Vector3(
                    (coords.x - 1) * squareSize,
                    0f,
                    (coords.z - 1) * squareSize);
            }

            // +Y FACE
            if (coords.y == 7)
            {
                return XZTransform.position + new Vector3(
                    (coords.x - 1) * squareSize,
                    flip_distance,
                    (coords.z - 1) * squareSize);
            }

            // -X FACE
            else if (coords.x == 0)
            {
                return YZTransform.position + new Vector3(
                    0f,
                    (coords.y - 1) * squareSize,
                    (coords.z - 1) * squareSize);
            }

            // +X FACE
            else if (coords.x == 7)
            {
                return YZTransform.position + new Vector3(
                    flip_distance,
                    (coords.y - 1) * squareSize,
                    (coords.z - 1) * squareSize);
            }

            // -Z FACE
            else if (coords.z == 0)
            {
                return XYTransform.position + new Vector3(
                    (coords.x - 1) * squareSize,
                    (coords.y - 1) * squareSize,
                    0f);
            }

            // +Z FACE
            else if (coords.z == 7)
            {
                return XYTransform.position + new Vector3(
                    (coords.x - 1) * squareSize,
                    (coords.y - 1) * squareSize,
                    flip_distance);
            }
        }


        Debug.LogError("Invalid Piece Location - Pos");
        return new Vector3Int(-1, -1, -1);
    }        
    
    internal Quaternion CalculateRotationFromCoords(Vector3Int coords)
    {
        // If 2 zeros or sevens, then coords are on a 45
        // If 1 zero or seven, then coords are on a wall
        // If 0 or 3 zeros or sevens, then coords are invalid
        int zeroOrMax = 0;
        for (int i = 0; i < 3; ++i)
        {
            zeroOrMax += (coords[i] == 0 || coords[i] == 7) ? 1 : 0;
        }

        // 45's
        if (zeroOrMax == 2)
        {
            if (coords.z == 0 && coords.y == 0)
            {
                return Quaternion.Euler(45, 0, 0);
            }
            else if (coords.x == 0 && coords.y == 0)
            {
                return Quaternion.Euler(0, 0, -45);
            }
            else if (coords.x == 0 && coords.z == 0)
            {
                return Quaternion.Euler(90, 45, 0);
            }
            else if (coords.x == 7 && coords.z == 7)
            {
                return Quaternion.Euler(90, 225, 0);
            }
            else if (coords.y == 7 && coords.z == 7)
            {
                return Quaternion.Euler(225, 0, 0);
            }
            else if (coords.x == 7 && coords.y == 7)
            {
                return Quaternion.Euler(0, 0, -225);
            }
            else if (coords.x == 7 && coords.y == 0)
            {
                return Quaternion.Euler(0, 0, 45);
            }
            else if (coords.z == 7 && coords.y == 0)
            {
                return Quaternion.Euler(-45, 0, 0);
            }
            else if (coords.x == 0 && coords.y == 7)
            {
                return Quaternion.Euler(0, 0, 225);
            }
            else if (coords.z == 0 && coords.y == 7)
            {
                return Quaternion.Euler(-225, 0, 0);
            }
            else if (coords.x == 0 && coords.z == 7)
            {
                return Quaternion.Euler(90, 135, 0);
            }
            else if (coords.x == 7 && coords.z == 0)
            {
                return Quaternion.Euler(90, -45, 0);
            }
        }
        // WALLS

        else if (zeroOrMax == 1)
        {
            // -Y FACE
            if (coords.y == 0 && coords.x != 0 && coords.z != 0)
            {
                return Quaternion.Euler(0, 90, 0);
            }

            // +Y FACE
            else if (coords.y == 7 && coords.x != 7 && coords.z != 7)
            {
                return Quaternion.Euler(0, 90, 180);
            }

            // -X FACE
            else if (coords.x == 0 && coords.y != 0 && coords.z != 0)
            {
                return Quaternion.Euler(0, 0, 270);
            }

            // +X FACE
            else if (coords.x == 7 && coords.y != 7 && coords.z != 7)
            {
                return Quaternion.Euler(0, 0, 90);
            }

            // -Z FACE
            else if (coords.z == 0 && coords.x != 0 && coords.y != 0)
            {
                return Quaternion.Euler(90, 0, 0);
            }

            // +Z FACE
            else if (coords.z == 7 && coords.x != 7 && coords.y != 7)
            {
                return Quaternion.Euler(270, 0, 0);
            }
        }

        // NOT ON A FACE
        
        Debug.LogError("Invalid Piece Location - Rot");
        return new Quaternion();
    }
}
