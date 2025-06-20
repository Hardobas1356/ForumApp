using System.ComponentModel.DataAnnotations;
using static ForumApp.GCommon.ValidationConstants.ReplyConstants;

namespace ForumApp.Web.ViewModels.Reply;

public class ReplyCreateInputModel
{
    [Required]
    [MinLength(ContentMinLength)]
    [MaxLength(ContentMaxLength)]
    public string Content { get; set; } = null!;

    [Required]
    public Guid PostId { get; set; }
}
