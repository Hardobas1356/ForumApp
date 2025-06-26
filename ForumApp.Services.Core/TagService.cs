using ForumApp.Data;
using ForumApp.Services.Core.Interfaces;
using ForumApp.Web.ViewModels.Tag;
using Microsoft.EntityFrameworkCore;

namespace ForumApp.Services.Core;

public class TagService : ITagService
{
    private readonly ForumAppDbContext dbContext;

    public TagService(ForumAppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<ICollection<TagViewModel>> GetTagsAsync()
    {
        ICollection<TagViewModel> tags = await dbContext
            .Tags
            .AsNoTracking()
            .Select(t => new TagViewModel
            {
                Id = t.Id,
                Name = t.Name,
                ColorHex = t.ColorHex,
            })
            .ToArrayAsync();

        return tags;
    }
}
