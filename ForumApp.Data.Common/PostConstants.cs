namespace ForumApp.Data.Common;

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
