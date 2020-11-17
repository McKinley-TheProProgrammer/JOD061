using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class Pooling : NetworkBehaviour
{
    [SerializeField] List<GameObject> listaDeObjetos = new List<GameObject>();
    [SerializeField] GameObject preFab;
    [SerializeField] int numeroDeObjetos;
    [SerializeField] bool vaiCrescer;
    void Start()
    { 
        for (int i = 0; i < numeroDeObjetos; i++)
        {
            GameObject obj = Instantiate(preFab);
            obj.SetActive(false);
            listaDeObjetos.Add(obj);
        }
    }

    // Update is called once per frame
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < listaDeObjetos.Count; i++)
        {
            if (!listaDeObjetos[i].activeInHierarchy)
            {
                return listaDeObjetos[i];
            }
        }
        if (vaiCrescer)
        {
            GameObject obj = Instantiate(preFab);
            listaDeObjetos.Add(obj);
            return obj;
        }
        return null;
    }
}
