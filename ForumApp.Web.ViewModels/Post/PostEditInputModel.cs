using ForumApp.Web.ViewModels.Tag;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace ForumApp.Web.ViewModels.Post;
using static ForumApp.GCommon.ValidationConstants.PostConstants;

public class PostEditInputModel
{
    [Required]
    public Guid Id { get; set; }

    [Required(ErrorMessage = TitleRequiredErrorMessage)]
    [MaxLength(TitleMaximumLength, ErrorMessage = TitleInvalidLengthErrorMessage)]
    [MinLength(TitleMinimumLength, ErrorMessage = TitleInvalidLengthErrorMessage)]
    public string Title { get; set; } = null!;

    [Required(ErrorMessage = ContentInvalidLengthErrorMessage)]
    [MaxLength(ContentMaximumLength, ErrorMessage = ContentInvalidLengthErrorMessage)]
    [MinLength(ContentMinimumLength, ErrorMessage = ContentInvalidLengthErrorMessage)]
    public string Content { get; set; } = null!;

    public string? ImageUrl { get; set; }

    public ICollection<TagViewModel>? Tags { get; set; }
        = new HashSet<TagViewModel>();

    public ICollection<Guid>? TagIds { get; set; } 
        = new HashSet<Guid>();

    public ICollection<TagViewModel> AvailableTags { get; set; } 
        = new HashSet<TagViewModel>();
}
