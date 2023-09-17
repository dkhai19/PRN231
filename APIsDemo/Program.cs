var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Those lines need to be added.
//1
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//2
builder.Services.AddEndpointsApiExplorer();
//3
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//4
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

//5
app.MapControllers();

app.Run();
