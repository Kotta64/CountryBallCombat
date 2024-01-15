using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class Game : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var players = PhotonNetwork.PlayerList;
        GameObject.Find("player1name").GetComponent<Text>().text = players[0].NickName;
        GameObject.Find("player2name").GetComponent<Text>().text = players[1].NickName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
