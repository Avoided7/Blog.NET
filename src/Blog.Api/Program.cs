using Blog.Api.BackgroundServices;
using Blog.Application.Extensions;
using Blog.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services
  .AddDbContextInMemory()
  .AddRepository()
  .AddUnitOfWork()
  .AddApplicationMediatR();

builder.Services.AddCors();

builder.Services.AddHostedService<PostPublisher>();

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
  app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
}

app.MapControllers();

app.Run();
