namespace CloudNativeCamp.KeyValuePairList;
internal sealed class CustomeDictionary<TKey, TValue>() where TKey : class
{
    private static readonly int _initialSize = 5;
    private int _entriesCount;
    private int _capacity = _initialSize;
    private KeyValuePair[] _entries = new KeyValuePair[_initialSize];

    public void Set(TKey key, TValue value)
    {
        for (int i = 0; i < _entries.Length; i++)
        {
            if (_entries[i] is not null && _entries[i].Key == key)
            {
                _entries[i].Value = value;
                return;
            }
        }
        ResizeOrNot();
        var newPair = new KeyValuePair(key, value);
        _entries[_entriesCount] = newPair;
        _entriesCount++;
    }

    public void AddOrUpdate(TKey key, TValue value)
    {
        foreach (var entry in _entries)
        {
            if (entry is not null && entry.Key == key)
            {
                entry.Value = value;
                return;
            }
        }

        ResizeOrNot();
        _entries[_entriesCount] = new(key, value);
        _entriesCount++;
    }

    public TValue Get(TKey key)
    {
        foreach (var entry in _entries)
        {
            if (entry is not null && entry.Key == key)
            {
                return entry.Value;
            }
        }
        return default;
    }

    public bool Remove(TKey key)
    {
        for (int i = 0; i < _entries.Length; i++)
        {
            if (_entries[i] is not null && _entries[i].Key == key)
            {
                _entries[i] = _entries[_entriesCount - 1];
                _entries[_entriesCount - 1] = null;
                _entriesCount--;
                return true;
            }
        }
        return false;
    }


    public void Print()
    {
        Console.WriteLine("----------");
        Console.WriteLine("[size] " + Size());
        for (int i = 0; i < _entries.Length; i++)
        {
            if (_entries[i] == null)
            {
                Console.WriteLine("[" + i + "]");
                continue;
            }
            else
            {
                Console.WriteLine("[" + i + "]" + _entries[i].Key
                  + ":" + _entries[i].Value);
            }
        }
        Console.WriteLine("==========");
    }

    private void ResizeOrNot()
    {
        if (_entriesCount < _entries.Length - 1)
        {
            return;
        }

        _capacity = _capacity * 2;
        var newArray = new KeyValuePair[_capacity];
        Array.Copy(_entries, newArray, _entries.Length);
        _entries = newArray;
    }

    public int Size() => _entriesCount;

    private class KeyValuePair(TKey key, TValue value)
    {
        public TKey Key => key;
        public TValue Value { get; set; } = value;
    }
}
