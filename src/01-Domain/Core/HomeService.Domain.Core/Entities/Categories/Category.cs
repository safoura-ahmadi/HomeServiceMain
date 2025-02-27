using System.ComponentModel.DataAnnotations;

namespace HomeService.Domain.Core.Entities.Categories;

public class Category
{
    #region Properties
    [Key]
    public int Id { get; set; }

    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "وارد کردن عنوان کتگوری الزامی است.")]
    [MinLength(2, ErrorMessage = "عنوان باید حداقل 2 کاراکتر باشد.")]
    [MaxLength(100, ErrorMessage = "عنوان نمی‌تواند بیشتر از 100 کاراکتر باشد.")]
    public string Title { get; set; } = null!;

    [Display(Name = "مسیر تصویر")]
    [Required(ErrorMessage = "وارد کردن مسیر تصویر الزامی است.")]
    [MaxLength(500, ErrorMessage = "مسیر تصویر نمی‌تواند بیشتر از 500 کاراکتر باشد.")]
    public string ImagePath { get; set; } = null!;

    [Display(Name = "فعال")]
    public bool IsActive { get; set; } = true;
    #endregion

    #region NavigationProperties
    public List<SubCategory> SubCategories { get; set; } = [];
    #endregion
}