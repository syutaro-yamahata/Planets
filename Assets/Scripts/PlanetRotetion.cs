using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotetion : MonoBehaviour
{
    [Header("惑星の回転軸")]
    // 公開プロパティ：惑星の回転軸
    [SerializeField] private Vector3 rotationAxis = Vector3.up; // デフォルトはY軸回転
    [Header("惑星の回転速度")]
    // 公開プロパティ：惑星の回転速度
    [SerializeField] private float rotationSpeed = 10f;

    void Update()
    {
        // 自転処理
        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
    }
}
