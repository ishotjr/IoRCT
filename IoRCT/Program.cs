using System;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace IoRCT
{
    public class Program
    {
        const byte steeringPot = 0; // no address offset for steering
        const byte throttlePot = 1; // +1 address offset for throttle
        const byte bothPots = 6;    // +6 address offset for both

        // * these may need adjusting per car/KT-18 so may need to externalize; use KT-18 adjustments first though
        const byte steeringCenter = 10;
        const byte steeringRight = 0;
        const byte steeringLeft = 20;

        const byte throttleCenter = 11;
        const byte throttleReverseMax = 1;
        const byte throttleMax = 23;

        const int delay = 1000; // (ms)

        // create an empty I2C device object
        static I2CDevice ds1803 = new I2CDevice(new I2CDevice.Configuration(0, 0));

        // create output port object for onboard LED
        static OutputPort led;


        // pot = 0 for wiper 0, 1 for wiper 1, *6* for both
        public static int setPot(byte pot, byte value)
        {
            // DS1803 slave addr is set to 01010000 - lsb is r/w bit  (0 W / 1 R) = 0x50 (80)
            // see also data sheet: https://datasheets.maximintegrated.com/en/ds/DS1803.pdf

            // but we drop that last bit, so = 0101111 = 0x28 (40)
            ds1803.Config = new I2CDevice.Configuration(0x28, 50); // SCL Clock Frequency fSCL 0-400 kHz


            int bytesTransferred = 0;

            byte baseInstruction = 0xA9; // 10101001 0xA9 (169) = wiper 0; 10101010 0xAA (170) = wiper 1; 10101111 0xAF (175) = BOTH

            I2CDevice.I2CTransaction[] bytesToSend = new I2CDevice.I2CTransaction[]
                {
                               I2CDevice.CreateWriteTransaction(new byte[] { (byte)(baseInstruction + pot), value })
                };

            bytesTransferred = ds1803.Execute(bytesToSend, 5000);

            Debug.Print("[setPot|instruction:" + (byte)(baseInstruction + pot) + "|data:" + value + "|s/x:" + bytesToSend.Length + "/" + bytesTransferred + "]");

            return bytesTransferred;
        }

        public static int sweepPot(byte pot, int from, int to)
        {
            if (to > from)
            {
                for (int i = from; i <= to; i++)
                {
                    setPot(pot, (byte)i);
                    Thread.Sleep(delay);
                }
            }
            else
            {
                for (int i = from; i >= to; i--)
                {
                    setPot(pot, (byte)i);
                    Thread.Sleep(delay);
                }
            }

            return (to - from);
        }

        public static byte centerPots()
        {
            setPot(steeringPot, steeringCenter);
            setPot(throttlePot, throttleCenter);

            return 0;
        }


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
            // initialize and turn LED off
            led = new OutputPort(Pins.ONBOARD_LED, false);


            // set both pots to neutral
            centerPots();


            // quick triple LED pulse to show we're starting
            pulseLed(3);
            // wait one second afterwards
            Thread.Sleep(1000);

            

            // sweeps

            // steering center to right
            Debug.Print("C->R");
            sweepPot(steeringPot, steeringCenter, steeringRight);
            // steering right to left
            Debug.Print("R->L");
            sweepPot(steeringPot, steeringRight, steeringLeft);
            // steering left to right
            Debug.Print("L->R");
            sweepPot(steeringPot, steeringLeft, steeringRight);
            // steering back to center
            Debug.Print("R->C");
            sweepPot(steeringPot, steeringRight, steeringCenter);


            // throttle neutral to max fwd
            Debug.Print("N->MF");
            sweepPot(throttlePot, throttleCenter, throttleMax);
            // throttle max fwd to neutral
            Debug.Print("MF->N");
            sweepPot(throttlePot, throttleMax, throttleCenter);
            // throttle neutral to max reverse
            Debug.Print("N->MR");
            sweepPot(throttlePot, throttleCenter, throttleReverseMax);
            // throttle max reverse back to neutral
            Debug.Print("MR->N");
            sweepPot(throttlePot, throttleReverseMax, throttleCenter);



            // slow figure 8

            Debug.Print("C/C");
            centerPots();
            Thread.Sleep(delay);


            // circle right
            Debug.Print("CR");
            setPot(steeringPot, steeringRight);

            // speed up to half throttle while turning
            sweepPot(throttlePot, throttleCenter, (throttleCenter + throttleMax) / 2);
            

            // circle left
            Debug.Print("CL");
            setPot(steeringPot, steeringLeft);

            // slow down to a stop while turning
            sweepPot(throttlePot, throttleCenter, ((throttleCenter + throttleMax) / 2 + throttleCenter) / 2);



            // set both to neutral
            Debug.Print("STOP");
            centerPots();


            // flash LED to show it's over
            while (true)
            {
                pulseLed(1);
            }

        }
    }
}
