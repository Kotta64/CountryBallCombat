using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class CharaSelector : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private int charaCount = 3;
    [SerializeField]
    private int playerId = 1;
    private int charaId;
    private Image charaImg;
    [SerializeField]
    private Sprite[] Images;
    private GameObject player = null;
    // Start is called before the first frame update
    void Start()
    {
        charaImg = GameObject.Find("player" + playerId.ToString() + "img").GetComponent<Image>();
    }

    [PunRPC]
    private void changeID(int d) {
        charaId+=d;
        charaId = Mathf.Clamp(charaId, 0, charaCount-1);
        charaImg.sprite = Images[charaId*2];
    }

    [PunRPC]
    private void changeFace() {
        charaImg.sprite = Images[charaId*2+1];
    }

    public void IdUp() {
        if(PhotonNetwork.PlayerList[playerId-1].NickName == PhotonNetwork.LocalPlayer.NickName){
            photonView.RPC("changeID", RpcTarget.All, -1);
        }
    }

    public void IdDown() {
        if(PhotonNetwork.PlayerList[playerId-1].NickName == PhotonNetwork.LocalPlayer.NickName){
            photonView.RPC("changeID", RpcTarget.All, 1);
        }
    }

    public void Spawn() {
        if(PhotonNetwork.PlayerList[playerId-1].NickName == PhotonNetwork.LocalPlayer.NickName){
            photonView.RPC("changeFace", RpcTarget.All);
            if(player != null) {
                PhotonNetwork.Destroy(player);
            }
            player = PhotonNetwork.Instantiate("Player", new Vector3(2.3f * (playerId*2-3), 1f, 0f), Quaternion.identity);
            player.GetComponent<PlayerController>().ChangeChara(charaId+1);
        }
    }
}
