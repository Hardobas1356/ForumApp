using ForumApp.Web.ViewModels.Tag;

namespace ForumApp.Services.Core.Interfaces;

public interface ITagService
{
    Task<ICollection<TagViewModel>> GetTagsAsync();
}
