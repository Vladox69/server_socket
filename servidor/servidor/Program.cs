using System;

namespace servidor
{
    class Program
    {
        static void Main(string[] args)
        {
            string ip = Properties.Resources.ip;
            int puerto = int.Parse(Properties.Resources.port);
            Server s = new Server(ip, puerto);
            s.Start();
        }
    }
}
