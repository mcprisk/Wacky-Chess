using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Piece
{
    private Vector3Int[] directions = new Vector3Int[] { 
        new Vector3Int(1,0,0)
        // Add More Directions Here!
    };
    public override List<Vector3Int> SelectAvaliableSquares()
    {
        avaliableMoves.Clear();
        int range = 28;
        foreach (var direction in directions)
        {
            for (int i = 1; i < range; ++i)
            {
                Vector3Int nextCoords = occupiedSquare + direction * i;
                Piece piece = board.GetPieceOnSquare(nextCoords);
                if (!board.CheckIfCoordsAreOnBoard(nextCoords))
                    break;
                if (piece == null)
                    TryToAddMove(nextCoords);
                else if (!piece.IsFromSameTeam(this))
                {
                    TryToAddMove(nextCoords);
                    break;
                }
                else if (piece.IsFromSameTeam(this))
                    break;
            }
        }
        return avaliableMoves;
    }
}
