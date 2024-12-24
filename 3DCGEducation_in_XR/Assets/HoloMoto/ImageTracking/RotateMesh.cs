using UnityEngine;

public class RotateMesh : MonoBehaviour
{
    public ComputeShader computeShader; // コンピュートシェーダー
    private Mesh mesh;
    private ComputeBuffer inputBuffer;
    private ComputeBuffer outputBuffer;

    private Vector3[] vertices;
    private int kernel;

    void Start()
    {
        // メッシュの取得
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;

        // コンピュートバッファの作成
        inputBuffer = new ComputeBuffer(vertices.Length, sizeof(float) * 3);
        outputBuffer = new ComputeBuffer(vertices.Length, sizeof(float) * 3);

        // 元の頂点データをバッファにセット
        inputBuffer.SetData(vertices);

        // カーネルを取得
        kernel = computeShader.FindKernel("CSMain");
        computeShader.SetBuffer(kernel, "inputVertices", inputBuffer);
        computeShader.SetBuffer(kernel, "outputVertices", outputBuffer);
    }

    void Update()
    {
        // 時間を送る
        computeShader.SetFloat("time", Time.time);

        // ディスパッチを実行
        computeShader.Dispatch(kernel, Mathf.CeilToInt(vertices.Length / 256.0f), 1, 1);

        // 結果を取得してメッシュに反映
        Vector3[] outputVertices = new Vector3[vertices.Length];
        outputBuffer.GetData(outputVertices);
        mesh.vertices = outputVertices;
        mesh.RecalculateNormals();
    }

    void OnDestroy()
    {
        // バッファの解放
        inputBuffer.Release();
        outputBuffer.Release();
    }
}