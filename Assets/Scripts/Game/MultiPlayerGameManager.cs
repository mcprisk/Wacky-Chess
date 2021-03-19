using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MultiPlayerGameManager : GameController, IOnEventCallback
{
    protected const byte SET_GAME_STATE_EVENT = 1;

    private Player localPlayer;
    private NetworkManager networkManager;

    public void SetMultiPlayerDependencies(NetworkManager networkManager)
    {
        this.networkManager = networkManager;
    }

    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public override bool CanPreformMove()
    {
        if (!IsGameInProgress() || !localPlayersTurn())
            return false;
        return true;
    }

    private bool localPlayersTurn()
    {
        return localPlayer == activePlayer;
    }

    public void SetLocalPlayer(TeamColor team)
    {
        localPlayer = team == TeamColor.White ? whitePlayer : blackPlayer;
    }

    public override void TryToStartGame()
    {
        if (networkManager.IsRoomFull())
        {
            SetGameState(GameState.Play);
        }
    }

    protected override void SetGameState(GameState state)
    {
        object[] content = new object[] { (int)state };
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(SET_GAME_STATE_EVENT, content, raiseEventOptions, SendOptions.SendReliable);
    }

    public void OnEvent(EventData photonEvent)
    {
        byte eventCode = photonEvent.Code;
        if (eventCode == SET_GAME_STATE_EVENT)
        {
            object[] data = (object[])photonEvent.CustomData;
            GameState state = (GameState)data[0];
            this.state = state;
        }
    }
}
