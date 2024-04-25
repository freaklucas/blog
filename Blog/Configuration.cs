namespace Blog;

public static class Configuration
{
    public static string? JWTKey = "WFd0ZklWbVhnMDYvOVpjaU84OFl0Zz09";

    public static string? ApiKeyName = "api_key";

    public static string? ApiKey = "curso_api_IlTevUM/z0ey3NwCV/unWg==";

    public static SmtpConfiguration Smtp = new();
    
    public class SmtpConfiguration
    {
        public string? Host { get; set; }
        public int Port { get; set; } = 25;
        public string? Username { get; set; }
        public string? Password { get; set; }

    }
}