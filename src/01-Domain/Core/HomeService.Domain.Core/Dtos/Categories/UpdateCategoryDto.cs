using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace HomeService.Domain.Core.Dtos.Categories;

public class UpdateCategoryDto
{
    public int Id { get; set; }
    [Required(ErrorMessage = "وارد کردن عنوان کتگوری الزامی است")]
    public string Title { get; set; } = null!;
    public string? ImagePath { get; set; }

    public IFormFile? ImageFile { get; set; }

}
