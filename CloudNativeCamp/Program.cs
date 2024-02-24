using CloudNativeCamp.LinkedListImplementation.DoublyLinkedList;

const string base64Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
//SearchValueDemo();
//await ArrayExample();
//await LinkedListExample<SinglyLinkedListExp<int>>();
//await LinkedListExample<DoublyLinkedListExp<int>>();
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

