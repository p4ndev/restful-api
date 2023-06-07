namespace Etag.Models;

public class Product : Base
{
    public string Name { get; set; }
    public bool Available { get; set; }

    public Product(string name, bool available)
    {
        Id = Guid.NewGuid();
        Name = name;
        Available = available;
        UpdatedAt = DateTime.Now;
    }
}
