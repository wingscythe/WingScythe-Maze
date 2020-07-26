using UnityEngine;
using Photon.Pun;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class PUN2_RoomController : MonoBehaviourPunCallbacks {

    //Player instance prefab, must be located in the Resources folder
    public GameObject playerPrefab;

    public GameObject mazePrefab;

    public GameObject currMaze;
    //Player spawn point
    public Transform[] _spawnPoint;

    public int seed;

    public double cooldown;

    public double lastTime;

    // Use this for initialization
    void Start () {
        //In case we started this demo with the wrong scene being active, simply load the menu scene
        if (PhotonNetwork.CurrentRoom == null) {
            Debug.Log("Is not in the room, returning back to Lobby");
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameLobby");
            return;
        }

        PhotonNetwork.Instantiate(playerPrefab.name, _spawnPoint[PhotonNetwork.LocalPlayer.ActorNumber - 1].transform.position, _spawnPoint[PhotonNetwork.LocalPlayer.ActorNumber - 1].transform.rotation);
    
        bool isMasterClient = PhotonNetwork.IsMasterClient;
        
        if(isMasterClient){
            //Control and sync maze spawn
            seed = Random.Range(int.MinValue, int.MaxValue);
            photonView.RPC("setSeed", RpcTarget.All, seed);
            currMaze = PhotonNetwork.Instantiate(mazePrefab.name, Vector3.zero , Quaternion.identity);
        }

        lastTime=PhotonNetwork.Time;
        cooldown = Random.Range(30, 60);
    }

    void Update(){
        bool isMasterClient = PhotonNetwork.IsMasterClient;
        if(isMasterClient){
            if(cooldown<=0){
                //Control and sync maze spawn
                PhotonNetwork.Destroy(mazePrefab);
                seed = Random.Range(int.MinValue, int.MaxValue);
                photonView.RPC("setSeed", RpcTarget.All, seed);
                currMaze = PhotonNetwork.Instantiate(mazePrefab.name, Vector3.zero , Quaternion.identity);
                cooldown = Random.Range(30,60);
                lastTime= PhotonNetwork.Time;
            }else{
                cooldown-=PhotonNetwork.Time-lastTime;
                lastTime= PhotonNetwork.Time;
            }

        }
    }

    [PunRPC]
    void setSeed(int sentSeed){
        seed = sentSeed;
        Debug.Log("RECIEVED SEED: " + seed);
    }

    public int getSeed(){
        return seed;
    }

    void OnGUI () {
        if (PhotonNetwork.CurrentRoom == null)
            return;

        //Leave this Room
        // if (GUI.Button(new Rect(5, 5, 125, 25), "Leave Room")) {
        //     PhotonNetwork.LeaveRoom();
        // }

        //Show the Room name
        GUI.Label(new Rect(135, 5, 200, 25), PhotonNetwork.CurrentRoom.Name);

        //Show the list of the players connected to this Room
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++) {
            //Show if this player is a Master Client. There can only be one Master Client per Room so use this to define the authoritative logic etc.)
            string isMasterClient = (PhotonNetwork.PlayerList[i].IsMasterClient ? ": MasterClient" : "");
            GUI.Label(new Rect(5, 35 + 30 * i, 200, 25), PhotonNetwork.PlayerList[i].NickName + isMasterClient);
        }
    }

    public override void OnLeftRoom () {
        //We have left the Room, return back to the GameLobby
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameLobby");
    }
}