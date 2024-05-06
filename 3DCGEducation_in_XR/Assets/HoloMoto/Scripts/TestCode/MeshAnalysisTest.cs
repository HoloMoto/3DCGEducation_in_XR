using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  HoloMoto.Test
{
    public class MeshAnalysisTest : MonoBehaviour
    {
        [SerializeField] GameObject[] point;
        [SerializeField] private GameObject line;
        // Start is called before the first frame update
        void Start()
        {
           Wireframe();
        }

        public void Wireframe()
        {
            line.transform.position = (point[0].transform.position + point[1].transform.position) / 2;
            line.transform.LookAt(point[1].transform.position);
            line.transform.localScale = new Vector3(0.01f, 0.01f, Vector3.Distance(point[0].transform.position, point[1].transform.position));
        }
        // Update is called once per frame
        void Update()
        {
            
        }
    }
    
}
