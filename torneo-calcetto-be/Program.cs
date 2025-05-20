using Microsoft.EntityFrameworkCore;
using torneo_calcetto.EF.Context;
using Microsoft.Extensions.DependencyInjection;
using torneo_calcetto_be.Core.Repository;
using System.Reflection;
using torneo_calcetto_be.Core.Options;
var builder = WebApplication.CreateBuilder(args);
// Aggiungi CORS
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll",
          builder => {
              builder
                //.AllowAnyOrigin()
                .AllowAnyMethod()
                //.AllowAnyHeader()
                //.AllowCredentials()
                ;
          });
});
var confBuilder = new ConfigurationBuilder();
var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
confBuilder.AddJsonFile(Path.Combine(basePath, "appSettings.json"), false);
var configuration = confBuilder.Build();
var connectionString = configuration.GetConnectionString("TorneoCalcetto");
builder.Services.Configure<AppOptions>(configuration.GetSection("Options"));
// Registrazione dei servizi necessari per il controllo delle API o delle Razor Pages
builder.Services.AddControllersWithViews()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

// Aggiungi il contesto del database (DbContext) alla DI container
builder.Services.AddDbContext<TorneoCalcettoContext>(options =>
    options.UseSqlServer(connectionString)
);

builder.Services.AddTransient<PartiteRepository>();
builder.Services.AddTransient<SquadraRepository>();
builder.Services.AddTransient<TorneoRepository>();

var app = builder.Build();

// Applica automaticamente le migrazioni al database all'avvio
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<TorneoCalcettoContext>();
    dbContext.Database.Migrate();  // Applica le migrazioni al database
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Mappatura dei controller API
app.MapControllers(); // Registra i controller API per il routing

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseCors(
        options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
);
app.Run();
