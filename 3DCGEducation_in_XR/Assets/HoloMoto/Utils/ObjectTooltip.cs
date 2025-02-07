using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ObjectTooltip : MonoBehaviour
{
    public Transform anchorPoint; // ラインの開始地点
    public Transform endPoint;    // ラインの終点
    public Transform menu;        // ツールチップ（Quad + TextMeshPro）
    
    private LineRenderer lineRenderer;
    private Camera arCamera; // ARカメラ用

    void Start()
    {

        // LineRenderer のセットアップ
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // シンプルな線
        lineRenderer.startColor = Color.gray;
        lineRenderer.endColor = Color.gray;
    }

    void Update()
    {
        // ラインの更新
        if (anchorPoint != null && endPoint != null)
        {
            lineRenderer.SetPosition(0, anchorPoint.position);
            lineRenderer.SetPosition(1, endPoint.position);
        }

      
    }
}