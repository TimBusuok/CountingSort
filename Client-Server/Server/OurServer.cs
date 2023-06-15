using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Server
{


    class OurServer
    {
        private TcpListener server;

        public OurServer()
        {
            server = new TcpListener(IPAddress.Parse("127.0.0.1"),5555);
            server.Start();

            LoopClients();
        }

        void LoopClients()
        {
            while(true)
            {
                TcpClient client = server.AcceptTcpClient();

                Thread thread = new Thread(() => HadleClient(client));
                thread.Start(); 
            }


            void HadleClient(TcpClient client)
            {
                StreamReader reader = new StreamReader(client.GetStream(), Encoding.UTF8);
                StreamWriter writer = new StreamWriter(client.GetStream(), Encoding.UTF8);

                while (true)
                {
                    string message = reader.ReadLine();
                    Console.WriteLine($"Клиент написал -- {message}");
                    

                    Console.WriteLine("Дайте сообщение клиенту: ");
                    string answer = Console.ReadLine();
                    writer.WriteLine(answer);
                    writer.Flush();
                }
            }
        }
    }
}