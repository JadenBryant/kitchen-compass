namespace CustomerBackend.Models;

public class Config
{
    public Guid Id { get; set; }
    public string Key { get; set; }
    public string Value { get; set; }

    public Config(string key, string value)
    {
        Id = Guid.NewGuid();
        Key = key;
        Value = value;
    }
}