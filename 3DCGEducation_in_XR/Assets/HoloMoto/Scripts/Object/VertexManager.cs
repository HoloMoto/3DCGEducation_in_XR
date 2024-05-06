using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace HoloMoto.Manager
{
   //Manage of vertices Objects 
    public class VertexManager : MonoBehaviour
    {
        public float _verticesSize = 1;//頂点の大きさ
        
        public bool isVertexMode = false;//頂点モードの場合
        public float _verticesChangedSsize = 5;
        // Start is called before the first frame update
        void Start()
        {
            //頂点の初期大きさを変更
            transform.localScale = new Vector3(_verticesSize, _verticesSize, _verticesSize);
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        
        //頂点の大きさを滑らかに変更 
        public void ChangeVerticesSize(float size)
        {
            //for文で指定された大きさまで時間とともに緩やかに変更する
            StartCoroutine(ChangeVerticesSizeCoroutine(size));
        }
        
        //頂点の大きさを滑らかに変更するコルーチン
        IEnumerator ChangeVerticesSizeCoroutine(float size)
        {
            float time = 0;
            float duration = 1.0f;
            float startSize = _verticesSize;
            while (time < duration)
            {
                time += Time.deltaTime;
                float t = time / duration;
                _verticesSize = Mathf.Lerp(startSize, size, t);
                transform.localScale = new Vector3(_verticesSize, _verticesSize, _verticesSize);
                yield return null;
            }
        }
    }
    
   
}