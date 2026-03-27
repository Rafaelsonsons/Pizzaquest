using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class barra_Vermelha : MonoBehaviour
{
    //Variavel que armazena o objeto da barra verde e sua layer
    public barra_Verde verde_script;
    public int layer_verde;


    //Variaveis de controle da barra
    public int velocidade_queda;
    public int tempo_mudar_direcao;
    public int minimo_mudar_direcao;
    public int maximo_mudar_direcao;
    public int divisor_contador;
    public int contador = 0;
    public int minimo_numero_aleatorio;
    public int maximo_numero_aleatorio;
    private int numero_aleatorio;

    // Start is called before the first frame update
    void Start()
    {
        verde_script = GameObject.FindGameObjectWithTag("Barrinha").GetComponent<barra_Verde>();
 
        if (Random.Range(0, 2) == 0)
        {
            velocidade_queda = velocidade_queda * -1;
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Define um tempo aleatório para mudar a direção da barra
        tempo_mudar_direcao = Random.Range(minimo_mudar_direcao, maximo_mudar_direcao);

        //Muda a direção da barra após o tempo definido
        if (contador > tempo_mudar_direcao)
        {
            contador = 0;
            velocidade_queda = velocidade_queda * -1;
        }

        //Acrescenta 1 ao contador para todo numero divido por "divisor_contador" que de resto 1
        numero_aleatorio = Random.Range(minimo_numero_aleatorio, maximo_numero_aleatorio);
        if (numero_aleatorio % divisor_contador == 1)
        {
            contador = contador + 1;
        }
    }

    void Update()
    {
        //Att a posicao da barra vermelha
        transform.position += Vector3.left * velocidade_queda * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Para a barrinha verde entrando chamar a funcao no objeto da barra verde, para outra colisao inverter a direcao da barra
        if (collision.gameObject.layer == layer_verde)
        {
            verde_script.adentrou();
        }
        else
        {
            velocidade_queda = velocidade_queda * -1;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Para a barra verde saindo chamar a funcao no objeto da barra verde
        if (collision.gameObject.layer == layer_verde)
        {
            verde_script.saiu();
        }
    }

}

