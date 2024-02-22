using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

/// <summary>
///
/// DESCRIPCION:
///
/// </summary>

public class Enemigo : MonoBehaviour
{

    // -----------------------------------------------------------------
    #region 1) Definicion de Variables

    public bool estaPlayerDentro;
    Transform objetivo;

#endregion
// -----------------------------------------------------------------
#region 2) Funciones Predeterminadas de Unity 
void Awake (){

        objetivo = GameObject.FindWithTag("Player").transform;
	}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (estaPlayerDentro)
        {

        }
        else
        {

        }
        
    }

    private void FixedUpdate()
    {
        if (estaPlayerDentro)
        {
            Ray ray = new Ray(transform.position + Vector3.up * 1.5f, objetivo.position - transform.position);
            RaycastHit hit;

            bool resultado = Physics.Raycast(ray, out hit, 5f);

            if (resultado) Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red);
            else Debug.DrawRay(ray.origin, ray.direction * 5f, Color.blue);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player esta dentro de la esfera");
            estaPlayerDentro = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player esta fuera de la esfera");
            estaPlayerDentro = false;
        }
    }
    #endregion
    // -----------------------------------------------------------------
    #region 3) Metodos Originales

    #endregion
    // -----------------------------------------------------------------

}
