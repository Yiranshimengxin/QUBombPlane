using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerTurn
{
    None,
    Player1,
    Player2
}

public class Main : MonoBehaviour
{
    static PlayerTurn playerTurn = PlayerTurn.Player1;

    Image startGamePanel;  //开始游戏界面
    InputField inputText;
    Button btnStart;  //开始游戏按钮
    Button btnQuit;  //结束游戏按钮
    Button btnQuitGame;  //结束游戏按钮
    Image bgChoice;  //选择按钮组
    Transform player1;  //玩家1
    Image player1Panel;  //玩家1的面板
    Text p1ID;  //玩家1的ID
    Text p1Score;  //玩家1的成绩
    Transform player2;  //玩家2
    Image player2Panel;  //玩家2的面板
    Text p2ID;  //玩家2的ID
    Text p2Score; //玩家2的成绩
    Button btnPVE;  //PVE按钮
    Button btnPVP;  //PVP按钮
    Image sorryPanel;  //滑跪面板
    Image waitPanel;  //等待面板
    Image errorPanel;  //错误面板
    Button btnCloseSorry;  //退出滑跪面板
    Transform hint;  //提示
    Image cover;  //battleField1的遮挡
    Transform resultPanel;  //结束面板
    Image win;  //胜利面板
    Image lost;  //失败面板
    Text winText;  //结束文字
    Text lostText;  //结束文字

    static int winNum = 0;
    public static string myName;

    public BattleField battleField1;
    public BattleField2 battleField2;
    static int nullCount = 0;  //空格子的数量


    public static Player me;
    public static Player opponent;
    public static bool isStart;  //游戏是否开始

    private void Start()
    {
        Init();

        NetManager.Connect("127.0.0.1", 8888);
        NetManager.AddListener("EnterGame", OnEnter);
        NetManager.AddListener("Play", OnPlay);
        NetManager.AddListener("GameStart", OnGameStart);
        NetManager.AddListener("Leave", OnLeave);
        NetManager.AddListener("Result", OnResult);
        NetManager.AddListener("SetPlane", OnSetPlane);  //放置飞机
    }

    private void OnResult(string str)
    {
        print("OnResult:" + str);
        string s = str;
        if (me.pname == s)
        {
            win.gameObject.SetActive(true);
            resultPanel.GetComponent<Image>().raycastTarget = true;
        }
        else
        {
            lost.gameObject.SetActive(true);
            resultPanel.GetComponent<Image>().raycastTarget = true;
        }
    }

    private void OnSetPlane(string str)
    {
        string[] split = str.Split('_');
        int type = int.Parse(split[0]);  //状态
        int y = int.Parse(split[1]);
        int x = int.Parse(split[2]);

        Transform t = battleField2.transform.Find(y + "_" + x);
        t.GetComponent<Check2>().buttonType = (ButtonType)type;
    }

    private void OnLeave(string str)
    {
        me.win++;
        me.SetId();
        opponent.lost++;
        opponent.SetId();
        win.gameObject.SetActive(true);
    }

    private void OnGameStart(string str)
    {
        myName = p1ID.text.ToString();
        cover.raycastTarget = true;
        waitPanel.gameObject.SetActive(false);
        isStart = true;
        battleField2.enabled = true;
        btnPVP.interactable = false;
        string[] s = str.Split('_');
        me.type = (PlayerTurn)int.Parse(s[0]);
        opponent.type = 3 - me.type;
        opponent.pname = s[1];
        opponent.SetId();
        opponent.id.text = "对手：" + s[1];

        for (int j = 0; j < 100; j++)//同步表
        {
            if (battleField1.buttonTypes[j] == ButtonType.Null)
            {
                continue;
            }
            if (battleField1.buttonTypes[j] == ButtonType.Plane)
            {
                NetManager.Send("SetPlane|" + 1 + "_" + battleField1.transform.GetChild(j).name);
                System.Threading.Thread.Sleep(100);
            }
            if (battleField1.buttonTypes[j] == ButtonType.PlaneHead)
            {
                NetManager.Send("SetPlane|" + 2 + "_" + battleField1.transform.GetChild(j).name);
                System.Threading.Thread.Sleep(100);
            }
        }
    }

    private void OnPlay(string str)
    {
        battleField2.enabled = false;
        string[] s = str.Split('_');
        string name = battleField2.transform.Find(s[0] + "_" + s[1]).GetComponent<Check2>().buttonType.ToString();
        battleField2.SetBattleField(s[0] + "_" + s[1], name, p1ID.text.ToString(), s[2]);
        playerTurn = (PlayerTurn)(3 - int.Parse(s[3]));
        hint.gameObject.SetActive(me.type == playerTurn);
    }

