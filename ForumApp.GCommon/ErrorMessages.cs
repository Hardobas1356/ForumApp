namespace ForumApp.GCommon;

public static class ErrorMessages
{
    public static class ApplicationUser
    {
        public const string UserNameRegexError = "Handle can only contain letters, numbers, underscores, dots and dashes.";
        public const string PasswordMissmatchError = "The password and confirmation password do not match.";
    }
}
