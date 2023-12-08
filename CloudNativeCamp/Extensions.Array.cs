namespace CloudNativeCamp;
public static partial class Extensions
{
    public static T[] Resize<T>(this T[] source, int newSize)
        where T : struct
    {
        if (newSize <= 0 || source is null)
        {
            return Array.Empty<T>();
        }
        if (source?.Length == newSize)
        {
            return source;
        }
        var newArray = new T[newSize];
        var sourceArrayByteLength = Buffer.ByteLength(source);
        Buffer.BlockCopy(source, 0, newArray, 0, sourceArrayByteLength);
        return newArray;
    }

    public static void PrintArray<T>(this T[] source)
         where T : struct
    {
        Console.WriteLine(JsonSerializer.Serialize(source));
    }

    public static T GetAt<T>(this T[] source, int index)
        where T : struct
    {

        if (index < 0)
        {
            return default;
        }
        ref var zeroAddress = ref MemoryMarshal.GetArrayDataReference((Array)source);
        ref var indexReference = ref Unsafe.Add(ref zeroAddress, index * Unsafe.SizeOf<T>());
        ref var result = ref Unsafe.As<byte, T>(ref indexReference);
        return result;
    }
}
