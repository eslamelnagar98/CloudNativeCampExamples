namespace CloudNativeCamp.LinkedListImplementation;
internal sealed class LinkedListNodeExp<T>
        where T : struct, INumber<T>, ISignedNumber<T>, ISpanParsable<T>, IMinMaxValue<T>
{
    public T Data { get; set; }
    public LinkedListNodeExp<T> Next { get; set; }
    public LinkedListNodeExp<T> Back { get; set; }
    public LinkedListNodeExp(T data)
    {
        Data = data;
    }
}
