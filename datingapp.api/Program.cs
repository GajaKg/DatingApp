using datingapp.api.Extensions;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationServices(builder.Configuration);
// builder.Services.AddDbContext<ApplicationDBContext>(options =>
//     options.UseNpgsql(
//         builder.Configuration.GetConnectionString("DefaultConnection")
//     )
// );
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Dating Demo API", Version = "v1" });
    // option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    // {
    //     In = ParameterLocation.Header,
    //     Description = "Please enter a valid token",
    //     Name = "Authorization",
    //     Type = SecuritySchemeType.Http,
    //     BearerFormat = "JWT",
    //     Scheme = "Bearer"
    // });
    // option.AddSecurityRequirement(new OpenApiSecurityRequirement
    // {
    //     {
    //         new OpenApiSecurityScheme
    //         {
    //             Reference = new OpenApiReference
    //             {
    //                 Type=ReferenceType.SecurityScheme,
    //                 Id="Bearer"
    //             }
    //         },
    //         new string[]{}
    //     }
    // });

});
// builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddIdentityService(builder.Configuration);
/************************ Moved to extensions *******************/
// builder.Services.AddScoped<ITokenService, TokenService>(); // AddSingleton() best for cashing
// builder.Services
//     .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddJwtBearer(options =>
//     {
//         options.TokenValidationParameters = new TokenValidationParameters
//         {
//             ValidateIssuerSigningKey = true, // essential
//             IssuerSigningKey = new SymmetricSecurityKey(
//                 Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"]!)
//             ),
//             ValidateIssuer = false,
//             ValidateAudience = false,
//         };
//     });



/************************ Moved to extensions *******************/
// var connectionString = builder.Configuration
//     .GetConnectionString("DefaultConnection");

// if (string.IsNullOrWhiteSpace(connectionString))
//     throw new InvalidOperationException(
//         "Connection string 'DefaultConnection' not found.");

// builder.Services.AddDbContext<DataContext>(opts =>
//     opts.UseMySql(
//         connectionString,
//         ServerVersion.AutoDetect(connectionString),
//         mySqlOpts => mySqlOpts.MigrationsAssembly("datingapp.data")
//     )
// );

// builder.Services.AddApplicationServices(builder.Configuration);
// builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();


// Configure the HTTP request pipeline.
// app.UseMiddleware<ExceptionMiddleware>();

// app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:5219"));

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


// Database SEED
// using var scope = app.Services.CreateScope();
// var services = scope.ServiceProvider;
// try
// {
//     var context = services.GetRequiredService<DataContext>();
//     await context.Database.MigrateAsync(); // ukoliko nema baze kreirace novu sa sve tabelama
//     await Seed.SeedUsers(context);
// }
// catch (Exception ex)
// {
//     var logger = services.GetService<ILogger<Program>>();
//     logger?.LogError(ex, "An error occurred druing migration.");
// }

app.Run();
