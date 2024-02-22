using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeteccionArqueros : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            AlertarArqueros();
        }
    }

    private void AlertarArqueros() {
        GameObject[] enemigoArqueros = GameObject.FindGameObjectsWithTag("Enemigo_Arquero");

        for (int i = 0; i < enemigoArqueros.Length; i++) {
            //Debug.Log("Alertado Arquero: " + i.ToString());
            enemigoArqueros[i].GetComponent<Enemigo_Arquero_Controlador>().AlertarArqueros();

        }

    }
}
