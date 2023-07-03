using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BombPlaneServer
{
    internal class Player
    {
        public Player(Socket s)
        {
            turn = PlayerTurn.Player1;
            socket = s;
        }
        public string pname { set; get; }
        public bool isPlayer { set; get; }
        public PlayerTurn turn { set; get; }
        public int win { set; get; }
        public int lost { set; get; }
        public Socket socket { set; get; }
    }
}
