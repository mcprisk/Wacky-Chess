using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    private const string ROOM = "room";
    private const string TEAM = "team";
    private const int MAX_PLAYERS = 2;

    private RoomName roomName;

    [SerializeField] private UIManager uiManager;
    [SerializeField] private GameInitializer gameInitializer;
    private MultiPlayerGameManager gameController;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void SetDependencies(MultiPlayerGameManager gameController)
    {
        this.gameController = gameController;
    }

    private void Update()
    {
        uiManager.SetConnectionStatus(PhotonNetwork.NetworkClientState.ToString());
    }

    public void Connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            Debug.Log($"Connected to server.  Looking for room {roomName}");
            PhotonNetwork.JoinRandomRoom(
                new ExitGames.Client.Photon.Hashtable() { { ROOM, roomName } }, MAX_PLAYERS);
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
        }  
    }

    public bool IsRoomFull()
    {
        return PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers;
    }

    #region Photon Callbacks

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to server.  Looking for random room");
        PhotonNetwork.JoinRandomRoom(
            new ExitGames.Client.Photon.Hashtable() { { ROOM, roomName } }, MAX_PLAYERS);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log($"Joining random room failed because of {message}. Creating room {roomName}.");
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions
        {
            CustomRoomPropertiesForLobby = new string[] { ROOM },
            MaxPlayers = MAX_PLAYERS,
            CustomRoomProperties = new ExitGames.Client.Photon.Hashtable { 
                { ROOM, roomName } 
            }
        });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log($"Player {PhotonNetwork.LocalPlayer.ActorNumber} " +
            $"joined room {(RoomName)PhotonNetwork.CurrentRoom.CustomProperties[ROOM]}");
        gameInitializer.CreateMultiPlayerBoard();
        PrepareTeamSelectionOption();
        uiManager.ShowTeamSelectionScreen();
    }

    private void PrepareTeamSelectionOption()
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount > 1)
        {
            var firstPlayer = PhotonNetwork.CurrentRoom.GetPlayer(1);
            if (firstPlayer.CustomProperties.ContainsKey(TEAM))
            {
                var occupiedTeam = firstPlayer.CustomProperties[TEAM];
                uiManager.RestrictTeamChoice((TeamColor)occupiedTeam);
            }
        }
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Debug.Log($"Player {PhotonNetwork.LocalPlayer.ActorNumber} entered the room");
    }

    internal void SelectTeam(int team)
    {
        PhotonNetwork.LocalPlayer.SetCustomProperties(
            new ExitGames.Client.Photon.Hashtable { { TEAM, team } });
        gameInitializer.InitializeMultiPlayerController();
        gameController.SetLocalPlayer((TeamColor)team);
        gameController.StartNewGame();
        gameController.SetupCamera((TeamColor)team);
    }

    public void SetRoom(RoomName room)
    {
        roomName = room;
        PhotonNetwork.LocalPlayer.SetCustomProperties(
            new ExitGames.Client.Photon.Hashtable { { ROOM, room } });
    }

    #endregion
}
