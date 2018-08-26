using Maple;
using System.Net;
using System.Collections;
using System.Text;

namespace IoRCT
{
    public class RequestHandler : RequestHandlerBase
    {
        // example is outdated per https://community.wildernesslabs.co/t/maple-project-error/623/2?u=ishotjr
        //public RequestHandler(HttpListenerContext context) : base(context)
        public RequestHandler(HttpListenerContext context)
        {
            this.Context = context;
        }


        // TODO: really these should be PUT/PATCH, but using GET for now since it's easier to work w/ during prototyping
        // *oh, also Maple only supports GET and POST?

        // TODO: wanted this to return for / but reflection doesn't seem to work for as expected for ""?
        //public void get()
        public void getHome()
        {
            this.Context.Response.ContentType = "application/text";
            this.Context.Response.StatusCode = 200;

            string message = "TODO: return documentation page!";
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            this.Context.Response.OutputStream.Write(messageBytes, 0, messageBytes.Length);
            this.Context.Response.Close();
        }

        public void getDoSomething()
        {
            this.Context.Response.ContentType = "application/json";
            this.Context.Response.StatusCode = 200;
            Hashtable result = new Hashtable { { "message", "hello world!" } };
            this.Send(result);
        }
    }
}
