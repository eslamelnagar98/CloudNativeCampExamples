namespace CloudNativeCamp.KeyValuePairList;
public static class Hash
{
    private const uint OffsetBasis32 = 2166136261;
    private const uint FNVPrime32 = 16777619;
    private const ulong OffsetBasis64 = 14695981039346656037;
    private const ulong FNVPrime64 = 1099511628211;

    public static uint GetHash32<T>(T key)
    {
        if (key == null) throw new ArgumentNullException(nameof(key));

        var byteArray = Encoding.UTF8.GetBytes(key.ToString());
        return ComputeHash32(byteArray);
    }

    public static ulong GetHash64(string value)
    {
        if (string.IsNullOrEmpty(value)) throw new ArgumentException("Value cannot be null or empty.", nameof(value));

        var byteArray = Encoding.ASCII.GetBytes(value);
        return ComputeHash64(byteArray);
    }

    private static uint ComputeHash32(byte[] byteArray)
    {
        uint hash = OffsetBasis32;
        foreach (var b in byteArray)
        {
            hash ^= b;
            hash *= FNVPrime32;
        }
        Console.WriteLine($"{BitConverter.ToString(byteArray)}, {hash}, {hash:x}");
        return hash;
    }

    private static ulong ComputeHash64(byte[] byteArray)
    {
        ulong hash = OffsetBasis64;
        foreach (var b in byteArray)
        {
            hash ^= b;
            hash *= FNVPrime64;
        }
        Console.WriteLine($"{BitConverter.ToString(byteArray)}, {hash}, {hash:x}");
        return hash;
    }
}
