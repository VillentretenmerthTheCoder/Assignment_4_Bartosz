using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Assignment_1_Bartosz;

namespace Assignment_4_Bartosz
{
    class Server
    {
        static List<Book> books = new List<Book>()
        {
             new Book("Wiedzmin","Andrzej Sapkowski",456,"1234567891011"),
            new Book("Game of Thrones","J.R.R.Martin",650,"1213141516171"),
            new Book("How to do stuff","Orwell Adam",666,"8192021232425"),
            new Book("Greta Van Fleet","Adam Sanders",250,"2627282930313"),
            new Book("Led Zappelin","Jon Jonsen",789,"3233343537637")
        };

        public void ServerStart()
        {
            TcpListener ServerSocket = null;
            try
            {
                int clientNumber = 0;
                Console.WriteLine("Hello!");
                ServerSocket = new TcpListener(IPAddress.Loopback, 4646);
                ServerSocket.Start();
                while(true)
                { 
                    Console.WriteLine("Server has started. Waiting for connection...");
                    TcpClient Client = ServerSocket.AcceptTcpClient();
                    Console.WriteLine("Server Connected!");
                    Task.Run(() => StreamHandling(Client, ref clientNumber));
                }
            }
            catch(SocketException e)
            {
                Console.WriteLine($"Socket Exception: {e}");
            }
            finally
            {
                ServerSocket.Stop();
            }
            Console.WriteLine("Hit any button to continue...");
            Console.Read();
        }



        public void StreamHandling(TcpClient client, ref int clientNumber)
        {
            Byte[] bytes = new byte[256];
            String data = null;
            clientNumber++;

            NetworkStream stream = client.GetStream();
            int i;
            while(( i = stream.Read(bytes,0,bytes.Length))!=0)
            {
                data = Encoding.UTF8.GetString(bytes, 0, i).Trim();
                Console.WriteLine($"Received: {data} from client {clientNumber}");

                string message = "Not a valid command. Try again!";
                string[] words = data.ToLower().Split();
                if(words[0]=="getall")
                {
                    message = JsonConvert.SerializeObject(books);
                }
                if(words[0]=="get")
                {
                    message = JsonConvert.SerializeObject(books.Find(e => e.Isbn13 == words[1]));
                }
                if(words[0]=="save")
                {
                    string myjson = data.Split("{")[1].Split("}")[0];
                    myjson = "{" + myjson + "}";
                    books.Add(JsonConvert.DeserializeObject<Book>(myjson));
                    message = "";
                }

                byte[] msge = Encoding.ASCII.GetBytes(message);
                Thread.Sleep(1000);
                stream.Write(msge, 0, msge.Length);
                Console.WriteLine($"Sent: {message}");
            }

            client.Close();
            clientNumber--;

        }





    }
}
