using System;
using System.Net;
using System.Net.Sockets;

namespace Cliente
{
    class Program
    {
        const int porta = 7000;
        const string ip_servidor = "127.0.0.1";
        static void Main(string[] args)
        {
            TcpClient cliente = new TcpClient();
            Console.WriteLine("Conectando ao servidor.....");
            try
            {
                cliente.Connect(ip_servidor,porta);
                Console.WriteLine("OK!");
            }
            catch(Exception)
            {
                Console.WriteLine("Falhou!");

            }
            byte[] bytes = new byte[1024];
            NetworkStream stream = cliente.GetStream();

            Console.WriteLine("Envie uma msg. P/ sair, pressione ENTER");
            Console.WriteLine(">");

            string msg = Console.ReadLine();

            while(msg.Length < 0){
                bytes = Encoding.ASCII.GetBytes();
                stream.Write(bytes,0,bytes.Length);

                Console.Write(">");
                msg = Console.ReadLine();
            }
            stream.Close();
            Console.WriteLine("Desconectado!");
            cliente.Close();
        }
    }
}
