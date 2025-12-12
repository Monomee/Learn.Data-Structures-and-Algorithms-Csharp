using System;

namespace MyLinkedList
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("========== TEST LINKED LIST ==========");
            LinkedList<int> list = new LinkedList<int>();

            // 1. TEST ADD (Thêm)
            PrintHeader("1. Test Add Methods");

            Console.WriteLine($"Is Empty initially? {list.IsEmpty()}"); // Expect: True

            list.AddLast(10);
            list.AddLast(20);
            Console.WriteLine("Added Last (10, 20)");

            list.AddFirst(5);
            Console.WriteLine("Added First (5)");

            list.AddAt(15, 2); // Chèn 15 vào index 2
            Console.WriteLine("Added At Index 2 (15)");

            list.Display();
            // Expect: 0. 5 | 1. 10 | 2. 15 | 3. 20

            // 2. TEST INFO (Kiểm tra thông tin)
            PrintHeader("2. Test Check/Get Methods");

            Console.WriteLine($"Count: {list.Count()}"); // Expect: 4
            Console.WriteLine($"Contains 15? {list.Contains(15)}"); // Expect: True
            Console.WriteLine($"Contains 99? {list.Contains(99)}"); // Expect: False

            var node = list.Get(2);
            Console.WriteLine($"Get(2) Data: {node.Data}"); // Expect: 15

            // 3. TEST REMOVE BY INDEX (Xóa theo vị trí)
            PrintHeader("3. Test Remove By Index/Position");

            list.RemoveFirst();
            Console.WriteLine("Removed First:");
            list.Display(); // Expect: 10, 15, 20

            list.RemoveLast();
            Console.WriteLine("Removed Last:");
            list.Display(); // Expect: 10, 15

            list.AddLast(100);
            list.AddLast(200); // List: 10, 15, 100, 200
            list.RemoveAt(2); // Xóa số 100
            Console.WriteLine("Removed At Index 2 (value 100):");
            list.Display(); // Expect: 10, 15, 200

            // 4. TEST REMOVE BY VALUE & DUPLICATES (Xóa theo giá trị)
            PrintHeader("4. Test Remove By Value (Duplicates)");

            list.RemoveAll(); // Xóa sạch làm lại
            Console.WriteLine("Cleared list. Adding duplicates: 10, 20, 10, 30, 10");
            list.AddLast(10);
            list.AddLast(20);
            list.AddLast(10);
            list.AddLast(30);
            list.AddLast(10);
            list.Display();

            Console.WriteLine("\n--- RemoveFirst(10) ---");
            list.RemoveFirst(10); // Chỉ xóa số 10 đầu tiên
            list.Display();
            // Expect: 20, 10, 30, 10

            Console.WriteLine("\n--- RemoveAll(10) ---");
            list.RemoveAll(10); // Xóa hết các số 10 còn lại
            list.Display();
            // Expect: 20, 30

            // 5. TEST EXCEPTIONS (Test bắt lỗi)
            PrintHeader("5. Test Exceptions (Edge Cases)");

            try
            {
                Console.WriteLine("Action: Try Get(100) (Index out of range)");
                list.Get(100);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Caught Error: {ex.Message}");
                Console.ResetColor();
            }

            try
            {
                LinkedList<int> emptyList = new LinkedList<int>();
                Console.WriteLine("Action: Try RemoveFirst() on empty list");
                emptyList.RemoveFirst();
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

        // Hàm phụ để in tiêu đề cho đẹp
        static void PrintHeader(string title)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n--- {title} ---");
            Console.ResetColor();
        }
    }
}
