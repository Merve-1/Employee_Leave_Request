using LeaveRequests.Client.Components;
using LeaveRequests.Client.Auth;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddHttpClient("LeaveApi", client =>
{
    client.BaseAddress = new Uri("http://localhost:5127/");
});
builder.Services.Configure<AuthOptions>(builder.Configuration.GetSection("Auth"));
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

//app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
