using FilterEx;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<MySampleResultFilterAttribute>();

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new MySampleActionFilter("Global",-8)); //this is global level
                                                                //commenting above so that I can apply filter to specific controller/action methods..
    options.Filters.Add(new MySampleResourceFilterAttribute("Global"));
    //options.Filters.AddService<MySampleResultFilterAttribute>();
    options.Filters.Add<MySampleResultFilterAttribute>();
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi


builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
