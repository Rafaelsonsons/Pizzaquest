using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Conjunto_barras : MonoBehaviour
{
    public barra_Verde barrinha;
    public GameObject canvas_ganhou;
    public GameObject canvas_perdeu;
    public float tempo_passado;
    public float tempo_para_ganhar;
    public bool ganhou;
    public bool tempo_esgotado;

    // Start is called before the first frame update
    void Start()
    {
        barrinha = GameObject.FindGameObjectWithTag("Barrinha").GetComponent<barra_Verde>();
    }

    // Update is called once per frame
    void Update()
    {
        if (barrinha.pontuacao > barrinha.pontos_ganhar)
        {
            ganhou = true;
        }

        if (ganhou)
        {
            gameObject.SetActive(false);
            canvas_ganhou.SetActive(true);
        }
        
        if (tempo_esgotado)
        {
            gameObject.SetActive(false);
            canvas_perdeu.SetActive(true);
        }
    }


    private void FixedUpdate()
    {
        if (!ganhou && !tempo_esgotado)
        {
            // Soma o tempo decorrido desde o último FixedUpdate
            tempo_passado += Time.fixedDeltaTime;

            // Quando passar de 500 segundos (~8 minutos e 20 segundos)
            if (tempo_passado >= tempo_para_ganhar)
            {
                tempo_esgotado = true;
            }
        }
    }

}

