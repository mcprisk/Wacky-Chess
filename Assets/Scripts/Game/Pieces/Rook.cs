using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Piece
{
    public override List<Vector3Int> SelectAvaliableSquares()
    {
        avaliableMoves.Clear();
        avaliableMoves.Add(occupiedSquare + new Vector3Int(1, 0, 1));
        return avaliableMoves;
    }
}
