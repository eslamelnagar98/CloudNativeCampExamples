namespace CloudNativeCamp.LinkedListImplementation;
public struct LinkedListIteratorExp<T> : IEnumerator<LinkedListNodeExp<T>> where T : ISignedNumber<T>, ISpanParsable<T>, IMinMaxValue<T>
{
    private LinkedListNodeExp<T> _currentNode;
    private bool _firstIteration = true;
    public LinkedListIteratorExp(LinkedListNodeExp<T> node)
    {
        _currentNode = node;
    }

    public LinkedListNodeExp<T> Current => _currentNode;

    object IEnumerator.Current => Current;

    public bool MoveNext()
    {
        if (_currentNode is null)
        {
            return false;
        }
        if (!_firstIteration)
            _currentNode = _currentNode?.Next;

        _firstIteration = false;
        return _currentNode is not null;
    }
    public void Dispose()
    {

    }
    public void Reset()
    {

    }
}

