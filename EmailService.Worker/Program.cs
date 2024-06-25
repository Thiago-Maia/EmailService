using Amazon;
using Amazon.Runtime;
using Amazon.SimpleEmail;
using dotenv.net;
using EmailService.Infra;
using EmailService.Worker;
using EmailService.Worker.Consumer;
using MassTransit;

var builder = Host.CreateApplicationBuilder(args);

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
var awsSESConfig = builder.Configuration.GetSection("AWSSES");

var crential = new BasicAWSCredentials(
    awsSESConfig.GetSection("AWS_ACCESS_KEY_ID").Value, 
    awsSESConfig.GetSection("AWS_SECRET_ACCESS_KEY").Value
    );

var awsRegion = RegionEndpoint.GetBySystemName(awsSESConfig.GetSection("AWS_REGION").Value);

builder.Services.AddTransient(sp =>  new AmazonSimpleEmailServiceClient(crential, awsRegion));
builder.Services.AddTransient<AmazonSES>();
#endregion

builder.Services.AddHostedService<Worker>();
builder.Services.AddMassTransitHostedService(true);

var host = builder.Build();
host.Run();

