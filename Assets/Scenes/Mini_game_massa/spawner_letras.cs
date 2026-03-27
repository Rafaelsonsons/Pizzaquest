using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner_letras : MonoBehaviour
{
    //Prefab da letra a ser criada
    public GameObject letra;

    public logic_manager logic;

    //Canvas onde as letras serao criadas
    public Transform canvasPai; // arraste o Canvas aqui no Inspector

    //Variaveis de controle do spwan
    public float spawn_rate;
    private float tempo;

    //Limites de posicao das letras na tela
    public int max_x;
    public int min_x;
    public int max_y;
    public int min_y;

    //Numero total de letras criadas ate o momento
    public int letras_criadas;

    //Codigo da letra que deve ser apertada no momento
    public int letra_atual;

    //contador de cliques certos
    public int cliques_certos;

    void Start() {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<logic_manager>();
    }

    void Update() {

        tempo += Time.deltaTime;

        if (tempo >= spawn_rate) {
            //cria a letra como filha do Canvas
            GameObject novaLetra = Instantiate(letra, canvasPai);

            //define uma posição aleatória dentro da tela
            novaLetra.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(min_x, max_x), Random.Range(min_y, max_y));

            //atualiza o numero de letras criadas
            letras_criadas = letras_criadas + 1;

            tempo = 0;
        }


    }

    public void att_letra_atual() {

        letra_atual = letra_atual + 1;

    }

}
