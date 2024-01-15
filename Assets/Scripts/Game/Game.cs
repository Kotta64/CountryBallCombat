using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class Game : MonoBehaviourPunCallbacks
{
    private GameObject LeftWindow;

    // Start is called before the first frame update
    void Start()
    {
        var players = PhotonNetwork.PlayerList;
        GameObject.Find("player1name").GetComponent<Text>().text = players[0].NickName;
        GameObject.Find("player2name").GetComponent<Text>().text = players[1].NickName;

        LeftWindow = GameObject.Find("PlayerLeft");
        LeftWindow.SetActive(false);
    }

    public override void OnPlayerLeftRoom(Player player){
        LeftWindow.SetActive(true);
    }

    public void back2room() {
        SceneManager.LoadScene("RoomScene");
    }

    public void back2title() {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("TitleScene");
    }
}
