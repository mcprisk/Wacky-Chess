using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [Header("Game mode dependent objects")]

    [SerializeField] private SinglePlayerGameController singlePlayerGameControllerPrefab;
    [SerializeField] private MultiPlayerGameManager multiPlayerGameControllerPrefab; // If you are reading this, hopefully Unity lets you change names easily now...
    [SerializeField] private SinglePlayerBoard singlePlayerGameBoardPrefab;
    [SerializeField] private MultiPlayerBoard multiPlayerGameBoardPrefab;

    [Header("Scene references")]

    [SerializeField] private NetworkManager networkManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private Transform boardLocation;
    [SerializeField] private CameraFlip cameraFlip;

    public void CreateMultiPlayerBoard()
    {
        if (!networkManager.IsRoomFull())
        {
            PhotonNetwork.Instantiate(multiPlayerGameBoardPrefab.name, boardLocation.position, boardLocation.rotation);
        }
    }

    public void CreateSinglePlayerBoard()
    {
        Instantiate(singlePlayerGameBoardPrefab, boardLocation);
    }

    public void InitializeMultiPlayerController()
    {
        MultiPlayerBoard board = FindObjectOfType<MultiPlayerBoard>();
        if (board)
        {
            MultiPlayerGameManager controller = Instantiate(multiPlayerGameControllerPrefab);
            controller.SetDependencies(uiManager, board, cameraFlip);
            controller.CreatePlayers();
            controller.SetMultiPlayerDependencies(networkManager);
            networkManager.SetDependencies(controller);
            board.SetDependencies(controller);
        }
    }

    public void InitializeSinglePlayerController()
    {
        SinglePlayerBoard board = FindObjectOfType<SinglePlayerBoard>();
        if (board)
        {
            SinglePlayerGameController controller = Instantiate(singlePlayerGameControllerPrefab);
            controller.SetDependencies(uiManager, board, cameraFlip);
            controller.CreatePlayers();
            board.SetDependencies(controller);
            controller.StartNewGame();
        }
    }
}
