using System.Net.Sockets;
using System.Text;



namespace Client
{

    class OurClient
    {
        private TcpClient client;

        private StreamWriter writer;

        private StreamReader reader;

        public OurClient()
        {
            client = new TcpClient("127.0.0.1",5555);
            writer = new StreamWriter(client.GetStream(), Encoding.UTF8);
            reader = new StreamReader(client.GetStream(), Encoding.UTF8);

            HadleCommunication();
        }

        void HadleCommunication()
        {
            while(true)
            {
                Console.WriteLine("> ");
                string message = Console.ReadLine();
                writer.WriteLine(message);
                writer.Flush();

                string answerServer = reader.ReadLine();
                Console.WriteLine($"Сервер ответил -> {answerServer}");
            }
        }
    }

}