using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace servidor_framework
{
    class Servidor
    {
        IPHostEntry host;
        IPAddress ipAddr;
        IPEndPoint endPoint;

        Socket s_Server;
        Socket s_Client;

        public Servidor(string ip, int port)
        {
            host = Dns.GetHostEntry(ip);
            ipAddr = host.AddressList[0];
            endPoint = new IPEndPoint(ipAddr, port);

            s_Server = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            s_Server.Bind(endPoint);
            s_Server.Listen(10);
        }

        public void Start()
        {
            byte[] buffer;
            string message;
            s_Client = s_Server.Accept();

            while (true)
            {
                buffer = new byte[1024];
                s_Client.Receive(buffer);
                message = byte2string(buffer);

                Console.WriteLine("Se recivio el mensaje: " + message);
                Console.WriteLine(message.Length);

                byte[] response = Encoding.ASCII.GetBytes("Listo");
                s_Client.Send(response);

                byte[] question = Encoding.ASCII.GetBytes("Desea enviar mas datos?(S/N):");
                s_Client.Send(question);

                int[] conteo = conteoCaracteres(message);

                Console.WriteLine("Palabras:" + conteo[0]);
                Console.WriteLine("Vocales:" + conteo[1]);
                Console.WriteLine("Letras:" + conteo[2]);
                Console.WriteLine("Consantes:" + conteo[3]);
            }

        }

        public string byte2string(byte[] buffer)
        {
            string message;
            int endIndex;

            message = Encoding.ASCII.GetString(buffer);
            endIndex = message.IndexOf('\0');
            if (endIndex > 0)
            {
                message = message.Substring(0, endIndex);
            }
            return message;
        }

        public int[] conteoCaracteres(string message)
        {
            int[] valores = new int[4];
            valores[0] = contarPalabras(message);
            valores[1] = contarVocales(message);
            valores[2] = contarLetras(message);
            valores[3] = contarConsonantes(message);
            return valores;
        }

        public int contarPalabras(string message)
        {
            int punto = 0;
            MatchCollection palabras = Regex.Matches(message, @"[\W]+");
            if (message[message.Length - 1] == '.')
            {
                punto++;
            }
            return palabras.Count + 1 - punto;
        }

        public int contarVocales(string message)
        {

            return Regex.Matches(message, @"[AEIOUaeiou]").Count;
        }

        public int contarLetras(string message)
        {

            return Regex.Matches(message, @"[a-z-A-Z]").Count;
        }

        public int contarConsonantes(string message)
        {
            int consonantes = contarLetras(message) - contarVocales(message);
            if (consonantes < 0)
            {
                consonantes = consonantes * -1;
            }
            return consonantes;
        }

    }
}
