using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

using static ForumApp.GCommon.ValidationConstants.TagConstants;

namespace ForumApp.Data.Models;

public class Tag
{
    [Comment("Id of tag which can be used in posts on a board")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Comment("Name of tag")]
    public string Name { get; set; } = null!;

    [Comment("Hex color code for the tag")]
    [RegularExpression(ColorHexRegexValidation, ErrorMessage = ColorHexError)]
    public string ColorHex { get; set; } = null!;
    public virtual ICollection<PostTag> PostTags { get; set; }
        = new HashSet<PostTag>();
}
