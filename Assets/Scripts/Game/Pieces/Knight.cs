using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Piece
{
    public override List<Vector3Int> SelectAvaliableSquares()
    {
        avaliableMoves.Clear();
        avaliableMoves.Add(occupiedSquare + new Vector3Int(1, 0, 2));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(1, 0, -2));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(-1, 0, 2));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(-1, 0, -2));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(2, 0, 1));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(2, 0, -1));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(-2, 0, 1));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(-2, 0, -1));

        avaliableMoves.Add(occupiedSquare + new Vector3Int(1, 2, 0));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(1, -2, 0));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(-1, 2, 0));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(-1, -2, 0));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(2, 1, 0));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(2, -1, 0));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(-2, 1, 0));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(-2, -1, 0));

        avaliableMoves.Add(occupiedSquare + new Vector3Int(0, 1, 2));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(0, 1, -2));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(0, -1, 2));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(0, -1, -2));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(0, 2, 1));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(0, 2, -1));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(0, -2, 1));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(0, -2, -1));


        // All moves forbidden on -Y:
        if (occupiedSquare.y == 0 && occupiedSquare.x != 0 && occupiedSquare.z != 0
            && occupiedSquare.x != 7 && occupiedSquare.z != 7)
        {
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(1, 2, 0));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(-1, 2, 0));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(2, 1, 0));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(-2, 1, 0));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(0, 1, 2));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(0, 1, -2));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(0, 2, 1));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(0, 2, -1));

            avaliableMoves.Add(occupiedSquare + new Vector3Int(1, 1, 1));
            avaliableMoves.Add(occupiedSquare + new Vector3Int(1, 1, -1));
            avaliableMoves.Add(occupiedSquare + new Vector3Int(-1, 1, 1));
            avaliableMoves.Add(occupiedSquare + new Vector3Int(-1, 1, -1));
        }
        // All moves forbidden on +Y:
        if (occupiedSquare.y == 7 && occupiedSquare.x != 0 && occupiedSquare.z != 0
            && occupiedSquare.x != 7 && occupiedSquare.z != 7)
        {
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(1, -2, 0));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(-1, -2, 0));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(2, -1, 0));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(-2, -1, 0));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(0, -1, 2));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(0, -1, -2));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(0, -2, 1));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(0, -2, -1));

            avaliableMoves.Add(occupiedSquare + new Vector3Int(1, -1, 1));
            avaliableMoves.Add(occupiedSquare + new Vector3Int(1, -1, -1));
            avaliableMoves.Add(occupiedSquare + new Vector3Int(-1, -1, 1));
            avaliableMoves.Add(occupiedSquare + new Vector3Int(-1, -1, -1));
        }
        // All moves forbidden on -X:
        if (occupiedSquare.x == 0 && occupiedSquare.y != 0 && occupiedSquare.z != 0
            && occupiedSquare.y != 7 && occupiedSquare.z != 7)
        {
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(1, 0, 2));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(1, 0, -2));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(2, 0, 1));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(2, 0, -1));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(1, 2, 0));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(1, -2, 0));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(2, 1, 0));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(2, -1, 0));

            avaliableMoves.Add(occupiedSquare + new Vector3Int(1, 1, 1));
            avaliableMoves.Add(occupiedSquare + new Vector3Int(1, 1, -1));
            avaliableMoves.Add(occupiedSquare + new Vector3Int(1, -1, 1));
            avaliableMoves.Add(occupiedSquare + new Vector3Int(1, -1, -1));
        }
        // All moves forbidden on +X:
        if (occupiedSquare.x == 7 && occupiedSquare.y != 0 && occupiedSquare.z != 0
            && occupiedSquare.y != 7 && occupiedSquare.z != 7)
        {
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(-1, 0, 2));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(-1, 0, -2));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(-2, 0, 1));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(-2, 0, -1));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(-1, 2, 0));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(-1, -2, 0));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(-2, 1, 0));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(-2, -1, 0));

            avaliableMoves.Add(occupiedSquare + new Vector3Int(-1, 1, 1));
            avaliableMoves.Add(occupiedSquare + new Vector3Int(-1, 1, -1));
            avaliableMoves.Add(occupiedSquare + new Vector3Int(-1, -1, 1));
            avaliableMoves.Add(occupiedSquare + new Vector3Int(-1, -1, -1));
        }
        // All moves forbidden on -Z:
        if (occupiedSquare.z == 0 && occupiedSquare.x != 0 && occupiedSquare.y != 0
            && occupiedSquare.x != 7 && occupiedSquare.y != 7)
        {
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(1, 0, 2));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(-1, 0, 2));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(2, 0, 1));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(-2, 0, 1));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(0, 1, 2));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(0, -1, 2));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(0, 2, 1));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(0, -2, 1));

            avaliableMoves.Add(occupiedSquare + new Vector3Int(1, 1, 1));
            avaliableMoves.Add(occupiedSquare + new Vector3Int(-1, 1, 1));
            avaliableMoves.Add(occupiedSquare + new Vector3Int(1, -1, 1));
            avaliableMoves.Add(occupiedSquare + new Vector3Int(-1, -1, 1));
        }
        // All moves forbidden on +Z:
        if (occupiedSquare.z == 7 && occupiedSquare.x != 0 && occupiedSquare.y != 0
            && occupiedSquare.x != 7 && occupiedSquare.y != 7)
        {
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(1, 0, -2));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(-1, 0, -2));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(2, 0, -1));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(-2, 0, -1));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(0, 1, -2));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(0, -1, -2));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(0, 2, -1));
            avaliableMoves.Remove(occupiedSquare + new Vector3Int(0, -2, -1));

            avaliableMoves.Add(occupiedSquare + new Vector3Int(1, 1, -1));
            avaliableMoves.Add(occupiedSquare + new Vector3Int(-1, 1, -1));
            avaliableMoves.Add(occupiedSquare + new Vector3Int(1, -1, -1));
            avaliableMoves.Add(occupiedSquare + new Vector3Int(-1, -1, -1));
        }

        base.SelectAvaliableSquares();

        return avaliableMoves;
    }
}
