namespace CloudNativeCamp.LinkedListImplementation;
public class LinkedListExp<T> : IEnumerable<LinkedListNodeExp<T>> where T : ISignedNumber<T>, ISpanParsable<T>, IMinMaxValue<T>
{
    private int _length;
    private LinkedListNodeExp<T> _head;
    private LinkedListNodeExp<T> _tail;
    public LinkedListExp<T> InsertLast(T data)
    {
        var linkedListNode = new LinkedListNodeExp<T>(data);
        if (_head is null)
        {
            return AssignNodeToEmptyLinkedList(linkedListNode);
        }
        return InsertLastInternal(linkedListNode);
    }

    public LinkedListExp<T> InsertAfter(T nodeData, T data)
    {
        var existsNode = Find(nodeData);
        if (existsNode is null)
        {
            return this;
        }
        var linkedListNode = new LinkedListNodeExp<T>(data)
        {
            Next = existsNode.Next
        };
        existsNode.Next = linkedListNode;
        if (linkedListNode.Next is null)
        {
            _tail = linkedListNode;
        }
        _length++;
        return this;
    }

    public LinkedListExp<T> InsertBefore(T nodeData, T data)
    {
        var nextNode = Find(nodeData);
        var linkedListNode = new LinkedListNodeExp<T>(data) { Next = nextNode };
        var parentNode = FindParent(nextNode);
        if (parentNode is null)
        {
            _head = linkedListNode;
            _length++;
            return this;
        }
        parentNode.Next = linkedListNode;
        if (nextNode.Next is null)
        {
            _tail = nextNode;
        }
        _length++;
        return this;
    }

    public LinkedListExp<T> DeleteNode(T data)
    {
        var nodeToBeDeleted = Find(data);
        return nodeToBeDeleted is null ? this : DeleteNode(nodeToBeDeleted);
    }
    public async Task<LinkedListExp<T>> Print()
    {
        var linkedListTraversal = this.Aggregate(new StringBuilder(), Action);
        await Console.Out.WriteLineAsync(linkedListTraversal);
        return this;
    }

    public T Sum() => this.Aggregate(T.Zero, (current, next) => current + next.Data);

    private LinkedListExp<T> DeleteNode(LinkedListNodeExp<T> nodeToBeDeleted)
    {
        if (_head == _tail)
        {
            return ResetLinkedList();
        }

        if (_head == nodeToBeDeleted)
        {
            return DeleteFirstElementInLinkedList(nodeToBeDeleted);
        }
        return DeleteNodeInsideLinkedList(nodeToBeDeleted);

    }

    private LinkedListExp<T> ResetLinkedList()
    {
        _head = null;
        _tail = null;
        _length--;
        return this;
    }

    private LinkedListExp<T> DeleteFirstElementInLinkedList(LinkedListNodeExp<T> nodeToBeDeleted)
    {
        _head = nodeToBeDeleted.Next;
        _length--;
        return this;
    }

    private LinkedListExp<T> DeleteNodeInsideLinkedList(LinkedListNodeExp<T> nodeToBeDeleted)
    {
        var parentNode = FindParent(nodeToBeDeleted);
        if (_tail == nodeToBeDeleted)
        {
            _tail = parentNode;
            _length--;
            return this;
        }

        parentNode.Next = nodeToBeDeleted.Next;
        _length--;
        return this;
    }
    private LinkedListNodeExp<T> FindParent(LinkedListNodeExp<T> nextNode)
    {
        if (nextNode is null)
        {
            return null;
        }
        foreach (var linkedListNode in this)
        {
            if (linkedListNode.Next == nextNode)
            {
                return linkedListNode;
            }
        }

        return null;
    }

    private LinkedListNodeExp<T> Find(T data)
    {
        foreach (var linkedListNodeValue in this)
        {
            if (linkedListNodeValue.Data == data)
            {
                return linkedListNodeValue;
            }
        }
        return null;
    }

    private LinkedListExp<T> AssignNodeToEmptyLinkedList(LinkedListNodeExp<T> linkedListNode)
    {
        _head = linkedListNode;
        _tail = linkedListNode;
        _length++;
        return this;
    }

    private LinkedListExp<T> InsertLastInternal(LinkedListNodeExp<T> linkedListNode)
    {
        _tail.Next = linkedListNode;
        _tail = linkedListNode;
        _length++;
        return this;
    }

    public IEnumerator<LinkedListNodeExp<T>> GetEnumerator()
    {
        return new LinkedListIteratorExp<T>(_head);
    }

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
