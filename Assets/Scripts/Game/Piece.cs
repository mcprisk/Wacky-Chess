using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(MaterialSetter))]

public abstract class Piece : MonoBehaviour
{
    private MaterialSetter materialSetter;

    public Board board { protected get; set; }

    public Vector3Int occupiedSquare;

    public TeamColor team { get; set; }

    public bool hasMoved { get; private set; }
    public List<Vector3Int> avaliableMoves;

    private Mover mover;

    public virtual List<Vector3Int> SelectAvaliableSquares()
    {
        foreach (var move in avaliableMoves.ToArray())
        {
            if (!board.CheckIfCoordsAreOnBoard(move))
                avaliableMoves.Remove(move);
        }
        return avaliableMoves;
    }

    private void Awake()
    {
        avaliableMoves = new List<Vector3Int>();
        mover = GetComponent<Mover>();
        materialSetter = GetComponent<MaterialSetter>();
        hasMoved = false;
    }

    public void SetMaterial(Material material)
    {
        if (materialSetter == null)
            materialSetter = GetComponent<MaterialSetter>();
        materialSetter.SetSingleMaterial(material);
    }

    public bool IsFromSameTeam(Piece piece)
    {
        return team == piece.team;
    }

    public bool isAttackingPiece<T>() where T : Piece
    {
        foreach (var square in avaliableMoves)
        {
            if (board.GetPieceOnSquare(square) is T && 
                !this.IsFromSameTeam(board.GetPieceOnSquare(square))) 
                return true;
        }
        return false;
    }

    public bool CanMoveTo(Vector3Int coords)
    {
        return avaliableMoves.Contains(coords);
    }

    public virtual void MovePiece(Vector3Int coords)
    {
        Vector3 targetPosition = board.CalculatePositionFromCoords(coords);
        Quaternion targetRotation = board.CalculateRotationFromCoords(coords);
        occupiedSquare = coords;
        hasMoved = true;
        mover.MoveTo(transform, targetPosition, targetRotation);
    }

    protected bool TryToAddMove(Vector3Int coords)
    {
        if (avaliableMoves.Contains(coords)) return false;
        
        avaliableMoves.Add(coords);
        return true;
    }

    public void SetData(Vector3Int coords, TeamColor team, Board board)
    {
        this.team = team;
        occupiedSquare = coords;
        this.board = board;
        transform.position = board.CalculatePositionFromCoords(coords);
        transform.rotation = board.CalculateRotationFromCoords(coords);
    }
}
