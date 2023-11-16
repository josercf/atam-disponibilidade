namespace CustomerProfileAPI;

public class UserProfile
{
    public string Nome { get; set; }
    public string Endereco { get; set; }
    public int Avaliacao { get; set; }
}

public class UserRationg
{
    public int userId { get; set; }
    public int rating { get; set; }
}