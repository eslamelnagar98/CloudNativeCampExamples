namespace CloudNativeCamp;
public class CustomeArray
{
    public void Resize<T>(ref T[] source, int newSize)
        where T : struct
    {
        if (newSize <= 0 || source is null || source?.Length == newSize)
        {
            return;
        }

        var newArray = new T[newSize];
        var sourceArrayByteLength = Buffer.ByteLength(source);
        Buffer.BlockCopy(source, 0, newArray, 0, sourceArrayByteLength);

    }
}
