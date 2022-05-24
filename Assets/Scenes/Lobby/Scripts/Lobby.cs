using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMPro.TMP_InputField createRoom = null;
    [SerializeField] private TMPro.TMP_InputField joinRoom = null;
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(createRoom.text);
    }
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinRoom.text);
    }
}
