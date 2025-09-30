var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var app = builder.Build();

//app.MapDefaultControllerRoute(); //by default this takes Controller as Home and Action as Index..

//app.MapGet("/", () => "Hello World!");

//app.MapControllerRoute(
//    name:"default",
//    pattern:"{controller=Home}/{action=Index}/{id?}"
//    );

app.MapControllers();


app.Run();
