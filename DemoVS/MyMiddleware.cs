using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using System.Diagnostics;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace DemoVS
{
    public class MyMiddleware
    {
        private readonly RequestDelegate _next;

        //O método construtor precisa ter o mesmo nome da classe
        public MyMiddleware(RequestDelegate next) { 
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            //Chama o próximo middleware no pipeline
            await _next(httpContext);
        }
    }

    public class LogTempoMiddleware
    {
        // Delegate vai fazer a chamada e aguardar o retorno
        private readonly RequestDelegate _next;

        public LogTempoMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var sw = Stopwatch.StartNew();

            //Chama o próximo middleware no pipeline
            await _next(httpContext);

            sw.Stop();

            Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
            Log.Information($"A execucao demorou {sw.Elapsed.TotalSeconds}ms ({sw.Elapsed.TotalSeconds} segundos)");

            // Faz algo depois
        }
    }

    // Criar uma extensão para o LogTempoMiddleware
    public static class LogTempoMiddlewareExtension
    { 
        
        public static void UseLogTempo(this IApplicationBuilder app)
        {
            app.UseMiddleware<LogTempoMiddleware>();
        }

    }

}

