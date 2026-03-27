using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tecla2 : MonoBehaviour
{
    //Referencia ao script de logica do jogo e do spawner de letras
    public logic_manager logic;
    public spawner_letras spawner;

    //Referencia ao texto que mostra a letra e o codigo dela
    public Text letras;
    public int codigo_letra;

    //Variavel que guarda a letra aleatoria que deve ser apertada
    public KeyCode letra_aleatoria;

    //Velocidade de queda da letra
    public int velocidade_queda;

    //Variavel que indica se a letra ja foi apertada ou nao
    public int sumiu;

    //Numero da ordem em foi nascido. Se tiver sido o terceiro a nascer, numero_de_nasenca = 3
    public int numero_de_nasenca;

    //Codigo da proxima letra que deve ser apertada
    public int codigo_primeria_letra;


    // Start is called before the first frame update
    void Start()
    {
        //Pega as referencias aos outros scripts
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<logic_manager>();
        spawner = GameObject.FindGameObjectWithTag("spawn").GetComponent<spawner_letras>();

        //Determina o codigo da telcla(setas) a ser apertada
        /*left = 276
          rigth = 275
          up = 273
          down = 274*/
        codigo_letra = Random.Range(273, 277);

        //Transforma o inteiro sorteado em KeyCode 
        letra_aleatoria = (KeyCode)(codigo_letra);

        //Mostra a letra na tela
        switch (codigo_letra) {
            case 273:
                letras.text = "up";
                break;
            case 274:
                letras.text = "down";
                break;
            case 275:
                letras.text = "->";
                break;
            case 276:
                letras.text = "<-";
                break;
        }

        //Pega o numero de nasenca da letra
        numero_de_nasenca = spawner.letras_criadas;

    }

    // Update is called once per frame
    void Update()
    {
        //Verifica se a letra passou do limite inferior da tela, se passou adiciona um erro e destroi a letra
        RectTransform posicao = GetComponent<RectTransform>();
        if (posicao.anchoredPosition.y < -450) {
            Destroy(gameObject);
            logic.erros();
        }

        //Faz a imagem da tecla que deve ser apertada cair
        transform.position += Vector3.down * velocidade_queda * Time.deltaTime;

    }

}
