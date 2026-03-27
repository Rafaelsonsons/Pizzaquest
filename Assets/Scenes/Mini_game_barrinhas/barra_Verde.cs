using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class barra_Verde : MonoBehaviour
{
    //Variaveis do Objeto barra vemelha
    public barra_Vermelha vermelelho_script;

    //Variaveis de controle da movimentacao da barra verde
    public int velocidade;

    //Variaveis de controle da pontuacao de acordo com o tempo da verde dentro da vermelha
    public bool acertou;
    public int pontuacao = 0;
    public int pontos_ganhar;

    
    // Start is called before the first frame update
    void Start()
    {
        vermelelho_script = GameObject.FindGameObjectWithTag("vermelho").GetComponent<barra_Vermelha>();
    }

    // Update is called once per frame
    void Update()
    {
        //Incrementa a pontuacao se a barra verde estiver dentro da vermelha
        if (acertou)
        {
            pontuacao = pontuacao + 1;
        }

        //Conjunto de movimentacao da barrinha
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * velocidade * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * velocidade * Time.deltaTime;
        }

    }

    //Funcao chamada pela barra vermelha para sinalizar q a barra verde a adentrou
    public void adentrou()
    {
        acertou = true;
    }

    //Funcao chamada pela barra vermelha para sinalizar q a barra verde saiu
    public void saiu()
    {
        acertou = false;
    }
}
