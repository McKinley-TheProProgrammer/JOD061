using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetCountdown : NetworkBehaviour
{
    [SyncVar]
    public float countdown = 3;
    float aux;
    public bool timeStart;
    
    //[SerializeField] PlayerMovement[] playerMovement;

    public override void OnStartServer()
    {
        timeStart = true;
        
        
        //CountingDown();
    }
    void ActivatePlayerMovements(bool activationYes)
    {
        foreach (PlayerMovement player in FindObjectsOfType<PlayerMovement>())
        {
            player.enabled = activationYes;
        }
    }
    private void Start()
    {
        ActivatePlayerMovements(false);
    }
    //[SerializeField] NetworkManagerHUD managerHUD;
    [ClientRpc]
    public void CountingDown()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0)
        {
            ActivatePlayerMovements(true);
            countdown = aux;
        }
        
    }
    
    private void Update()
    {
        CountingDown();
    }





}
