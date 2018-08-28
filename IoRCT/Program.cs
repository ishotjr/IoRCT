using System.Threading;
using Microsoft.SPOT;
using Maple;

namespace IoRCT
{
    public class Program
    {

        protected static MapleServer maple;
        protected static DS1803Controller remoteController;

        public static void Main()
        {

            // TODO: can't seem to get this example to work regardless of using directives/references - so going w/ this hack for now!
            //while (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            while (Microsoft.SPOT.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces()[0].IPAddress == "0.0.0.0")
            {
                Debug.Print("Waiting for network...");
                Thread.Sleep(10);
            };
            Debug.Print("IP: " + Microsoft.SPOT.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces()[0].IPAddress);

            remoteController = new DS1803Controller();

            var handler = new RequestHandler();

            handler.SweepSteeringPotTest += (s, e) => { remoteController.sweepSteeringPotTest(); };

            maple = new MapleServer();
            maple.AddHandler(handler);
            maple.Start();
            Debug.Print("Maple serving on http://" + Microsoft.SPOT.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces()[0].IPAddress);
            
        }
    }
}
