using SistemaControlDeInventario.Interface;
using SistemaControlDeInventario.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var AllowsCorsOrigin = "*";

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options => options.AddPolicy(name: AllowsCorsOrigin, policy =>
{
    policy.AllowAnyMethod()
          .AllowAnyHeader()
          .AllowAnyOrigin(); // Esto es suficiente
}));

builder.Services.AddScoped<IProductosInterface,IProductosService>();
builder.Services.AddScoped<IMantenimientoInterface, IMantenimientoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(AllowsCorsOrigin);

app.UseAuthorization();

app.MapControllers();

app.Run();
