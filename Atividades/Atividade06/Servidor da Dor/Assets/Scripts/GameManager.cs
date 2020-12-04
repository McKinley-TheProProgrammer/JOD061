using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] Transform[] spawnFields;
    [SerializeField] float countdown = 3;
    PlayerController scriptToDeactivate;
    [SerializeField] TextMeshProUGUI countdownText;
    void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.LoadLevel(0);
            return;
        }
        GameObject playerController = PhotonNetwork.Instantiate("ServerPlayer", spawnFields[Random.Range(0,spawnFields.Length)].position, Quaternion.identity, 0);
        scriptToDeactivate = playerController.GetComponent<PlayerController>();
        scriptToDeactivate.enabled = false;
    }

    void CountingDown()
    {
        countdown -= Time.deltaTime;
        int someInt = (int)countdown;
        countdownText.text = someInt.ToString();
        if(countdown <= 0)
        {
            scriptToDeactivate.enabled = true;
            countdown = 0;
            countdownText.gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsConnected)
        {
            CountingDown();
        }
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
