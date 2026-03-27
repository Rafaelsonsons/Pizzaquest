using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class draw_script : MonoBehaviour {

    [Header("Referências")]
    //Camera principal da cena
    public Camera m_camera;
    //Prefab do pincel (brush)
    public GameObject brush;
    //Referencia ao script comparador
    public comparador comparador_script;

    //Linha atual do pincel
    private LineRenderer currentLineRenderer;
    //Última posição do mouse
    private Vector2 lastPos;

    private void Start() {
        //Pega a referencia ao script comparador
        comparador_script = GameObject.FindGameObjectWithTag("Comparador").GetComponent<comparador>();
    }
    private void Update() {
        Draw();
    }

    //Funcao para desenhar na tela
    void Draw() {

        //Detecta o primeiro clique para iniciar o desenho
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            CreateBrush();
        }

        //Detecta o movimento do mouse para ir adicionando pontos na linha
        if (Input.GetKey(KeyCode.Mouse0)) {

            //Pega a posicao atual do mouse
            Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);

            //Adiciona um ponto na linha se a posicao do mouse mudou
            if (lastPos != mousePos) {
                AddPoint(mousePos);
                lastPos = mousePos;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0)) { //Detecta quando o jogador solta o mouse
            // Quando existir uma linha desenhada, faz a comparação com a linha alvo
            if (currentLineRenderer != null) {
                //Faz a comparação entre a linha desenhada e a linha alvo
                comparador_script.CompareWithTarget(currentLineRenderer);

                //Após a comparação, remove a linha desenhada da cena
                Destroy(currentLineRenderer.gameObject);

                //Tira a linha desenhada
                currentLineRenderer = null;
            }
        }
    }

    //Funcao para criar o traço do pincel
    void CreateBrush() {

        //intancia o pincel (brush) na cena
        GameObject brushInstance = Instantiate(brush);

        //Pega o modelo de linha que sera usado para desenhar
        currentLineRenderer = brushInstance.GetComponent<LineRenderer>();

        //Converte a posicao do mouse (pixels) para coordenadas do mundo
        Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);

        //Criando os dois primeiros pontos da linha para ser possivel desenhar a linha visualmente
        currentLineRenderer.SetPosition(0, mousePos);
        currentLineRenderer.SetPosition(1, mousePos);
    }

    //Funcao para adicionar pontos(coordenadas) na linha do pincel
    void AddPoint(Vector2 pointPos) {

        //Adiciona um novo ponto na contagem de pontos da linha
        currentLineRenderer.positionCount++;

        //Pega o indice do ultimo ponto adicionado (todos sao iniciados com 0)
        int positionIndex = currentLineRenderer.positionCount - 1;

        //Define a posicao do ponto adicionado
        currentLineRenderer.SetPosition(positionIndex, pointPos);
    }

}
