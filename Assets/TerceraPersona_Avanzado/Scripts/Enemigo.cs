using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
///
/// DESCRIPCION:
///
/// </summary>

public class Enemigo : MonoBehaviour
{
    // -----------------------------------------------------------------
    #region 1) Definicion de Variables
    public Estados estado;

    [Range(0f, 360f)]
    public float fov; // field of view o campo de vision
    public bool playerEnFov;
    public bool estaPlayerDentro;

    bool dannoProcesado;

    public GameObject hitBoxEspada;

    public int vidas;

    Animator animator;
    Transform objetivo;
    NavMeshAgent agente;

    Coroutine corrutina;
    #endregion
    // -----------------------------------------------------------------
    #region 2) Funciones Predeterminadas de Unity 
    void Awake()
    {

        animator = transform.GetComponent<Animator>();

        objetivo = GameObject.FindWithTag("Player").transform;
        agente = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        EstablecerEstado(Estados.Reposo);
    }

    // Update is called once per frame
    void Update()
    {
        if (estaPlayerDentro)
        {
            Vector3 _posOrigen = transform.position + Vector3.up * 1.5f;
            Vector3 _fovDirIzq = Quaternion.Euler(Vector3.up * fov * -0.5f) * transform.forward;
            Vector3 _fovDirDer = Quaternion.Euler(Vector3.up * fov * 0.5f) * transform.forward;

            Vector3 _dirHaciaPlayer = objetivo.position - transform.position;
            playerEnFov = Vector3.Angle(transform.forward, _dirHaciaPlayer) < fov * 0.5f;


            // Se dibujan los limites del campo de vision
            Debug.DrawRay(_posOrigen, _fovDirIzq, Color.magenta);
            Debug.DrawRay(_posOrigen, _fovDirDer, Color.magenta);
        }
        else
        {

        }

        if (estado == Estados.Reposo && estaPlayerDentro && playerEnFov) EstablecerEstado(Estados.PlayerDetectado);

        if (estado == Estados.PlayerDetectado)
        {
            float distancia = Vector3.Distance(transform.position, objetivo.position);
            if (distancia < 1.5f) EstablecerEstado(Estados.Atacando);
        }

        if (estado == Estados.Atacando)
        {
            float distancia = Vector3.Distance(transform.position, objetivo.position);
            if (distancia > 1.5f) EstablecerEstado(Estados.PlayerDetectado);
        }
    }

    private void FixedUpdate()
    {
        if (estaPlayerDentro)
        {
            Ray ray = new Ray(transform.position + Vector3.up * 1.5f, objetivo.position - transform.position);
            RaycastHit hit;

            bool resultado = Physics.Raycast(ray, out hit, 5f);

            if (resultado)
            {
                if (hit.collider.CompareTag("Player") && playerEnFov)
                {
                    Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red);
                }
                else Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.yellow);

            }
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

        // Es la espada de Player..?
        if (other.gameObject.layer == 7 && !dannoProcesado)
        {
            Debug.Log("Enemigo recibe ataque espada de Player");
            vidas--;

            if (vidas <= 0 && estado != Estados.Muerto) EstablecerEstado(Estados.Muerto);

            dannoProcesado = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player esta fuera de la esfera");
            estaPlayerDentro = false;
            playerEnFov = false;
        }

        // Es la espada de Player..?
        if (other.gameObject.layer == 7) dannoProcesado = false;
    }

    private void OnAnimatorIK()
    {
        animator.SetLookAtPosition(objetivo.position + Vector3.up * 1.5f);


        if (estado == Estados.Atacando) animator.SetLookAtWeight(1f, 1f, 1f);

        if (estado == Estados.PlayerDetectado) animator.SetLookAtWeight(1f, 0.25f, 0.75f);
    }

    public void HabilitarHitBox()
    {
        hitBoxEspada.gameObject.SetActive(true);
    }

    public void DeshabilitarHitBox()
    {
        hitBoxEspada.gameObject.SetActive(false);
    }
    #endregion
    // -----------------------------------------------------------------
    #region 3) Metodos Originales
    void EstablecerEstado(Estados _nuevoEstado)
    {
        estado = _nuevoEstado;
        Debug.Log("<color=yellow>ESTADO ENEMIGO: </color>" + estado.ToString());

        // -----------------------------------------------------------------
        switch (estado)
        {
            // -----------------------------------------------------------------
            case Estados.Reposo:

                agente.isStopped = true;

                animator.SetFloat("movimiento", 0f);
                animator.SetBool("atacando", false);
                break;
            // -----------------------------------------------------------------
            case Estados.PlayerDetectado:

                agente.isStopped = false;
                Corrutina_Empieza();

                animator.SetFloat("movimiento", 1f);
                animator.SetBool("atacando", false);
                break;
            // -----------------------------------------------------------------
            case Estados.Atacando:

                // IA
                agente.isStopped = true;
                agente.velocity = Vector3.zero;
                Corrutina_Termina();

                // ANIMATOR
                animator.SetFloat("movimiento", 0f);
                animator.SetBool("atacando", true);


                break;
            // -----------------------------------------------------------------
            case Estados.Muerto:

                // IA
                agente.isStopped = true;
                agente.velocity = Vector3.zero;
                Corrutina_Termina();

                // ANIMATOR
                animator.SetFloat("movimiento", 0f);
                animator.SetBool("atacando", true);

                vidas = 0;

                animator.SetBool("muerte", true);
                break;
                // -----------------------------------------------------------------
        }
    }

    void Corrutina_Empieza()
    {
        if (corrutina == null) StartCoroutine(EnemigoSiguePlayer());
    }

    void Corrutina_Termina()
    {
        if (corrutina != null)
        {
            StopCoroutine(corrutina);
            corrutina = null;
        }
    }

    IEnumerator EnemigoSiguePlayer()
    {
        while (true)
        {
            agente.SetDestination(objetivo.position);
            yield return new WaitForSeconds(0.2f);
        }
    }
    #endregion
    // -----------------------------------------------------------------

    public enum Estados
    {
        Reposo,
        PlayerDetectado,
        Atacando,
        Muerto
    }
}
