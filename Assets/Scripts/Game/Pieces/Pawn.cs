using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
    public override List<Vector3Int> SelectAvaliableSquares()
    {
        avaliableMoves.Clear();

        // Directions Always Acceptable:
        avaliableMoves.Add(occupiedSquare + new Vector3Int(1, 0, 0));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(-1, 0, 0));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(0, 0, 1));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(0, 0, -1));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(0, 1, 0));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(0, -1, 0));


        // Some Directions have to be curtailed based off of current face:
        // If on -Y face, no +Y and +X or +Z movement as that will result 
        // in moving 2 places, ect.

        avaliableMoves.Add(occupiedSquare + new Vector3Int(1, 1, 0));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(0, 1, 1));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(0, 1, -1));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(-1, 1, 0));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(1, -1, 0));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(0, -1, 1));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(0, -1, -1));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(-1, -1, 0));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(1, 0, 1));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(1, 0, -1));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(-1, 0, -1));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(-1, 0, 1));

        // All moves forbidden on -Y:
        if (occupiedSquare.y == 0 && occupiedSquare.x != 0 && occupiedSquare.z != 0
            && occupiedSquare.x != 7 && occupiedSquare.z != 7)
        {
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(1, 1, 0));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(0, 1, 1));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(0, 1, -1));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(-1, 1, 0));
        }
        // All moves forbidden on +Y:
        if (occupiedSquare.y == 7 && occupiedSquare.x != 0 && occupiedSquare.z != 0
            && occupiedSquare.x != 7 && occupiedSquare.z != 7)
        {
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(1, -1, 0));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(0, -1, 1));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(0, -1, -1));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(-1, -1, 0));
        }
        // All moves forbidden on -X:
        if (occupiedSquare.x == 0 && occupiedSquare.y != 0 && occupiedSquare.z != 0
            && occupiedSquare.y != 7 && occupiedSquare.z != 7)
        {
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(1, 1, 0));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(1, -1, 0));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(1, 0, 1));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(1, 0, -1));
        }
        // All moves forbidden on +X:
        if (occupiedSquare.x == 7 && occupiedSquare.y != 0 && occupiedSquare.z != 0
            && occupiedSquare.y != 7 && occupiedSquare.z != 7)
        {
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(-1, -1, 0));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(-1, 0, -1));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(-1, 0, 1));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(-1, 1, 0));
        }
        // All moves forbidden on -Z:
        if (occupiedSquare.z == 0 && occupiedSquare.x != 0 && occupiedSquare.y != 0
            && occupiedSquare.x != 7 && occupiedSquare.y != 7)
        {
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(-1, 0, 1));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(1, 0, 1));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(0, -1, 1));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(0, 1, 1));
        }
        // All moves forbidden on +Z:
        if (occupiedSquare.z == 7 && occupiedSquare.x != 0 && occupiedSquare.y != 0
            && occupiedSquare.x != 7 && occupiedSquare.y != 7)
        {
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(-1, 0, -1));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(1, 0, -1));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(0, -1, -1));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(0, 1, -1));
        }

        base.SelectAvaliableSquares();

        return avaliableMoves;
    }
}