    private void OnEnter(string str)
    {
        me.SetId();
        startGamePanel.gameObject.SetActive(false);
        bgChoice.gameObject.SetActive(true);
        player1Panel.gameObject.SetActive(true);
        player2Panel.gameObject.SetActive(true);
    }

    private void Init()
    {
        #region 变量赋值

        startGamePanel = transform.Find("StartGamePanel").GetComponent<Image>();
        inputText = startGamePanel.transform.Find("InputField").GetComponent<InputField>();
        btnStart = startGamePanel.transform.Find("BtnStart").GetComponent<Button>();
        btnQuit = startGamePanel.transform.Find("BtnQuit").GetComponent<Button>();
        btnQuitGame = transform.Find("BtnQuitGame").GetComponent<Button>();
        bgChoice = transform.Find("BGChoice").GetComponent<Image>();
        player1 = transform.Find("Player1");
        player1Panel = player1.Find("Player1Panel").GetComponent<Image>();
        p1ID = player1Panel.transform.Find("ID").GetComponent<Text>();
        p1Score = player1Panel.transform.Find("Score").GetComponent<Text>();
        player2 = transform.Find("Player2");
        player2Panel = player2.Find("Player2Panel").GetComponent<Image>();
        p2ID = player2Panel.transform.Find("ID").GetComponent<Text>();
        p2Score = player2Panel.transform.Find("Score").GetComponent<Text>();
        btnPVE = bgChoice.transform.Find("BtnPVE").GetComponent<Button>();
        btnPVP = bgChoice.transform.Find("BtnPVP").GetComponent<Button>();
        sorryPanel = btnPVE.transform.Find("SorryPanel").GetComponent<Image>();
        waitPanel = btnPVP.transform.Find("WaitPanel").GetComponent<Image>();
        errorPanel = btnPVP.transform.Find("ErrorPanel").GetComponent<Image>();
        btnCloseSorry = sorryPanel.GetComponentInChildren<Button>();
        hint = transform.Find("Hint");
        cover = transform.Find("Cover").GetComponent<Image>();
        resultPanel = transform.Find("ResultPanel");
        win = resultPanel.transform.Find("Win").GetComponent<Image>();
        lost = resultPanel.transform.Find("Lost").GetComponent<Image>();
        winText = win.transform.Find("Text").GetComponent<Text>();
        lostText = lost.transform.Find("Text").GetComponent<Text>();


        battleField1 = player1.Find("BattleField1").GetComponent<BattleField>();
        battleField2 = player2.Find("BattleField2").GetComponent<BattleField2>();

        me = new Player(p1ID, p1Score);
        opponent = new Player(p2ID, p2Score);

        #endregion
        btnStart.onClick.AddListener(delegate
        {
            if (inputText.text == "")
            {
                return;
            }
            me.pname = inputText.text;
            NetManager.Send("EnterGame|" + me.pname);
        });
        btnQuit.onClick.AddListener(delegate
        {
            Application.Quit();
        });
        btnQuitGame.onClick.AddListener(delegate
        {
            Application.Quit();
        });
        btnPVE.onClick.AddListener(delegate
        {
            sorryPanel.gameObject.SetActive(true);
        });
        btnPVP.onClick.AddListener(delegate
        {
            nullCount = 0;
            GetBattleField1();
        });
        btnCloseSorry.onClick.AddListener(delegate
        {
            sorryPanel.gameObject.SetActive(false);
        });

    }

    public void GetBattleField1()
    {
        battleField1 = GameObject.Find("BattleField1").GetComponent<BattleField>();
        battleField2 = GameObject.Find("BattleField2").GetComponent<BattleField2>();
        for (int i = 0; i < battleField1.buttonTypes.Count; i++)
        {
            if (battleField1.buttonTypes[i] == ButtonType.Null)
            {
                nullCount++;
            }
        }
        if (nullCount != 70)
        {
            errorPanel.gameObject.SetActive(true);
            Invoke("SetErrorPanel", 3);
        }
        else
        {
            waitPanel.gameObject.SetActive(true);
            NetManager.Send("GameStart|");
        }
    }

    void SetErrorPanel()
    {
        errorPanel.gameObject.SetActive(false);
    }


    void Update()
    {
        NetManager.Update();
    }

    public static bool MayPlay()
    {
        return playerTurn == me.type;
    }

    public static void BlowUpPlaneHead()
    {
        winNum++;
        if (winNum == 3)
        {
            print("发送了");
            NetManager.Send("Result|" + me.pname);
        }
    }
}
