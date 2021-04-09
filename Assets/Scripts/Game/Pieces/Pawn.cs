using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
    private Vector3Int firstSquare;
    private List<Vector3Int> moveDirections = new List<Vector3Int>();
    private List<Vector3Int> attackDirections = new List<Vector3Int>();

    private void Start()
    {
        firstSquare = occupiedSquare;
        if (team == TeamColor.White)
        {
            if (occupiedSquare.z >= 4)
            {
                moveDirections.Add(new Vector3Int(0,0,1));
                moveDirections.Add(new Vector3Int(0, 1, 0));
                attackDirections.Add(new Vector3Int(1, 0, 1));
                attackDirections.Add(new Vector3Int(-1, 0, 1));
                attackDirections.Add(new Vector3Int(1, 1, 0));
                attackDirections.Add(new Vector3Int(-1, 1, 0));
            }
            else
            {
                moveDirections.Add(new Vector3Int(0, 0, -1));
                moveDirections.Add(new Vector3Int(0, 1, 0));
                attackDirections.Add(new Vector3Int(1, 0, -1));
                attackDirections.Add(new Vector3Int(-1, 0, -1));
                attackDirections.Add(new Vector3Int(1, 1, 0));
                attackDirections.Add(new Vector3Int(-1, 1, 0));
            }

            SelectAvaliableSquares();
        } 
        else
        {
            if (occupiedSquare.x >= 4)
            {
                moveDirections.Add(new Vector3Int(1, 0, 0));
                moveDirections.Add(new Vector3Int(0, -1, 0));
                attackDirections.Add(new Vector3Int(1, 0, 1));
                attackDirections.Add(new Vector3Int(1, 0, -1));
                attackDirections.Add(new Vector3Int(0, -1, -1));
                attackDirections.Add(new Vector3Int(0, -1, -1));
            }
            else
            {
                moveDirections.Add(new Vector3Int(-1, 0, 0));
                moveDirections.Add(new Vector3Int(0, -1, 0));
                attackDirections.Add(new Vector3Int(-1, 0, 1));
                attackDirections.Add(new Vector3Int(-1, 0, -1));
                attackDirections.Add(new Vector3Int(0, -1, -1));
                attackDirections.Add(new Vector3Int(0, -1, -1));
            }

            SelectAvaliableSquares();
        }
    }

    public override List<Vector3Int> SelectAvaliableSquares()
    {
        avaliableMoves.Clear();

        MakeMove(occupiedSquare);
        if (firstSquare == occupiedSquare)
        {
            foreach (var direction in moveDirections)
            {
                if (!board.GetPieceOnSquare(occupiedSquare + direction))
                    MakeMove(occupiedSquare + direction);
            }
        }

        foreach (var direction in attackDirections)
        {
            if (board.GetPieceOnSquare(occupiedSquare + direction))
                avaliableMoves.Add(occupiedSquare + direction);
        }

        base.SelectAvaliableSquares();

        return avaliableMoves;
    }

    private void MakeMove(Vector3Int square)
    {
        foreach (var direction in moveDirections)
        {
            if (!avaliableMoves.Contains(square + direction) && !board.GetPieceOnSquare(square + direction))
                avaliableMoves.Add(square + direction);
            if (board.CheckIfCoordsAreOnBlocker(square + direction))
                PromotePawn();
        }
    }

    private void PromotePawn()
    {
        board.PromotePawn(this);
    }
}
