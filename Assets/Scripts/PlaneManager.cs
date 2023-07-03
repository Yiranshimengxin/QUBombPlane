using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaneManager : MonoBehaviour
{
    public GameObject planePrefab;  //飞机预制体
    public RectTransform airportPos;  //机场坐标，小飞机图标置于其图层之上
    public int planeCount = 3;  //飞机的生成数量
    void Start()
    {
        for (int i = 0; i < planeCount; i++)
        {
            Instantiate(planePrefab, transform);
            planePrefab.name = "Plane" + i;
            Vector2 v = new Vector2(0, 22);
            planePrefab.GetComponent<RectTransform>().localPosition = Vector2.zero + v;
        }
    }
}
