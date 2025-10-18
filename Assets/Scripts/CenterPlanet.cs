using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterPlanet : MonoBehaviour
{
    [Header("中心となるオブジェクト(太陽)")]
    [SerializeField]
    private Transform centerObject;  // 中心となるオブジェクト
    [Header("惑星が周期する速さ")]
    [SerializeField]
    private float orbitSpeed = 10f; // 軌道速度
    [Header("惑星と太陽の距離")]
    [SerializeField]
    private float orbitRadius = 10f; // 軌道半径


    void Update()
    {
        // 軌道運動
        transform.RotateAround(centerObject.position, Vector3.up, orbitSpeed * Time.deltaTime);

        // 軌道半径を維持
        Vector3 offset = transform.position - centerObject.position;
        transform.position = centerObject.position + offset.normalized * orbitRadius;
    }
}
