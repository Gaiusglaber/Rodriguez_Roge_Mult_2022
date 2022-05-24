using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RoomController : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GridController roomController;
    [SerializeField] private Color[] randomColors = null;
    [SerializeField] private GameObject parent = null;
    [SerializeField] private PanelView UIPanel = null;
    [SerializeField] private WiningPanelView winingPanel = null;
    private List<PlayerController> players = new List<PlayerController>();
    private List<PanelView> panelViews = new List<PanelView>();
    private List<int> playersScore = new List<int>();
    private List<string> playerNames = new List<string>();
    void Start()
    {
        int randomIndex = Random.Range(0, roomController.Positions.Count);
        GameObject actualPlayer = PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(roomController.Positions[randomIndex].transform.position.x,
            roomController.Positions[randomIndex].transform.position.y+0.4f,0),Quaternion.identity);
        Color randomColor = randomColors[Random.Range(0, randomColors.Length)];
        actualPlayer.GetComponent<SpriteRenderer>().color = randomColor;
        GameObject actualPanelGo = PhotonNetwork.Instantiate(UIPanel.gameObject.name, Vector3.zero, Quaternion.identity);
        actualPanelGo.transform.parent = parent.transform;
        PanelView actualPanelView = actualPanelGo.GetComponent<PanelView>();
        actualPanelView.Configure(randomColor, PhotonNetwork.LocalPlayer.NickName);
        playerNames.Add(PhotonNetwork.LocalPlayer.NickName);
        PlayerController actualPlayerController = actualPlayer.GetComponent<PlayerController>();
        actualPlayerController.OnWin += RestartRoom;
        players.Add(actualPlayerController);
        playersScore.Add(0);
    }
    private void RestartRoom(PlayerController winningPlayer,PlayerController losingPlayer)
    {
        int indexOfPlayer = players.IndexOf(winningPlayer);
        if (players.Count == 1)
        {
            winingPanel.gameObject.SetActive(true);
            winingPanel.winningPlayerTxt.text = playerNames[indexOfPlayer] + "Won!";
            winingPanel.scoreTxt.text = "Score:" + playersScore[indexOfPlayer];
        }
        else if (players.Count < 1)
        {
            winingPanel.gameObject.SetActive(true);
            winingPanel.winningPlayerTxt.text = "Tie!";
            return;
        }
        panelViews[indexOfPlayer].UpdateValue(playersScore[indexOfPlayer]++);
        players.Remove(losingPlayer);
    }
}
