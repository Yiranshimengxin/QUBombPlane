using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Text id;
    public Text score;
    public string pname;
    public PlayerTurn type { set; get; }
    public int win { set; get; }
    public int lost { set; get; }
    public Socket socket;

    public void SetId()
    {
        id.text = pname;
        score.text = win + "胜" + lost + "负";
    }
    public Player(Text _id, Text _score)
    {
        id = _id;
        score = _score;
    }
}
