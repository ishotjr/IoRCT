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
            // for some reason, MA-010 throttle endpoints are inverted...?
            remote = new DS1803(10, 0, 20, 11, 23, 1, 100);

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
        public void sweepSteeringPotLeftToCenter()
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
            sweepSteeringPotLeftToCenter();
        }
        

        public void sweepThrottlePotCenterToMaxForward()
        {
            remote.sweepPot(remote.throttlePot, remote.throttleCenter, remote.throttleMax);
        }
        public void sweepThrottlePotMaxForwardToCenter()
        {
            remote.sweepPot(remote.throttlePot, remote.throttleMax, remote.throttleCenter);
        }
        public void sweepThrottlePotCenterToMaxReverse()
        {
            remote.sweepPot(remote.throttlePot, remote.throttleCenter, remote.throttleReverseMax);
        }
        public void sweepThrottlePotMaxReverseToCenter()
        {
            remote.sweepPot(remote.throttlePot, remote.throttleReverseMax, remote.throttleCenter);
        }

        public void sweepThrottlePotCenterToMidForward()
        {
            remote.sweepPot(remote.throttlePot, remote.throttleCenter, (remote.throttleCenter + remote.throttleMax) / 2);
        }
        public void sweepThrottlePotMidForwardToCenter()
        {
            remote.sweepPot(remote.throttlePot, (remote.throttleCenter + remote.throttleMax) / 2, remote.throttleCenter);
        }
        public void sweepThrottlePotCenterToMidReverse()
        {
            remote.sweepPot(remote.throttlePot, remote.throttleCenter, (remote.throttleCenter + remote.throttleReverseMax) / 2);
        }
        public void sweepThrottlePotMidReverseToCenter()
        {
            remote.sweepPot(remote.throttlePot, (remote.throttleCenter + remote.throttleReverseMax) / 2, remote.throttleCenter);
        }


        public void circleRight()
        {
            remote.setPot(remote.steeringPot, remote.steeringRight);
            remote.sweepPot(remote.throttlePot, remote.throttleCenter, remote.throttleMax);
        }
        public void circleLeft()
        {
            remote.setPot(remote.steeringPot, remote.steeringLeft);
            remote.sweepPot(remote.throttlePot, remote.throttleCenter, remote.throttleMax);
        }


        public void stop()
        {
            remote.centerPots();
        }

    }
}
