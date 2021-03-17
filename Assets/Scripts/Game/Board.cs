using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SquareSelectorCreator))]

public class Board : MonoBehaviour
{
    public const int BOARD_SIZE = 8;

    [SerializeField] private Transform XZTransform;
    [SerializeField] private Transform YZTransform;
    [SerializeField] private Transform XYTransform;

    [SerializeField] private int squareSize;

    private Piece[,,] grid;
    private Piece selectedPiece;
    private GameController gameController;
    private SquareSelectorCreator squareSelector;

    private void Awake()
    {
        squareSelector = GetComponent<SquareSelectorCreator>();
        CreateGrid();
    }

    public void SetDependencies(GameController gameController)
    {
        this.gameController = gameController;
    }

    private void CreateGrid()
    {
        grid = new Piece[BOARD_SIZE, BOARD_SIZE, BOARD_SIZE];
    }

    public Vector3 CalculatePositionFromCoords(Vector3Int coords)
    {
        // If 2 zeros or sevens, then coords are on a 45
        // If 1 zero or seven, then coords are on a wall
        // If 0 or 3 zeros or sevens, then coords are invalid
        int zeroOrMax = 0;
        for (int i = 0; i < 3; ++i)
        {
            zeroOrMax += (coords[i] == 0 || coords[i] == 7) ? 1 : 0;
        }

        // 45's

        if (zeroOrMax == 2)
        {
            if (coords.z == 0 && coords.y == 0)
            {
                return new Vector3(
                    6 + squareSize * (coords.x - 1),
                    2,
                    -30);
            }
            else if (coords.x == 0 && coords.y == 0)
            {
                return new Vector3(
                    2,
                    2,
                    -26 + squareSize * (coords.z - 1));
            }
            else if (coords.x == 0 && coords.z == 0)
            {
                return new Vector3(
                    2,
                    6 + squareSize * (coords.y - 1),
                    -30);
            }
            else if (coords.x == 7 && coords.z == 7)
            {
                return new Vector3(
                    30,
                    6 + squareSize * (coords.y - 1),
                    -2);
            }
            else if (coords.y == 7 && coords.z == 7)
            {
                return new Vector3(
                    6 + squareSize * (coords.x - 1),
                    30,
                    -2);
            }
            else if (coords.x == 7 && coords.y == 7)
            {
                return new Vector3(
                    30,
                    30,
                    -26 + squareSize * (coords.z - 1));
            }
            else if (coords.x == 7 && coords.y == 0)
            {
                return new Vector3(
                    30,
                    2,
                    -26 + squareSize * (coords.z - 1));
            }
            else if (coords.z == 7 && coords.y == 0)
            {
                return new Vector3(
                    6 + squareSize * (coords.x - 1),
                    2,
                    -2);
            }
            else if (coords.x == 0 && coords.y == 7)
            {
                return new Vector3(
                    2,
                    30,
                    -26 + squareSize * (coords.z - 1));
            }
            else if (coords.z == 0 && coords.y == 7)
            {
                return new Vector3(
                    6 + squareSize * (coords.x - 1),
                    30,
                    -30);
            }
            else if (coords.x == 0 && coords.z == 7)
            {
                return new Vector3(
                    2,
                    6 + squareSize * (coords.y - 1),
                    -2);
            }
            else if (coords.x == 7 && coords.z == 0)
            {
                return new Vector3(
                    30,
                    6 + squareSize * (coords.y - 1),
                    -30);
            }
        }

        // WALLS

        else if (zeroOrMax == 1)
        {
            // -Y FACE
            if (coords.y == 0)
            {
                return XZTransform.position + new Vector3(
                    (coords.x - 1) * squareSize,
                    0f,
                    (coords.z - 1) * squareSize);
            }

            // +Y FACE
            if (coords.y == 7)
            {
                return XZTransform.position + new Vector3(
                    (coords.x - 1) * squareSize,
                    32f,
                    (coords.z - 1) * squareSize);
            }

            // -X FACE
            else if (coords.x == 0)
            {
                return YZTransform.position + new Vector3(
                    0f,
                    (coords.y - 1) * squareSize,
                    (coords.z - 1) * squareSize);
            }

            // +X FACE
            else if (coords.x == 7)
            {
                return YZTransform.position + new Vector3(
                    32f,
                    (coords.y - 1) * squareSize,
                    (coords.z - 1) * squareSize);
            }

            // -Z FACE
            else if (coords.z == 0)
            {
                return XYTransform.position + new Vector3(
                    (coords.x - 1) * squareSize,
                    (coords.y - 1) * squareSize,
                    0f);
            }

            // +Z FACE
            else if (coords.z == 7)
            {
                return XYTransform.position + new Vector3(
                    (coords.x - 1) * squareSize,
                    (coords.y - 1) * squareSize,
                    32f);
            }
        }


        //Debug.LogError("Invalid Piece Location - Pos");
        return new Vector3Int(-1, -1, -1);
    }

