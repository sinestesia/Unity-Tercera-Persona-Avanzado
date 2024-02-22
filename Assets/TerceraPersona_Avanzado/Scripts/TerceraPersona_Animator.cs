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

    Transform huesoColumna;
    #endregion
    // -----------------------------------------------------------------
    #region 2) Funciones Predeterminadas de Unity 
    void Awake (){

        anim = GetComponent<Animator>();
        huesoColumna = anim.GetBoneTransform(HumanBodyBones.UpperChest);

        cam = Camera.main.transform;
	}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public bool atacando;

    // Update is called once per frame
    void Update()
    {
        atacando = Input.GetMouseButton(0);

        anim.SetBool("atacando", atacando);
    }

    private void OnAnimatorIK()
    {
        if (!atacando)
        {
            anim.SetLookAtPosition(cam.position + cam.forward * 5f);
            anim.SetLookAtWeight(1f, 0.125f, 1f);
        }
        else
        {
            anim.SetLookAtPosition(huesoColumna.position + transform.forward * 5f);
            anim.SetLookAtWeight(1f, 1f, 1f);
        }
    }
    #endregion
    // -----------------------------------------------------------------
    #region 3) Metodos Originales

    #endregion
    // -----------------------------------------------------------------

}
