using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo_Arquero_Controlador : MonoBehaviour
{
    private EstadoArquero estadoArquero;
    Transform objetivo;

    private void Awake()
    {
        objetivo = GameObject.FindWithTag("Player").transform;
        EstablecerEstadoArquero(EstadoArquero.Reposo);
    }

    private void Update()
    {
        if (EstadoArquero.Apuntando == estadoArquero) { 
            ApuntarRaycast();
        }
    }
    public void AlertarArqueros() {
        Debug.Log("Arquero Alertado");
        EstablecerEstadoArquero(EstadoArquero.Apuntando);
    }


    private void EstablecerEstadoArquero(EstadoArquero nuevoEstadoArquero) { 
        estadoArquero = nuevoEstadoArquero;
        switch (estadoArquero)
        {
            case EstadoArquero.Reposo:
                break;
            case EstadoArquero.Apuntando:
                 ApuntarRaycast();
                break;
            case EstadoArquero.Disparando:
                break;
        }
    }
    private void ApuntarRaycast() {

        Ray ray = new Ray(transform.position + Vector3.up * 1.5f, objetivo.position - transform.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 50f))
        {
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red);
            Debug.Log("Pinta rayo rojo");
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * 50, Color.yellow);
            Debug.Log("Pinta rayo Amarillo");
        }
    }

    enum EstadoArquero { 
        Reposo,
        Apuntando,
        Disparando
    }

}
