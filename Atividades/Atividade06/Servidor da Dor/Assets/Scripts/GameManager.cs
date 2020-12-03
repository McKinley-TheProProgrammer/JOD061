using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] Transform[] spawnFields;
    void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.LoadLevel(0);
            return;
        }
        PhotonNetwork.Instantiate("ServerPlayer", spawnFields[Random.Range(0,spawnFields.Length)].position, Quaternion.identity, 0);
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PhotonNetwork.LeaveRoom();
        }
    }
    public override void OnLeftRoom()
    {
        print("Jogador: " + PhotonNetwork.LocalPlayer.NickName + " saiu da sala do jogo");
        PhotonNetwork.Disconnect();
        PhotonNetwork.LoadLevel(0);
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        print("You have been disconnected");        
    }
}
