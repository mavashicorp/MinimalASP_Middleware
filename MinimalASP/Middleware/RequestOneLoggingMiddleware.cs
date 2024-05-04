﻿namespace MinimalASP.Middleware
{
    public class RequestOneLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestOneLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Логируем начало обработки запроса
            //Debug.WriteLine($"Запрос {context.Request.Method} {context.Request.Path} начал обрабатываться");


            Console.WriteLine($"Запрос {context.Request.Method} {context.Request.Path} начал обрабатываться Request One LoggingMiddleware ");
            // Передаем управление следующему компоненту в цепочке
            await _next(context);

            // Логируем завершение обработки запроса
            //Debug.WriteLine($"Запрос {context.Request.Method} {context.Request.Path} обработан с статус-кодом {context.Response.StatusCode}");

            Console.WriteLine($"Запрос {context.Request.Method} {context.Request.Path} обрабтан Request One LoggingMiddleware");
        }
    }
}
