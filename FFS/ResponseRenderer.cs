using System;
using System.IO;

namespace FFS
{
    public class ResponseRenderer
    {
        private string _path;
        private string _type;

        public ResponseRenderer(string path)
        {
            _type = FileUtilities.GetType(path);
            _path = path;
        }

        public string BasePagePath { get { return Environment.CurrentDirectory + "\\..\\..\\Pages\\"; } }
        public string BaseResourcesPath { get { return Environment.CurrentDirectory + "\\..\\..\\Resources\\"; } }

        public string DefaultLayoutPath { get { return BasePagePath + "_Layout.html"; } }

        public string MimeType
        {
            get
            {
                if(_path == null)
                    throw new InvalidOperationException("Can't get mime type from an empty path, stupid");
                return FileUtilities.GetMimetype(_type);
            }
        }

        public Byte[] Render()
        {
            if (_type == "html")
            {
                var renderPage = RenderPage();
                return FileUtilities.GetBytes(renderPage);
            }

            return File.ReadAllBytes(BaseResourcesPath + _path);
        }

        public string RenderPage()
        {
            if (_path == "/")
                _path = "/index";

            string fileContent = File.ReadAllText(BasePagePath + _path + ".html");
            if (File.Exists(DefaultLayoutPath))
            {
                var allText = File.ReadAllText(DefaultLayoutPath);
                return allText.Replace("{{body}}", fileContent);
            }
            return fileContent;
        }
    }
}