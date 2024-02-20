using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// DESCRIPCION:
///
/// </summary>

public class TerceraPersona_Animator : MonoBehaviour
{

    // -----------------------------------------------------------------
    #region 1) Definicion de Variables
    Animator anim;
    Transform cam;
    #endregion
    // -----------------------------------------------------------------
    #region 2) Funciones Predeterminadas de Unity 
    void Awake (){

        anim = GetComponent<Animator>();
        cam = Camera.main.transform;
	}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    private void OnAnimatorIK()
    {
        anim.SetLookAtPosition(cam.position + cam.forward * 5f);
        anim.SetLookAtWeight(1f, 0.125f, 1f);
    }
    #endregion
    // -----------------------------------------------------------------
    #region 3) Metodos Originales

    #endregion
    // -----------------------------------------------------------------

}
