using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace c03.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private static string _fileName = "Logs/requestsLog.txt";

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Request.EnableBuffering();
            if (context.Request != null)
            {
                string path = context.Request.Path;
                string method = context.Request.Method;
                string query = context.Request.QueryString.ToString();
                string body = "";

                using (var reader = new StreamReader(
                    context.Request.Body,
                    Encoding.UTF8,
                    true,
                    1024,
                    true
                ))
                {
                    body = await reader.ReadToEndAsync();
                    context.Request.Body.Position = 0;
                }

                using (var writer = File.AppendText(_fileName))
                {
                    string message = $"[{DateTime.Now}] {method} {path}{query}\n{body}";
                    writer.WriteLine(message);
                }
            }
            
            if (_next != null) await _next(context);
        }
        
    }
}