using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using TestMainANgular_Net.AggregateRoot.Validation;
using TestMainANgular_Net.Handler;
using TestMainANgular_Net.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Register Repository with AppDbContext
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

//handler register
builder.Services.AddTransient<StudentHandler>();
builder.Services.AddTransient<DepartmentHandler>();

//Fluent Validator Inject
builder.Services.AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<StudentDtoValidator>());

builder.Services.AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<DepartmentDtoValidator>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(c =>
{
    c.AllowAnyHeader();
    c.AllowAnyMethod();
    c.AllowAnyOrigin();
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
