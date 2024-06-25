using Amazon;
using Amazon.Runtime;
using Amazon.SimpleEmail;
using dotenv.net;
using EmailService.Worker;
using EmailService.Worker.Consumer;
using MassTransit;

var builder = Host.CreateApplicationBuilder(args);

DotEnv.Load();
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<EmailConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h => 
        {
            h.Username("admin");
            h.Password("admin");
            
        });
        cfg.ConfigureEndpoints(context);
    });
});
#region Amazon config
var accessKey = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID");
var secretKey = Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY");
var region = Environment.GetEnvironmentVariable("AWS_REGION");

var crential = new BasicAWSCredentials(accessKey, secretKey);
var awsRegion = RegionEndpoint.GetBySystemName(region);

builder.Services.AddTransient(sp =>  new AmazonSimpleEmailServiceClient(crential, awsRegion));
#endregion

builder.Services.AddHostedService<Worker>();
builder.Services.AddMassTransitHostedService(true);

var host = builder.Build();
host.Run();

