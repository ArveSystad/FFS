using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace FFS
{
    public class Ffs : OwinMiddleware
    {
        public Ffs(OwinMiddleware next) : base(next) { }

        public string BasePagePath { get {  return Environment.CurrentDirectory + "\\..\\..\\Pages\\";} }
        public string BaseResourcesPath { get { return Environment.CurrentDirectory + "\\..\\..\\Resources\\"; } }
        
        public string DefaultLayoutPath { get { return BasePagePath+ "_Layout.html"; } }

        public void WriteHtmlContent(IOwinContext context)
        {
            var path = context.Request.Path.Value;
            var type = FileUtilities.GetType(path);
            context.Response.ContentType = FileUtilities.GetMimetype(type);
            if (type == null)
                context.Response.WriteAsync(RenderPage(path));
            else
                context.Response.WriteAsync(RenderResource(path, type));
        }
        
        public Byte[] RenderResource(string path, string extension)
        {
            return File.ReadAllBytes(BaseResourcesPath + path);
        }

        private string RenderPage(string path)
        {
            if (path == "/")
                path = "/index";

            string fileContent = File.ReadAllText(BasePagePath + path + ".html");
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
