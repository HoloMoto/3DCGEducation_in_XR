using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HoloMoto.Manager
{
    public class TrialgesManager : MonoBehaviour
    {
        [SerializeField]GameObject[] vertices;
        [SerializeField]MeshRenderer renderer;
        
        //ポリゴン表示モードの場合
        [SerializeField]bool isTrianglesMode = false;
        public bool updateMesh = false;
        // Start is called before the first frame update
        void Start()
        {
        }

        private bool isUpdate = true;
        // Update is called once per frame
        void Update()
        {
            if (updateMesh)
            {
                if (isUpdate)
                {
                    StartCoroutine(UpdateMesh());
                }
            }
        }
        IEnumerator UpdateMesh()
        {
            isUpdate = false;
            yield return new WaitForSeconds(1.0f);
            CreateTrianglesMesh();
            isUpdate = true;
        }

        public void CreateTrianglesMesh()
        {
            //vertices３つからメッシュを作成して、三角形を描画する
            Mesh mesh = new Mesh();
            mesh.vertices = new Vector3[] { vertices[2].transform.position, vertices[1].transform.position, vertices[0].transform.position };
            mesh.triangles = new int[] { 0, 1, 2 };
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            //meshのuvを設定
            mesh.uv = new Vector2[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1) };

            //rendererのメッシュにmeshを追加
            renderer.GetComponent<MeshFilter>().mesh = mesh;
            
            
        }
        
        //マテリアルのアニメーション
        public void ChangeMaterialColor(Color color)
        {
            StartCoroutine(ChangeMaterialColorCoroutine(color));
        }
        
        //マテリアルのアニメーションコルーチン
        IEnumerator ChangeMaterialColorCoroutine(Color color)
        {
            float time = 0;
            float duration = 1.0f;
            Color startColor = renderer.material.color;
            while (time < duration)
            {
                time += Time.deltaTime;
                float t = time / duration;
                renderer.material.color = Color.Lerp(startColor, color, t);
                yield return null;
            }
        }


    }
}