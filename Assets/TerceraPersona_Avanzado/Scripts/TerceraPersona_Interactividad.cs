using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// DESCRIPCION:
///
/// </summary>

public class TerceraPersona_Interactividad : MonoBehaviour
{

    // -----------------------------------------------------------------
    #region 1) Definicion de Variables
    public int numRayosFrontales;
    public float distanciaEntreRayos;

    public float rayoAlturaOrigen;
    public float rayoDistanciaOrigen;

    public float rayoLongitud;

    public PuertaInterruptor puertaInterruptorDetectado;


    #endregion
    // -----------------------------------------------------------------
    #region 2) Funciones Predeterminadas de Unity 
void Awake (){

	}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (puertaInterruptorDetectado != null)
            {
                puertaInterruptorDetectado.Interactuar();
            }
        }
    }

    private void FixedUpdate()
    {
        RaycastFrontal();
    }
    #endregion
    // -----------------------------------------------------------------
    #region 3) Metodos Originales
    void RaycastFrontal()
    {
        // defino en la variable temporal el punto desde el que se va a originar el raycast:
        // desde la posicion actual de MiPersonaje "subo" en el eje Y un numero de unidades y le hago avanzar
        Vector3 _posOriginal = transform.position + Vector3.up * rayoAlturaOrigen + transform.forward * rayoDistanciaOrigen;
        Ray ray = new Ray(_posOriginal, transform.forward);

        RaycastHit hit;

        bool resultado = Physics.Raycast(ray, out hit, rayoLongitud);

        if (resultado)
        {
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red);

            if (hit.collider.CompareTag("ElementosInteractivos"))
            {
                if (puertaInterruptorDetectado == null)
                {
                    puertaInterruptorDetectado = hit.collider.transform.parent.GetComponent<PuertaInterruptor>();
                    puertaInterruptorDetectado.EfectoInterruptorDetectado(true);
                }
            }
            else
            {
                if (puertaInterruptorDetectado != null)
                {
                    puertaInterruptorDetectado.EfectoInterruptorDetectado(false);
                    puertaInterruptorDetectado = null;
                }
            }
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * rayoLongitud, Color.green);

            if (puertaInterruptorDetectado != null)
            {
                puertaInterruptorDetectado.EfectoInterruptorDetectado(false);
                puertaInterruptorDetectado = null;
            }
        }


        /*for (int i = 0; i < numRayosFrontales; i++)
        {
            // defino en la variable temporal el punto desde el que se va a originar el raycast:
            // desde la posicion actual de MiPersonaje "subo" en el eje Y un numero de unidades y le hago avanzar
            Vector3 _posOriginal = transform.position + Vector3.up * rayoAlturaOrigen + transform.forward * rayoDistanciaOrigen;

            //
            _posOriginal += Vector3.up * i * distanciaEntreRayos;

            Ray ray = new Ray(_posOriginal, transform.forward);

            RaycastHit hit;

            bool resultado = Physics.Raycast(ray, out hit, rayoLongitud);

            if (resultado) Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red);
            else Debug.DrawRay(ray.origin, ray.direction * rayoLongitud, Color.green);
        }*/
    }
    #endregion
    // -----------------------------------------------------------------

}
