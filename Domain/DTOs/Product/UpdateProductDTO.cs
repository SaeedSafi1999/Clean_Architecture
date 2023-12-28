namespace Core.Domain.DTOs.Product;

public class UpdateProductDTO
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public string Name { get; set; }
    public DateTime CreateTime { get; set; }
}

