using ForumApp.Web.ViewModels.Category;
using System.ComponentModel.DataAnnotations;
using static ForumApp.GCommon.ValidationConstants.BoardConstants;

namespace ForumApp.Web.ViewModels.Board;

public class BoardCreateInputModel
{
    [Required]
    [MaxLength(NameMaxLength)]
    [MinLength(NameMinLength)]
    public string Name { get; set; } = null!;

    [Required]
    [MaxLength(NameMaxLength)]
    [MinLength(NameMinLength)]
    public string Description { get; set; } = null!;

    [Display(Name = "Image URL")]
    public string? ImageUrl { get; set; }

    [Display(Name = "Select Categories")]
    public IEnumerable<Guid> SelectedCategoryIds { get; set; } 
        = new HashSet<Guid>();

    public ICollection<CategoryViewModel> AvailableCategories { get; set; } 
        =  new HashSet<CategoryViewModel>();
}