    private Vector3Int CalculateCoordsFromPosition(Vector3 inputPosition)
    {
        //Debug.Log(inputPosition);

        int x;
        int y;
        int z;

        if (inputPosition.x > 4 && inputPosition.x < 28)
            x = Mathf.RoundToInt((inputPosition.x - 2) / squareSize);
        else
            x = inputPosition.x < 4 ? 0 : 7;

        if (inputPosition.y > 4 && inputPosition.y < 28)
            y = Mathf.RoundToInt((inputPosition.y - 2) / squareSize);
        else
            y = inputPosition.y < 4 ? 0 : 7;

        if (inputPosition.z < -4 && inputPosition.z > -28)
            z = 7 - Mathf.RoundToInt((-inputPosition.z - 2) / squareSize);
        else
            z = inputPosition.z > -4 ? 7 : 0;

        //Debug.Log(new Vector3Int(x, y, z));
        return new Vector3Int(x, y, z);
    }

    public Quaternion CalculateRotationFromCoords(Vector3Int coords)
    {
        // If 2 zeros or sevens, then coords are on a 45
        // If 1 zero or seven, then coords are on a wall
        // If 0 or 3 zeros or sevens, then coords are invalid
        int zeroOrMax = 0;
        for (int i = 0; i < 3; ++i)
        {
            zeroOrMax += (coords[i] == 0 || coords[i] == 7) ? 1 : 0;
        }

        // 45's
        if (zeroOrMax == 2)
        {
            if (coords.z == 0 && coords.y == 0)
            {
                return Quaternion.Euler(45, 0, 0);
            }
            else if (coords.x == 0 && coords.y == 0)
            {
                return Quaternion.Euler(0, 0, -45);
            }
            else if (coords.x == 0 && coords.z == 0)
            {
                return Quaternion.Euler(90, 45, 0);
            }
            else if (coords.x == 7 && coords.z == 7)
            {
                return Quaternion.Euler(90, 225, 0);
            }
            else if (coords.y == 7 && coords.z == 7)
            {
                return Quaternion.Euler(225, 0, 0);
            }
            else if (coords.x == 7 && coords.y == 7)
            {
                return Quaternion.Euler(0, 0, -225);
            }
            else if (coords.x == 7 && coords.y == 0)
            {
                return Quaternion.Euler(0, 0, 45);
            }
            else if (coords.z == 7 && coords.y == 0)
            {
                return Quaternion.Euler(-45, 0, 0);
            }
            else if (coords.x == 0 && coords.y == 7)
            {
                return Quaternion.Euler(0, 0, 225);
            }
            else if (coords.z == 0 && coords.y == 7)
            {
                return Quaternion.Euler(-225, 0, 0);
            }
            else if (coords.x == 0 && coords.z == 7)
            {
                return Quaternion.Euler(90, 135, 0);
            }
            else if (coords.x == 7 && coords.z == 0)
            {
                return Quaternion.Euler(90, -45, 0);
            }
        }
        // WALLS

        else if (zeroOrMax == 1)
        {
            // -Y FACE
            if (coords.y == 0 && coords.x != 0 && coords.z != 0)
            {
                return Quaternion.Euler(0, 90, 0);
            }

            // +Y FACE
            else if (coords.y == 7 && coords.x != 7 && coords.z != 7)
            {
                return Quaternion.Euler(0, 90, 180);
            }

            // -X FACE
            else if (coords.x == 0 && coords.y != 0 && coords.z != 0)
            {
                return Quaternion.Euler(0, 0, 270);
            }

            // +X FACE
            else if (coords.x == 7 && coords.y != 7 && coords.z != 7)
            {
                return Quaternion.Euler(0, 0, 90);
            }

            // -Z FACE
            else if (coords.z == 0 && coords.x != 0 && coords.y != 0)
            {
                return Quaternion.Euler(90, 0, 0);
            }

            // +Z FACE
            else if (coords.z == 7 && coords.x != 7 && coords.y != 7)
            {
                return Quaternion.Euler(270, 0, 0);
            }
        }

        // NOT ON A FACE

        //Debug.LogError("Invalid Piece Location - Rot");
        return new Quaternion();
    }

