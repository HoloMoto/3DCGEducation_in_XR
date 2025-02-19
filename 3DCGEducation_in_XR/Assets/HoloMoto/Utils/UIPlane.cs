using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIPlane : MonoBehaviour
{

    public TextMeshPro _Title;
    public TextMeshPro _Description;

    public void SetText(string title, string description)
    {
        _Title.text = title;
        _Description.text = description;
    }
}
