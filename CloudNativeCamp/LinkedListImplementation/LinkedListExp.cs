namespace CloudNativeCamp.LinkedListImplementation;
internal abstract class LinkedListExp<T> : IEnumerable<LinkedListNodeExp<T>>
    where T : ISignedNumber<T>, ISpanParsable<T>, IMinMaxValue<T>
{
    private protected int _length;

    private protected LinkedListNodeExp<T> _head;

    private protected LinkedListNodeExp<T> _tail;

    public abstract LinkedListExp<T> InsertLast(T data);
    public abstract LinkedListExp<T> InsertAfter(T nodeData, T data);
    public abstract LinkedListExp<T> InsertBefore(T nodeData, T data);
    public abstract LinkedListExp<T> DeleteNode(T data);
    public IEnumerator<LinkedListNodeExp<T>> GetEnumerator()
    {
        var enumerator = new LinkedListIteratorExp<T>(_head);
        while (enumerator.MoveNext())
        {
            yield return enumerator.Current;
        }
    }
    public async Task<LinkedListExp<T>> Print()
    {
        var linkedListTraversal = this.Aggregate(new StringBuilder(), Action);
        await Console.Out.WriteLineAsync(linkedListTraversal);
        return this;
    }
    public T Sum() => this.Aggregate(T.Zero, (current, next) => current + next.Data);

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private StringBuilder Action(StringBuilder builder, LinkedListNodeExp<T> linkedListNode)
    {
        if (builder.Length is not default(short))
        {
            builder.Append(" =>");
            builder.Append(" ");
        }
        builder.Append(linkedListNode.Data);
        return builder;
    }

}
