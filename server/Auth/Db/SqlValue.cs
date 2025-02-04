namespace Auth.Db
{
    public static class SqlValue
    {
        public const string UtcNow = "timezone('utc'::text, now())";
        public const string RandomText = "md5(random()::text)";
    }
}