using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

[CreateAssetMenu(fileName = "New Player",menuName = "Server Players/Player",order = 1)]
public class PlayerProperties : ScriptableObject
{
    [SerializeField] static string playerName = "Mario";
    public static string PlayerName
    {
        get
        {
            int randomValue = Random.Range(0, 9999);
            return playerName + randomValue.ToString();
        }
    }
    public float playerSpeed;
    public Rigidbody playerBody;
    public GameObject daBullet;
    public Transform gun;
    public void Movement()
    {
        float xAxis = Input.GetAxisRaw("Horizontal") * playerSpeed;
        float zAxis = Input.GetAxisRaw("Vertical") * playerSpeed;
        Vector3 movement = new Vector3(xAxis * Time.fixedDeltaTime, playerBody.velocity.y, zAxis * Time.fixedDeltaTime);
        playerBody.velocity = movement;
    }

    
    
    
}

