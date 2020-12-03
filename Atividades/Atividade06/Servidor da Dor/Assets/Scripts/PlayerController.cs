using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerController : MonoBehaviour
{
    
    [SerializeField] Mesh[] meshes;
    [SerializeField] float speed;
    [SerializeField] PlayerProperties player;
    PhotonView photonView;
    Rigidbody rigid;
    [SerializeField] Transform arma;
    [SerializeField] GameObject bulletPrefab;
    Color color;
    MaterialPropertyBlock mat;
    float turnSmooth = 0.15f;
    bool hasJumped;
    //[SerializeField] int numberOfDifferentMeshes;
    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        photonView = GetComponent<PhotonView>();
        #region ScriptableObjectTried
        //player.playerBody = rigid;
        //player.playerPhotonView = photonView;
        //player.gun = arma;
        //player.daBullet = bulletPrefab;
        //GetComponent<MeshFilter>().sharedMesh = meshes[Random.Range(0, meshes.Length)];
        #endregion
    }
    void FixedUpdate()
    {
        if (!photonView.IsMine) return;
        Movement();
        //player.Movement();
    }
    public void Movement()
    {
        float xAxis = Input.GetAxisRaw("Horizontal") * speed;
        float zAxis = Input.GetAxisRaw("Vertical") * speed;
        
        Vector3 movement = new Vector3(xAxis * Time.fixedDeltaTime, rigid.velocity.y, zAxis * Time.fixedDeltaTime);
        Rotacao(movement);
        rigid.velocity = movement;
    }
    void Rotacao(Vector3 movement)
    {
        float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmooth, .35f);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        movement = moveDir;
    }
    private void Update()
    {
        if (!photonView.IsMine) return;
        if (Input.GetKeyDown(KeyCode.X))
        {
            photonView.RPC("ChangeColor", RpcTarget.AllBuffered, Random.Range(0f,1f), Random.Range(0f,1f),Random.Range(0f,1f));
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            photonView.RPC("Atirar", RpcTarget.AllBuffered);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            photonView.RPC("Pular", RpcTarget.AllBuffered);
        }
        
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bala"))
        {
            PhotonNetwork.LeaveRoom();
        }
        if (other.gameObject.CompareTag("Ground"))
        {
            hasJumped = false;
        }
    }

    [PunRPC]
    void Atirar()
    {
        Instantiate(bulletPrefab, arma.position, arma.rotation);
        

    }
    [PunRPC]
    void ChangeColor(float r, float g,float b)
    {
        color = new Color(r,g,b);
        Material myMat = GetComponent<Renderer>().material;
        myMat.SetColor("_Color", color);
    }
    [PunRPC]
    void Pular()
    {
        if (Input.GetKeyDown(KeyCode.F) && !hasJumped)
        {
            rigid.AddForce(new Vector3(0f, 2000f, 0f));
            hasJumped = true;
        }
    }
    /*void FixedUpdate()
    {
        float xAxis = Input.GetAxisRaw("Horizontal") * speed;
        float zAxis = Input.GetAxisRaw("Vertical") * speed;
        if (!photonView.IsMine)
        {
            return;
        }
        Vector3 movement = new Vector3(xAxis * Time.fixedDeltaTime, myBody.velocity.y , zAxis * Time.fixedDeltaTime);
        myBody.velocity = movement;
    }*/
}
