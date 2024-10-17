using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corrutina : MonoBehaviour
{
    public GameObject miEsfera;
    public int tiempo;
    void Start()
    {
        StartCoroutine(W_Apagar());
    }

    IEnumerator W_Apagar()
    {
        miEsfera.SetActive(false);
        yield return new WaitForSeconds(tiempo);
        miEsfera .SetActive(true);


    }

}
