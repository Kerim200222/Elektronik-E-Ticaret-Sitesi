namespace E_Ticaret;

public class EmailSettings
{
    public string Host { get; set; } = default!;
    public int Port { get; set; }
    public bool EnableSSL { get; set; }
    public string User { get; set; } = default!;
    public string Password { get; set; } = default!;
}
