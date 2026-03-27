using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    public float radius = 0.5f;
    public int segments = 50;

    private LineRenderer line;

    void Start() {
        line = GetComponent<LineRenderer>();
        line.positionCount = segments + 1;
        line.useWorldSpace = true;
        line.loop = true;
        line.startWidth = 0.02f;
        line.endWidth = 0.02f;
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.startColor = Color.red;
        line.endColor = Color.red;
    }

    void Update() {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f; // evita deslocar em profundidade
        DrawCircle(mousePos);
    }

    void DrawCircle(Vector3 center) {
        float angleStep = 360f / segments;

        for (int i = 0; i <= segments; i++) {
            float angle = Mathf.Deg2Rad * (i * angleStep);
            float x = center.x + Mathf.Cos(angle) * radius;
            float y = center.y + Mathf.Sin(angle) * radius;
            line.SetPosition(i, new Vector3(x, y, 0));
        }
    }
}
