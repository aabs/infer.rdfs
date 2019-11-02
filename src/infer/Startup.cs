using Autofac;
using Inference.Core;
using System;
using VDS.RDF.Query;
using VDS.RDF.Update;
using static Inference.CLI.Program;

namespace Inference.CLI
{
    internal class Startup
    {
        public static IContainer Container { get; private set; }

        public static void Configure(Options opts)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<InferenceEngine>().AsImplementedInterfaces();
            Console.WriteLine($"SPARQL Endpoint: {opts.Endpoint}");
            builder.Register<ISparqlQueryProcessor>(ctx => new RemoteQueryProcessor(new SparqlRemoteEndpoint(opts.Endpoint)));
            builder.Register<ISparqlUpdateProcessor>(ctx => new RemoteUpdateProcessor(new SparqlRemoteUpdateEndpoint(opts.Endpoint)));

            //// Register individual components
            //builder.RegisterInstance(new TaskRepository())
            //       .As<ITaskRepository>();
            //builder.RegisterType<TaskController>();
            //builder.Register(c => new LogManager(DateTime.Now))
            //       .As<ILogger>();

            //// Scan an assembly for components
            //builder.RegisterAssemblyTypes(myAssembly)
            //       .Where(t => t.Name.EndsWith("Repository"))
            //       .AsImplementedInterfaces();

            Container = builder.Build();
        }
    }
}