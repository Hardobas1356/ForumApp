﻿namespace ForumApp.Web.ViewModels.Reply;

public class ReplyDetailForPostDetailViewModel
{
    public Guid Id { get; set; }
    public string Content { get; set; } = null!;
    public string CreatedAt { get; set; } = null!;
    public string Author { get; set; } = null!;
    public bool IsPublisher { get; set; }
}
