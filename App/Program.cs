using System;
using FFS;
using Microsoft.Owin.Hosting;
using Owin;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            using (WebApp.Start<Pipeline>("http://localhost:1234"))
            {
                Console.WriteLine("Now listening on localhost:1234");
                Console.ReadLine();
            }
        }
    }

    public class Pipeline
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            appBuilder.Use<Ffs>();
        }
    }
}
