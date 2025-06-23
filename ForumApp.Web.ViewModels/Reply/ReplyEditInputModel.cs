using System.ComponentModel.DataAnnotations;
using static ForumApp.GCommon.ValidationConstants.ReplyConstants;

namespace ForumApp.Web.ViewModels.Reply;

public class ReplyEditInputModel
{
    public Guid Id { get; set; }
    public Guid PostId { get; set; }

    [Required]
    [MinLength(ContentMinLength)]
    [MaxLength(ContentMaxLength)]
    public string Content { get; set; } = null!;
}
