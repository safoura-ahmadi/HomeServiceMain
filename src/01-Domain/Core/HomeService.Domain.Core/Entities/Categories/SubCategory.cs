using System.ComponentModel.DataAnnotations;

namespace HomeService.Domain.Core.Entities.Categories;

public class SubCategory
{
    public int Id { get; set; }
    [MaxLength(100)]
    public string Title { get; set; } = null!;
    public bool IsActive { get; set; }
 
    //navigation
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public List<SubService> SubServices { get; set; } = [];
}
