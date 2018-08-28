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

            // TODO: sweepPot(byte pot, int from, int to)
            //handler.SweepPot += (s, e) => { remoteController.sweepPot(); };

            handler.SweepSteeringPotCenterToRight += (s, e) => { remoteController.sweepSteeringPotCenterToRight(); };
            handler.SweepSteeringPotCenterToLeft += (s, e) => { remoteController.sweepSteeringPotCenterToLeft(); };
            handler.SweepSteeringPotRightToLeft += (s, e) => { remoteController.sweepSteeringPotRightToLeft(); };
            handler.SweepSteeringPotLeftToRight += (s, e) => { remoteController.sweepSteeringPotLeftToRight(); };
            handler.SweepSteeringPotRightToCenter += (s, e) => { remoteController.sweepSteeringPotRightToCenter(); };
            handler.SweepSteeringPotLeftToCenter += (s, e) => { remoteController.sweepSteeringPotLeftToCenter(); };
            handler.SweepSteeringPotTest += (s, e) => { remoteController.sweepSteeringPotTest(); };

            handler.SweepThrottlePotCenterToMaxForward += (s, e) => { remoteController.sweepThrottlePotCenterToMaxForward(); };
            handler.SweepThrottlePotMaxForwardToCenter += (s, e) => { remoteController.sweepThrottlePotMaxForwardToCenter(); };
            handler.SweepThrottlePotCenterToMaxReverse += (s, e) => { remoteController.sweepThrottlePotCenterToMaxReverse(); };
            handler.SweepThrottlePotMaxReverseToCenter += (s, e) => { remoteController.sweepThrottlePotMaxReverseToCenter(); };
            handler.SweepThrottlePotCenterToMidForward += (s, e) => { remoteController.sweepThrottlePotCenterToMidForward(); };
            handler.SweepThrottlePotMidForwardToCenter += (s, e) => { remoteController.sweepThrottlePotMidForwardToCenter(); };
            handler.SweepThrottlePotCenterToMidReverse += (s, e) => { remoteController.sweepThrottlePotCenterToMidReverse(); };
            handler.SweepThrottlePotMidReverseToCenter += (s, e) => { remoteController.sweepThrottlePotMidReverseToCenter(); };

            handler.CircleRight += (s, e) => { remoteController.circleRight(); };
            handler.CircleLeft += (s, e) => { remoteController.circleLeft(); };

            handler.Stop += (s, e) => { remoteController.stop(); };

            maple = new MapleServer();
            maple.AddHandler(handler);
            maple.Start();
            Debug.Print("Maple serving on http://" + Microsoft.SPOT.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces()[0].IPAddress);
            
        }
    }
}
