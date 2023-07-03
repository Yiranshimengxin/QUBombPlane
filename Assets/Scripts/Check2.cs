using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Check2 : MonoBehaviour
{
    public ButtonType buttonType;
    public Button button;
    public Player player;

    private void Start()
    {
        buttonType = ButtonType.Null;
        button = GetComponent<Button>();
        button.onClick.AddListener(delegate
        {
            if (buttonType == ButtonType.PlaneHead)
            { 
                Main.BlowUpPlaneHead();
            }
        });
    }
}
