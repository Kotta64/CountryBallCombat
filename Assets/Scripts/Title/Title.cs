using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class Title : MonoBehaviourPunCallbacks
{
    private GameObject roomWindow;
    private InputField nameInput;
    private InputField roomInput;
    private Text nameError;
    private Text roomError;

    // Start is called before the first frame update
    void Start()
    {
        roomWindow = GameObject.Find("RoomIDUI");
        nameInput = GameObject.Find("PlayerNameInput").GetComponent<InputField>();
        roomInput = GameObject.Find("RoomIDInput").GetComponent<InputField>();
        nameError = GameObject.Find("NameError").GetComponent<Text>();
        roomError = GameObject.Find("RoomError").GetComponent<Text>();

        roomWindow.SetActive(false);

        PhotonNetwork.ConnectUsingSettings();
    }

    public void OnClick_Name(){
        string name = nameInput.text;
        if(name.Length > 0 && name.Length < 15){
            nameError.text = "";
            PhotonNetwork.NickName = nameInput.text;
            roomWindow.SetActive(true);
        }else{
            nameError.text = "Name must be between 0 and 14 characters";
        }
    }

    public void OnClick_Room(){
        string id =roomInput.text;
        if(id.Length > 0 && id.Length < 15){
            roomError.text = "";

            var roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 2;
            PhotonNetwork.JoinOrCreateRoom(roomInput.text, roomOptions, TypedLobby.Default);

            roomInput.text = "";
        }else{
            roomError.text = "RoomID must be between 0 and 14 characters";
        }
    }

    public override void OnJoinedRoom() {
        SceneManager.LoadScene("RoomScene");
    }

    public override void OnJoinRoomFailed(short returnCode, string message) {
        roomError.text = "Failed to join a room.";
    }

    public override void OnCreateRoomFailed(short returnCode, string message) {
        roomError.text = "Failed to create a room.";
    }
}
