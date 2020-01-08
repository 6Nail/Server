using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 3231);
            listener.Start();
            Console.WriteLine("Сервер запущен");
            while (true)
            {
                using (var client = listener.AcceptTcpClient())
                {
                    Console.WriteLine("Входящее соединение...");

                    using (var stream = client.GetStream())
                    {

                    }
                }
            }
        }
    }
}