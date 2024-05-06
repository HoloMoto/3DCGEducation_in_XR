using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering;

namespace HoloMoto.Object
{
    public class MeshInfoExtractor : MonoBehaviour
    {
        public GameObject target;
        public Mesh mesh;
        
        [SerializeField,CanBeNull] Material wireframeMaterial;

        [SerializeField] private Renderer[] renderer;
        [SerializeField]
        private Material[] materials;

        private void Reset()
        {
            target = this.gameObject;
            //現在アタッチされているオブジェクト、その子オブジェクトからすべてのレンダラーの数をint型で取得
            renderer = GetComponentsInChildren<Renderer>();
        }

        // Start is called before the first frame update
        void Start()
        {
            TargetToWireframe(true);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        public void ExtractMeshInfo()
        {
            mesh = target.GetComponent<MeshFilter>().mesh;
            Debug.Log("Mesh Name: " + mesh.name);
            Debug.Log("Mesh Vertices: " + mesh.vertices.Length);
            Debug.Log("Mesh Triangles: " + mesh.triangles.Length);
            Debug.Log("Mesh Normals: " + mesh.normals.Length);
            Debug.Log("Mesh UVs: " + mesh.uv.Length);
            Debug.Log("Mesh Colors: " + mesh.colors.Length);
            Debug.Log("Mesh Bounds: " + mesh.bounds);
        }

        public void TargetToWireframe(bool useWireframe)
        {
            if (useWireframe)
            {
                // すべてのrendererのマテリアルを１つの配列にまとめる
                Material[] materials = new Material[renderer.Length];
                for (int i = 0; i < renderer.Length; i++)
                {
                    materials[i] = renderer[i].material;
                }
                Debug.Log(materials.Length);
        
                // 現在のマテリアルスロットすべてにワイヤーフレームマテリアルを設定
                for (int i = 0; i < renderer.Length; i++)
                {
                    Material[] newMaterials = new Material[renderer[i].materials.Length];
                    for (int j = 0; j < newMaterials.Length; j++)
                    {
                        newMaterials[j] = wireframeMaterial;
                    }
                    renderer[i].materials = newMaterials; // この行を追加
                }
            }
            else
            {
                // 元のマテリアルに戻す処理をここに記述
                
            }
        }

    }
    
}
