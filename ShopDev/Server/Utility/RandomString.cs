namespace ShopDev.Server.Utility;

public static class RandomString
{
    private static Random random = new Random();

    public static string Get(int length)
    {
        const string chars = "+-@!?ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}