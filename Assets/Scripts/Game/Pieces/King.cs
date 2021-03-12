using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Piece
{
    public override List<Vector3Int> SelectAvaliableSquares()
    {
        avaliableMoves.Clear();
        avaliableMoves.Add(occupiedSquare + new Vector3Int(1, 0, 1));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(1, 0, 0));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(0, 0, 1));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(-1, 0, -1));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(1, 0, -1));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(-1, 0, 1));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(0, 0, -1));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(-1, 0, 0));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(1, 1, 1));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(1, 1, 0));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(0, 1, 1));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(-1, 1, -1));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(1, 1, -1));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(-1, 1, 1));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(0, 1, -1));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(-1, 1, 0));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(1, -1, 1));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(1, -1, 0));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(0, -1, 1));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(-1, -1, -1));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(1, -1, -1));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(-1, -1, 1));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(0, -1, -1));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(-1, -1, 0));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(0, 1, 0));
        avaliableMoves.Add(occupiedSquare + new Vector3Int(0, -1, 0));
        return avaliableMoves;
    }
}
