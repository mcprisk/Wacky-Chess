using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlayerBoard : Board
{
    public override void SelectPieceMoved(Vector3 coords)
    {
        Vector3Int intCoords = new Vector3Int(Mathf.RoundToInt(coords.x), Mathf.RoundToInt(coords.y), Mathf.RoundToInt(coords.z));
        OnSelectedPieceMoved(intCoords);
    }

    public override void SetSelectedPiece(Vector3 coords)
    {
        Vector3Int intCoords = new Vector3Int(Mathf.RoundToInt(coords.x), Mathf.RoundToInt(coords.y), Mathf.RoundToInt(coords.z));
        OnSetSelectedPiece(intCoords);
    }
}