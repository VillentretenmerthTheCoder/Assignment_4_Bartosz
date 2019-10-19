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
            try
            {
                Console.WriteLine("Hello!");
                TcpListener ServerSocket = new TcpListener(IPAddress.Loopback, 4646);
                ServerSocket.Start();
                Console.WriteLine("Server has started. Waiting for connection...");
                
            }
            catch
            {

            }
        }



    }
}
