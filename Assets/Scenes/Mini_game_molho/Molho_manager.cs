using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Molho_manager : MonoBehaviour
{

    public alvo alvo_sricpt;

    public int pontuacao;

    public bool ganhou = false;

    public int municoes = 7;


    // Start is called before the first frame update
    void Start()
    {
        alvo_sricpt = GameObject.FindGameObjectWithTag("Alvo").GetComponent<alvo>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pontuacao >= 3 && !ganhou) {
            Debug.Log("Parabéns, você venceu!");
            // Aqui você pode adicionar lógica para finalizar o jogo, mostrar uma tela de vitória, etc.
            Destroy(alvo_sricpt.gameObject);
            ganhou = true;
        }

        if (municoes <= 0 && !ganhou) {
            Debug.Log("Fim de jogo! Você perdeu.");
            // Aqui você pode adicionar lógica para finalizar o jogo, mostrar uma tela de derrota, etc.
            Destroy(alvo_sricpt.gameObject);
            ganhou = true;
        }
    }
}
