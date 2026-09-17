using System.Net.Security;
using System.Reflection;
using System.Text.Json.Serialization;
using LeaveRequests.Api.Data;
using LeaveRequests.Api.Middleware;
using LeaveRequests.Api.Services;
using LeaveRequests.Api.Services.LeaveRequests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LeaveDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LeaveDatabase")));

var connectionString= builder.Configuration.GetConnectionString("LeaveDatabase");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

builder.Services.AddHttpClient<IEmployeeService, EmployeeService>(client =>
{
    client.BaseAddress = new Uri("https://dummyjson.com/");
    client.Timeout = TimeSpan.FromSeconds(10);
});

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            return new BadRequestObjectResult(new
            {
                message = "Validation failed",
                errors = context.ModelState.Where(k => k.Value?.Errors.Count > 0)
                    .ToDictionary(k => k.Key, v => v.Value!.Errors.Select(x => x.ErrorMessage).ToArray())
            });
        };
    })
    .AddJsonOptions(options =>
    {
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });


builder.Services.AddScoped<ILeaveRequestService, LeaveRequestService>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath= Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

builder.Services.AddMemoryCache();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
builder.Services.AddAuthorization();

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapControllers();

app.Run();

