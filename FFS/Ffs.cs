using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace FFS
{
    public class Ffs : OwinMiddleware
    {
        public Ffs(OwinMiddleware next) : base(next) { }

        public string DefaultLayoutPath { get { return Environment.CurrentDirectory + "\\..\\..\\Pages\\_Layout.html"; } }

        public void WriteHtmlContent(IOwinContext context)
        {
            var path = context.Request.Path.Value;
            context.Response.ContentType = "text/html";
            if (path == "/")
                context.Response.WriteAsync(RenderPage("index.html"));
            else
                context.Response.WriteAsync(RenderPage("Pages\\" + path + ".html"));
        }

        private string RenderPage(string path)
        {
            var fileContent = File.ReadAllText(Environment.CurrentDirectory + "\\..\\..\\" + path);
            if (File.Exists(DefaultLayoutPath))
            {
                var allText = File.ReadAllText(DefaultLayoutPath);
                return allText.Replace("{{body}}", fileContent);
            }
            return fileContent;
        }

        public async override Task Invoke(IOwinContext context)
        {
            WriteHtmlContent(context);
            await Next.Invoke(context);
        }
    }
}
