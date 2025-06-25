using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

using static ForumApp.GCommon.ValidationConstants.CategoryConstants;

namespace ForumApp.Data.Models;

public class Category
{
    [Comment("Id of category")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Comment("Name of category")]
    public string Name { get; set; } = null!;

    [Comment("Hex color code for the category")]
    [RegularExpression(ColorHexRegexValidation, ErrorMessage = ColorHexError)]
    public string ColorHex { get; set; } = null!;

    public virtual ICollection<BoardCategory> BoardCategories { get; set; }
    = new HashSet<BoardCategory>();
}