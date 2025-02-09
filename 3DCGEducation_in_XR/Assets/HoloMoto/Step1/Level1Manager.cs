using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.HID;
using TMPro;

namespace HoloMoto.Manager
{
    public class Level1Manager : MonoBehaviour
    {
        [SerializeField] private GameObject _unityChan;
        public int _level = 0; //Status

        public bool _debugFlag = false;

        [SerializeField] UnityEvent[] _Events;

        [SerializeField] TextMeshPro _ToolTipText;
        [SerializeField] private Material _changeMaterial;
        [SerializeField] Material[] _unityChanMaterials;　
        public int _sMeshRendererMaterialCount = 0;
        public int _mRendererMaterialCount = 0;
        [SerializeField] UnityEngine.UI.Button _button;

        [SerializeField] private Material _setPolygonMaterial;
        [SerializeField] private Texture2D _initTexture;
        [SerializeField] Texture2D _texture;
        private void Awake()
        {
            //forDebug
            _button.onClick.AddListener(() =>
            {
                ChangeEventStatus(true);
            });
        }

        // Start is called before the first frame update
        void Start()
        {
            _setPolygonMaterial.SetTexture("_MainTex", _initTexture);
            Debug.Log("Level1 Start");
            Animator animator = _unityChan.GetComponent<Animator>();
            animator.SetTrigger("Next");
            //_unitychanの子オブジェクトが持つすべてのマテリアルを順番に取得して、_unityChanMaterialsに格納
            SkinnedMeshRenderer[] skinnedMeshRenderers = _unityChan.GetComponentsInChildren<SkinnedMeshRenderer>();
            int skinnedMeshRendererCount = skinnedMeshRenderers.Length;
            for (int i = 0; i < skinnedMeshRenderers.Length; i++)
            {
                _unityChanMaterials[i] = skinnedMeshRenderers[i].material;
            }
            MeshRenderer[] MeshRenderers = _unityChan.GetComponentsInChildren<MeshRenderer>();
            int j = 0;
            for (int i = skinnedMeshRendererCount + 1 ; i <skinnedMeshRendererCount + 1+ MeshRenderers.Length ; i++)
            {
                Debug.Log(MeshRenderers[j].material.name);
                _unityChanMaterials[i] = MeshRenderers[j].material;
                Debug.Log(_unityChanMaterials[i].name);
                j++;
            }
            j=0;
            
            _level = 1;
        }
        
        bool _isNext = true;
        
        public void ChangeEventStatus(bool check)
        {
            _isNext = check;
            if (_isNext)
            {
                _level++;
            }
            else
            {
                _level--;
            }

            switch (_level)
            {
                case 1:
                    _Events[0].Invoke();
                    break;
                case 2:
                    //_Events[1].Invoke();
                    SetColorModel();
                    _ToolTipText.text = "3DCG";
                    break;
                case 3:
                    _Events[2].Invoke();
                    _ToolTipText.text = "Wireframe";
                    break;
                
                case 4:
                    _Events[3].Invoke();
                    _ToolTipText.text = "Wireframe";
                    break;
                case 5:
                    _Events[4].Invoke();
                    _setPolygonMaterial.SetTexture("_MainTex", _texture);
                    _ToolTipText.text = "Polygon";
                    break;
                case 6:
                    _Events[5].Invoke();
                    _ToolTipText.text = "Polygon";
                    break;
                case 7:
                    _Events[6].Invoke();
                    _ToolTipText.text = "Vertex";
                    break;
                case 8:
                    _Events[7].Invoke();
                    break;
                case 9:
                    _Events[8].Invoke();
                    break;
                case 10:
                    _Events[9].Invoke();
                    break;
                default:
                    break;
            }
        }

        public void SetColorModel()
        {
            if (_isNext)
            {
                //_unityChanの子オブジェクトの持つすべてのマテリアルを_changeMaterialに変更
                SkinnedMeshRenderer[] skinnedMeshRenderers = _unityChan.GetComponentsInChildren<SkinnedMeshRenderer>();
                _sMeshRendererMaterialCount = skinnedMeshRenderers.Length;
                for (int i = 0; i < skinnedMeshRenderers.Length; i++)
                {
                    skinnedMeshRenderers[i].material = _changeMaterial;
                }
                MeshRenderer[] meshRenderers = _unityChan.GetComponentsInChildren<MeshRenderer>();
                _mRendererMaterialCount = meshRenderers.Length;
                for (int i = _sMeshRendererMaterialCount+1 ; i < _sMeshRendererMaterialCount+1+meshRenderers.Length; i++)
                {
                    meshRenderers[i].material = _changeMaterial;
                }
            }
            else
            {
                //_unityChanの子オブジェクトの持つすべてのマテリアルを元に戻す
                SkinnedMeshRenderer[] skinnedMeshRenderers = _unityChan.GetComponentsInChildren<SkinnedMeshRenderer>();
                _sMeshRendererMaterialCount = skinnedMeshRenderers.Length;
                for (int i = 0; i < skinnedMeshRenderers.Length; i++)
                {
                    skinnedMeshRenderers[i].material = _unityChanMaterials[i];
                }
                MeshRenderer[] meshRenderers = _unityChan.GetComponentsInChildren<MeshRenderer>();
                _mRendererMaterialCount = meshRenderers.Length;
                for (int i = _sMeshRendererMaterialCount+1; i < meshRenderers.Length; i++)
                {
                    meshRenderers[i].material = _unityChanMaterials[i];
                }
            }
        }
        
    }
}