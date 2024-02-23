namespace CloudNativeCamp;
using System;
public delegate void SomethingHappenedDelegate(object sender, EventArgs e);
public class DelegatePublisher
{
    public event SomethingHappenedDelegate somethingHappenedDelegate;
    public void DoSomething()
    {
        Console.WriteLine("Something is happening...");
        // Check if there are subscribers before invoking the delegate
        somethingHappenedDelegate?.Invoke(this, EventArgs.Empty);
    }
}

// Subscriber class
public class DelegateSubscriber
{
    // Delegate handler method
    public void HandleEvent(object sender, EventArgs e)
    {
        Console.WriteLine($"DelegateSubscriber: Something happened! Sender: {sender}");
    }
}

internal class Program
{
    private static void Main()
    {
        // Create instances of the publisher and subscribers
        DelegatePublisher publisher = new DelegatePublisher();
        DelegateSubscriber subscriber1 = new DelegateSubscriber();
        DelegateSubscriber subscriber2 = new DelegateSubscriber();

        publisher.somethingHappenedDelegate += subscriber1.HandleEvent;
        publisher.somethingHappenedDelegate += subscriber2.HandleEvent;


        // Trigger the delegate
        publisher.DoSomething();
    }
}

