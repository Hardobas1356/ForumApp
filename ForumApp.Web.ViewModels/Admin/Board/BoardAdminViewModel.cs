namespace ForumApp.Web.ViewModels.Admin.Board;

public class BoardAdminViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool IsApproved { get; set; }
    public bool IsDeleted { get; set; }
}
