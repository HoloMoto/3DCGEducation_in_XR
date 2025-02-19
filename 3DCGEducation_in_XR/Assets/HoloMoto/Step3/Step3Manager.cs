using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Unity.UI;
using UnityEngine.UIElements;
using TMPro;
using System.IO;
using ExitGames.Client.Photon.StructWrapping;

public class Step3Manager : MonoBehaviour
{
    [SerializeField] private TextMeshPro _ToolTipText;
    public GameObject _targetObject;
    public GameObject _DescriptionGameObject;
    [SerializeField] int _level;
    [SerializeField] UnityEvent[] _Events;
    [SerializeField] UnityEngine.UI.Button _nextButton;
    bool _isNext = true;
    [SerializeField] Material _targetMaterial;

    [SerializeField] private Texture2D[] _texture2Ds;
    [SerializeField] UIPlane _uiPlane;
    [SerializeField] private TextAsset _csvFile; // CSV file reference

    public List<LevelData> _levelDataList;

    private void Awake()
    {
        _nextButton.onClick.AddListener(() => { ChangeEventStatus(true); });

        _targetObject.GetComponent<Renderer>().material.SetTexture("_BumpMap", null);
        _uiPlane.SetText("Texture", "Whats Texture?");
    }

    // Start is called before the first frame update
    void Start()
    {
        _level = 0;
        _targetMaterial = _targetObject.GetComponent<MeshRenderer>().material;
        _ToolTipText.text = "non Texture";

        _levelDataList = CSVReader.ReadCSV(_csvFile.text);
        Debug.Log(_levelDataList.Count);
    }

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

        if (_level >= 0 && _level < _levelDataList.Count)
        {
            _uiPlane.SetText(_levelDataList[_level].Title, _levelDataList[_level].Description);
        }

        switch (_level)
        {
            //Object Show
            //Lighting off
            case 1:
                _Events[0].Invoke();
                break;
            //BaseMap Texture
            case 2:
                SetTexture(0, "BaseMap");
                _Events[1].Invoke();
                break;
            case 3:
                SetTexture(1, "NormalMap");
                _Events[2].Invoke();
                break;
            case 4:
                SetTexture(2, "EmissionMap ");   
                _Events[3].Invoke();
                break;
            case 5:
                SetTexture(3, "OcculusionMap");
                _Events[4].Invoke();
                break;
            case 6:
                SetTexture(4, "MetallicMap");
                _Events[5].Invoke();
                break;
            case 7:
                
                _Events[6].Invoke();
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

    [SerializeField] Light _directionalLight;

    public void TurnOfforOnDirectionalLight()
    {
        _directionalLight.enabled = !_directionalLight.enabled;
    }

    public void SetTexture(int i, string tips)
    {
        _DescriptionGameObject.GetComponent<Renderer>().material.SetTexture("_BaseMap", _texture2Ds[i]);
        switch (i)
        {
            case 0:
                _targetObject.GetComponent<Renderer>().material.SetTexture("_BaseMap", _texture2Ds[i]);
                break;
            case 1:
                _targetObject.GetComponent<Renderer>().material.mainTexture = null;
                _targetObject.GetComponent<Renderer>().material.SetTexture("_BumpMap", _texture2Ds[i]);
                break;
            case 2:
                _targetObject.GetComponent<Renderer>().material.SetTexture("_BumpMap", null);
                _targetObject.GetComponent<Renderer>().material.SetTexture("_EmissionMap", _texture2Ds[i]);
                _targetObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.white);
                break;
            case 3:
                _targetObject.GetComponent<Renderer>().material.SetTexture("_EmissionMap", null);
                _targetObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.black);
                _targetObject.GetComponent<Renderer>().material.SetTexture("_OcclusionMap", _texture2Ds[i]);
                _targetObject.GetComponent<Renderer>().material.SetFloat("_OcclusionStrength", 1.0f);
                break;
            case 4:
                _targetObject.GetComponent<Renderer>().material.SetTexture("_OcclusionMap", null);
                _targetObject.GetComponent<Renderer>().material.SetTexture("_MetallicGlossMap", _texture2Ds[i]);
                _targetObject.GetComponent<Renderer>().material.SetFloat("_Smoothness", 1.0f);

                break;
        }

        _ToolTipText.text = tips;
    }

    private IEnumerator RefreshScene()
    {
        yield return null; // 次のフレームまで待機
        Resources.UnloadUnusedAssets(); // メモリ整理で更新を促す
    }
}

[System.Serializable]
public class LevelData
{
    public string Title;
    public string Description;
}

public class CSVReader
{
    public static List<LevelData> ReadCSV(string csvText)
    {
        List<LevelData> levelDataList = new List<LevelData>();

        // CSVの行を改行で分割
        string[] lines = csvText.Split(new[] { "\r\n", "\n" }, System.StringSplitOptions.RemoveEmptyEntries);

        foreach (string line in lines)
        {
            string[] values = line.Split(',');
            if (values.Length < 2) continue; // データが不足していたらスキップ

            LevelData data = new LevelData
            {
                Title = values[0].Trim(),
                Description = values[1].Trim()
            };
            levelDataList.Add(data);
        }

        return levelDataList;
    }
}