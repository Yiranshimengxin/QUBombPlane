using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleField : MonoBehaviour
{
    public GameObject prefab;  //格子预制体
    const int line = 10;  //生成表格的行数和列数
    Check[] checks;  //查找生成的格子子物体
    public List<ButtonType> buttonTypes;  //查找每个格子的状态

    void Start()
    {
        InitBattleField1();

        checks = GetComponentsInChildren<Check>();

    }

    ////设置战场，点击按钮的位置会显示提示图片
    //public void SetBattleField(string name, string idxStr)
    //{
    //    Transform t = transform.Find(name);
    //    foreach (Transform item in t)
    //    {
    //        item.gameObject.SetActive(false);
    //    }
    //    t.Find(idxStr).gameObject.SetActive(true);
    //    t.GetComponent<Button>().interactable = false;
    //}

    //初始化
    public void InitBattleField1()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        for (int i = 0; i < line; i++)
        {
            for (int j = 0; j < line; j++)
            {
                GameObject p = Instantiate(prefab, transform);
                p.name = j + "_" + i;
                p.GetComponent<Button>().onClick.AddListener(delegate
                {

                });
            }
        }
    }

    void Update()
    {
        buttonTypes.Clear();
        for (int i = 0; i < checks.Length; i++)
        {
            buttonTypes.Add(checks[i].buttonType);
        }
    }
}
