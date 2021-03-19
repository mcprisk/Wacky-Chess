using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]

public class MultiPlayerBoard : Board
{
    private PhotonView photonView;

    protected override void Awake()
    {
        base.Awake();
        photonView = GetComponent<PhotonView>();
    }

    public override void SelectPieceMoved(Vector3 coords)
    {
        photonView.RPC(nameof(RPC_OnSelectedPieceMoved),
            RpcTarget.AllBuffered, new object[] { coords });
    }

    public override void SetSelectedPiece(Vector3 coords)
    {
        photonView.RPC(nameof(RPC_OnSetSelectedPiece),
            RpcTarget.AllBuffered, new object[] { coords });
    }

    [PunRPC]
    private void RPC_OnSelectedPieceMoved(Vector3 coords)
    {
        Vector3Int intCoords = new Vector3Int(Mathf.RoundToInt(coords.x), Mathf.RoundToInt(coords.y), Mathf.RoundToInt(coords.z));
        OnSelectedPieceMoved(intCoords);
    }

    [PunRPC]
    private void RPC_OnSetSelectedPiece(Vector3 coords)
    {
        Vector3Int intCoords = new Vector3Int(Mathf.RoundToInt(coords.x), Mathf.RoundToInt(coords.y), Mathf.RoundToInt(coords.z));
        OnSetSelectedPiece(intCoords);
    }
}
