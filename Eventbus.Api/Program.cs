using Eventbus.Api.Init;
using Eventbus.Core.Infrastructure.MessageBroker.Kafka;
using Eventbus.Core.Infrastructure.MesssageBroker;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Prometheus;


SwaggerService swagger = new();
Oauth2Service oauth2 = new();

WebHost.CreateDefaultBuilder()
.ConfigureServices(services =>
{
    services.AddScoped<IMessageBroker>(x=> new KafkaService(""));
    services.AddControllers();
    swagger.Register(services);
    oauth2.Register(services);
})
.ConfigureLogging(logBuilder=> {
    logBuilder.SetMinimumLevel(LogLevel.Trace);
    logBuilder.AddLog4Net("log4net.config");
})
.Configure(app =>
{
    var env = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();

    oauth2.Configure(app, env);

    swagger.Configure(app, env);

    app.UseHttpsRedirection();

    app.UseRouting();

    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
        endpoints.MapMetrics();
    });
}).Build().Run();