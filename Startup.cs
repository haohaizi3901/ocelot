using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using CacheManager.Core;
using ConfigurationBuilder = Microsoft.Extensions.Configuration.ConfigurationBuilder;
using System;
 
namespace ApiGateway{  

  public class Startup
    {       
   public Startup(IHostingEnvironment env)        {       
        var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile("configuration.json")
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }      
        
       public IConfigurationRoot Configuration { get; }     
         
       public void ConfigureServices(IServiceCollection services)        {
            services.AddOcelot(Configuration); //此处添加Ocelot服务
        }      
       
      public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));

            app.UseOcelot().Wait();//此处使用Ocelot服务
        }
    }
}