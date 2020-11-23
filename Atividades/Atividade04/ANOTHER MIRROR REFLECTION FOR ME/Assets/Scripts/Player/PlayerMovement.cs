using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Mirror;
public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] float playerSpeed = 100f, jumpForce = 200f;
    [SerializeField] float raioDoPulo, turnSmoothVelocity, turnSmoothTime = 0.2f;
    Rigidbody myBody;
    [SyncVar]
    Color color;
    Material mat;
    [SerializeField] bool estaNoChao;
    [SerializeField] Transform arma, cameraPosition;
    [SerializeField] CinemachineTargetGroup cameraGroupPlayers;
    //[SerializeField] GameObject prefabTiro;
    [SerializeField] Pooling prefabTiro;
    GameObject tirinho;
    [SerializeField] TakeDamage takeDamage;

    
    public override void OnStartServer()
    {
        color = InstanciarCorRandomica();      
    }
    public override void OnStartClient()
    {
       
    }
    public override void OnStartLocalPlayer()
    {
        //cam.transform.SetParent(transform);    
        //Camera.main.transform.localPosition = new Vector3(0,1.43f,-5.4f);
        //SetCameraSettings();
    }

    void SetCameraSettings()
    {
        cameraGroupPlayers = GameObject.FindWithTag("TargetCamera").GetComponent<CinemachineTargetGroup>();
        cameraGroupPlayers.AddMember(transform, 1, 10);
    }
    void Start()
    {
        SetCameraSettings();
        mat = GetComponent<Renderer>().material;
        mat.color = color;
        myBody = GetComponent<Rigidbody>();
        prefabTiro = GameObject.FindWithTag("Pooling").GetComponent<Pooling>();
    }
    Color InstanciarCorRandomica()
    {
        return new Color(
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            Random.Range(0f, 1f)
            );
    }
    [Command]
    public void CmdMudarCorDoMaterial(Color corParaTrocar)
    {
        color = corParaTrocar;
        RpcMudarCorDoMaterial(corParaTrocar);
    }
    [ClientRpc]
    public void RpcMudarCorDoMaterial(Color corParaTrocar)
    {
        mat.color = corParaTrocar;
    }
    void Pular()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        estaNoChao = Physics.CheckSphere(transform.GetChild(0).position, raioDoPulo);
        if (estaNoChao)
        {
            myBody.AddRelativeForce(Vector3.up * jumpForce * Time.fixedDeltaTime,ForceMode.Impulse);
        }
    }
    private void Update()
    {
        //CheckServer();
        //CheckClient();
        if (!isLocalPlayer)
        {
            return;
        }
        if (PressFtoPayRespect())
        {
            CmdMudarCorDoMaterial(InstanciarCorRandomica());
            Pular();
        }
        if (PressSpace())
        {
            CmdAtirar();
        }    
    }
    
    [Command]
    public void CmdAtirar()
    {
        tirinho = prefabTiro.GetPooledObject();
        RpCAtirar();
    }
    [ClientRpc]
    public void RpCAtirar()
    {
        tirinho = prefabTiro.GetPooledObject();
        if (tirinho != null)
        {
            tirinho.transform.position = arma.position;
            tirinho.transform.rotation = arma.rotation;
            tirinho.SetActive(true);
            NetworkServer.Spawn(tirinho);
        }
    }
    private void FixedUpdate()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        //CheckLocalPlayer();
        Movimentar();
    }
    //[Command]
    bool PressSpace()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
    //[Command]
    bool PressFtoPayRespect()
    {
        return Input.GetKeyDown(KeyCode.F);
    }
    /// <summary>
    /// Checa se está somente no servidor
    /// </summary>
    public void CheckServer()
    {
        if (isServer)
        {
            //print("Somente no servidor");
        }
    }
    /// <summary>
    /// Checa se está somente no cliente
    /// </summary>
    public void CheckClient()
    {
        if (isClient)
        {
            //print("Somente no cliente");
        }
        
    }
    /// <summary>
    /// Checa se tem o Local player ou não
    /// </summary>
    public void CheckLocalPlayer()
    {
        if (!isLocalPlayer)
        {
            return;
        }      
    }
    void Movimentar()
    {
        float xAxis = Input.GetAxisRaw("Horizontal") * playerSpeed;
        float zAxis = Input.GetAxisRaw("Vertical") * playerSpeed;
        Vector3 movement = new Vector3(xAxis, 0f, zAxis).normalized;
        if(movement.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            myBody.velocity = moveDir.normalized * playerSpeed * Time.fixedDeltaTime;
            //myBody.AddForce(moveDir.normalized * playerSpeed * Time.fixedDeltaTime);
        }
        /* float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + cameraPosition.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        myBody.velocity = moveDir.normalized * playerSpeed * Time.fixedDeltaTime; */
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "DeathPlatform":
                takeDamage.CmdInstaKill();
                cameraGroupPlayers.RemoveMember(transform);
                break;
        }
    }
}
