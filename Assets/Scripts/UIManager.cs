using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Scene Dependencies")]
    [SerializeField] private NetworkManager networkManager;

    [Header("Buttons")]
    [SerializeField] private Button whiteTeamButton;
    [SerializeField] private Button blackTeamButton;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private TextMeshProUGUI connectionStatusText;

    [Header("Screen Gameobjects")]
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject connect;
    [SerializeField] private GameObject teamSelect;
    [SerializeField] private GameObject gameModeSelect;

    [Header("Screen Gameobjects")]
    [SerializeField] private TMP_Dropdown gameRoomSelect;

    private void Awake()
    {
        gameRoomSelect.ClearOptions();
        gameRoomSelect.AddOptions(Enum.GetNames(typeof(RoomName)).ToList());
        OnGameLaunched();
    }

    private void OnGameLaunched()
    {
        DisableAllScreens();
        gameModeSelect.SetActive(true);
    }

    public void OnSinglePlayerModeSelected()
    {
        DisableAllScreens();
    }

    public void OnMultiPlayerModeSelected()
    {
        connectionStatusText.gameObject.SetActive(true);
        DisableAllScreens();
        connect.SetActive(true);
    }

    internal void OnGameStarted()
    {
        DisableAllScreens();
        connectionStatusText.gameObject.SetActive(false);
    }

    public void OnConnect()
    {
        networkManager.SetRoom((RoomName)gameRoomSelect.value);
        networkManager.Connect();
    }

    private void DisableAllScreens()
    {
        gameOver.SetActive(false);
        connect.SetActive(false);
        teamSelect.SetActive(false);
        gameModeSelect.SetActive(false);
    }

    public void SetConnectionStatus(string status)
    {
        connectionStatusText.text = status;
    }

    public void ShowTeamSelectionScreen()
    {
        DisableAllScreens();
        teamSelect.SetActive(true);
    }

    public void SelectTeam(int team)
    {
        networkManager.SelectTeam(team);
    }

    internal void RestrictTeamChoice(TeamColor occupiedTeam)
    {
        var buttonToDeactivate = occupiedTeam == TeamColor.White ? 
            whiteTeamButton : blackTeamButton;
        buttonToDeactivate.interactable = false;
    }

    internal void OnGameFinished(string winner)
    {
        gameOver.SetActive(true);
        resultText.text = string.Format("{0} Won!", winner);
    }
}
