using Maple;
using System.Net;
using System.Collections;
using System.Text;

using System.Threading;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace IoRCT
{
    public class RequestHandler : RequestHandlerBase
    {
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
            message += "  <h2>TODO</h2>";
            message += "  <p>add clickable API documentation here!</p>";
            message += "";
            message += "</body>";
            message += "";
            message += "</html>";

            this.Send(message);

        }

        public void getDoSomething()
        {

            Context.Response.ContentType = "application/json";
            Context.Response.StatusCode = (int)HttpStatusCode.OK;
            Hashtable result = new Hashtable { { "message", "hello world!" } };
            this.Send(result);

        }
    }
}
