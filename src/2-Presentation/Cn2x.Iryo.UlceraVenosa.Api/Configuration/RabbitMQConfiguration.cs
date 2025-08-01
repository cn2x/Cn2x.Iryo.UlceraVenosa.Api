namespace Cn2x.Iryo.UlceraVenosa.Api.Configuration;

public class RabbitMQConfiguration
{
    public string Host { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string VirtualHost { get; set; } = string.Empty;
    public int Port { get; set; } = 5672;
    public bool UseSsl { get; set; } = false;
    public int RetryCount { get; set; } = 10;
    public int RetryDelaySeconds { get; set; } = 1;
}
