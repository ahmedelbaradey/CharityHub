using CharityHub.Application.MiddleWare;
using CharityHub.Infrastructure;
using CharityHub.Identity;
using CharityHub.Logger;
using API.Extensions;
using NLog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using AspNetCoreRateLimit;
using Microsoft.AspNetCore.HttpOverrides;
using CharityHub.DomainService;

var builder = WebApplication.CreateBuilder(args);
LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
 
builder.Services.ConfigureRateLimitingOptions();
builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services
    .AddLoggerServices(builder.Configuration)
    .AddApplicationModuleDependencies()
    .AddInfrastructureService(builder.Configuration).AddInfrastructureModuleDependencies(builder.Configuration)
    .AddIdentityService(builder.Configuration)
    .AddIdentityModuleDependencies()
    .AddDomainServiceModuleDependencies();
builder.Services.ConfigureVersioning();
builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
builder.Services.ConfigureResponseCaching();
builder.Services.AddMemoryCache();
builder.Services.ConfigureRateLimitingOptions();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication();
builder.Services.ConfigureLocalization();
NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter() => new ServiceCollection().AddLogging().AddMvc().AddNewtonsoftJson().Services.BuildServiceProvider().GetRequiredService<IOptions<MvcOptions>>().Value.InputFormatters.OfType<NewtonsoftJsonPatchInputFormatter>().First();
builder.Services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true;
    config.ReturnHttpNotAcceptable = true;
    config.InputFormatters.Insert(0, GetJsonPatchInputFormatter());
    config.CacheProfiles.Add("120SecondsDuration", new CacheProfile { Duration = 120 });
}).AddXmlDataContractSerializerFormatters().AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);
builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.ConfigureHttpCacheHeaders();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "CharityHub API v2");
        options.DisplayRequestDuration();
    });
}
app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});
app.UseMiddleware<ErrorHandlerMiddleWare>();
app.UseIpRateLimiting();
app.UseResponseCaching();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
