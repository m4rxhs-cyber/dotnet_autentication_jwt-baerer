using ProjetoMySQL.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using ProjetoMySQL;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ProjetoMySQL.Repositories;
using ProjetoMySQL.Services;



 
var builder = WebApplication.CreateBuilder(args);
var key =Encoding.ASCII.GetBytes(Settings.Secret); 
builder.Services.AddAuthentication(x=>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(x=>
{
    x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };

});


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Candidato", policy => policy.RequireRole("candidato"));
    options.AddPolicy("Eleitor", policy => policy.RequireRole("eleitor"));
});



var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BDContexto>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});
 
// Add services to the container.
builder.Services.AddControllersWithViews();
 
var app = builder.Build();
 
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
 
app.UseHttpsRedirection();
app.UseStaticFiles();
 
app.UseRouting();

app.UseAuthentication(); 
app.UseAuthorization();

app.MapPost("/login", (User model) =>
{
    var user = UserRepository.Get(model.Username, model.Password);

    if (user == null)
        return Results.NotFound(new { message = "Invalid username or password" });

    var token = TokenService.GenerateToken(user);

    user.Password = "";

    return Results.Ok(new
    {
        user = user,
        token = token
    });
});

 
app.UseCors(option => option.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
 
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
 
app.Run();