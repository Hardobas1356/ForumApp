using ForumApp.Data.Models;
using ForumApp.Services.Core.Interfaces;
using ForumApp.Web.ViewModels.Tag;

namespace ForumApp.Services.Core;

public class TagService : ITagService
{
    private readonly IGenericRepository<Tag> repository;

    public TagService(IGenericRepository<Tag> repository)
    {
        this.repository = repository;
    }

    public async Task<ICollection<TagViewModel>> GetTagsAsync()
    {
        IEnumerable<Tag> tags = await repository
            .GetAllAsync(true);

        ICollection<TagViewModel> result = tags
            .Select(t => new TagViewModel
            {
                Id = t.Id,
                Name = t.Name,
                ColorHex = t.ColorHex,
            })
            .ToArray();

        return result;
    }
}
