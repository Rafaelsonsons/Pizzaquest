using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    //Pontuação do jogador
    public int pontuacao;

    //Número de erros do jogador
    public int erros;

    //Número de acertos necessários para vencer
    public int acertos_necessarios;

    //Número de erros permitidos antes de perder
    public int erros_permitidos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Checa se o jogador venceu ou perdeu
        if (pontuacao == acertos_necessarios) {
            Debug.Log("Você venceu o mini game!");
            gameObject.SetActive(false);
        }
        if (erros == erros_permitidos) {
            Debug.Log("Você perdeu o mini game!");
            gameObject.SetActive(false);
        }

    }

    //Função para adicionar pontuação
    public void adicionar_pontuacao() {
        pontuacao += 1;
    }

    //Função para adicionar erro
    public void adicionar_erro() {
        erros += 1;
    }
}
