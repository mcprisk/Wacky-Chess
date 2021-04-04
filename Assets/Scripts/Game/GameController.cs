using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PieceCreator))]
public abstract class GameController : MonoBehaviour
{
    public enum GameState { Init, Play, Finished };
    protected GameState state;

    [SerializeField] private BoardLayout startingBoardLayout;
    private Board board;
    private UIManager uiManager;
    private CameraFlip cameraFlip;

    private PieceCreator pieceCreator;

    protected Player whitePlayer;
    protected Player blackPlayer;
    protected Player activePlayer;

    protected abstract void SetGameState(GameState state);
    public abstract void TryToStartGame();
    public abstract bool CanPreformMove();

    private void Awake()
    {
        pieceCreator = GetComponent<PieceCreator>();
        Application.targetFrameRate = 144;
    }

    public void SetDependencies(UIManager uiManager, Board board, CameraFlip camera)
    {
        this.uiManager = uiManager;
        this.board = board;
        cameraFlip = camera;
    }

    public void CreatePlayers()
    {
        whitePlayer = new Player(TeamColor.White, board);
        blackPlayer = new Player(TeamColor.Black, board);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //uiManager.ToggleMenu();
        }
    }

    public void StartNewGame()
    {
        uiManager.OnGameStarted();
        SetGameState(GameState.Init);
        CreatePiecesFromLayout(startingBoardLayout);
        activePlayer = whitePlayer;
        GenerateAllPossiblePlayerMoves(activePlayer);
        TryToStartGame();
    }

    public void SetupCamera(TeamColor team)
    {
        cameraFlip.SetupCamera(team);
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

    public void CreatePieceAndInitialize(Vector3Int squareCoords, TeamColor team, Type type)
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
