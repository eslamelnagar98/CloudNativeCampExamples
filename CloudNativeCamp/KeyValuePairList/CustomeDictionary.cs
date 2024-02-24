namespace CloudNativeCamp.KeyValuePairList;
internal sealed class CustomeDictionary<TKey, TValue> where TKey : class
{
    private readonly int _initialSize = 5;
    private int _entriesCount;
    private int _capacity;
    private KeyValuePair[] _entries;

    public CustomeDictionary()
    {
        _entries = new KeyValuePair[_initialSize];
        _capacity = _initialSize;
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
            if (_entries[i] != null && _entries[i].Key == key)
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

    private class KeyValuePair
    {
        private readonly TKey _key;
        private readonly TValue _value;

        public KeyValuePair(TKey key, TValue value)
        {
            _key = key;
            _value = value;
        }

        public TKey Key => _key;
        public TValue Value { get; set; }
    }

}
