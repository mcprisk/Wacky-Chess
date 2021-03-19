using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Piece
{
    private Vector3Int[] directions = new Vector3Int[] {
        new Vector3Int(1,0,0),
        new Vector3Int(-1,0,0),
        new Vector3Int(0,1,0),
        new Vector3Int(0,-1,0),
        new Vector3Int(0,0,1),
        new Vector3Int(0,0,-1),
    };
    public override List<Vector3Int> SelectAvaliableSquares()
    {
        avaliableMoves.Clear();
        foreach (var direction in directions)
        {
            MoveUntilStop(occupiedSquare, direction);
        }
        return avaliableMoves;
    }

    private void ContinueInNewDirection(Vector3Int startingCoords, Vector3Int startingDirection)
    {
        if (startingCoords.y == 7 && 
            (startingDirection == directions[0] || 
            startingDirection == directions[1] ||
            startingDirection == directions[4] ||
            startingDirection == directions[5]))
        {
            MoveUntilStop(startingCoords, directions[3]);
        } 
        
        else if (startingCoords.y == 0 && 
            (startingDirection == directions[0] || 
            startingDirection == directions[1] ||
            startingDirection == directions[4] ||
            startingDirection == directions[5]))
        {
            MoveUntilStop(startingCoords, directions[2]);
        }

        else if (startingCoords.z == 7 &&
            (startingDirection == directions[.l] ||
            startingDirection == directions[1] ||
            startingDirection == directions[4] ||
            startingDirection == directions[5]))
        {

        }
    }

    private void MoveUntilStop(Vector3Int startingCoords, Vector3Int direction)
    {
        bool stopped = false;
        int i = 0;
        while (!stopped)
        {
            ++i;
            Vector3Int nextCoords = startingCoords + direction * i;
            Piece piece = board.GetPieceOnSquare(nextCoords);
            if (!board.CheckIfCoordsAreOnBoard(nextCoords))
            {
                int count = 0;
                if (nextCoords.x == 0 || nextCoords.x == 7) ++count;
                if (nextCoords.y == 0 || nextCoords.y == 7) ++count;
                if (nextCoords.z == 0 || nextCoords.z == 7) ++count;

                if (count >= 2)
                {
                    stopped = true;
                    break; // Hit a barrier, we do not want to continue
                }
                else
                {
                    ContinueInNewDirection(nextCoords - direction, direction);
                    stopped = true;
                    break;
                }
            }
            if (piece == null)
            {
                if (!TryToAddMove(nextCoords))
                {
                    stopped = true;
                    break;
                } 
            }
            else if (!piece.IsFromSameTeam(this))
            {
                TryToAddMove(nextCoords);
                stopped = true;
                break;
            }
            else if (piece.IsFromSameTeam(this))
            {
                stopped = true;
                break;
            }
        }
    }
}
