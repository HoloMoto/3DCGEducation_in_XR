using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TapToPlaceOnWall : MonoBehaviour
{
    [SerializeField]
    private ARRaycastManager raycastManager;

    [SerializeField]
    private GameObject objectToPlace;

    void Update()
    {
        // タッチが1つの場合のみ処理
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // タッチの種類が始まりの場合のみ処理
            if (touch.phase == TouchPhase.Began)
            {
                // タップ位置から壁を検出
                RaycastToWall(touch.position);
            }
        }
    }

    private void RaycastToWall(Vector2 screenPosition)
    {
        // 壁を検出するために、ARKitのトラック可能な平面タイプを限定
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        TrackableType trackableType = TrackableType.PlaneWithinPolygon | TrackableType.PlaneEstimated;

        if (raycastManager.Raycast(screenPosition, hits, trackableType))
        {
            // 検出された平面の中から最も近いものを選択
            ARRaycastHit hit = hits[0];

            // 壁面であるかどうかを確認（垂直の平面をチェック）
            Pose hitPose = hit.pose;
            ARPlane plane = hit.trackable as ARPlane;

            if (plane != null && plane.alignment == PlaneAlignment.Vertical)
            {
                // オブジェクトを壁の位置に移動
                PlaceObject(hitPose);
            }
        }
    }

    private void PlaceObject(Pose pose)
    {
        if (objectToPlace != null)
        {
            // オブジェクトの位置を更新
            objectToPlace.transform.position = pose.position;
            objectToPlace.transform.rotation = pose.rotation;
        }
    }
}