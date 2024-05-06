using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Management;


namespace HoloMoto.Manager
{
    public class GameManager : MonoBehaviour
    {
        public EventStatus eventStatus;
        public enum EventStatus
        {
           none,
           first_whats3d
        }
        private GameObject currentTarget;
        [SerializeField] SharingObjectManager sharingObjectManager;
        //マスターモードではないばあい＝ゲーム進行を他者がネット経由で行う場合UI等を削除
        [SerializeField] private GameObject[] _masterModeObjects;
        public UnityEvent[] _Events;
        
        // Start is called before the first frame update
        void Start()
        {
            eventStatus = EventStatus.none;
           Debug.Log(GetCurrentTarget());
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SelectGameMode(bool _MasterMode)
        {

            if (_MasterMode)
            {
                //_masterModeObjectsの中身を非表示
                foreach (var obj in _masterModeObjects)
                {
                    obj.SetActive(false);
                }
            }
        }
        
        public GameObject GetCurrentTarget()
        {
            return currentTarget;
        }
    }
}
