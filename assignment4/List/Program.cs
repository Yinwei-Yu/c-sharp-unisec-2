using System;

public class MyLinkedList<T> where T : IComparable<T>
{
    private Node head;

    private class Node
    {
        public T Value { get; set; }
        public Node Next { get; set; }

        public Node(T value)
        {
            Value = value;
            Next = null;
        }
    }

    public void Add(T value)
    {
        Node newNode = new Node(value);
        if (head == null)
        {
            head = newNode;
        }
        else
        {
            Node current = head;
            while (current.Next != null)
            {
                current = current.Next;
            }
            current.Next = newNode;
        }
    }

    public void ForEach(Action<T> action)
    {
        Node current = head;
        while (current != null)
        {
            action(current.Value);
            current = current.Next;
        }
    }
}

internal class Program
{
    private static void Main()
    {
        var list = new MyLinkedList<int>();
        list.Add(10);
        list.Add(2);
        list.Add(15);
        list.Add(7);

        // 打印元素
        Console.Write("链表元素：");
        list.ForEach(x => Console.Write($"{x} "));
        Console.WriteLine();

        // 求最大值
        int max = int.MinValue;
        list.ForEach(x => { if (x.CompareTo(max) > 0) max = x; });
        Console.WriteLine($"最大值：{max}");

        // 求最小值
        int min = int.MaxValue;
        list.ForEach(x => { if (x.CompareTo(min) < 0) min = x; });
        Console.WriteLine($"最小值：{min}");

        // 求和
        int sum = 0;
        list.ForEach(x => sum += x);
        Console.WriteLine($"元素总和：{sum}");
    }
}