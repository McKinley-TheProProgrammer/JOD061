using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetCountdown : NetworkBehaviour
{
    public float countdown = 3;
    float aux;
    public bool timeStart;
    //[SerializeField] NetworkManagerHUD managerHUD;
    public void CountingDown()
    {
        if (timeStart)
        {
            countdown -= Time.deltaTime;
            if (countdown <= 0)
            {
                countdown = aux;
            }
        }
    }
    void Awake()
    {
        aux = countdown;
    }

    private void Update()
    {
        if (NetworkClient.isConnected && ClientScene.ready)
        {
            timeStart = true;
        }
        CountingDown();
    }

}
