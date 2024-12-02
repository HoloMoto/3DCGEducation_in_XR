using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anchor : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject sharingObject = GameObject.Find("SharingObject");
        sharingObject.GetComponent<ImageAnchor>().SetAnchor(this.gameObject);
    }
}
