using Autofac;
using CommandLine;
using Inference.Core;
using System;

namespace Inference.CLI
{
    internal class Program
    {
        public class Options
        {
            [Option('e', "endpoint", Required = true, HelpText = "The full SPARQL endpoint of the triple store to infer on")]
            public Uri Endpoint { get; set; }

            [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
            public bool Verbose { get; set; }
        }

        private static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                   .WithParsed(o =>
                   {
                       Startup.Configure(o);
                       if (o.Endpoint.IsWellFormedOriginalString())
                       {
                           var eng = Startup.Container.Resolve<IInferenceEngine>();
                           eng.Infer(null);
                       }
                       else
                       {
                           Console.WriteLine($"Current Arguments: -v {o.Verbose}");
                           Console.WriteLine("Quick Start Example!");
                       }
                   });
            Console.WriteLine("Hello World!");
        }
    }
}