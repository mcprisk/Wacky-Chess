using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private TMP_Dropdown gameLevelSelect;

    private void Awake()
    {
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

    public void OnConnect()
    {
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
}
