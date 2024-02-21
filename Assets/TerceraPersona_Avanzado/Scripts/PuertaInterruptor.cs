using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/// <summary>
///
/// DESCRIPCION:
///
/// </summary>

public class PuertaInterruptor : MonoBehaviour
{

    // -----------------------------------------------------------------
    #region 1) Definicion de Variables

    Animator anim;
    Material matInterruptor;
    Color colorMatInterruptor;
    CinemachineVirtualCamera cv;

    Coroutine coroutine;

#endregion
// -----------------------------------------------------------------
    #region 2) Funciones Predeterminadas de Unity 
    void Awake (){
         
        anim = GetComponent<Animator>();
        matInterruptor = transform.GetChild(1).GetChild(0).GetComponent<Renderer>().material;

        colorMatInterruptor = matInterruptor.color;

        cv = transform.GetChild(0).GetChild(1).GetComponent<CinemachineVirtualCamera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        anim.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        
    }
#endregion
// -----------------------------------------------------------------
#region 3) Metodos Originales

    public void EfectoInterruptorDetectado (bool _estado)
    {
        if (_estado)
        {
            matInterruptor.color = Color.magenta;
        }
        else matInterruptor.color = colorMatInterruptor;
    }

    public void Interactuar()
    {
        if (coroutine == null) coroutine = StartCoroutine(InteractuarCoro());        
    }

    IEnumerator InteractuarCoro()
    {
        anim.enabled = true;
        cv.m_Priority = 2;
        yield return new WaitForSeconds(2f);
        cv.m_Priority = 0;

        StopCoroutine(coroutine);
        coroutine = null;
    }

#endregion
// -----------------------------------------------------------------
}
