using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HoloMoto.Manager
{
    public class GameManager : MonoBehaviour
    {
        // 
        private GameObject currentTarget;

        // Start is called before the first frame update
        void Start()
        {
           Debug.Log(GetCurrentTarget());
           
        }

        // Update is called once per frame
        void Update()
        {

        }

        // 
        public GameObject GetCurrentTarget()
        {
            
            return currentTarget;
        }
    }
}
