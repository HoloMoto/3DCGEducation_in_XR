using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageAnchor : MonoBehaviour
{
    [SerializeField] private GameObject SharingObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetAnchor(GameObject _anchor)
    {
        SharingObject.transform.position = _anchor.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
