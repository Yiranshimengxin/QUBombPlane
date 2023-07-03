using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleField2 : MonoBehaviour
{
    public GameObject prefab;  //格子预制体
    const int line = 10;  //生成表格的行数和列数
    public Check[] checks;  //查找生成的格子子物体
    public List<ButtonType> buttonTypes;  //查找每个格子的状态
    public BattleField battleField;
    public Button btnPVP;

    private void Start()
    {
        btnPVP = GameObject.Find("BtnPVP").GetComponent<Button>();
        InitBattleField2();

        Invoke("Init", 1);
    }

    //设置战场，点击按钮的位置会显示提示图片
    public void SetBattleField(string name, string idxStr,string playerName,string msgName)
    {
        if (playerName != msgName)
        {
            return;
        }
        Transform t = transform.Find(name);
        foreach (Transform item in t)
        {
            item.gameObject.SetActive(false);
        }
        t.Find(idxStr).gameObject.SetActive(true);
        t.GetComponent<Button>().interactable = false;
    }

    public void InitBattleField2()
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
                    if (!Main.isStart)
                    {
                        return;
                    }
                    if (!Main.MayPlay())
                    {
                        print("MAYPLAY");
                        return;
                    }
                    NetManager.Send("Play|" + p.name+"_"+Main.myName);
                });
            }
        }
    }

    private void Init()
    {
        checks = GetComponentsInChildren<Check>();
    }

    private void SetButtonType()
    {
        for (int i = 0; i < checks.Length; i++)
        {
            checks[i].buttonType = buttonTypes[i];
        }
    }

    /*public void Update()
    {
        buttonTypes.Clear();
        for (int i = 0; i < checks.Length; i++)//100
        {
            buttonTypes.Add(battleField.buttonTypes[i]);
            checks[i].buttonType = buttonTypes[i];
            //
            *//*for (int j = 0; j < checks.Length; j++)//同步表
            {
                if (battleField.buttonTypes[i] == ButtonType.Null)//0.1.2
                {
                    NetManager.Send("SetPlane|" + 0);
                }
                if (battleField.buttonTypes[i] == ButtonType.Plane)//0.1.2
                {
                    NetManager.Send("SetPlane|" + 1);
                }
                if (battleField.buttonTypes[i] == ButtonType.PlaneHead)//0.1.2
                {
                    NetManager.Send("SetPlane|" + 2);
                }
            }*//*
            //
        }
    }*/
}
