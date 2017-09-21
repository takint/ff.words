namespace ff.words.data.Common
{
    public class DbActionResponse
    {
        public static DbActionResponse Ok = new DbActionResponse { Success = true };
        public static DbActionResponse Fail = new DbActionResponse { Success = false };

        public DbActionResponse(bool success = false)
        {
            Success = success;
        }

        public bool Success { get; private set; }
    }
}