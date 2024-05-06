using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField]
    private Vector3 rotationBias;
    
    //ビルボードの向きを反転してほしい
    void Update()
    {
        transform.LookAt(Camera.main.transform , Vector3.down);
        transform.Rotate(rotationBias);
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

}
