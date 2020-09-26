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

                cliente.Close();
                Console.WriteLine("Conexão encerrada!");
            }
        }
    }
}
