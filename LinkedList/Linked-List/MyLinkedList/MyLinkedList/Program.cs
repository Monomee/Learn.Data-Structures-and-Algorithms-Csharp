using System;
using System.Collections.Generic; // Cần thêm để dùng EqualityComparer nếu chưa có

namespace MyLinkedList
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
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

            Console.Write("Current List: ");
            list.Display(); // Expect: 5 10 15 20

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
            Console.Write("Removed First (5). List: ");
            list.Display(); // Expect: 10 15 20

            list.RemoveLast();
            Console.Write("Removed Last (20). List: ");
            list.Display(); // Expect: 10 15

            list.AddLast(100);
            list.AddLast(200); // List: 10 15 100 200
            list.RemoveAt(2); // Xóa số 100
            Console.Write("Removed At Index 2 (value 100). List: ");
            list.Display(); // Expect: 10 15 200

            // 4. TEST REMOVE BY VALUE & DUPLICATES (Xóa theo giá trị)
            PrintHeader("4. Test Remove By Value (Duplicates)");

            list.RemoveAll(); // Xóa sạch làm lại
            Console.WriteLine("Cleared list. Adding duplicates: 10, 20, 10, 30, 10");
            list.AddLast(10);
            list.AddLast(20);
            list.AddLast(10);
            list.AddLast(30);
            list.AddLast(10);
            Console.Write("Current List: ");
            list.Display(); // Expect: 10 20 10 30 10

            Console.WriteLine("\n--- RemoveFirst(10) ---");
            list.RemoveFirst(10); // Chỉ xóa số 10 đầu tiên
            Console.Write("List: ");
            list.Display(); // Expect: 20 10 30 10

            Console.WriteLine("\n--- RemoveAll(10) ---");
            list.RemoveAll(10); // Xóa hết các số 10 còn lại
            Console.Write("List: ");
            list.Display(); // Expect: 20 30

            // ===============================================
            // 6. TEST REPLACE METHODS (Kiểm tra hàm thay thế mới)
            PrintHeader("6. Test Replace Methods");

            // Chuẩn bị list mới: 10, 20, 30, 20, 40
            list.RemoveAll();
            list.AddLast(10);
            list.AddLast(20);
            list.AddLast(30);
            list.AddLast(20);
            list.AddLast(40);
            Console.Write("Starting List: ");
            list.Display(); // Expect: 10 20 30 20 40

            // 6.1. ReplaceAt(data, index)
            list.ReplaceAt(99, 0); // Thay 10 (index 0) bằng 99
            Console.Write("ReplaceAt(99, 0). List: ");
            list.Display(); // Expect: 99 20 30 20 40

            // 6.2. ReplaceFirst(_old, _new)
            list.ReplaceFirst(20, 55); // Thay 20 đầu tiên bằng 55
            Console.Write("ReplaceFirst(20, 55). List: ");
            list.Display(); // Expect: 99 55 30 20 40 (chỉ 20 đầu tiên bị thay)

            // 6.3. ReplaceAll(_old, _new)
            list.ReplaceAll(20, 77); // Thay tất cả 20 (chỉ còn 1) bằng 77
            Console.Write("ReplaceAll(20, 77). List: ");
            list.Display(); // Expect: 99 55 30 77 40

            // 6.4. Test ReplaceAt with Exception
            try
            {
                Console.WriteLine("\nAction: Try ReplaceAt(1, 10) (Index out of range)");
                list.ReplaceAt(1, 10);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Caught Error: {ex.Message}");
                Console.ResetColor();
            }

            // ===============================================

            // 5. TEST EXCEPTIONS (Test bắt lỗi)
            PrintHeader("5. Test Exceptions (Edge Cases)");
            // Giữ lại phần test Exception cũ để đảm bảo các hàm khác vẫn ổn

            // Hiện tại list có 5 phần tử. count=5. index hợp lệ là 0..4
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