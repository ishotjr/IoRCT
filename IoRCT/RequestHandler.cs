using Maple;
using System.Net;
using System.Collections;
using Microsoft.SPOT;

namespace IoRCT
{
    public class RequestHandler : RequestHandlerBase
    {

        // TODO:
        //public event EventHandler SweepPot = delegate { };

        public event EventHandler SweepSteeringPotCenterToRight = delegate { };
        public event EventHandler SweepSteeringPotCenterToLeft = delegate { };
        public event EventHandler SweepSteeringPotRightToLeft = delegate { };
        public event EventHandler SweepSteeringPotLeftToRight = delegate { };
        public event EventHandler SweepSteeringPotRightToCenter = delegate { };
        public event EventHandler SweepSteeringPotLeftToCenter = delegate { };
        public event EventHandler SweepSteeringPotTest = delegate { };

        public event EventHandler SweepThrottlePotCenterToMaxForward = delegate { };
        public event EventHandler SweepThrottlePotMaxForwardToCenter = delegate { };
        public event EventHandler SweepThrottlePotCenterToMaxReverse = delegate { };
        public event EventHandler SweepThrottlePotMaxReverseToCenter = delegate { };
        public event EventHandler SweepThrottlePotCenterToMidForward = delegate { };
        public event EventHandler SweepThrottlePotMidForwardToCenter = delegate { };
        public event EventHandler SweepThrottlePotCenterToMidReverse = delegate { };
        public event EventHandler SweepThrottlePotMidReverseToCenter = delegate { };


        // example is outdated per https://community.wildernesslabs.co/t/maple-project-error/623/2?u=ishotjr
        //public RequestHandler(HttpListenerContext context) : base(context)

        // turns out this is wrong too:
        /*
        public RequestHandler(HttpListenerContext context)
        {
            this.Context = context;
        }
        */

        // finally, the solution to everything, via
        // https://github.com/WildernessLabs/Netduino_Samples/blob/master/Connected_RgbLed/RgbLedHost/src/RgbLedHost/RgbLedHost/RequestHandler.cs#L14
        public RequestHandler() { }


        // TODO: really these should be PUT/PATCH, but using GET for now since it's easier to work w/ during prototyping
        // *oh, also Maple only supports GET and POST?

        // TODO: wanted this to return for / but reflection doesn't seem to work for as expected for ""?
        //public void get()
        public void getHome()
        {

            Context.Response.ContentType = "text/html";
            Context.Response.StatusCode = (int)HttpStatusCode.OK;

            string message = "<!doctype html>";
            message += "<html>";
            message += "<head>";
            message += "  <meta charset=\"utf-8\">";
            message += "  <title>IoRCT</title>";
            message += "</head>";
            message += "";
            message += "<body>";
            message += "";
            message += "  <h1>IoRCT</h1>";
            message += "  <h2><a href=\"/Steering\">/Steering</href></h2>";
            message += "  <h2><a href=\"/Throttle\">/Throttle</href></h2>";
            message += "";
            message += "</body>";
            message += "";
            message += "</html>";

            this.Send(message);

        }

        // had to break into multiple pages since combined single page caused Netduino.IP to crash?!

        public void getSteering()
        {

            Context.Response.ContentType = "text/html";
            Context.Response.StatusCode = (int)HttpStatusCode.OK;

            string message = "<!doctype html>";
            message += "<html>";
            message += "<head>";
            message += "  <meta charset=\"utf-8\">";
            message += "  <title>IoRCT</title>";
            message += "</head>";
            message += "";
            message += "<body>";
            message += "";
            message += "  <h1>IoRCT</h1>";
            message += "  <h2>Steering</h2>";
            message += "  <p><a href=\"/SweepSteeringPotTest\">/SweepSteeringPotTest</href></p>";
            message += "  <p><a href=\"/SweepSteeringPotCenterToRight\">/SweepSteeringPotCenterToRight</href></p>";
            message += "  <p><a href=\"/SweepSteeringPotCenterToLeft\">/SweepSteeringPotCenterToLeft</href></p>";
            message += "  <p><a href=\"/SweepSteeringPotRightToLeft\">/SweepSteeringPotRightToLeft</href></p>";
            message += "  <p><a href=\"/SweepSteeringPotLeftToRight\">/SweepSteeringPotLeftToRight</href></p>";
            message += "  <p><a href=\"/SweepSteeringPotRightToCenter\">/SweepSteeringPotRightToCenter</href></p>";
            message += "  <p><a href=\"/SweepSteeringPotLeftToCenter\">/SweepSteeringPotLeftToCenter</href></p>";
            message += "";
            message += "</body>";
            message += "";
            message += "</html>";

            this.Send(message);

        }


