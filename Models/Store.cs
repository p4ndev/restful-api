namespace Etag.Models;

public class Store : Base
{
    public string Title { get; set; }
    public string Address { get; set; }
    public long ZipCode { get; set; }

    public Store(string title, string address, long zipCode)
    {
        Id = Guid.NewGuid();
        Title = title;
        Address = address;
        ZipCode = zipCode;
        UpdatedAt = DateTime.Now;
    }
}
