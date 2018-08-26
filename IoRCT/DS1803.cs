using System;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace IoRCT
{
    class DS1803
    {
        public byte steeringPot = 0; // no address offset for steering
        public byte throttlePot = 1; // +1 address offset for throttle
        public byte bothPots = 6;    // +6 address offset for both

        // * these may need adjusting per car/KT-18; use KT-18 adjustments first though
        public byte steeringCenter = 12;
        public byte steeringRight = 0;
        public byte steeringLeft = 24;

        public byte throttleCenter = 12;
        public byte throttleReverseMax = 0;
        public byte throttleMax = 24;

        public int delay = 1000; // (ms)

        // create an empty I2C device object
        static I2CDevice ds1803 = new I2CDevice(new I2CDevice.Configuration(0, 0));


        public DS1803(byte adjustedSteeringCenter = 12, byte adjustedSteeringRight = 0, byte adjustedSteeringLeft = 24,
                      byte adjustedThrottleCenter = 12, byte adjustedThrottleReverseMax = 0, byte adjustedThrottleMax = 24,
                      int adjustedDelay = 1000)
        {
            steeringCenter = adjustedSteeringCenter;
            steeringRight = adjustedSteeringRight;
            steeringLeft = adjustedSteeringLeft;

            throttleCenter = adjustedThrottleCenter;
            throttleReverseMax = adjustedThrottleReverseMax;
            throttleMax = adjustedThrottleMax;

            delay = adjustedDelay; // (ms)
        }


        // pot = 0 for wiper 0, 1 for wiper 1, *6* for both
        public int setPot(byte pot, byte value)
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

        public int sweepPot(byte pot, int from, int to)
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

        public byte centerPots()
        {
            setPot(steeringPot, steeringCenter);
            setPot(throttlePot, throttleCenter);

            return 0;
        }

    }
}
