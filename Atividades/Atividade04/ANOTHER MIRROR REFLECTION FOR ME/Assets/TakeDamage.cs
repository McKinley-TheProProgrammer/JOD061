using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class TakeDamage : NetworkBehaviour
{
    [SerializeField] GameObject whoTakesDamage;
    [SerializeField] bool isItself;
    public GameObject WhoTakesDamage { get { return whoTakesDamage; } }

    private void Awake()
    {
        if (isItself)
        {
            whoTakesDamage = gameObject;
        }
    }
    [Command]
    public void CmdInstaKill()
    {
        WhoTakesDamage.SetActive(false);
        RpCInstaKill();
    }
    [ClientRpc]
    public void RpCInstaKill()
    {
        WhoTakesDamage.SetActive(false);
    }
}
