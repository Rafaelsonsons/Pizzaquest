using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class letra_o : MonoBehaviour
{
    public float radius;       // Raio do círculo
    public int segments;      // Quantidade de pontos do círculo

    private LineRenderer lr;

    void Start() {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = segments + 1; // +1 para fechar o círculo
        DrawCircle();
    }

    void DrawCircle() {
        float angleStep = 360f / segments;

        for (int i = 0; i <= segments; i++) {
            float angle = Mathf.Deg2Rad * i * angleStep; // converte para radianos
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;

            lr.SetPosition(i, new Vector3(x, y, 0f));
        }
    }
}
