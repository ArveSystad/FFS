using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace FFS
{
    public class Ffs : OwinMiddleware
    {
        public Ffs(OwinMiddleware next)
            : base(next)
        {
        }

        public void WriteHtmlContent(IOwinContext context)
        {
            var path = context.Request.Path.Value;
            context.Response.ContentType = "text/html";
            if (path == "/")
                context.Response.WriteAsync(GetFileContent("index.html"));
            else
                context.Response.WriteAsync(GetFileContent("Pages\\" + path + ".html"));
        }

        private byte[] GetFileContent(string path)
        {
            return File.ReadAllBytes(Environment.CurrentDirectory + "\\..\\..\\" + path);
        }

        public async override Task Invoke(IOwinContext context)
        {
            WriteHtmlContent(context);
            await Next.Invoke(context);
        }
    }
}
