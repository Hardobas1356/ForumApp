namespace ForumApp.GCommon
{
    public static class ValidationConstants
    {
        public static class BoardConstants
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 130;
            public const int DescriptionMaxLength = 600;
            public const int DescriptionMinLength = 10;
        }
        public static class TagConstants
        {
            public const int NameMaxLength = 128;
            public const int ColorHexLength = 7;
            public const string ColorHexRegexValidation = "^#([0-9A-Fa-f]{6})$";
            public const string ColorHexDefaultValue = "#FFFFFF";
            public const string ColorHexError = "Color must be a valid hex code.";
        }
        public static class CategoryConstants
        {
            public const int NameMaxLength = 130;
            public const int ColorHexLength = 7;
            public const string ColorHexRegexValidation = "^#([0-9A-Fa-f]{6})$";
            public const string ColorHexDefaultValue = "#FFFFFF";
            public const string ColorHexError = "Color must be a valid hex code.";
        }
        public static class PostConstants
        {
            public const int ContentMaximumLength = 20000;
            public const int ContentMinimumLength = 10;

            public const int TitleMaximumLength = 128;
            public const int TitleMinimumLength = 3;

            public const string TitleRequiredErrorMessage = "Title is required";
            public const string ContentRequiredErrorMessage = "Content message is required";
            public const string TitleInvalidLengthErrorMessage = "Title length should be within 10 and 20000 characters";
            public const string ContentInvalidLengthErrorMessage = "Content length should be withing 3 and 128 characters";
        }

        public static class ReplyConstants
        {
            public const int ContentMinLength = 3;
            public const int ContentMaxLength = 150;
        }

        public static class ApplicationUserConstants
        {
            public const int DisplayNameMinLength = 3;
            public const int DisplayNameMaxLength = 80;
            public const string UserNameRegex = "^[a-zA-Z0-9_.-]+$";
            public const int UserNameMinLength = 3;
            public const int UserNameMaxLength = 50;
            public const int PasswordMinLength = 5;
            public const int PasswordMaxLength = 80;
        }
    }
}
