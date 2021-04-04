using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece
{
    private Vector3Int[] directions = new Vector3Int[] {
        new Vector3Int(1, 1, 0),
        new Vector3Int(0, 1, 1),
        new Vector3Int(0, 1, -1),
        new Vector3Int(-1, 1, 0),
        new Vector3Int(1, -1, 0),
        new Vector3Int(0, -1, 1),
        new Vector3Int(0, -1, -1),
        new Vector3Int(-1, -1, 0),
        new Vector3Int(1, 0, 1),
        new Vector3Int(1, 0, -1),
        new Vector3Int(-1, 0, -1),
        new Vector3Int(-1, 0, 1)
    };

    public override List<Vector3Int> SelectAvaliableSquares()
    {
        avaliableMoves.Clear();
        foreach (var direction in directions)
        {
            if (occupiedSquare.y == 0 && occupiedSquare.x != 0 && occupiedSquare.z != 0
                && occupiedSquare.x != 7 && occupiedSquare.z != 7 && direction.y != 0)
            {
                continue;
            }
            // All moves forbidden on +Y:
            if (occupiedSquare.y == 7 && occupiedSquare.x != 0 && occupiedSquare.z != 0
                && occupiedSquare.x != 7 && occupiedSquare.z != 7 && direction.y != 0)
            {
                continue;
            }
            // All moves forbidden on -X:
            if (occupiedSquare.x == 0 && occupiedSquare.y != 0 && occupiedSquare.z != 0
                && occupiedSquare.y != 7 && occupiedSquare.z != 7 && direction.x != 0)
            {
                continue;
            }
            // All moves forbidden on +X:
            if (occupiedSquare.x == 7 && occupiedSquare.y != 0 && occupiedSquare.z != 0
                && occupiedSquare.y != 7 && occupiedSquare.z != 7 && direction.x != 0)
            {
                continue;
            }
            // All moves forbidden on -Z:
            if (occupiedSquare.z == 0 && occupiedSquare.x != 0 && occupiedSquare.y != 0
                && occupiedSquare.x != 7 && occupiedSquare.y != 7 && direction.z != 0)
            {
                continue;
            }
            // All moves forbidden on +Z:
            if (occupiedSquare.z == 7 && occupiedSquare.x != 0 && occupiedSquare.y != 0
                && occupiedSquare.x != 7 && occupiedSquare.y != 7 && direction.z != 0)
            {
                continue;
            }

            MoveUntilStop(occupiedSquare, direction);

        }
        return avaliableMoves;
    }

    protected override void ContinueInNewDirection(Vector3Int startingCoords, Vector3Int startingDirection)
    {
        if (startingCoords.y == 7 &&
            (startingDirection == directions[11] ||
            startingDirection == directions[8]))
        {
            MoveUntilStop(startingCoords, directions[5]);
        }

        if (startingCoords.y == 7 &&
            (startingDirection == directions[10] ||
            startingDirection == directions[9]))
        {
            MoveUntilStop(startingCoords, directions[6]);
        }

        if (startingCoords.y == 0 &&
            (startingDirection == directions[8] ||
            startingDirection == directions[9]))
        {
            MoveUntilStop(startingCoords, directions[0]);
        }

        if (startingCoords.y == 0 &&
            (startingDirection == directions[11] ||
            startingDirection == directions[10]))
        {
            MoveUntilStop(startingCoords, directions[3]);
        }

        // X

        if (startingCoords.x == 7 &&
            (startingDirection == directions[1] ||
            startingDirection == directions[2]))
        {
            MoveUntilStop(startingCoords, directions[3]);
        }

        if (startingCoords.x == 7 &&
            (startingDirection == directions[5] ||
            startingDirection == directions[6]))
        {
            MoveUntilStop(startingCoords, directions[7]);
        }

        if (startingCoords.x == 7 &&
            (startingDirection == directions[1] ||
            startingDirection == directions[5]))
        {
            MoveUntilStop(startingCoords, directions[11]);
        }

        if (startingCoords.x == 7 &&
            (startingDirection == directions[2] ||
            startingDirection == directions[6]))
        {
            MoveUntilStop(startingCoords, directions[10]);
        }

        if (startingCoords.x == 0 &&
            (startingDirection == directions[1] ||
            startingDirection == directions[2]))
        {
            MoveUntilStop(startingCoords, directions[0]);
        }

        if (startingCoords.x == 0 &&
            (startingDirection == directions[5] ||
            startingDirection == directions[6]))
        {
            MoveUntilStop(startingCoords, directions[4]);
        }

        if (startingCoords.x == 0 &&
            (startingDirection == directions[1] ||
            startingDirection == directions[5]))
        {
            MoveUntilStop(startingCoords, directions[8]);
        }

        if (startingCoords.x == 0 &&
            (startingDirection == directions[2] ||
            startingDirection == directions[6]))
        {
            MoveUntilStop(startingCoords, directions[9]);
        }

        // +Z

        if (startingCoords.z == 7 &&
            (startingDirection == directions[3] ||
            startingDirection == directions[0]))
        {
            MoveUntilStop(startingCoords, directions[2]);
        }

        if (startingCoords.z == 7 &&
            (startingDirection == directions[4] ||
            startingDirection == directions[7]))
        {
            MoveUntilStop(startingCoords, directions[6]);
        }

        if (startingCoords.z == 7 &&
            (startingDirection == directions[4] ||
            startingDirection == directions[0]))
        {
            MoveUntilStop(startingCoords, directions[9]);
        }

        if (startingCoords.z == 7 &&
            (startingDirection == directions[3] ||
            startingDirection == directions[7]))
        {
            MoveUntilStop(startingCoords, directions[10]);
        }

        // -Z

        if (startingCoords.z == 0 &&
            (startingDirection == directions[3] ||
            startingDirection == directions[0]))
        {
            MoveUntilStop(startingCoords, directions[1]);
        }

        if (startingCoords.z == 0 &&
            (startingDirection == directions[4] ||
            startingDirection == directions[7]))
        {
            MoveUntilStop(startingCoords, directions[5]);
        }

        if (startingCoords.z == 0 &&
            (startingDirection == directions[4] ||
            startingDirection == directions[0]))
        {
            MoveUntilStop(startingCoords, directions[8]);
        }

        if (startingCoords.z == 0 &&
            (startingDirection == directions[3] ||
            startingDirection == directions[7]))
        {
            MoveUntilStop(startingCoords, directions[11]);
        }
    }
}
