using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo_Controlador : MonoBehaviour
{
    public EstadoEnemigo estadoEnemigo;
    Material matInterruptor;

    [SerializeField]
    int vidaEnemigo;

    private void Awake()
    {
        matInterruptor = transform.GetChild(1).GetComponent<Renderer>().material;
        EstablecerEstadoEnemigo(EstadoEnemigo.Reposo);
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (estadoEnemigo == EstadoEnemigo.Atacando) { 
            //TODO: Atacar
        }

        if (estadoEnemigo == EstadoEnemigo.SiguePlayer)
        {
            //TODO: Seguir a jugador
        }
        if (vidaEnemigo < 1) {
            EstablecerEstadoEnemigo(EstadoEnemigo.Muerto);
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        
    }

    public void DentroAreaColision() {
        Debug.Log("Entra en Area");
        if (estadoEnemigo == EstadoEnemigo.Reposo || estadoEnemigo != EstadoEnemigo.Atacando || estadoEnemigo != EstadoEnemigo.Muerto || estadoEnemigo != EstadoEnemigo.SiguePlayer){
            EstablecerEstadoEnemigo(EstadoEnemigo.SiguePlayer);
        }
    }
    public void FueraAreaColision()
    {
        Debug.Log("Sale de Area");
        if (estadoEnemigo == EstadoEnemigo.SiguePlayer || estadoEnemigo == EstadoEnemigo.Atacando || estadoEnemigo != EstadoEnemigo.Muerto || estadoEnemigo != EstadoEnemigo.Reposo)
        {
            EstablecerEstadoEnemigo(EstadoEnemigo.Reposo);
        }
    }
    public void DentroAreaAtaque()
    {
        Debug.Log("Entra en AreaAtaque");
        if (estadoEnemigo == EstadoEnemigo.Reposo || estadoEnemigo != EstadoEnemigo.SiguePlayer || estadoEnemigo != EstadoEnemigo.Atacando || estadoEnemigo != EstadoEnemigo.Muerto)
        {
            EstablecerEstadoEnemigo(EstadoEnemigo.Atacando);
        }

    }
    public void FueraAreaAtaque()
    {
        Debug.Log("Sale de AreaAtaque");
        if (estadoEnemigo == EstadoEnemigo.Reposo || estadoEnemigo != EstadoEnemigo.SiguePlayer || estadoEnemigo == EstadoEnemigo.Atacando || estadoEnemigo != EstadoEnemigo.Muerto)
        {
            EstablecerEstadoEnemigo(EstadoEnemigo.SiguePlayer);
        }
    }
    

    public void EstablecerEstadoEnemigo(EstadoEnemigo nuevoEstadoEnemigo) {
        estadoEnemigo = nuevoEstadoEnemigo;

        switch (estadoEnemigo)
        {
            case EstadoEnemigo.Reposo:
                //TODO: AnimacionParado
                matInterruptor.color = Color.white;
                break;
            case EstadoEnemigo.SiguePlayer:
                //TODO: mover hacia jugador
                matInterruptor.color = Color.yellow;
                break;
            case EstadoEnemigo.Atacando:
                //TODO: Animacion atacando
                matInterruptor.color = Color.red;
                break;
            case EstadoEnemigo.Muerto:
                //TODO: Animacion muerto
                matInterruptor.color = Color.black;
                break;
        }
    }

    public enum EstadoEnemigo { 
        Reposo,
        SiguePlayer,
        Atacando,
        Muerto
    }
}