        public void getThrottle()
        {

            Context.Response.ContentType = "text/html";
            Context.Response.StatusCode = (int)HttpStatusCode.OK;

            string message = "<!doctype html>";
            message += "<html>";
            message += "<head>";
            message += "  <meta charset=\"utf-8\">";
            message += "  <title>IoRCT</title>";
            message += "</head>";
            message += "";
            message += "<body>";
            message += "";
            message += "  <h1>IoRCT</h1>";
            message += "  <h2>Throttle</h2>";
            message += "  <p><a href=\"/SweepThrottlePotCenterToMaxForward\">/SweepThrottlePotCenterToMaxForward</href></p>";
            message += "  <p><a href=\"/SweepThrottlePotMaxForwardToCenter\">/SweepThrottlePotMaxForwardToCenter</href></p>";
            message += "  <p><a href=\"/SweepThrottlePotCenterToMaxReverse\">/SweepThrottlePotCenterToMaxReverse</href></p>";
            message += "  <p><a href=\"/SweepThrottlePotMaxReverseToCenter\">/SweepThrottlePotMaxReverseToCenter</href></p>";
            message += "  <p><a href=\"/SweepThrottlePotCenterToMidForward\">/SweepThrottlePotCenterToMidForward</href></p>";
            message += "  <p><a href=\"/SweepThrottlePotMidForwardToCenter\">/SweepThrottlePotMidForwardToCenter</href></p>";
            message += "  <p><a href=\"/SweepThrottlePotCenterToMidReverse\">/SweepThrottlePotCenterToMidReverse</href></p>";
            message += "  <p><a href=\"/SweepThrottlePotMidReverseToCenter\">/SweepThrottlePotMidReverseToCenter</href></p>";
            message += "";
            message += "</body>";
            message += "";
            message += "</html>";

            this.Send(message);

        }


        // sweeps

        // TODO: getSweepPot(byte pot, int from, int to)

        public void getSweepSteeringPotCenterToRight()
        {

            SweepSteeringPotCenterToRight(this, EventArgs.Empty);

            Context.Response.ContentType = "application/json";
            Context.Response.StatusCode = (int)HttpStatusCode.OK;
            Hashtable result = new Hashtable { { "result", "swept center to right" } };
            this.Send(result);

        }

        public void getSweepSteeringPotCenterToLeft()
        {

            SweepSteeringPotCenterToLeft(this, EventArgs.Empty);

            Context.Response.ContentType = "application/json";
            Context.Response.StatusCode = (int)HttpStatusCode.OK;
            Hashtable result = new Hashtable { { "result", "swept center to left" } };
            this.Send(result);

        }

        public void getSweepSteeringPotRightToLeft()
        {

            SweepSteeringPotRightToLeft(this, EventArgs.Empty);

            Context.Response.ContentType = "application/json";
            Context.Response.StatusCode = (int)HttpStatusCode.OK;
            Hashtable result = new Hashtable { { "result", "swept right to left" } };
            this.Send(result);

        }

        public void getSweepSteeringPotLeftToRight()
        {

            SweepSteeringPotLeftToRight(this, EventArgs.Empty);

            Context.Response.ContentType = "application/json";
            Context.Response.StatusCode = (int)HttpStatusCode.OK;
            Hashtable result = new Hashtable { { "result", "swept left to right" } };
            this.Send(result);

        }

