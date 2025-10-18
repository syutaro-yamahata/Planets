using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetInteraction : MonoBehaviour
{
    [Header("遠近するカメラを設定")]
    [SerializeField]
    private Camera mainCamera;  // メインカメラ
    [Header("惑星にズームする速度")]
    [SerializeField]
    private float zoomSpeed = 5f;  // ズーム速度
    [Header("惑星にズームした際の惑星との距離")]
    [SerializeField]
    private float zoomDistance = 5f; // ズーム後の距離
    [Header("ズーム後のY軸の調整")]
    [SerializeField]
    private float planetSpecificYOffset; // 各惑星ごとに設定可能なYオフセット

    private Vector3 originalPosition; // カメラの初期位置
    private Quaternion originalRotation; // カメラの初期回転
    private Transform targetPlanet; // 現在ズーム中の惑星
    private bool isZooming = false; // ズーム中かどうかのフラグ
    private bool isReturning = false; // 元の位置に戻るフラグ

    void Start()
    {
        // カメラの初期位置と回転を保存
        originalPosition = mainCamera.transform.position;
        originalRotation = mainCamera.transform.rotation;
    }

    void OnMouseDown()
    {
        // 惑星がクリックされたときにズームを開始
        targetPlanet = transform;
        isZooming = true;
        isReturning = false; // 元の位置に戻るフラグをリセット
    }

    void Update()
    {
        if (isZooming && targetPlanet != null)
        {
            // 惑星にズームする位置を計算
            Vector3 targetPosition = targetPlanet.position - targetPlanet.forward * zoomDistance;
            targetPosition.y += planetSpecificYOffset; // 各惑星ごとの高さを適用

            // カメラをズーム位置に移動
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, Time.deltaTime * zoomSpeed);

            // カメラを惑星の方向に向ける
            Quaternion targetRotation = Quaternion.LookRotation(targetPlanet.position - mainCamera.transform.position);
            mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, targetRotation, Time.deltaTime * zoomSpeed);

            // 一定距離と向きに到達したらズームを停止
            if (Vector3.Distance(mainCamera.transform.position, targetPosition) < 0.1f &&
                Quaternion.Angle(mainCamera.transform.rotation, targetRotation) < 1f)
            {
                isZooming = false;
            }
        }

        if (isReturning)
        {
            // 元の位置に戻る処理
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, originalPosition, Time.deltaTime * zoomSpeed);
            mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, originalRotation, Time.deltaTime * zoomSpeed);

            // 初期位置と回転に戻ったらリセット
            if (Vector3.Distance(mainCamera.transform.position, originalPosition) < 0.1f &&
                Quaternion.Angle(mainCamera.transform.rotation, originalRotation) < 1f)
            {
                isReturning = false;
                targetPlanet = null;
            }
        }

        // 右クリックで元の位置に戻る
        if (Input.GetMouseButtonDown(1))
        {
            isZooming = false; // ズームを停止
            isReturning = true; // 元の位置に戻る処理を開始
        }
    }
}
