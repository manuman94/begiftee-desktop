namespace BeGiftee.Source.Services.Network
{
    public static class ApiEndpoints
    {
        // AUTH
        public const string Login = "auth/login";
        public const string Register = "auth/register";
        public const string RecoverPassword = "recover";

        // GIFT
        public const string GetAllMyGifts = "gift";
        public const string CreateGift = "gift";
        public const string EditGift = "gift/$1";
        public const string DeleteGift = "gift/$1";
    }
}
