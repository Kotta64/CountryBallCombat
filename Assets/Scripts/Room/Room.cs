using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class Room : MonoBehaviourPunCallbacks
{
    private Text P1Name;
    private Text P2Name;
    private Text log;
    private InputField msg;
    private List<string> log_data = new List<string>();
    
    void Start()
    {
        GameObject.Find("RoomID").GetComponent<Text>().text = "RoomID: " + PhotonNetwork.CurrentRoom.Name;
        P1Name = GameObject.Find("Player1_Name").GetComponent<Text>();
        P2Name = GameObject.Find("Player2_Name").GetComponent<Text>();
        log = GameObject.Find("Log").GetComponent<Text>();
        msg = GameObject.Find("MessageBox").GetComponent<InputField>();

        refreshName();
    }

    void refreshName(){
        var players = PhotonNetwork.PlayerList;
        P1Name.text = players[0].NickName;
        if(players.Length > 1) P2Name.text = PhotonNetwork.PlayerList[1].NickName;
        else P2Name.text = "";
    }

    [PunRPC]
    private void addLog(string data) {
        log_data.Add(data);

        if(log_data.Count > 6) {
            log_data.RemoveAt(0);
        }

        log.text = "";
        foreach(string str in log_data) {
            log.text += str + "\n";
        }
    }

    public void SendMessage() {
        if(msg.text.Length > 0){
            string text = string.Format("{0} > {1}", PhotonNetwork.LocalPlayer.NickName, msg.text);
            photonView.RPC("addLog", RpcTarget.All, text);
            msg.text = "";
        }
    }

    public void Back2Menu() {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("TitleScene");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer) {
       refreshName();
       addLog(String.Format("{0} enters the room.", newPlayer.NickName));
    }

    public override void OnPlayerLeftRoom(Player player) {
        refreshName();
        addLog(String.Format("{0} left the room.", player.NickName));
    }
}
