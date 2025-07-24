using System.Diagnostics;

namespace ForumApp.GCommon.Enums;

public static class SortEnums
{
    public static class UserSort
    {
        public enum UserSortBy
        {
            JoinDateAsc,
            JoinDateDesc,
            UsernameAsc,
            UsernameDesc,
            EmailAsc,
            EmailDesc,
            IsDeletedFirst,
            IsModeratorFirst
        }
    }

    public static class BoardSort
    {
        public enum BoardAllSortBy
        {
            None,
            CreateTimeAscending,
            CreateTimeDescending,
            NameAscending,
            NameDescending,
            Popularity,
        }


    }

    public static class PostSort
    {
        public enum PostSortBy
        {
            Default,
            CreateTimeAscending,
            CreateTimeDescending,
            TitleAscending,
            TitleDescending,
            Popularity,
        }
    }

    public static class ReplySort
    {
        public enum ReplySortBy
        {
            Default,
            CreateTimeAscending,
            CreateTimeDescending,
            ContentAscending,
            ContentDescending,
        }
    }
}
