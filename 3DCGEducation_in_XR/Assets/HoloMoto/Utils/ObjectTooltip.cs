using System.Collections.Generic;
using UnityEngine;

public class ObjectTooltip : MonoBehaviour
{
    public Transform[] anchorPoints; // ラインの開始地点（複数）
    public Transform endPoint;       // ラインの終点（共通）
    public Transform menu;           // ツールチップ（Quad + TextMeshPro）

    private List<LineRenderer> lineRenderers = new List<LineRenderer>(); // ライン管理用

    void Start()
    {
        if (anchorPoints == null || anchorPoints.Length == 0 || endPoint == null)
        {
            Debug.LogError("AnchorPoints または EndPoint が設定されていません。");
            return;
        }

        // 各AnchorPointごとにLineRendererを作成
        foreach (Transform anchor in anchorPoints)
        {
            LineRenderer lr = anchor.gameObject.AddComponent<LineRenderer>();
            lr.positionCount = 2;
            lr.startWidth = 0.01f;
            lr.endWidth = 0.01f;
            lr.material = new Material(Shader.Find("Sprites/Default")); // シンプルな線
            lr.startColor = Color.gray;
            lr.endColor = Color.gray;

            lineRenderers.Add(lr);
        }
    }

    void Update()
    {
        if (anchorPoints == null || endPoint == null) return;

        // 各LineRendererを更新
        for (int i = 0; i < anchorPoints.Length; i++)
        {
            if (lineRenderers[i] != null && anchorPoints[i] != null)
            {
                lineRenderers[i].SetPosition(0, anchorPoints[i].position);
                lineRenderers[i].SetPosition(1, endPoint.position);
            }
        }
    }
}