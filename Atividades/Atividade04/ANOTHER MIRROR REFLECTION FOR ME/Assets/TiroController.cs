using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class TiroController : NetworkBehaviour
{
    [SerializeField] float forca = 100f;
    [SerializeField] float countDownToDeactivate = 3f;
    Rigidbody rigid;
    float aux;
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        aux = countDownToDeactivate;     
    }

    // Update is called once per frame
    void Update()
    {
        countDownToDeactivate -= Time.deltaTime;
        if(countDownToDeactivate <= 0)
        {
            countDownToDeactivate = aux;
            gameObject.SetActive(false);          
        }
    }
    private void FixedUpdate()
    {
        rigid.velocity = transform.forward * forca * Time.fixedDeltaTime;
    }
}
