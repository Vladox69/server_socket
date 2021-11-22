using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace servidor_framework
{
    class Program
    {
        static void Main(string[] args)
        {
            string ip = Properties.Settings.Default.ip;
            int puerto = Properties.Settings.Default.port;
            Servidor s = new Servidor(ip, puerto);
            s.Start();
        }
    }
}
