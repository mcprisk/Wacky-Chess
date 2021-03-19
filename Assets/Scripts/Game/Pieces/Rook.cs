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

    protected override void ContinueInNewDirection(Vector3Int startingCoords, Vector3Int startingDirection)
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
            (startingDirection == directions[0] ||
            startingDirection == directions[1] ||
            startingDirection == directions[2] ||
            startingDirection == directions[3]))
        {
            MoveUntilStop(startingCoords, directions[5]);
        }

        else if (startingCoords.z == 0 &&
            (startingDirection == directions[0] ||
            startingDirection == directions[1] ||
            startingDirection == directions[2] ||
            startingDirection == directions[3]))
        {
            MoveUntilStop(startingCoords, directions[4]);
        }

        else if (startingCoords.x == 7 &&
            (startingDirection == directions[2] ||
            startingDirection == directions[3] ||
            startingDirection == directions[4] ||
            startingDirection == directions[5]))
        {
            MoveUntilStop(startingCoords, directions[1]);
        }

        else if (startingCoords.x == 0 &&
            (startingDirection == directions[2] ||
            startingDirection == directions[3] ||
            startingDirection == directions[4] ||
            startingDirection == directions[5]))
        {
            MoveUntilStop(startingCoords, directions[0]);
        }
    }
}
