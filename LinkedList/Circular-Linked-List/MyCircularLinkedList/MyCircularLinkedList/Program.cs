using System;
using System.Collections.Generic;

namespace MyCircularLinkedList
{
    // Giả định class Node<T> đã có sẵn trong namespace này
    // internal class Node<T> { public T Data; public Node<T>? Next; public Node(T data) { Data = data; } }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("========== TEST CIRCULAR LINKED LIST ==========");

            CircularLinkedList<int> list = new CircularLinkedList<int>();

            // 1. TEST ADD METHODS (Thêm)
            PrintHeader("1. Add Methods (AddFirst, AddLast, AddAt)");

            Console.WriteLine($"List empty initially? {list.IsEmpty()}"); // Expect: True

            list.AddLast(10);  // List: 10
            list.AddLast(20);  // List: 10 -> 20
            list.AddFirst(5);  // List: 5 -> 10 -> 20

            // Chèn vào giữa
            list.AddAt(15, 2); // List: 5 -> 10 -> 15 -> 20

            // Chèn vào cuối bằng AddAt
            list.AddAt(25, 4); // List: 5 -> 10 -> 15 -> 20 -> 25

            Console.Write("List after additions: ");
            list.Display();
            // Expect: 0. 5 | 1. 10 | 2. 15 | 3. 20 | 4. 25 (Count = 5)

            // 2. TEST CHECK/TRAVERSE METHODS
            PrintHeader("2. Check and Traverse Methods");

            Console.WriteLine($"Count: {list.Count()}"); // Expect: 5
            Console.WriteLine($"Contains 15? {list.Contains(15)}"); // Expect: True
            Console.WriteLine($"Contains 99? {list.Contains(99)}"); // Expect: False
            Console.WriteLine($"Get(2) Data: {list.Get(2).Data}"); // Expect: 15

            // 3. TEST REPLACE METHODS
            PrintHeader("3. Replace Methods");

            // List hiện tại: 5, 10, 15, 20, 25

            list.ReplaceAt(99, 0); // Thay 5 (index 0) bằng 99
            Console.Write("ReplaceAt(99, 0): ");
            list.Display(); // Expect: 99, 10, 15, 20, 25

            // Thêm trùng lặp để test ReplaceAll/First
            list.AddLast(10);
            list.AddLast(10); // List: 99, 10, 15, 20, 25, 10, 10

            list.ReplaceFirst(10, 11);
            Console.Write("ReplaceFirst(10, 11): ");
            list.Display(); // Expect: 99, 11, 15, 20, 25, 10, 10 (chỉ 10 đầu tiên đổi)

            list.ReplaceAll(10, 50);
            Console.Write("ReplaceAll(10, 50): ");
            list.Display(); // Expect: 99, 11, 15, 20, 25, 50, 50 (hai số 10 cuối đổi thành 50)

            // 4. TEST REMOVE METHODS (Xóa)
            PrintHeader("4. Remove Methods");

            // List: 99, 11, 15, 20, 25, 50, 50 (Count = 7)

            // 4.1. RemoveAt (Xóa giữa)
            list.RemoveAt(2); // Xóa 15 (index 2)
            Console.Write("RemoveAt(2) - (15): ");
            list.Display(); // Expect: 99, 11, 20, 25, 50, 50

            // 4.2. RemoveFirst (Xóa đầu)
            list.RemoveFirst(); // Xóa 99
            Console.Write("RemoveFirst() - (99): ");
            list.Display(); // Expect: 11, 20, 25, 50, 50

            // 4.3. RemoveLast (Xóa cuối)
            list.RemoveLast(); // Xóa 50 cuối cùng
            Console.Write("RemoveLast() - (50): ");
            list.Display(); // Expect: 11, 20, 25, 50

            // 4.4. RemoveFirst(T data)
            list.RemoveFirst(50); // Xóa số 50 còn lại
            Console.Write("RemoveFirst(50): ");
            list.Display(); // Expect: 11, 20, 25

            // 4.5. RemoveAll(T data) - Logic quan trọng cần test kỹ
            list.AddLast(11);
            list.AddLast(11); // List: 11, 20, 25, 11, 11

            Console.Write("Before RemoveAll(11): ");
            list.Display();

            list.RemoveAll(11); // Xóa hết số 11 (đầu, giữa, cuối)
            Console.Write("After RemoveAll(11): ");
            list.Display(); // Expect: 20, 25 (Count = 2)

            // 5. TEST EDGE CASES (Trường hợp biên quan trọng với Circular List)
            PrintHeader("5. Edge Cases (Single Element & Empty List)");

            // 5.1. Xóa khi list chỉ còn 1 phần tử
            list.RemoveAll(20);
            list.RemoveAll(25); // List giờ rỗng

            list.AddLast(100); // List: 100 (Count = 1)
            Console.Write("Single element list: ");
            list.Display();

            list.RemoveLast(); // Hoặc RemoveFirst() đều phải xử lý last = null
            Console.Write("After removing single element: ");
            list.Display(); // Expect: List is empty.
            Console.WriteLine($"Is Empty? {list.IsEmpty()}");

            // 5.2. Exception Handling
            try
            {
                Console.WriteLine("Action: Try RemoveFirst() on empty list");
                list.RemoveFirst();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Caught Error: {ex.Message}");
                Console.ResetColor();
            }

            try
            {
                list.AddLast(1);
                Console.WriteLine("Action: Try Get(10) (Index out of range)");
                list.Get(10);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Caught Error: {ex.Message}");
                Console.ResetColor();
            }

            Console.WriteLine("\n========== END TEST ==========");
            Console.ReadKey();
        }

        static void PrintHeader(string title)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n--- {title} ---");
            Console.ResetColor();
        }
    }
}