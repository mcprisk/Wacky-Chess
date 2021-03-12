using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Piece
{
    public override List<Vector3Int> SelectAvaliableSquares()
    {
        avaliableMoves.Clear();
        avaliableMoves.Add(occupiedSquare + new Vector3Int(0, 1, 0));
        return avaliableMoves;
    }
}
