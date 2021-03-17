using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public TeamColor team { get; set; }
    public Board board { get; set; }
    public List<Piece> activePieces { get; private set; }
    public Player(TeamColor team, Board board)
    {
        this.board = board;
        this.team = team;
        activePieces = new List<Piece>();
    }

    public void AddPiece(Piece piece)
    {
        if(!activePieces.Contains(piece))
            activePieces.Add(piece);
    }

    public void RemovePiece(Piece piece)
    {
        if (activePieces.Contains(piece))
            activePieces.Remove(piece);
    }

    public void GenerateAllPossibleMoves()
    {
        foreach (var piece in activePieces)
        {
            if (board.HasPiece(piece))
            {
                piece.SelectAvaliableSquares();
            }
        }
    }

    public Piece[] GetPiecesAttacking<T>() where T : Piece
    {
        return activePieces.Where(p => p.isAttackingPiece<T>()).ToArray();
    }

    public Piece[] GetPieces<T>() where T : Piece
    {
        return activePieces.Where(p => p is T).ToArray();
    }

    public void RemoveMovesEnablingAttackOn<T>(Player opponent, Piece selectedPiece) where T : Piece
    {
        List<Vector3Int> coordsToRemove = new List<Vector3Int>();
        foreach (var coords in selectedPiece.avaliableMoves)
        {
            Piece pieceOnSquare = board.GetPieceOnSquare(coords);
            board.UpdateBoardOnPieceMove(coords, selectedPiece.occupiedSquare, selectedPiece, null);
            opponent.GenerateAllPossibleMoves();
            if (opponent.CheckIfIsAttackingPiece<T>())
                coordsToRemove.Add(coords);
            board.UpdateBoardOnPieceMove(selectedPiece.occupiedSquare, coords, selectedPiece, pieceOnSquare);
        }
        foreach (var coords in coordsToRemove)
        {
            selectedPiece.avaliableMoves.Remove(coords);
        }
    }

    private bool CheckIfIsAttackingPiece<T>() where T : Piece
    {
        foreach (var piece in activePieces)
        {
            if (board.HasPiece(piece) && piece.isAttackingPiece<T>()) 
                return true;
        }
        return false;
    }

    internal bool CanHidePieceFromAttack<T>(Player opponent) where T : Piece
    {
        foreach (var piece in activePieces)
        {
            foreach (var coords in piece.avaliableMoves)
            {
                Piece pieceOnCoords = board.GetPieceOnSquare(coords);
                board.UpdateBoardOnPieceMove(coords, piece.occupiedSquare, piece, null);
                opponent.GenerateAllPossibleMoves();
                if (!opponent.CheckIfIsAttackingPiece<T>())
                {
                    board.UpdateBoardOnPieceMove(piece.occupiedSquare, coords, piece, pieceOnCoords);
                    return true;
                }
                board.UpdateBoardOnPieceMove(piece.occupiedSquare, coords, piece, pieceOnCoords);
            }
        }
        return false;
    }
}
