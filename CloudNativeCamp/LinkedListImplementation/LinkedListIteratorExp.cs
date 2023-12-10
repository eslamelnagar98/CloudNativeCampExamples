namespace CloudNativeCamp.LinkedListImplementation;
internal struct LinkedListIteratorExp<T> : IEnumerator<LinkedListNodeExp<T>>
    where T : ISignedNumber<T>, ISpanParsable<T>, IMinMaxValue<T>
{
    public LinkedListNodeExp<T> Current => _currentNode;

    private LinkedListNodeExp<T> _currentNode;

    private bool _firstIteration = true;
    object IEnumerator.Current => Current;

    public LinkedListIteratorExp(LinkedListNodeExp<T> node)
    {
        _currentNode = node;
    }

    public bool MoveNext()
    {
        if (_currentNode is null)
        {
            return false;
        }

        if (!_firstIteration)
        {
            _currentNode = _currentNode?.Next;
        }
        else
        {
            _firstIteration = false;
        }

        return _currentNode is not null;
    }
    public void Dispose()
    {

    }
    public void Reset()
    {

    }
}

