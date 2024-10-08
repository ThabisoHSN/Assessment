using Backend.Data;
using Backend.Utility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SharedLibrary.DTO.Account;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
      builder => builder.WithOrigins("https://localhost:7119", "http://localhost:5000") // Replace with your frontend origin
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddExtension();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.FullName);

});


builder.Services.AddDbContext<ApplicationContext>(options =>{
    options.UseInMemoryDatabase(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options =>
           {
               var tokenSettings = builder.Configuration.GetSection(nameof(JWTSettings)).Get<JWTSettings>();
               options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidIssuer = tokenSettings.Issuer,

                   ValidateAudience = true,
                   ValidAudience = tokenSettings.Audience,

                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.Secret)),

                   ClockSkew = TimeSpan.Zero
               };

           });

builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection(nameof(JWTSettings)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.DefaultModelsExpandDepth(-1);
    });
}
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
    DataSeeder.Seed(dbContext);
}

app.UseCors("AllowSpecificOrigin");
app.UseHttpsRedirection();

app.UseAuthorization(); // Include authorization middleware if applicable

// Map the default controller routes
app.MapControllers();

app.Run();


