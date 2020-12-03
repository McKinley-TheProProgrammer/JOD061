using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class PhotonConexao : MonoBehaviourPunCallbacks
{
    //[SerializeField] PlayerProperties serverPlayer;
    // Start is called before the first frame update
    [SerializeField] InputField nomeDoJogador;
    [SerializeField] Button btnConnect, iniciarGayme;
    private void Start()
    {
        iniciarGayme.interactable = false;
    }
    public void Conectar()
    {
        nomeDoJogador.interactable = false;
        btnConnect.interactable = false;
        
        PhotonNetwork.GameVersion = "0.0.2";
        PhotonNetwork.NickName = nomeDoJogador.text/*"Mario" + Random.Range(0,9999)*/;
        PhotonNetwork.ConnectUsingSettings();
    }
    public void IniciarJogo()
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 20;
        PhotonNetwork.JoinOrCreateRoom("JOD061 Classroom", options, TypedLobby.Default);
    }
    public void Desconectar()
    {
        PhotonNetwork.LeaveRoom();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Conectar();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!PhotonNetwork.IsConnected)
            {
                return;
            }
            Desconectar();
        }
    }
    // Update is called once per frame
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        iniciarGayme.interactable = true;
    }
    public override void OnJoinedRoom()
    {
        print("Jogador " + PhotonNetwork.LocalPlayer.NickName + " entrou na sala de aula: " + PhotonNetwork.CurrentRoom.Name);


        //PhotonNetwork.Instantiate("ServerPlayer", new Vector3(Random.Range(-5, 5), 0f, Random.Range(-2, 2)),Quaternion.identity);
        PhotonNetwork.LoadLevel(1);
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Jogador " + PhotonNetwork.LocalPlayer.NickName + " não conseguiu entrar no servidor porque " + message);
    }
    
}