    public void OnSquareSelected(Vector3 inputPosition)
    {
        if (!gameController.IsGameInProgress()) return;

        Vector3Int coords = CalculateCoordsFromPosition(inputPosition);
        Piece piece = GetPieceOnSquare(coords);
        if (selectedPiece)
        {
            if (piece != null && selectedPiece == piece)
            {
                DeselectPiece();
            }
            else if (piece != null && selectedPiece != piece && 
                gameController.IsTeamTurnActive(piece.team))
            {
                SelectPiece(piece);
            }
            else if (selectedPiece.CanMoveTo(coords))
            {
                OnSelectedPieceMoved(coords, selectedPiece);
            }
        }
        else
        {
            if (piece != null && gameController.IsTeamTurnActive(piece.team))
            {
                SelectPiece(piece);
            }
            //Debug.Log(selectedPiece);
        }
    }

    private void SelectPiece(Piece piece)
    {
        gameController.RemoveMovesEnablingAttackOn<King>(piece);
        gameController.RemoveMovesEnablingAttackOnSameColor(piece);
        selectedPiece = piece;
        List<Vector3Int> selection = selectedPiece.avaliableMoves;
        ShowSelectionSquares(selection);
    }

    private void ShowSelectionSquares(List<Vector3Int> selection)
    {
        Dictionary<Tuple<Vector3, Quaternion>, bool> squaresData = 
            new Dictionary<Tuple<Vector3, Quaternion>, bool>();
        for (int i = 0; i < selection.Count; ++i)
        {
            Vector3 position = CalculatePositionFromCoords(selection[i]);
            if (position != new Vector3(-1,-1,-1))
            {
                Quaternion rotation = CalculateRotationFromCoords(selection[i]);
                bool isSquareFree = GetPieceOnSquare(selection[i]) == null;
                squaresData.Add(new Tuple<Vector3, Quaternion>(position, rotation), isSquareFree);
            }
        }
        squareSelector.ShowSelection(squaresData);
    }
    private void DeselectPiece()
    {
        selectedPiece = null;
        squareSelector.ClearSelections();
    }
    private void OnSelectedPieceMoved(Vector3Int coords, Piece piece)
    {
        TryToTakeOppositePiece(coords);
        UpdateBoardOnPieceMove(coords, piece.occupiedSquare, piece, null);
        selectedPiece.MovePiece(coords);
        DeselectPiece();
        EndTurn();
    }

    private void TryToTakeOppositePiece(Vector3Int coords)
    {
        Piece piece = GetPieceOnSquare(coords);
        if (piece != null && !selectedPiece.IsFromSameTeam(piece))
            TakePiece(piece);

    }

    private void TakePiece(Piece piece)
    {
        if (piece)
        {
            grid[piece.occupiedSquare.x, piece.occupiedSquare.y, piece.occupiedSquare.z] = null;
            gameController.OnPieceRemoved(piece);
        }
    }

    private void EndTurn()
    {
        gameController.EndTurn();
    }

    public void UpdateBoardOnPieceMove(Vector3Int newCoords, Vector3Int oldCoords, 
        Piece newPiece, Piece oldPiece)
    {
        grid[oldCoords.x, oldCoords.y, oldCoords.z] = oldPiece;
        grid[newCoords.x, newCoords.y, newCoords.z] = newPiece;
    }

    public Piece GetPieceOnSquare(Vector3Int coords)
    {
        if (CheckIfCoordsAreOnBoard(coords))
        {
            return grid[coords.x, coords.y, coords.z];
        }
        return null;
    }

    public bool CheckIfCoordsAreOnBoard(Vector3Int coords)
    {
        if (coords.x < 0 || coords.x >= BOARD_SIZE ||
            coords.y < 0 || coords.y >= BOARD_SIZE ||
            coords.z < 0 || coords.z >= BOARD_SIZE ||
            ((coords.x == 0 || coords.x == 7) &&
            (coords.y == 0 || coords.y == 7) &&
            (coords.z == 0 || coords.z == 7)) || 
            (coords.x == 0 && coords.y == 0) || // Blocked off areas
            (coords.x == 7 && coords.y == 0) ||
            (coords.z == 0 && coords.y == 7) ||
            (coords.z == 7 && coords.y == 7))
            return false;
        return true;
    }

    public bool HasPiece(Piece piece)
    {
        for (int i = 0; i < BOARD_SIZE; ++i)
        {
            for (int j = 0; j < BOARD_SIZE; ++j)
            {
                for (int k = 0; k < BOARD_SIZE; ++k)
                {
                    if (grid[i, j, k] == piece)
                        return true;
                }
            }
        }
        return false;
    }

    public void SetPieceOnBoard(Vector3Int coords, Piece piece)
    {
        if (CheckIfCoordsAreOnBoard(coords))
        {
            grid[coords.x, coords.y, coords.z] = piece;
        }
    }
}
