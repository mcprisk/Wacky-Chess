using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Board/Layout")]

public class BoardLayout : ScriptableObject
{
    [Serializable]
    private class BoardSquareSetup
    {
        public Vector3Int position;
        public PieceType pieceType;
        public TeamColor teamColor;
    }

    [SerializeField] private BoardSquareSetup[] boardSquares;

    public int GetPiecesCount()
    {
        return boardSquares.Length;
    }

    public Vector3Int GetSquareCoordsAtIndex(int index)
    {
        if(boardSquares.Length <= index)
        {
            Debug.LogError("Index of piece is out of range");
            return new Vector3Int(-1, -1,-1);
        }
        return new Vector3Int(boardSquares[index].position.x-1,
            boardSquares[index].position.y - 1,
            boardSquares[index].position.z - 1);
    }

    public string GetSquarePieceNameAtIndex(int index)
    {
        if (boardSquares.Length <= index)
        {
            Debug.LogError("Index of piece is out of range");
            return "";
        }
        return boardSquares[index].pieceType.ToString();
    }

    public TeamColor GetSquareTeamColorAtIndex(int index)
    {
        if (boardSquares.Length <= index)
        {
            Debug.LogError("Index of piece is out of range");
            return TeamColor.Black;
        }
        return boardSquares[index].teamColor;
    }
}
