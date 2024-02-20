using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// DESCRIPCION:
///
/// </summary>

public class TerceraPersona_ControladorAvanzado : MonoBehaviour
{
    // -----------------------------------------------------------------
    #region 1) Definicion de Variables
    public Movimiento movimiento;
    public Vertical vertical;

    public Vector2 ejesVirtuales;
    public Vector2 ejesVirtualesGraduales;

    Vector3 dirMov;


    Animator anim;
    Rigidbody rb;
    Transform cam;
#endregion
// -----------------------------------------------------------------
    #region 2) Funciones Predeterminadas de Unity 
    void Awake (){

        cam = Camera.main.transform;

        anim = transform.GetChild(0).GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
	}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EstablecerEjesVirtuales();
        EstablecerDirMov();

        Rotacion();

        ActualizarEstadoMovimiento();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + dirMov * Time.fixedDeltaTime);
    }
    #endregion
    // -----------------------------------------------------------------
    #region 3) Metodos Originales
    void EstablecerEjesVirtuales()
    {
        ejesVirtuales.x = Input.GetAxisRaw("Horizontal");
        ejesVirtuales.y = Input.GetAxisRaw("Vertical");

        ejesVirtualesGraduales.x = Mathf.MoveTowards(ejesVirtualesGraduales.x, ejesVirtuales.x, 10f * Time.deltaTime);
        ejesVirtualesGraduales.y = Mathf.MoveTowards(ejesVirtualesGraduales.y, ejesVirtuales.y, 10f * Time.deltaTime);
    }

    void EstablecerDirMov()
    {
        dirMov = cam.right * ejesVirtuales.x + cam.forward * ejesVirtuales.y;
        dirMov.y = 0f;
        dirMov.Normalize();
    }

    void Rotacion()
    {
        if (dirMov.magnitude == 0f) return;


        // Eje virtual Vertical equivale a 1f y eje virtual horizontal equivale a 0f
        // Estoy yendo hacia delante
        if (ejesVirtuales.y == 1f && ejesVirtuales.x == 0f)
        {
            // Rotacion frontalizada: Hacia donde se desplace Player se orienta el eje forward
            // de manera gradual

            Quaternion rotActual = transform.rotation;
            Quaternion rotFinal = Quaternion.LookRotation(dirMov);
            Quaternion rotGradual = Quaternion.RotateTowards(rotActual, rotFinal, 720f * Time.deltaTime);

            transform.rotation = rotGradual;
        }
        else
        {
            Vector3 camFwd = cam.forward;
            camFwd.y = 0f;
            camFwd.Normalize();

            Quaternion rotActual = transform.rotation;
            Quaternion rotFinal = Quaternion.LookRotation(camFwd);
            Quaternion rotGradual = Quaternion.RotateTowards(rotActual, rotFinal, 720f * Time.deltaTime);

            transform.rotation = rotGradual;
        }
    }

    void EstablecerMovimiento(Movimiento _nuevoEstado)
    {
        movimiento = _nuevoEstado;
        Debug.Log("Estado movimiento: " + movimiento.ToString());

        // ---------------------------------------------------
        switch (movimiento)
        {
            // -----------------------------------------------
            case Movimiento.Reposo:

                break;
            // -----------------------------------------------
            case Movimiento.Camina:

                break;
            // -----------------------------------------------
        }
        // ---------------------------------------------------
    }

    void EstablecerVertical(Vertical _nuevoEstado)
    {
        vertical = _nuevoEstado;
        Debug.Log("Estado vertical: " + vertical.ToString());

        // ---------------------------------------------------
        switch (vertical)
        {
            // -----------------------------------------------
            case Vertical.EnSuelo:

                break;
            // -----------------------------------------------
            case Vertical.EnSalto:

                break;
            // -----------------------------------------------
            case Vertical.EnCaida:

                break;
            // -----------------------------------------------
        }
        // ---------------------------------------------------
    }

    void ActualizarEstadoMovimiento()
    {
        if (ejesVirtuales.magnitude == 0f && movimiento != Movimiento.Reposo)
        {
            EstablecerMovimiento(Movimiento.Reposo);
        }

        if (ejesVirtuales.magnitude != 0f && movimiento != Movimiento.Camina)
        {
            EstablecerMovimiento(Movimiento.Camina);
        }



            anim.SetFloat("ejeH", ejesVirtualesGraduales.x);
            anim.SetFloat("ejeV", ejesVirtualesGraduales.y);
        
    }
    #endregion
    // -----------------------------------------------------------------

    public enum Movimiento
    {
        Reposo,
        Camina
    }

    public enum Vertical
    {
        EnSuelo,
        EnSalto,
        EnCaida
    }
}
