using CloudNativeCamp.KeyValuePairList;
using CloudNativeCamp.Tree;

#region Linked List
const string base64Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
//SearchValueDemo();
//await ArrayExample();
//await LinkedListExample<SinglyLinkedListExp<int>>();
//await LinkedListExample<DoublyLinkedListExp<int>>(); 
#endregion
//HashTest();
//HashTableTest();
BinaryTreeTest();
Console.ReadKey();

static async Task LinkedListExample<T>() where T : LinkedListExp<int>, new()
{
    var linkedList = await new T()
        .InsertLast(5)
        .InsertLast(10)
        .InsertLast(20)
        .InsertAfter(10, 15)
        .InsertBefore(5, 1)
        .InsertAfter(20, 30)
        .InsertBefore(30, 25)
        .DeleteNode(5)
        .Print();
    await Console.Out.WriteLineAsync($"Sum Of Data In Linked List Is {linkedList.Sum()}");
}
static async Task ArrayExample()
{
    var array = new int[5];
    Array.Fill(array, 15);
    var newArray = array.Resize(10);
    newArray.PrintArray();
    await Console.Out.WriteLineAsync($"{newArray.GetAt(10)}");
}
void SearchValueDemo()
{
    var base64SearchValues = SearchValues.Create(base64Chars);
    var exampleText = "asgdggdAh^hhhf=";
    Console.WriteLine(IsBase64(base64SearchValues, exampleText));
}


bool IsBase64(SearchValues<char> searchValue, string text)
{
    return text
        .AsSpan()
        .ContainsAnyExcept(searchValue) is false;
}
static void DictionaryTest()
{
    var dictionary = new CustomeDictionary<string, string>();
    dictionary.Print();

    dictionary.Set("Sinar", "sinar@gmail.com");
    dictionary.Set("Elvis", "elvis@gmail.com");
    dictionary.Print();

    dictionary.Set("Tane", "tane@gmail.com");
    dictionary.Set("Gerti", "gerti@gmail.com");
    dictionary.Set("Arist", "arist@gmail.com");


    dictionary.Print();

    Console.WriteLine(dictionary.Get("Tane"));
    Console.WriteLine(dictionary.Get("Sinar"));
    Console.WriteLine(dictionary.Get("Elviaaa"));

    dictionary.Remove("Sinar");
    dictionary.Remove("Elvis");
    dictionary.Remove("Tane");
    dictionary.Remove("Gerti");
    dictionary.Remove("Arist");
    dictionary.Print();
    dictionary.Set("Sinar", "sinar@gmail.com");
    dictionary.Print();
}

static void HashTest()
{
    Hash.GetHash32("This is Original Text");
    Hash.GetHash64("This is Original Text");
}

static void HashTableTest()
{
    var CutomeHashTable = new CustomHashTable<string, string>();
    CutomeHashTable.Print();
    CutomeHashTable.Set("Sinar", "sinar@gmail.com");
    CutomeHashTable.Set("Elvis", "elvis@gmail.com");
    CutomeHashTable.Set("Tane", "tane@gmail.com");
    CutomeHashTable.Print();
    Console.WriteLine("[get] " + CutomeHashTable.Get("Sinar"));
    //Console.WriteLine("[get] " + table.Get("Tane"));
    CutomeHashTable.Set("Gerti", "gerti@gmail.com");
    CutomeHashTable.Set("Arist", "arist@gmail.com");
    CutomeHashTable.Print();
    Console.WriteLine("[get] " + CutomeHashTable.Get("Sinar"));
}

static void BinaryTreeTest()
{
    var tree = new BinaryTree<char>();
    tree.Insert('A');
    tree.Insert('B');
    tree.Insert('C');
    tree.Insert('D');
    tree.Insert('E');
    tree.Insert('F');
    tree.Insert('G');
    tree.Insert('H');
    tree.Insert('I');
    tree.Print();

}
