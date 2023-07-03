using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 按钮状态，Null为空格子，Plane为飞机机身，PlaneHead为飞机机头，机头会触发暴击
/// </summary>
public enum ButtonType
{
    Null,
    Plane,
    PlaneHead
}

public class Check : MonoBehaviour
{
    public ButtonType buttonType;
    public Button button;
    public bool isUse = false;
    public static int checkUseCount = 0;

    private void Start()
    {
        buttonType = ButtonType.Null;
        button = GetComponent<Button>();
    }

    private void Update()
    {
        if (buttonType == ButtonType.Null)
        {
            isUse = false;
            button.interactable = true;
            gameObject.tag = "Null";
            checkUseCount--;
        }
        else if (buttonType == ButtonType.Plane)
        {
            isUse = true;
            button.interactable = false;
            gameObject.tag = "Plane";
            checkUseCount++;
        }
        else if (buttonType == ButtonType.PlaneHead)
        {
            isUse = true;
            button.interactable = false;
            gameObject.tag = "PlaneHead";
            checkUseCount++;
        }
    }
}
