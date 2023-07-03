using Infrastructure.Persistence;

using Swashbuckle.AspNetCore.SwaggerUI;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.Local.json");

builder.Services.AddCors(options =>
{
  options.AddPolicy(MyAllowSpecificOrigins,
                    policy =>
                    {
                      policy //.WithOrigins("http://localhost:3000")
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
});


builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebAPIServices();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI(options =>
  {
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.DocExpansion(DocExpansion.None);
  });

  app.UseMigrationsEndPoint();

  // Initialise and seed database
  using var scope = app.Services.CreateScope();
  var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
  await initialiser.InitialiseAsync();
  await initialiser.SeedAsync();
}
else
{
  app.UseHsts();
}

app.UseHealthChecks("/health");
//app.UseHttpsRedirection();
//app.UseStaticFiles();

app.UseRouting();
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
