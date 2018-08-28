using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using Maple;

namespace IoRCT
{
    public class Program
    {
        // create output port object for onboard LED
        static OutputPort led;

        // create an empty DS1803 object
        static DS1803 remote;

        public static int pulseLed(int count)
        {
            for (int i = 1; i <= count; i++)
            {
                led.Write(true);
                Thread.Sleep(250);
                led.Write(false);
                Thread.Sleep(150);
            }

            return count;
        }


        public static void Main()
        {
            // initialize and turn LED on during initialization
            led = new OutputPort(Pins.ONBOARD_LED, true);

            // initialize DS1803 object with tweaked parameters for current car
            remote = new DS1803(10, 0, 20, 11, 1, 23);

            // set both pots to neutral
            remote.centerPots();

            // TODO: can't seem to get this example to work regardless of using directives/references - so going w/ this hack for now!
            //while (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            while (Microsoft.SPOT.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces()[0].IPAddress == "0.0.0.0")
            {
                Debug.Print("Waiting for network...");
                Thread.Sleep(10);
            };
            Debug.Print("IP: " + Microsoft.SPOT.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces()[0].IPAddress);

            MapleServer server = new MapleServer();
            server.Start();
            Debug.Print("Maple serving on http://" + Microsoft.SPOT.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces()[0].IPAddress);

            // quick triple LED pulse to show we're starting
            pulseLed(3);
            // wait one second afterwards
            Thread.Sleep(1000);


            /*
            // sweeps

            // steering center to right
            Debug.Print("C->R");
            remote.sweepPot(remote.steeringPot, remote.steeringCenter, remote.steeringRight);
            // steering right to left
            Debug.Print("R->L");
            remote.sweepPot(remote.steeringPot, remote.steeringRight, remote.steeringLeft);
            // steering left to right
            Debug.Print("L->R");
            remote.sweepPot(remote.steeringPot, remote.steeringLeft, remote.steeringRight);
            // steering back to center
            Debug.Print("R->C");
            remote.sweepPot(remote.steeringPot, remote.steeringRight, remote.steeringCenter);


            // throttle neutral to max fwd
            Debug.Print("N->MF");
            remote.sweepPot(remote.throttlePot, remote.throttleCenter, remote.throttleMax);
            // throttle max fwd to neutral
            Debug.Print("MF->N");
            remote.sweepPot(remote.throttlePot, remote.throttleMax, remote.throttleCenter);
            // throttle neutral to max reverse
            Debug.Print("N->MR");
            remote.sweepPot(remote.throttlePot, remote.throttleCenter, remote.throttleReverseMax);
            // throttle max reverse back to neutral
            Debug.Print("MR->N");
            remote.sweepPot(remote.throttlePot, remote.throttleReverseMax, remote.throttleCenter);



            // slow figure 8

            Debug.Print("C/C");
            remote.centerPots();


            // circle right
            Debug.Print("CR");
            remote.setPot(remote.steeringPot, remote.steeringRight);

            // speed up to half throttle while turning
            remote.sweepPot(remote.throttlePot, remote.throttleCenter, (remote.throttleCenter + remote.throttleMax) / 2);
            

            // circle left
            Debug.Print("CL");
            remote.setPot(remote.steeringPot, remote.steeringLeft);

            // slow down to a stop while turning
            remote.sweepPot(remote.throttlePot, remote.throttleCenter, ((remote.throttleCenter + remote.throttleMax) / 2 + remote.throttleCenter) / 2);



            // set both to neutral
            Debug.Print("STOP");
            remote.centerPots();
            
            
            */
            // flash LED to show it's over
            while (true)
            {
                //pulseLed(1);
            }
        }
    }
}
