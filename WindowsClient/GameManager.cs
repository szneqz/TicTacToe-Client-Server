using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace WindowsClient
{
    static class GameManager
    {
        private static string nickname;
        private static string ip_addr;
        private static int port = 8888;
        private static Socket fd;
        private static mainForm form1;
        private static byte[] fdID;

        public static string Nickname
        {
            get { return nickname; }
            set { nickname = value; }
        }

        public static string IP_addr
        {
            get { return ip_addr; }
            set { ip_addr = value; }
        }

        public static byte[] FdID
        {
            get { return fdID; }
            set { fdID = value; }
        }

        public static void init(mainForm mainF, string address)
        {
            try
            {
                IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse(address), port);
                fd = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                form1 = mainF;
                fd.Connect(remoteEP);
                //byte[] msg = Encoding.ASCII.GetBytes("This is a test<EOF>");
                byte[] data = new byte[128];
                fd.Receive(data, sizeof(byte) * 128, SocketFlags.None);
                //String text = System.Text.Encoding.UTF8.GetString(data).TrimEnd('\0');
                //System.Console.WriteLine(text);
                mainF.proceed();
            }
            catch(System.Net.Sockets.SocketException ex)
            {
                closeFd();
                mainF.setErrorText("Nie można połączyć się z serwerem!");
            }
            catch(System.FormatException ex)
            {
                mainF.setErrorText("Nieprawidłowy adres IP!");
            }
        }

        public static byte[] sendData(byte[] msg)
        {
            int bytesSent = fd.Send(msg);
            byte[] data = new byte[1024];
            fd.Receive(data, sizeof(byte) * 1024, SocketFlags.None);
            return data;
        }

        public static void closeFd()
        {
            try
            {
                fd.Shutdown(SocketShutdown.Both);
                fd.Close();
            }
            catch(System.Net.Sockets.SocketException ex)
            {

            }
        }

    }
}
