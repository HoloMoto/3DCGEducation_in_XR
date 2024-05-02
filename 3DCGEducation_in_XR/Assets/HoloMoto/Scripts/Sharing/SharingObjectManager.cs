using UnityEngine;

namespace HoloMoto.Manager
{
    public class SharingObjectManager : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
            // タッチがあった場合
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                // タッチした座標を取得
                Vector2 touchPosition = Input.GetTouch(0).position;

                // タッチした座標からRayを飛ばす
                Ray ray = Camera.main.ScreenPointToRay(touchPosition);
                RaycastHit hit;

                // Rayが何かに当たった場合
                if (Physics.Raycast(ray, out hit))
                {
                    // 当たったオブジェクトが床だった場合、自身のオブジェクトをその座標に移動
                    if (hit.transform.tag == "floor")
                    {
                        transform.position = hit.point;
                    }
                }
            }
        }
    }
}
