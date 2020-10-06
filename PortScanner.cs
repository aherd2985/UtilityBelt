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

        /// <summary>
        /// Fully scans the hostname for all opened ports in range [minPort; maxPort]
        /// </summary>
        /// <param name="host">Hostname to scan</param>
        /// <param name="minPort">Starting port</param>
        /// <param name="maxPort">Maximal port</param>
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

            List<int> openPorts = new List<int>();

            for (int portNbr = minPort; portNbr <= maxPort; portNbr++)
            {
                if (IsPortOpenAsync(host, portNbr))
                {
                    openPorts.Add(portNbr);
                    Console.WriteLine($"Port {portNbr} is open!");
                }
                else if (portNbr == maxPort - 1)
                    Console.WriteLine($"There are no ports open for {host} between {minPort} and {maxPort}.");
            }
        }

        /// <summary>
        /// Tests if a port is open or not
        /// </summary>
        /// <param name="host">Destination hostname</param>
        /// <param name="port">Destination port</param>
        /// <returns>Whether or not the port is open</returns>
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
