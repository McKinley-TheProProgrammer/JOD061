using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 100, countdown = 3;
    float countAux;
    Rigidbody rbBullet;
    void Awake()
    {
        ChangeBulletColor();
        countAux = countdown;
        rbBullet = GetComponent<Rigidbody>();
        Destroy(gameObject, countdown);
    }
    
    void ChangeBulletColor()
    {
        Material m = GetComponent<Renderer>().material;
        Color randomColorA = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        //Color randomColorB = Color.Lerp(Color.black, Color.white, Random.Range(0f, 1f));
        m.color = Color.Lerp(randomColorA, randomColorA, 1f);
    }
    
    void FixedUpdate()
    {
        rbBullet.velocity = (transform.forward * bulletSpeed) * Time.fixedDeltaTime + Vector3.Lerp(transform.forward,transform.position, bulletSpeed * .5f);
    }
    
}
