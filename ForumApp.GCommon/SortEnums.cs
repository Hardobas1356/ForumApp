using System.Diagnostics;

namespace ForumApp.GCommon;

public static class SortEnums
{
    public static class Board
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

    public static class Post
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

    public static class Reply
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
