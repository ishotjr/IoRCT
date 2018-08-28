using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace IoRCT
{
    public class DS1803Controller
    {
        
        // create output port object for onboard LED
        protected static OutputPort led;

        // create an empty DS1803 object
        protected static DS1803 remote;


        public DS1803Controller()
        {

            // initialize and turn LED on during initialization
            led = new OutputPort(Pins.ONBOARD_LED, true);

            // initialize DS1803 object with tweaked parameters for current car
            remote = new DS1803(10, 0, 20, 11, 1, 23);

            // set both pots to neutral
            remote.centerPots();



            // quick triple LED pulse to show we're starting
            pulseLed(3);
            // wait one second afterwards
            Thread.Sleep(1000);

        }


        // helper - could be moved elsewhere
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


        // sweeps

        public void sweepPot(byte pot, int from, int to)
        {
            remote.sweepPot(pot, from, to);
        }

        public void sweepSteeringPotCenterToRight()
        {
            remote.sweepPot(remote.steeringPot, remote.steeringCenter, remote.steeringRight);
        }
        public void sweepSteeringPotCenterToLeft()
        {
            remote.sweepPot(remote.steeringPot, remote.steeringCenter, remote.steeringLeft);
        }
        public void sweepSteeringPotRightToLeft()
        {
            remote.sweepPot(remote.steeringPot, remote.steeringRight, remote.steeringLeft);
        }
        public void sweepSteeringPotLeftToRight()
        {
            remote.sweepPot(remote.steeringPot, remote.steeringLeft, remote.steeringRight);
        }
        public void sweepSteeringPotRightToCenter()
        {
            remote.sweepPot(remote.steeringPot, remote.steeringRight, remote.steeringCenter);
        }
        public void sweepSteeringPotLefttToCenter()
        {
            remote.sweepPot(remote.steeringPot, remote.steeringLeft, remote.steeringCenter);
        }

        public void sweepSteeringPotTest()
        {
            sweepSteeringPotCenterToRight();
            sweepSteeringPotRightToCenter();
            sweepSteeringPotCenterToLeft();
            sweepSteeringPotLeftToRight();
            sweepSteeringPotRightToLeft();
            sweepSteeringPotLefttToCenter();
        }

        /*
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

    }
}
