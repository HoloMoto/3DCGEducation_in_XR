using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace  HoloMoto.Manager
{
    
    public class StageManager : MonoBehaviour
    {
        public UnityEvent[] _StageEvents;
        public int _currentEvent = 0;

        public void StageEvent()
        {
            _StageEvents[_currentEvent].Invoke();
            
            _currentEvent++;
        }
        // Update is called once per frame
        void Update()
        {
        
        }
    }
   
}