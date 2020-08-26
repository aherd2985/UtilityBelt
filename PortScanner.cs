using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace UtilityBelt
{
    public class PortScanner
    {
        private const int PORT_MIN_VALUE = 1;
        private const int PORT_MAX_VALUE = 65535;

        public static void Scanner(string host, int minPort = PORT_MIN_VALUE, int maxPort = PORT_MAX_VALUE)
        {
            if (minPort > maxPort)
                throw new ArgumentException("Min port cannot be greater than max port");

            if (minPort < PORT_MIN_VALUE || minPort > PORT_MAX_VALUE)
                throw new ArgumentOutOfRangeException(
                    $"Min port cannot be less than {PORT_MIN_VALUE} " +
                    $"or greater than {PORT_MAX_VALUE}");

            if (maxPort < PORT_MIN_VALUE || maxPort > PORT_MAX_VALUE)
                throw new ArgumentOutOfRangeException(
                    $"Max port cannot be less than {PORT_MIN_VALUE} " +
                    $"or greater than {PORT_MAX_VALUE}");


            List<int> portList = Enumerable.Range(minPort, maxPort - minPort).ToList();
            List<int> openPorts = new List<int>();
            List<int> closedPorts = new List<int>();
            
            foreach (int portNbr in portList)
            {
                if(IsPortOpenAsync(host, portNbr)){
                    openPorts.Add(portNbr);
                    Console.WriteLine(string.Format("Port Nbr {0} is OPEN", portNbr));
                }
                else
                    closedPorts.Add(portNbr);
            }
            

        }

        private static bool IsPortOpenAsync(string host, int port)
        {
            Socket socket = null;
            
            try
            {
                // make a TCP based socket
                socket = new Socket(AddressFamily.InterNetwork, SocketType
                    .Stream, ProtocolType.Tcp);

                // connect
                bool connected = false;
                Task result = socket.ConnectAsync(host, port);
                int index = Task.WaitAny(new[] { result }, 100 );
                connected = socket.Connected;
                socket.Close();
                if (!connected)
                    return false;
                else
                    return true;
            }
            catch (SocketException ex)
            {
                if (ex.SocketErrorCode == SocketError.ConnectionRefused)
                {
                    return false;
                }

                //An error occurred when attempting to access the socket
                Console.WriteLine(ex);
            }
            finally
            {
                if (socket?.Connected ?? false)
                {
                    socket?.Disconnect(false);
                }
                socket?.Close();
            }

            return false;
        }


    }
}
