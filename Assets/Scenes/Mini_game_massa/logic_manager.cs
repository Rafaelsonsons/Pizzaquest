using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class logic_manager : MonoBehaviour
{

    // Variaveis da pontuacao do jogador
    public int pontuacao;
    public int errou;

    // Limite de erros e acertos
    public int erros_maximos;
    public int acertos_necessarios;

    // Referencia ao spawner de letras
    public spawner_letras spawn;

    // Start is called before the first frame update
    void Start()
    {
        spawn = GameObject.FindGameObjectWithTag("spawn").GetComponent<spawner_letras>();
    }

    void Update()
    {
        // Checa se o jogador perdeu
        if (errou >= erros_maximos) {
            //Escreve no console que o jogo acabou e a pontuacao final
            Debug.Log("Game Over! Sua pontuação foi: " + pontuacao);

            //Desativa o spawner de letras
            spawn.enabled = false;

            //Apaga todas as letras restantes na tela
            tecla2[] letrasRestantes = FindObjectsOfType<tecla2>();
            foreach (var letra in letrasRestantes) {
                Destroy(letra.gameObject);
            }

        }

        //Checa se o jogador venceu
        if (pontuacao >= acertos_necessarios) {
            //Escreve no console que o jogador venceu e a pontuacao final
            Debug.Log("Parabéns! Você venceu com uma pontuação de: " + pontuacao);
            //Desativa o spawner de letras
            spawn.enabled = false;
            //Apaga todas as letras restantes na tela
            tecla2[] letrasRestantes = FindObjectsOfType<tecla2>();
            foreach (var letra in letrasRestantes) {
                Destroy(letra.gameObject);
            }
        }

        // Checa se alguma das teclas de seta foi pressionada
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            DestruirPrimeiraLetra(KeyCode.UpArrow);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            DestruirPrimeiraLetra(KeyCode.DownArrow);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            DestruirPrimeiraLetra(KeyCode.LeftArrow);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            DestruirPrimeiraLetra(KeyCode.RightArrow);
        }

    }

    void DestruirPrimeiraLetra(KeyCode tecla)
    {
        //Armazena todas as letras da tela
        tecla2[] letras = FindObjectsOfType<tecla2>();
        //Define o alvo como nulo inicialmente
        tecla2 alvo = null;

        // pega a letra mais antiga ou mais baixa na tela, a armazenando em 'alvo'
        foreach (var l in letras)
        {
            //Entra no if caso a letra seja a mesma que a tecla apertada
            if (l.letra_aleatoria == tecla) {
                //Checa se o alvo ainda esta null ou se a letra atual esta mais baixa que o alvo
                if (alvo == null || l.GetComponent<RectTransform>().anchoredPosition.y < alvo.GetComponent<RectTransform>().anchoredPosition.y)
                    alvo = l;
            }
        }

        //Se o alvo for diferente de nulo, destroi o alvo, pontua e atualiza a letra atual do spawner
        if (alvo != null)
        {
            Destroy(alvo.gameObject);
            pontuar();
            spawn.att_letra_atual();
        }
    }

    //Funcoes para atualizar a pontuacao e os erros
    public void pontuar() {
        pontuacao += 1;
    }

    public void erros() {
        errou = errou + 1;
    }
}
