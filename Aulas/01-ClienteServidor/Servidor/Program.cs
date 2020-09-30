using System;
using System.Net;
using System.Net.Sockets;

namespace Servidor
{
    class Program
    {
        //Objetos para criar uma porta
        object madeira, maçaneta, furadeira, martelo, pregos, tranca;
        const int porta = 7000;
        static void Main(string[] args)
        {
            TcpListener servidor = new TcpListener(IPAddress.Any,porta);
            try
            {
                Console.WriteLine("Servidor no ar");
                servidor.Start();
            }
            catch(Exception)
            {
                Console.WriteLine("Você é um fracasso");
            }
          
            while(true){
                TcpClient cliente = servidor.AcceptTcpClient();
                Console.WriteLine("Conexão estabelecida");

                byte[] bytes = new byte[1024];
                NetworkStream stream = cliente.GetStream();

                while(true)
                {
                    int bytesRead = stream.Read(bytes, 0, bytes.Length);

                    if(bytesRead == 0)
                    {
                        break;
                    }
                    Console.WriteLine("Mensagem Recebida: {0}",Encoding.ASCII.GetString(bytes, 0,bytes.Length));
                }
                cliente.Close();
                Console.WriteLine("Conexão encerrada!");
            }
        }
    }
}
