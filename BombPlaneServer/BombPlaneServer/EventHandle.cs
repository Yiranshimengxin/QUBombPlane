﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombPlaneServer
{
    internal class EventHandle
    {
        public static void OnDisconnect(ClientState cs)
        {
            Console.WriteLine("下线");
            string desc = cs.socket.RemoteEndPoint.ToString();
            string sendStr = "Leave|" + desc;
            ClientState c = Program.GetOpp(cs);
            Program.Send(c, sendStr);
        }
    }
}
