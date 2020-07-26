using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using ExitGames.Client.Photon;
using Photon.Realtime;

public class CounterText : MonoBehaviour, IOnEventCallback
{
    public GameObject player;
    public Text pointCounter;
    public GameObject winnerText;
    public const byte GameOverCode = 1;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        int points = player.GetComponent<PlayerController>().points;
        pointCounter.text = points.ToString();
        if(points >= 3){
            winnerText.SetActive(true);
            winnerText.GetComponent<Text>().text = "Congratulations! You got the most toppings!\nWinner";
            onGameEnd();
        }
    }

    private void OnEnable(){
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable(){
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    private void onGameEnd(){
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // You would have to set the Receivers to All in order to receive this event on the local client as well
        PhotonNetwork.RaiseEvent(GameOverCode, PUN2_RoomController.getNickname(), raiseEventOptions, SendOptions.SendReliable);
    }

    public void OnEvent(EventData photonEvent) {
        byte eventCode = photonEvent.Code;
        if(eventCode == GameOverCode){
            Debug.Log("ROAD ROLLA DA");
        }
    }
}
