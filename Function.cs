using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using aws_stuff.models;
using aws_stuff.controllers;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;
using System.Collections;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace aws_stuff
{

    public class Function
    {
        private static ServiceProvider ServiceProvider { get; set; }
        public Function()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();
        }
        public async Task<string> FunctionHandler(ILambdaContext context)
        {
            return await ServiceProvider.GetService<App>().Run(context, ServiceProvider);
        }

        private void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<HttpClient>();
            serviceCollection.AddTransient<App>();
        }
    }

}

class App
{
    public async Task<string> Run(ILambdaContext context, IServiceProvider service)
    {
        APIController controller = new APIController(service);
        try {
            string url = Environment.GetEnvironmentVariable("ROOTAPI");
            PersonDTO res = await controller.FetchUserFactory(url);
            return JsonConvert.SerializeObject(res);
        } catch (InvalidOperationException e) {
            throw new Exception("environment variables are not set", e);
        }
    }
}
