using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class sorteador : MonoBehaviour
{
    //Referências aos desenhos (linhas) disponíveis
    public LineRenderer desenho1;
    public LineRenderer desenho2;

    //Desenho escolhido aleatoriamente
    public LineRenderer desenho_escolhido;

    //Tempo decorrido desde o início do desenho atual
    public float tempo;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        tempo += Time.deltaTime;
    }

    public LineRenderer escolher_desenho() {
        //Escolhe aleatoriamente um dos desenhos
        int numero_sorteado = Random.Range(1, 3);

        //int numero_sorteado = 1;

        //Associa o desenho escolhido a variavel desenho_escolhido
        switch (numero_sorteado) {
            case 1:
                desenho_escolhido = desenho1;
                break;
            case 2:
                desenho_escolhido = desenho2;
                break;
        }

        //Coloca o desenho escolhido como visível na cena
        desenho_escolhido.gameObject.SetActive(true);

        return desenho_escolhido;
    }

    //Funcao para sumir com desenho após acerto
    public void sumir_desenho() {
        desenho_escolhido.gameObject.SetActive(false);
    }
}
