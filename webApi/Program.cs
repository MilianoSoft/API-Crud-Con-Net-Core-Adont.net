//usamos la data
using Data;

// se crea el contenedor
var builder = WebApplication.CreateBuilder(args);

// se agregan los servicios al contenedor
builder.Services.AddControllers();
// aprende mas aserca de openIA y swagger 
builder.Services.AddEndpointsApiExplorer();// puntos finales
builder.Services.AddSwaggerGen();
builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection("ConnectionStrings"));
//agremaos la clase de empleado, pero solo quiero una instancia
builder.Services.AddSingleton<EmpleadoData>();

var app = builder.Build();  // se construye la aplicacion

// Configure the HTTP request pipeline.
// defino si es un entorno de desarrollo o no para mostrar la interfaces de swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// middeware

app.UseAuthorization(); //autorizaciones 

app.MapControllers(); // mapeo de los controladores

app.Run(); //corremos la app
