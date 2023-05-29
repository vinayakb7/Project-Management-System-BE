using Microsoft.Extensions.DependencyInjection.Extensions;
using ProjectManagementSystem.Business;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IUserClass, UserClass>();
builder.Services.AddTransient<IProjectClass, ProjectClass>();
builder.Services.AddTransient<IFeedbackClass, FeedbackClass>();
builder.Services.AddTransient<IRoleClass, RoleClass>();
builder.Services.AddTransient<IPermissionClass, PermissionClass>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddCors(p => p.AddPolicy("corspolicy", build =>
{
    build.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("corspolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
