var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/Home", async (context) => {
        await context.Response.WriteAsync("This is my Home Page.. GET");
    });

    endpoints.MapPost("/Home", async (context) =>
    {
        await context.Response.WriteAsync("This is my Home Page.. POST");
    });

    endpoints.MapPut("/Home", async (context) =>
    {
        await context.Response.WriteAsync("This is Home Page.. PUT");
    });

    endpoints.MapDelete("/Home", async(context) =>
    {
        await context.Response.WriteAsync("This is Home Page.. DELETE");
    });

});

//app.Map("/Home", () => "Hello World!");
//app.MapGet("/Home", () => "Hello World! - GET");
//app.MapPost("/Home", () => "Hello World! - POST");
//app.MapPut("/Home", () => "Hello World! - PUT");
//app.MapDelete("/Home", () => "Hello World! - DELETE");

app.Run(async(HttpContext context) => {
    await context.Response.WriteAsync("Page Not Found - 404");
});

app.Run();
