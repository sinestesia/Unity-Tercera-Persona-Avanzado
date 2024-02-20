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
    public Vector2 ejesVirtuales;
    Vector3 dirMov;

    Rigidbody rb;
    Transform cam;
#endregion
// -----------------------------------------------------------------
#region 2) Funciones Predeterminadas de Unity 
void Awake (){

        cam = Camera.main.transform;
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

        if (ejesVirtuales.y > 0f)
        {
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
#endregion
// -----------------------------------------------------------------

}
