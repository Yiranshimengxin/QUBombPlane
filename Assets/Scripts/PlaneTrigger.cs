using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaneTrigger : MonoBehaviour
{
    public Button button;  //获取碰撞到的按钮
    public Check check;  //获取碰撞到的格子
    public bool isCheckUse = false;  //判断格子是否被占用

    public static int triggerCount = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Trigger" || other.tag=="Trigger9")
        {
            triggerCount++;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Null")
        {
            isCheckUse = true;
            check = other.GetComponent<Check>();
            button = other.GetComponent<Button>();
            button.interactable = false;
            if (gameObject.name == "Trigger9")
            {
                check.buttonType = ButtonType.PlaneHead;
                return;
            }
            check.buttonType = ButtonType.Plane;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        try
        {
            if (other.tag == "Plane" || other.tag == "PlaneHead")
            {
                isCheckUse = false;
                button.interactable = true;
                check.buttonType = ButtonType.Null;
            }

            if (other.tag == "Trigger" || other.tag == "Trigger9")
            {
                triggerCount--;
            }
        }
        catch(Exception e)
        {
            Debug.Log("触发器故障！");
        }

    }
}
