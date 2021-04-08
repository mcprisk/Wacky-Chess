using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlayerGameController : GameController
{
    public override bool CanPreformMove()
    {
        if (!IsGameInProgress()) 
            return false;
        return true;
    }

    public override void TryToStartGame()
    {
        SetGameState(GameState.Play);
        SetupCamera(TeamColor.White);
    }

    protected override void SetGameState(GameState state)
    {
        this.state = state;
    }
}
