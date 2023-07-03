using System;

namespace BombPlaneServer
{
    internal class MsgHandle
    {
        public static void MsgEnterGame(ClientState c, string msgArgs)
        {
            c.player.pname = msgArgs;
            string sendStr = "EnterGame|" + c.socket.LocalEndPoint;
            Console.WriteLine("玩家" + msgArgs + "上线");
            Program.Send(c, sendStr);
        }

        public static void MsgGameStart(ClientState c, string msgArgs)
        {
            c.player.isPlayer = true;
            if (Program.clients.Count < 2)
            {
                return;
            }
            ClientState opp = Program.GetOpp(c);
            if (!opp.player.isPlayer)
            {
                return;
            }
            c.player.turn = 3 - c.player.turn;
            opp.player.turn = 3 - c.player.turn;
            foreach (ClientState item in Program.clients.Values)
            {
                ClientState other = Program.GetOpp(item);
                Program.Send(item, "GameStart|" + (int)item.player.turn + "_" + other.player.pname);
            }
        }

        public static void MsgList(ClientState c, string msgArgs)
        {
            string sendStr = "List|";
            foreach (ClientState cs in Program.clients.Values)
            {
                sendStr += cs.socket.RemoteEndPoint.ToString();
            }
            Program.Send(c, sendStr);
        }

        public static void MsgPlay(ClientState c, string msgArgs)
        {
            string sendStr = "Play|" + msgArgs + "_" + (int)c.player.turn;
            ClientState other = Program.GetOpp(c);
            Program.Send(c, sendStr);
            Program.Send(other, sendStr);
            Console.WriteLine("发送：" + sendStr);
            Console.WriteLine("___________");
        }

        public static void MsgSetPlane(ClientState c, string msgArgs)
        {
            string sendStr = "SetPlane|" + msgArgs;
            ClientState other = Program.GetOpp(c);
            Program.Send(other, sendStr);
            Console.WriteLine("发送：" + sendStr);
            Console.WriteLine("___________");
        }

        public static void MsgResult(ClientState c, string msgArgs)
        {
            string sendStr = "Result|" + msgArgs;
            ClientState other = Program.GetOpp(c);
            if (msgArgs == c.player.pname.ToString())
            {
                Console.WriteLine(msgArgs + "胜利了！");
            }
            Program.Send(c, sendStr);
            Program.Send(other, sendStr);
            Console.WriteLine("发送：" + sendStr);
            Console.WriteLine("___________");
        }
    }
}