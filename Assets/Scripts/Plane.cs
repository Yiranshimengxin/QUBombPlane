using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Plane : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public GameObject littlePlane;  //小飞机，用于图标显示
    public GameObject bigPlane;  //大飞机，用于拖动到战场中

    public Button btnRotate;  //旋转按钮
    public GameObject checkCentre;  //飞机最中心所占用的格子

    public PlaneTrigger[] triggers;  //获取飞机上的所有触发器子物体
    public GameObject trigger9;  //获取飞机上的Trigger9子物体，该物体用来判断飞机在特定位置能不能旋转

    public Vector3 lastPos;  //飞机上一次部署的Vector3坐标
    public Quaternion lastRot;  //飞机上一次部署的Quaternion坐标

    public Button btnPVP;

    Vector3 v = new Vector3(596, 112, 0);

    void Start()
    {
        bigPlane = transform.Find("BigPlane").gameObject;
        littlePlane = transform.Find("LittlePlane").gameObject;
        btnRotate = transform.Find("BtnRotate").GetComponent<Button>();
        triggers = GetComponentsInChildren<PlaneTrigger>();
        trigger9 = transform.Find("Trigger9").gameObject;
    }

    //开始拖拽
    public void OnBeginDrag(PointerEventData eventData)
    {
        SetDraggedPosition(eventData);
        btnPVP = GameObject.Find("BtnPVP").GetComponent<Button>();
        bigPlane.SetActive(true);
        littlePlane.SetActive(false);

        //获取飞机上一次部署的坐标，在鼠标开始拖拽时获取
        if (140 < lastPos.x && lastPos.x < 740)
        {
            if (210 < lastPos.y && lastPos.y < 810)
            {
                lastPos = transform.position;
                lastRot = transform.rotation;
                return;
            }
        }
        else
        {
            lastPos = Vector3.zero + v;
        }

    }

    // 拖拽中
    public void OnDrag(PointerEventData eventData)
    {
        SetDraggedPosition(eventData);
        bigPlane.SetActive(true);
    }

    //结束拖拽
    public void OnEndDrag(PointerEventData eventData)
    {
        SetDraggedPosition(eventData);
        if (checkCentre == null)
        {
            //返回起始位置
            transform.position = Vector3.zero + v;
            return;
        }

        for (int i = 0; i < triggers.Length; i++)
        {
            if (triggers[i].isCheckUse == false)
            {
                //返回上一个位置，UI提示
                print(triggers[i].button);
                transform.position = v;
                transform.rotation = lastRot;
                return;
            }
            else
            {
                transform.position = checkCentre.transform.position;
                checkCentre.GetComponent<Button>().interactable = false;
            }
        }
    }

    private void SetDraggedPosition(PointerEventData eventData)
    {
        var rt = gameObject.GetComponent<RectTransform>();
        // 将屏幕上的点转换为世界上的点
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, eventData.position, eventData.pressEventCamera, out globalMousePos))
        {
            rt.position = globalMousePos;
        }
    }

    public void Rotate()
    {
        if (trigger9.GetComponent<PlaneTrigger>().button == null)
        {
            return;
        }
        if (PlaneTrigger.triggerCount == 2 || PlaneTrigger.triggerCount == 4)
        {
            transform.position = Vector3.zero + v;
            transform.rotation = lastRot;
            return;
        }
        string triggerName9 = trigger9.GetComponent<PlaneTrigger>().button.name;

        if (triggerName9 != 2 + "_" + 0 && triggerName9 != 3 + "_" + 0 && triggerName9 != 4 + "_" + 0 && triggerName9 != 5 + "_" + 0 && triggerName9 != 6 + "_" + 0 && triggerName9 != 7 + "_" + 0
        && triggerName9 != 9 + "_" + 2 && triggerName9 != 9 + "_" + 3 && triggerName9 != 9 + "_" + 4 && triggerName9 != 9 + "_" + 5 && triggerName9 != 9 + "_" + 6 && triggerName9 != 9 + "_" + 7
        && triggerName9 != 2 + "_" + 9 && triggerName9 != 3 + "_" + 9 && triggerName9 != 4 + "_" + 9 && triggerName9 != 5 + "_" + 9 && triggerName9 != 6 + "_" + 9 && triggerName9 != 7 + "_" + 9
        && triggerName9 != 0 + "_" + 2 && triggerName9 != 0 + "_" + 3 && triggerName9 != 0 + "_" + 4 && triggerName9 != 0 + "_" + 5 && triggerName9 != 0 + "_" + 6 && triggerName9 != 0 + "_" + 7)
        {
            transform.Rotate(0, 0, -90);
        }

        //for (int i = 0; i < triggers.Length; i++)
        //{
        //    if (triggers[i].isCheckUse == false)
        //    {
        //        print(gameObject.name + "_" + triggers[i].name);
        //        transform.rotation = lastRot;
        //        return;
        //    }
        //}
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Plane")
        {
            checkCentre = GameObject.Find(other.name);
        }
    }

    private void Update()
    {
        if (btnPVP == null)
        {
            return;
        }
        else if (btnPVP.interactable == false)
        {
            transform.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
