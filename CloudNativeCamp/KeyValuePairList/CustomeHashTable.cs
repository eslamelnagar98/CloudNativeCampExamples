namespace CloudNativeCamp.KeyValuePairList;
public sealed class CustomHashTable<TKey, TValue> where TKey : class
{
    private static readonly int InitialSize = 3;
    private int _entriesCount;
    private Entry[] _entries = new Entry[InitialSize];

    public void Set(TKey key, TValue value)
    {
        ResizeIfNeeded();
        AddToEntries(key, value);
    }

    public TValue Get(TKey key)
    {
        var index = FindEntryIndex(key);
        return index != -1 && _entries[index] is not null ? _entries[index].Value : default;
    }
    public void Print()
    {
        Console.WriteLine("-----------");
        Console.WriteLine($"[Size] {_entriesCount}");

        for (var i = 0; i < _entries.Length; i++)
        {
            var entry = _entries[i];
            var entryDisplay = entry is null
                ? "null"
                : $"{entry.Key}:{entry.Value}";

            Console.WriteLine($"[{i}] {entryDisplay}");
        }

        Console.WriteLine("============");
    }


    private void ResizeIfNeeded()
    {
        if (_entriesCount >= _entries.Length)
        {
            Resize();
        }
    }

    private void Resize()
    {
        var newSize = CalculateNewSize();
        LogResize(newSize);

        var entriesCopy = CopyEntriesToNewArray();
        ReinitializeEntries(newSize);

        ReinsertEntries(entriesCopy);
    }

    private int CalculateNewSize()
    {
        return _entries.Length * 2;
    }

    private void LogResize(int newSize)
    {
        Console.WriteLine($"[Resize] from {_entries.Length} to {newSize}");
    }

    private Entry[] CopyEntriesToNewArray()
    {
        var entriesCopy = new Entry[_entries.Length];
        Array.Copy(_entries, entriesCopy, entriesCopy.Length);
        return entriesCopy;
    }

    private void ReinitializeEntries(int newSize)
    {
        _entries = new Entry[newSize];
        _entriesCount = 0;
    }

    private void ReinsertEntries(Entry[] entriesCopy)
    {
        foreach (var entry in entriesCopy)
        {
            if (entry is not null)
            {
                AddToEntries(entry.Key, entry.Value); 
            }
        }
    }

    private void AddToEntries(TKey key, TValue value)
    {
        var hash = GetHash(key);

        if (IsCollision(hash, key))
        {
            hash = ResolveCollision(key, hash, true);
            if (hash == -1)
            {
                throw new InvalidOperationException("Failed to resolve collision: no available slot found.");
            }
        }

        if (_entries[hash] is null)
        {
            _entries[hash] = new Entry(key, value);
            _entriesCount++;
        }
        else
        {
            _entries[hash] = _entries[hash] with { Value = value };
        }
    }

    private int FindEntryIndex(TKey key, bool isInsertOperation = false)
    {
        var hash = GetHash(key);

        return _entries[hash] is null || _entries[hash].Key.Equals(key)
            ? hash
            : ResolveCollision(key, hash, isInsertOperation);
    }

    private int GetHash(TKey key, int length = 0)
    {
        var hashTableLength = length > 0 ? length : _entries.Length;
        var hashCode = Hash.GetHash32(key);
        var index = (int)(hashCode % (uint)hashTableLength);
        Console.WriteLine($"[Hash] For {key} Is {hashCode}, Hexa Representation Is {hashCode:x}, Index Is {index}");
        return index;
    }

    private int ResolveCollision(TKey key, int initialHash, bool isInsertOperation)
    {
        for (var i = 1; i < _entries.Length; i++)
        {
            var newHash = (initialHash + i) % _entries.Length;
            if (IsValidSlot(newHash, key, isInsertOperation))
            {
                return newHash;
            }
        }
        return -1;
    }

    private bool IsCollision(int hashCode, TKey key)
    {
        return _entries[hashCode] is not null && !_entries[hashCode].Key.Equals(key);
    }

    private bool IsValidSlot(int index, TKey key, bool isInsertOperation)
    {
        return isInsertOperation
            ? _entries[index] is null || _entries[index].Key.Equals(key)
            : _entries[index]?.Key.Equals(key) ?? false;
    }

    private record Entry(TKey Key, TValue Value);
}
