using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace FFS
{
    public class Ffs : OwinMiddleware
    {
        public Ffs(OwinMiddleware next) : base(next) { }
        
        public void WriteHtmlContent(IOwinContext context)
        {
            var path = context.Request.Path.Value;
            
            var response = new ResponseRenderer(path);
            context.Response.ContentType = response.MimeType;
            context.Response.WriteAsync(response.Render());
        }

        public async override Task Invoke(IOwinContext context)
        {
            WriteHtmlContent(context);
            await Next.Invoke(context);
        }
    }
}
