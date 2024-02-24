namespace CloudNativeCamp.LinkedListImplementation.DoublyLinkedList;
internal sealed class DoublyLinkedListExp<T> : LinkedListExp<T>
       where T : struct, INumber<T>, ISignedNumber<T>, ISpanParsable<T>, IMinMaxValue<T>
{
    public override DoublyLinkedListExp<T> InsertLast(T data)
    {
        var linkedListNode = new LinkedListNodeExp<T>(data);
        if (_head is null)
        {
            return AssignNodeToEmptyLinkedList(linkedListNode);
        }
        return InsertLastInternal(linkedListNode);
    }

    public override DoublyLinkedListExp<T> InsertAfter(T nodeData, T data)
    {
        var existsNode = Find(nodeData);
        if (existsNode is null)
        {
            return this;
        }
        var linkedListNode = new LinkedListNodeExp<T>(data)
        {
            Next = existsNode.Next,
            Back = existsNode
        };
        existsNode.Next = linkedListNode;
        if (linkedListNode.Next is null)
        {
            _tail = linkedListNode;
        }
        else
        {
            linkedListNode.Next.Back = linkedListNode;
        }
        _length++;
        return this;
    }

    public override DoublyLinkedListExp<T> InsertBefore(T nodeData, T data)
    {
        var nextNode = Find(nodeData);
        var parentNode = nextNode.Back;
        var linkedListNode = new LinkedListNodeExp<T>(data);
        if (parentNode is null)
        {
            _head = linkedListNode;
            _length++;
            return this;
        }
        parentNode.Next = linkedListNode;
        parentNode.Next.Back = parentNode;
        if (nextNode.Next is null)
        {
            _tail = nextNode;
        }
        nextNode.Back = linkedListNode;
        nextNode.Back.Next = nextNode;
        _length++;
        return this;
    }

    public override DoublyLinkedListExp<T> DeleteNode(T data)
    {
        var nodeToBeDeleted = Find(data);
        return nodeToBeDeleted is null ? this : DeleteNode(nodeToBeDeleted);
    }


    private DoublyLinkedListExp<T> DeleteNode(LinkedListNodeExp<T> nodeToBeDeleted)
    {
        if (_head == _tail)
        {
            return ResetLinkedList();
        }

        if (nodeToBeDeleted.Back is null)
        {
            return DeleteFirstElementInLinkedList(nodeToBeDeleted);
        }

        if (nodeToBeDeleted.Next is null)
        {
            DeleteLatElementInLinkedList(nodeToBeDeleted);
        }
        return DeleteNodeInsideLinkedList(nodeToBeDeleted);
    }

    private DoublyLinkedListExp<T> ResetLinkedList()
    {
        _head = null;
        _tail = null;
        _length--;
        return this;
    }

    private DoublyLinkedListExp<T> DeleteFirstElementInLinkedList(LinkedListNodeExp<T> nodeToBeDeleted)
    {
        _head = nodeToBeDeleted.Next;
        nodeToBeDeleted.Next.Back = null;
        _length--;
        return this;
    }

    private DoublyLinkedListExp<T> DeleteLatElementInLinkedList(LinkedListNodeExp<T> nodeToBeDeleted)
    {
        _tail = nodeToBeDeleted;
        nodeToBeDeleted.Back.Next = null;
        _length--;
        return this;
    }

    private DoublyLinkedListExp<T> DeleteNodeInsideLinkedList(LinkedListNodeExp<T> nodeToBeDeleted)
    {
        var parentNode = nodeToBeDeleted.Back;
        parentNode.Next = nodeToBeDeleted.Next;
        nodeToBeDeleted.Next.Back = parentNode;
        _length--;
        return this;
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

    private DoublyLinkedListExp<T> AssignNodeToEmptyLinkedList(LinkedListNodeExp<T> linkedListNode)
    {
        _head = linkedListNode;
        _tail = linkedListNode;
        _length++;
        return this;
    }

    private DoublyLinkedListExp<T> InsertLastInternal(LinkedListNodeExp<T> linkedListNode)
    {
        linkedListNode.Back = _tail;
        _tail.Next = linkedListNode;
        _tail = linkedListNode;
        _length++;
        return this;
    }

}
