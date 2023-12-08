//await ArrayExample();
await LinkedListExample();
Console.ReadKey();

static async Task LinkedListExample()
{
    var linkedList = await new LinkedListExp<int>()
        .InsertLast(5)
        .InsertLast(10)
        .InsertLast(20)
        .InsertAfter(10, 15)
        .InsertBefore(5, 1)
        .InsertAfter(20, 30)
        .InsertBefore(30, 25)
        .DeleteNode(5)
        .Print();
    await Console.Out.WriteLineAsync($"{linkedList.Sum()}");
}
static async Task ArrayExample()
{
    var array = new int[5];
    Array.Fill(array, 15);
    var newArray = array.Resize(10);
    newArray.PrintArray();
    await Console.Out.WriteLineAsync($"{newArray.GetAt(10)}");
}

