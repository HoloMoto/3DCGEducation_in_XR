using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineRendererConnector : MonoBehaviour
{
    public Transform[] points; // 接続する点のリスト
    private LineRenderer lineRenderer;

    public void RendererStart()
    {
        if (points == null || points.Length < 2)
        {
            Debug.LogError("ポイントを2つ以上設定してください。");
            return;
        }

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = points.Length + 1; // 始点と終点をつなげるため +1
        lineRenderer.loop = true; // ループして閉じた形状を描画

        // LineRendererの見た目の設定
        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;

        UpdateLinePositions();
    }

    void UpdateLinePositions()
    {
        for (int i = 0; i < points.Length; i++)
        {
            lineRenderer.SetPosition(i, points[i].position);
        }
        // 最後の点を最初の点と同じ位置にして閉じる
        lineRenderer.SetPosition(points.Length, points[0].position);
    }

    void Update()
    {
        // 動的に点が動く場合は更新
        UpdateLinePositions();
    }
}