namespace MyDoubleLinkedList
{
    // Giả định bạn có class Node<T> trong cùng namespace
    // internal class Node<T> { public T Data; public Node<T>? Prev; public Node<T>? Next; public Node(T data) { Data = data; } }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("========== TEST DOUBLE LINKED LIST ==========");

            // Sử dụng kiểu int, vốn đã implement IComparable<int>
            DoubleLinkedList<int> list = new DoubleLinkedList<int>();

            // 1. TEST ADD METHODS (Thêm)
            PrintHeader("1. Add Methods (AddFirst, AddLast, AddAt)");

            Console.WriteLine($"List empty initially? {list.IsEmpty()}"); // Expect: True

            list.AddLast(10);
            list.AddLast(30);
            list.AddFirst(5);
            list.AddAt(20, 2); // Chèn 20 vào index 2 (giữa 10 và 30)
            list.AddAt(40, 4); // Chèn 40 vào index 4 (cuối list, gọi AddLast)

            Console.Write("List after additions: ");
            list.Display();
            // Expect: 0. 5 | 1. 10 | 2. 20 | 3. 30 | 4. 40 (Count = 5)

            // 2. TEST CHECK/TRAVERSE METHODS
            PrintHeader("2. Check and Traverse Methods (Count, Contains, Get)");

            Console.WriteLine($"Count: {list.Count()}"); // Expect: 5
            Console.WriteLine($"Contains 20? {list.Contains(20)}"); // Expect: True
            Console.WriteLine($"Get(4) Data (Optimized Tail Access): {list.Get(4).Data}"); // Expect: 40

            // 3. TEST REPLACE METHODS
            PrintHeader("3. Replace Methods");

            // List hiện tại: 5, 10, 20, 30, 40

            // 3.1. ReplaceAt(data, index)
            list.ReplaceAt(99, 0); // Thay 5 (index 0) bằng 99
            Console.Write("ReplaceAt(99, 0): ");
            list.Display(); // Expect: 99, 10, 20, 30, 40

            // Thêm giá trị trùng lặp để test ReplaceAll/First
            list.AddLast(10);
            list.AddLast(10); // List: 99, 10, 20, 30, 40, 10, 10

            // 3.2. ReplaceFirst(_old, _new)
            list.ReplaceFirst(10, 11);
            Console.Write("ReplaceFirst(10, 11): ");
            list.Display(); // Expect: 99, 11, 20, 30, 40, 10, 10 (chỉ 10 đầu tiên đổi thành 11)

            // 3.3. ReplaceAll(_old, _new)
            list.ReplaceAll(10, 50);
            Console.Write("ReplaceAll(10, 50): ");
            list.Display(); // Expect: 99, 11, 20, 30, 40, 50, 50 (hai số 10 cuối đổi thành 50)

            // 4. TEST REMOVE METHODS (Xóa)
            PrintHeader("4. Remove Methods (Position and Value)");

            // List: 99, 11, 20, 30, 40, 50, 50 (Count = 7)

            // 4.1. RemoveAt(index) - Xóa ở giữa
            list.RemoveAt(2); // Xóa 20 (index 2)
            Console.Write("RemoveAt(2) - (20): ");
            list.Display(); // Expect: 99, 11, 30, 40, 50, 50 (Count = 6)

            // 4.2. RemoveFirst()
            list.RemoveFirst(); // Xóa 99
            Console.Write("RemoveFirst() - (99): ");
            list.Display(); // Expect: 11, 30, 40, 50, 50 (Count = 5)

            // 4.3. RemoveLast()
            list.RemoveLast(); // Xóa 50 cuối
            Console.Write("RemoveLast() - (50): ");
            list.Display(); // Expect: 11, 30, 40, 50 (Count = 4)

            // 4.4. RemoveFirst(T data)
            list.RemoveFirst(50); // Xóa 50 còn lại
            Console.Write("RemoveFirst(50): ");
            list.Display(); // Expect: 11, 30, 40 (Count = 3)

            // 4.5. RemoveAll(T data)
            list.AddLast(11);
            list.AddLast(11); // List: 11, 30, 40, 11, 11 (Count = 5)
            list.RemoveAll(11); // Xóa hết 11
            Console.Write("RemoveAll(11): ");
            list.Display(); // Expect: 30, 40 (Count = 2)

            // 4.6. Test RemoveAll on a list with only one type (Should result in empty list)
            list.RemoveAll();
            list.AddLast(5);
            list.RemoveAll(5);
            Console.Write("RemoveAll(5) on a single-node list: ");
            list.Display(); // Expect: List is empty.

            // 5. TEST EXCEPTIONS (Kiểm tra bắt lỗi)
            PrintHeader("5. Test Exceptions (Accessing outside boundaries)");

            // List hiện tại: Empty (Count = 0)

            // 5.1. RemoveFirst on Empty List
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

            // 5.2. Get(index) out of range
            list.AddLast(1); // List: 1
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

            // 5.3. AddAt out of range
            try
            {
                Console.WriteLine("Action: Try AddAt(1, -1) (Index out of range)");
                list.AddAt(1, -1);
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