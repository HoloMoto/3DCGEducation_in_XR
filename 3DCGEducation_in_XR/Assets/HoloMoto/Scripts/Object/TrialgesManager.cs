using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HoloMoto.Manager
{
    public class TrialgesManager : MonoBehaviour
    {
        [SerializeField]GameObject[] vertices;
        [SerializeField]MeshRenderer renderer;
        [SerializeField]GameObject[] edge;
        //ポリゴン表示モードの場合
        [SerializeField]bool isTrianglesMode = false;
        public bool isEdgeShowMode = false;
        public bool updateMesh = false;
        // Start is called before the first frame update
        void Start()
        {
            if (isEdgeShowMode)
            {
                foreach (var e in edge)
                {
                    e.SetActive(true);
                }
            }
            else
            {
                foreach (var e in edge)
                {
                    e.SetActive(false);
                }
            }
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

            if (isEdgeShowMode)
            {
                EdgeSet();
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

        public void EdgeSet()
        {
          //edgeobjectのそれぞれの辺をvertexとvertexを結ぶように設定
edge[0].transform.position = (vertices[0].transform.position + vertices[1].transform.position) / 2;
edge[0].transform.LookAt(vertices[1].transform.position);
edge[0].transform.localScale = new Vector3(0.01f, 0.01f, Vector3.Distance(vertices[0].transform.position, vertices[1].transform.position));
edge[1].transform.position = (vertices[1].transform.position + vertices[2].transform.position) / 2;
edge[1].transform.LookAt(vertices[2].transform.position);
edge[1].transform.localScale = new Vector3(0.01f, 0.01f, Vector3.Distance(vertices[1].transform.position, vertices[2].transform.position));
edge[2].transform.position = (vertices[2].transform.position + vertices[0].transform.position) / 2;
edge[2].transform.LookAt(vertices[0].transform.position);
edge[2].transform.localScale = new Vector3(0.01f, 0.01f, Vector3.Distance(vertices[2].transform.position, vertices[0].transform.position));
            
        }

    }
}