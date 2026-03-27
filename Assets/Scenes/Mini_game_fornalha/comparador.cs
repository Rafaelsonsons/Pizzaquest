using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class comparador : MonoBehaviour
{
    //Linha alvo para comparação
    public LineRenderer targetLine;

    //Referencia ao script sorteador para pegar a linha alvo sorteada
    public sorteador sorteador_script;

    //Referecia ao script manager para adicionar pontuação
    public Manager manager_script;

    //Diferença máxima aceitável entre as linhas para considerar como semelhante (quanto mais perto de 0, mais rigoroso será o teste)
    public float diferenca_maxima;

    public void Start() {
        //Pega a referencia ao script sorteador
        sorteador_script = GameObject.FindGameObjectWithTag("Sorteador").GetComponent<sorteador>();

        //Pega a referencia ao script manager
        manager_script = GameObject.FindGameObjectWithTag("GameController").GetComponent<Manager>();

        //Define a linha alvo como a linha sorteada
        targetLine = sorteador_script.escolher_desenho();
        
    }

    public void Update() {

        //Checa se o tempo para desenhar esgotou
        if (sorteador_script.tempo > 10f) {
            //adiciona erro por tempo esgotado e avisa no console
            Debug.Log("Tempo esgotado!");
            manager_script.adicionar_erro();

            //Reseta o tempo e sorteia um novo desenho
            sorteador_script.tempo = 0f;
            sorteador_script.sumir_desenho();
            targetLine = sorteador_script.escolher_desenho();
        }

    }

    //Funcao para comparar a linha desenhada com a linha alvo
    //Recebe como parametro a linha do jogador e a linha alvo
    public void CompareWithTarget(LineRenderer playerLine) {
        // Coleta os pontos
        Vector3[] playerPoints = GetLinePoints(playerLine); //Vetor com os pontos da linha do jogador
        Vector3[] targetPoints = GetLinePoints(targetLine); //Vetor com os pontos da linha alvo

        //Centralizacao de ambas as linhas (ambas devem sofrer o memso processo para garantir a comparacao correta)
        CenterPoints(playerPoints);
        CenterPoints(targetPoints);

        //Normalizacao de escala de ambas as linhas (ambas devem sofrer o memso processo para garantir a comparacao correta)
        NormalizeScale(playerPoints);
        NormalizeScale(targetPoints);

        //Faz ambas as linhas terem o mesmo numero de pontos para facilitar a comparacao
        playerPoints = Resample(playerPoints, 100);
        targetPoints = Resample(targetPoints, 100);

        //Comparacao entre as linhas(retornando a diferenca entre elas)
        float diff = CompareShapes(playerPoints, targetPoints);

        //Mostra o resultado da comparacao no console
        Debug.Log($"Diferença: {diff}");
        //Verifica se a diferenca esta dentro do limite aceitavel
        if (diff < diferenca_maxima) {
            Debug.Log("Desenho semelhante a letra!");
            //Some com o desenho atual e adiciona pontuacao
            sorteador_script.sumir_desenho();
            manager_script.adicionar_pontuacao();
            //Zera o tempo e sorteia um novo desenho
            sorteador_script.tempo = 0f;
            targetLine = sorteador_script.escolher_desenho();
        }
        else {
            //Adiciona erro por desenho diferente
            Debug.Log("Desenho diferente da letra.");
            manager_script.adicionar_erro();
        }
    }

    //Funcao para associar os pontos da linha a um vetor com suas coordenadas
    Vector3[] GetLinePoints(LineRenderer lr) {

        //Cria um novo vetor e determina a quantidade de pontos na linha para determinar o tamanho do vetor
        Vector3[] points = new Vector3[lr.positionCount];

        //Copia todos os pontos da linha para o vetor
        lr.GetPositions(points);

        //Retorna o vetor com as coordenadas dos pontos da linha
        return points;
    }

    void CenterPoints(Vector3[] points) {
        //O ponto centro refere-se ao ponto medio entre todos os pontos do vetor

        //Vetor para armazenar o centro dos pontos
        Vector3 center = Vector3.zero;

        //Calcula o centro dos pontos
        foreach (var p in points) {
            center += p;
        }

        //Calcula a media dos pontos(ponto centro)
        center /= points.Length;

        //Centraliza os pontos subtraindo o ponto centro de cada ponto do vetor
        for (int i = 0; i < points.Length; i++)
            points[i] -= center;
    }

    //Funcao para deixar a linha em uma escala padronizada
    void NormalizeScale(Vector3[] points) {
        //Calculo da maior distancia entre todos os pontos e a origem (0,0)
        float maxDist = 0f;
        foreach (var p in points)
            maxDist = Mathf.Max(maxDist, p.magnitude);

        //Divide todos os pontos pela maior distancia para normalizar a escala(faz com que o ponto mais distante fique na borda de um circulo de raio 1)
        for (int i = 0; i < points.Length; i++)
            points[i] /= maxDist;
    }

    //Funcao para padronizar a quantidade de pontos de uma linha
    Vector3[] Resample(Vector3[] input, int sampleCount) {
        //Cria um novo vetor com o tamanho padronizado desejado
        Vector3[] result = new Vector3[sampleCount];

        //Step = quantos índices do array original devemos avançar (em float) para cada ponto da linha reamostrada
        float step = (float)(input.Length - 1) / (sampleCount - 1);

        //Criacao dos novos pontos por interpolacao linear entre os pontos originais
        for (int i = 0; i < sampleCount; i++) {
            float index = i * step;
            int low = Mathf.FloorToInt(index);
            int high = Mathf.Min(low + 1, input.Length - 1);
            float t = index - low;
            result[i] = Vector3.Lerp(input[low], input[high], t);
        }

        //Retorna o vetor com os pontos reamostrados
        return result;
    }

    //Funcao para comparar duas formas (linhas) e retornar a diferenca entre elas
    /*float CompareShapes(Vector3[] a, Vector3[] b) {
        //Soma das distancias entre os pontos correspondentes das duas linhas
        float totalDist = 0f;

        //Calcula a soma das distancias entre os pontos correspondentes
        for (int i = 0; i < a.Length; i++) {
            totalDist += Vector3.Distance(a[i], b[i]);
        }

        //Distancia media entre os pontos correspondentes das duas linhas
        float dist_media = totalDist / a.Length;

        //Retorna a distancia media entre os pontos das duas linhas
        return dist_media;
    }*/

    float CompareShapes(Vector3[] playerPoints, Vector3[] targetPoints) {
        int n = playerPoints.Length;

        // Detecta se a linha é fechada
        bool isClosed = Vector3.Distance(targetPoints[0], targetPoints[n - 1]) < 0.01f;

        if (isClosed) {
            // Curvas fechadas: deslocamento circular + ambos os sentidos
            float minDist = float.MaxValue;

            for (int offset = 0; offset < n; offset++) {
                float distForward = 0f;
                float distBackward = 0f;

                for (int i = 0; i < n; i++) {
                    int jForward = (i + offset) % n;
                    int jBackward = (n + offset - i) % n;

                    distForward += Vector3.Distance(playerPoints[i], targetPoints[jForward]);
                    distBackward += Vector3.Distance(playerPoints[i], targetPoints[jBackward]);
                }

                float avgForward = distForward / n;
                float avgBackward = distBackward / n;

                minDist = Mathf.Min(minDist, avgForward, avgBackward);
            }

            return minDist;
        }
        else {
            // Linhas abertas: permitir deslocamento parcial
            int maxShift = Mathf.FloorToInt(n * 0.2f); // permite 20% de deslocamento
            float minDist = float.MaxValue;

            for (int shift = -maxShift; shift <= maxShift; shift++) {
                float totalDist = 0f;
                for (int i = 0; i < n; i++) {
                    int j = Mathf.Clamp(i + shift, 0, n - 1);
                    totalDist += Vector3.Distance(playerPoints[i], targetPoints[j]);
                }
                float avgDist = totalDist / n;
                minDist = Mathf.Min(minDist, avgDist);
            }

            return minDist;
        }
    }


}
