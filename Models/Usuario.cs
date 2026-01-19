using Microsoft.Net.Http.Headers;

public class Usuario
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
}