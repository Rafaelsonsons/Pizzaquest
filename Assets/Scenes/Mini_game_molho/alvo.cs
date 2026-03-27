using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alvo : MonoBehaviour {
    public Cursor cursor_script;
    public Molho_manager molho_manager_script;

    [Header("Configuração de Movimento")]
    public float velocidadeMin = 1;
    public float velocidadeMax = 5;

    private Vector2 direcao;
    public float velocidade;

    private float tempoMudancaDirecao;
    private float contadorTempo;

    public float clickRange;

    // Limites da área
    public float limiteX = 10f;
    public float limiteY = 5f;

    public bool acertou;

    void Start() {
        GameObject cursorObj = GameObject.FindGameObjectWithTag("Cursor");
        if (cursorObj != null)
            cursor_script = cursorObj.GetComponent<Cursor>();

        GameObject logicObj = GameObject.FindGameObjectWithTag("Logic");
        if (logicObj != null)
            molho_manager_script = logicObj.GetComponent<Molho_manager>();

        GerarNovoMovimento();
    }

    void Update() {

        if (cursor_script != null)
            clickRange = cursor_script.radius;

        // =========================
        // MOVIMENTO
        // =========================

        transform.position += (Vector3)(direcao * velocidade * Time.deltaTime);

        contadorTempo += Time.deltaTime;

        if (contadorTempo >= tempoMudancaDirecao) {
            GerarNovoMovimento();
        }

        // =========================
        // CORREÇÃO DE LIMITES (ANTI-TREMOR)
        // =========================

        Vector3 pos = transform.position;

        bool bateuX = false;
        bool bateuY = false;

        if (pos.x <= -limiteX || pos.x >= limiteX)
            bateuX = true;

        if (pos.y <= -limiteY || pos.y >= limiteY)
            bateuY = true;

        // Clampa posição dentro da área
        pos.x = Mathf.Clamp(pos.x, -limiteX, limiteX);
        pos.y = Mathf.Clamp(pos.y, -limiteY, limiteY);

        transform.position = pos;

        // Reflete direção apenas uma vez
        if (bateuX)
            direcao.x *= -1;

        if (bateuY)
            direcao.y *= -1;

        // =========================
        // DETECÇÃO DE CLIQUE
        // =========================

        if (Input.GetMouseButtonDown(0)) {

            acertou = false;

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D[] hits = Physics2D.OverlapCircleAll(mousePos, clickRange);

            foreach (Collider2D hit in hits) {

                if (hit.CompareTag("Alvo")) {

                    if (molho_manager_script != null) {
                        molho_manager_script.pontuacao += 1;
                        acertou = true;
                    }
                }
            }

            if (!acertou) {
                if (molho_manager_script != null) {
                    molho_manager_script.municoes -= 1;
                }
            }
        }
    }

    void GerarNovoMovimento() {

        direcao = Random.insideUnitCircle.normalized;
        velocidade = Random.Range(velocidadeMin, velocidadeMax);

        tempoMudancaDirecao = Random.Range(1f, 2f);
        contadorTempo = 0f;
    }
}
