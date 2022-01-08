using System;
using NBomber;
using NBomber.Contracts;
using NBomber.CSharp;
using NBomber.Plugins.Http.CSharp;
using NBomber.Plugins.Network.Ping;

namespace LoadTesting
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var step = Step.Create("fetch_html_page",
                                  clientFactory: HttpClientFactory.Create(),
                                  execute: context =>
                                  {
                                      var request = Http.CreateRequest("GET", "http://localhost:5000/swagger/index.html")
                                                         .WithHeader("Accept", "text/html");

                                      return Http.Send(request, context);
                                  });


           var scenario = ScenarioBuilder
               .CreateScenario("simple_http", step)
               .WithWarmUpDuration(TimeSpan.FromSeconds(5))
               .WithLoadSimulations(
                   Simulation.InjectPerSec(rate: 100, during: TimeSpan.FromSeconds(30))
               );

            // creates ping plugin that brings additional reporting data
            var pingPluginConfig = PingPluginConfig.CreateDefault(new[] { "localhost" });
            var pingPlugin = new PingPlugin(pingPluginConfig);

            NBomberRunner
                .RegisterScenarios(scenario)
                .WithWorkerPlugins(pingPlugin)
                .Run();
            Console.ReadKey();
        }
    }
}
