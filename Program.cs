using Chat.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSignalR(); // Agrega la referencia a SignalR

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseCors(builder =>
{
    builder.WithOrigins("http://localhost:4200") // Reemplaza con el origen de tu aplicación Angular
     .AllowAnyMethod()
     .AllowAnyHeader()
     .AllowCredentials();

});


app.UseAuthorization();

app.MapControllers();
app.MapHub<ChatHubs>("/Hubs/ChatHubs");

app.Run();
