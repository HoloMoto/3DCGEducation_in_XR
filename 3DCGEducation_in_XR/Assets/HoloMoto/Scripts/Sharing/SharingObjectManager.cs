using System.Collections;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Unity.VisualScripting;
using TMPro;


namespace HoloMoto.Manager
{
    //Sharing用のアンカーを作成するクラス、ShaeringAncher作成後にモードがSharingに変更される
    public class SharingObjectManager :MonoBehaviourPunCallbacks
    {
        //falseの場合はシングルプレイ(Photonを使用しない)
        public bool isMultiPlayMode = true;
        public SharingMode sharingMode = SharingMode.None;
        public GameObject avatarSpawner;
        public GameObject[] sharingTransform;
        public Transform sharingAnchor;

        
        //UnityのInspectorに表示するラベル
        [Header("UIs")]
        [SerializeField]
        TextMeshProUGUI RoomMenberNum;
        
        
        
        public enum SharingMode
        {
            None,
            Sharing,
            NotSharing
        }

        public void ConnectPhoton()
        {
            DontDestroyOnLoad(this);
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                isMultiPlayMode = false;
            }
            else
            {
                PhotonAccsessSettings();
            }
        }

        
        void PhotonAccsessSettings()
        {
            PhotonNetwork.ConnectUsingSettings();

        }
        public override void OnConnectedToMaster() {
            // "Room"という名前のルームに参加する（ルームが存在しなければ作成して参加する）
            PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions(), TypedLobby.Default);
        }
        // ゲームサーバーへの接続が成功した時に呼ばれるコールバック
        public override void OnJoinedRoom() {
            
            GameObject avatar = PhotonNetwork.Instantiate("Avatar", Vector3.zero, Quaternion.identity);
            // avatarSpawnerの子オブジェクトとしてアバターを設定
            avatar.transform.parent = avatarSpawner.transform;
            avatar.transform.localPosition = Vector3.zero;
            // Roomにすでにいる人数をログで出力
            RoomMenberNum.text = "Player:" +PhotonNetwork.CurrentRoom.PlayerCount.ToString();  
            Debug.Log("Player:" +PhotonNetwork.CurrentRoom.PlayerCount.ToString());
            sharingMode = SharingMode.NotSharing;
        }

        bool isUpdate = true;
        // Update is called once per frame
        void Update()
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && isUpdate)
            {
                Vector2 touchPosition = Input.GetTouch(0).position;
                Ray ray = Camera.main.ScreenPointToRay(touchPosition);
                RaycastHit hit;
                if (SharingMode.NotSharing == sharingMode)
                {
                    //画面をタップしたときfloorにレイを当てその位置をsharingTransform[0]に取得
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.transform.tag == "floor")
                        {
                            if (sharingTransform[0].transform.position == Vector3.zero )
                            {
                                sharingTransform[0].transform.position = hit.point;
                                isUpdate = false;
                                StartCoroutine(StanCounter());
                                return;
                            }
                            if (sharingTransform[1].transform.position == Vector3.zero  )
                            {
                                sharingTransform[1].transform.position = hit.point;
                                isUpdate = false;
                                StartCoroutine(StanCounter());
                                return;
                            }
                            if (sharingTransform[2].transform.position == Vector3.zero  )
                            {
                                sharingTransform[2].transform.position = hit.point;
                                isUpdate = false;
                                StartCoroutine(StanCounter());
                                return;
                            }
                            else
                            {
                                sharingTransform[3].transform.position = hit.point;
                                //sharingTransformの3つの座標の重心にsharingAnchorを設定
                                sharingTransform[4].transform.position = (sharingTransform[0].transform.position + sharingTransform[1].transform.position+ sharingTransform[2].transform.position) / 3;
                                sharingTransform[5].transform.position = (sharingTransform[1].transform.position + sharingTransform[2].transform.position + sharingTransform[3].transform.position) / 3;
                                sharingAnchor.position = (sharingTransform[4].transform.position + sharingTransform[5].transform.position ) / 2;
                                //sharingAnchorの回転はsharingTransform[4]とsharingTransform[5]の垂線の方向に設定
                                sharingAnchor.rotation = Quaternion.LookRotation(sharingTransform[5].transform.position - sharingTransform[4].transform.position);
                                
                                sharingMode = SharingMode.Sharing;
                                isUpdate = false;
                                StartCoroutine(StanCounter());
                                this.transform.position = sharingAnchor.transform.position;
                                //y軸のみこのオブジェクトの回転角とsharingAnchorの回転角を同じにする
                                this.transform.rotation = Quaternion.Euler(0, sharingAnchor.transform.rotation.eulerAngles.y, 0);
                            }
                        }
                    }
                    return;
                }
            }
        }

        IEnumerator StanCounter()
        {
            yield return new WaitForSeconds(2f);
            isUpdate = true;
        }
    }
}
