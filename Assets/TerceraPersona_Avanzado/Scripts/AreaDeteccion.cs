using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDeteccion : MonoBehaviour
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
            enemigo.GetComponent<Enemigo_Controlador>().DentroAreaColision();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemigo.GetComponent<Enemigo_Controlador>().FueraAreaColision();
        }
    }
}
