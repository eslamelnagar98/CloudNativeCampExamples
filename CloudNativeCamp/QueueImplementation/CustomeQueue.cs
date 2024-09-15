namespace CloudNativeCamp.QueueImplementation;
public class CustomeQueue<Tdata>
{
    LinkedList<Tdata> _dataList = new();
    public void Enqueue(Tdata _data)
    {
        _dataList.AddLast(_data);
    }

    public Tdata Dequeue()
    {
        var nodeData = _dataList.First.Value;
        _dataList.RemoveFirst();
        return nodeData;
    }
    public Tdata Peek()
    {
        return _dataList.First.Value is null
            ? default
            : _dataList.First.Value;
    }
    public bool HasData() => _dataList.Count > 0;

    public int Size() => _dataList.Count;

}
