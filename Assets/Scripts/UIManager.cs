using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject UIParent;
    [SerializeField] private TextMeshProUGUI resultText;

    public void HideUI()
    {
        UIParent.SetActive(false);
    }

    public void OnGameFinished(string winner)
    {
        UIParent.SetActive(true);
        resultText.text = (winner != "Pause Menu:") ? string.Format("{0} Won!", winner) : winner;
    }

    internal void ToggleMenu()
    {
        if (UIParent.activeSelf) HideUI();
        else OnGameFinished("Pause Menu:");
    }
}
