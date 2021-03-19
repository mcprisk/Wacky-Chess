using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PieceCreator))]
public class GameController : MonoBehaviour
{
    private enum GameState { Init, Play, Finished };
    private GameState state;

    [SerializeField] private BoardLayout startingBoardLayout;
    [SerializeField] private Board board;
    [SerializeField] private UIManager uiManager;

    private PieceCreator pieceCreator;

    private Player whitePlayer;
    private Player blackPlayer;
    private Player activePlayer;

    private void Awake()
    {
        Application.targetFrameRate = 144;
        SetDependencies();
        CreatePlayers();
    }

    private void SetDependencies()
    {
        pieceCreator = GetComponent<PieceCreator>();
    }

    private void CreatePlayers()
    {
        whitePlayer = new Player(TeamColor.White, board);
        blackPlayer = new Player(TeamColor.Black, board);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            uiManager.ToggleMenu();
        }
    }

    private void StartNewGame()
    {
        uiManager.HideUI();
        SetGameState(GameState.Init);
        board.SetDependencies(this);
        CreatePiecesFromLayout(startingBoardLayout);
        activePlayer = whitePlayer;
        GenerateAllPossiblePlayerMoves(activePlayer);
        SetGameState(GameState.Play);
    }

    public void RestartGame()
    {
        DestroyPieces();
        board.onGameRestart();
        whitePlayer.OnGameRestart();
        blackPlayer.OnGameRestart();
        StartNewGame();
    }

    private void DestroyPieces()
    {
        whitePlayer.activePieces.ForEach(p => Destroy(p.gameObject));
        blackPlayer.activePieces.ForEach(p => Destroy(p.gameObject));
    }

    private void CreatePiecesFromLayout(BoardLayout layout)
    {
        for (int i = 0; i < layout.GetPiecesCount(); ++i)
        {
            Vector3Int squareCoords = layout.GetSquareCoordsAtIndex(i);
            TeamColor team = layout.GetSquareTeamColorAtIndex(i);
            string typeName = layout.GetSquarePieceNameAtIndex(i);

            Type type = Type.GetType(typeName);
            CreatePieceAndInitialize(squareCoords, team, type);
        }
    }

    private void CreatePieceAndInitialize(Vector3Int squareCoords, TeamColor team, Type type)
    {
        Piece newPiece = pieceCreator.CreatePiece(type).GetComponent<Piece>();
        newPiece.SetData(squareCoords, team, board);

        Material teamMaterial = pieceCreator.GetTeamMaterial(team);
        newPiece.SetMaterial(teamMaterial);

        board.SetPieceOnBoard(squareCoords, newPiece);

        Player currentPlayer = team == TeamColor.White ? whitePlayer : blackPlayer;
        currentPlayer.AddPiece(newPiece);
    }

    private void GenerateAllPossiblePlayerMoves(Player player)
    {
        player.GenerateAllPossibleMoves();
    }

    internal bool IsTeamTurnActive(TeamColor team)
    {
        return team == activePlayer.team;
    }

    internal void EndTurn()
    {
        GenerateAllPossiblePlayerMoves(activePlayer);
        GenerateAllPossiblePlayerMoves(GetOpponentToPlayer(activePlayer));
        if (CheckIfGameIsFinished()) EndGame();
        else ChangeActiveTeam();
    }

    private void EndGame()
    {
        uiManager.OnGameFinished(activePlayer.team.ToString());
        SetGameState(GameState.Finished);
    }

    private void SetGameState(GameState state)
    {
        this.state = state;
    }

    private bool CheckIfGameIsFinished()
    {
        Piece[] piecesAttackingKing = activePlayer.GetPiecesAttacking<King>();
        if (piecesAttackingKing.Length > 0)
        {
            Player oppositePlayer = GetOpponentToPlayer(activePlayer);
            Piece attackedKing = oppositePlayer.GetPieces<King>().FirstOrDefault();
            oppositePlayer.RemoveMovesEnablingAttackOn<King>(activePlayer, attackedKing);

            int avaliableKingMoves = attackedKing.avaliableMoves.Count;
            if (avaliableKingMoves == 0)
            {
                bool canProtectKing = oppositePlayer.CanHidePieceFromAttack<King>(activePlayer);
                if (!canProtectKing) 
                    return true;
            }
        }
        return false;
    }

    public bool IsGameInProgress()
    {
        return state == GameState.Play;
    }

    private void ChangeActiveTeam()
    {
        activePlayer = activePlayer == whitePlayer ? blackPlayer : whitePlayer;
    }

    private Player GetOpponentToPlayer(Player player)
    {
        return player == whitePlayer ? blackPlayer : whitePlayer;
    }

    public void RemoveMovesEnablingAttackOn<T>(Piece piece) where T : Piece
    {
        activePlayer.RemoveMovesEnablingAttackOn<T>(GetOpponentToPlayer(activePlayer), piece);
    }

    public void OnPieceRemoved(Piece piece)
    {
        Player owner = (piece.team == TeamColor.White) ? whitePlayer : blackPlayer;
        owner.RemovePiece(piece);
        Destroy(piece.gameObject);
    }

    public void RemoveMovesEnablingAttackOnSameColor(Piece piece)
    {
        activePlayer.RemoveMovesEnablingAttackOnSameColor(piece);
    }
}