        public void getSweepSteeringPotRightToCenter()
        {

            SweepSteeringPotRightToCenter(this, EventArgs.Empty);

            Context.Response.ContentType = "application/json";
            Context.Response.StatusCode = (int)HttpStatusCode.OK;
            Hashtable result = new Hashtable { { "result", "swept right to center" } };
            this.Send(result);

        }

        public void getSweepSteeringPotLeftToCenter()
        {

            SweepSteeringPotLeftToCenter(this, EventArgs.Empty);

            Context.Response.ContentType = "application/json";
            Context.Response.StatusCode = (int)HttpStatusCode.OK;
            Hashtable result = new Hashtable { { "result", "swept left to center" } };
            this.Send(result);

        }

        public void getSweepSteeringPotTest()
        {

            SweepSteeringPotTest(this, EventArgs.Empty);

            Context.Response.ContentType = "application/json";
            Context.Response.StatusCode = (int)HttpStatusCode.OK;
            Hashtable result = new Hashtable { { "result", "test complete" } };
            this.Send(result);

        }


        public void getSweepThrottlePotCenterToMaxForward()
        {

            SweepThrottlePotCenterToMaxForward(this, EventArgs.Empty);

            Context.Response.ContentType = "application/json";
            Context.Response.StatusCode = (int)HttpStatusCode.OK;
            Hashtable result = new Hashtable { { "result", "swept center to max forward" } };
            this.Send(result);

        }

        public void getSweepThrottlePotMaxForwardToCenter()
        {

            SweepThrottlePotMaxForwardToCenter(this, EventArgs.Empty);

            Context.Response.ContentType = "application/json";
            Context.Response.StatusCode = (int)HttpStatusCode.OK;
            Hashtable result = new Hashtable { { "result", "swept max forward to center" } };
            this.Send(result);

        }

        public void getSweepThrottlePotCenterToMaxReverse()
        {

            SweepThrottlePotCenterToMaxReverse(this, EventArgs.Empty);

            Context.Response.ContentType = "application/json";
            Context.Response.StatusCode = (int)HttpStatusCode.OK;
            Hashtable result = new Hashtable { { "result", "swept center to max reverse" } };
            this.Send(result);

        }

        public void getSweepThrottlePotMaxReverseToCenter()
        {

            SweepThrottlePotMaxReverseToCenter(this, EventArgs.Empty);

            Context.Response.ContentType = "application/json";
            Context.Response.StatusCode = (int)HttpStatusCode.OK;
            Hashtable result = new Hashtable { { "result", "swept max reverse to center" } };
            this.Send(result);

        }

        public void getSweepThrottlePotCenterToMidForward()
        {

            SweepThrottlePotCenterToMidForward(this, EventArgs.Empty);

            Context.Response.ContentType = "application/json";
            Context.Response.StatusCode = (int)HttpStatusCode.OK;
            Hashtable result = new Hashtable { { "result", "swept center to mid forward" } };
            this.Send(result);

        }

        public void getSweepThrottlePotMidForwardToCenter()
        {

            SweepThrottlePotMidForwardToCenter(this, EventArgs.Empty);

            Context.Response.ContentType = "application/json";
            Context.Response.StatusCode = (int)HttpStatusCode.OK;
            Hashtable result = new Hashtable { { "result", "swept mid forward to center" } };
            this.Send(result);

        }

        public void getSweepThrottlePotCenterToMidReverse()
        {

            SweepThrottlePotCenterToMidReverse(this, EventArgs.Empty);

            Context.Response.ContentType = "application/json";
            Context.Response.StatusCode = (int)HttpStatusCode.OK;
            Hashtable result = new Hashtable { { "result", "swept center to mid reverse" } };
            this.Send(result);

        }

        public void getSweepThrottlePotMidReverseToCenter()
        {

            SweepThrottlePotMidReverseToCenter(this, EventArgs.Empty);

            Context.Response.ContentType = "application/json";
            Context.Response.StatusCode = (int)HttpStatusCode.OK;
            Hashtable result = new Hashtable { { "result", "swept mid reverse to center" } };
            this.Send(result);

        }


    }
}
