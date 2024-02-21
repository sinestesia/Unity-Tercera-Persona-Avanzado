using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaAtaque : MonoBehaviour
{
    private Transform enemigo;

    private void Awake()
    {
        enemigo = transform.parent;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemigo.GetComponent<Enemigo_Controlador>().DentroAreaAtaque();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemigo.GetComponent<Enemigo_Controlador>().FueraAreaAtaque();
        }
    }
}
