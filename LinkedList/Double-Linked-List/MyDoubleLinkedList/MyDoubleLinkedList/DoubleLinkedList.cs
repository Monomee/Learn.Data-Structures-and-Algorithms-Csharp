namespace MyDoubleLinkedList
{
    internal class DoubleLinkedList<T> where T : IComparable<T>
    {
        private Node<T>? head;
        private Node<T>? tail;
        private int count;

        public DoubleLinkedList()
        {
            head = null;
            tail = null;
            count = 0;
        }

        // ========== Check ==========
        public bool IsEmpty()
        {
            return count == 0;
        }
        public int Count()
        {
            return count;
        }
        public bool Contains(T data)
        {
            if (IsEmpty()) return false;

            Node<T> current = head;
            while (current != null)
            {
                // Usage of EqualityComparer.Default is best practice for Generics comparison.
                if (EqualityComparer<T>.Default.Equals(data, current.Data))
                {
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        // ========== Add ==========
        public void AddLast(T data)
        {
            Node<T> newNode = new Node<T>(data);
            if (IsEmpty())
            {
                // Case 1: List is empty. Head and Tail point to the new node.
                head = newNode;
                tail = newNode;
            }
            else
            {
                // Case 2: List is not empty. Link new node to the current Tail.
                tail!.Next = newNode;
                newNode.Prev = tail; // Back-link from new node to old Tail.
                tail = newNode; // Update Tail reference.
            }
            count++;
        }
        public void AddFirst(T data)
        {
            Node<T> newNode = new Node<T>(data);
            if (IsEmpty())
            {
                // Case 1: List is empty. Head and Tail point to the new node.
                head = newNode;
                tail = newNode;
            }
            else
            {
                // Case 2: List is not empty. Link old Head to new node.
                head!.Prev = newNode; // Back-link from old Head to new node.
                newNode.Next = head;
                head = newNode; // Update Head reference.
            }
            count++;
        }
        public void AddAt(T data, int index)
        {
            if (index < 0 || index > count)
            {
                throw new Exception("Can not add at this index!");
            }
            // Handle edge cases efficiently using existing methods
            if (index == 0)
            {
                AddFirst(data);
                return;
            }
            if (index == count)
            {
                AddLast(data);
                return;
            }

            Node<T> newNode = new Node<T>(data);
            Node<T>? current = head; // Start at index 0

            // Traverse to the node at position (index - 1)
            for (int i = 0; i < index - 1; i++)
            {
                current = current!.Next;
            }

            // At this point: current is Node P (Previous), current.Next is Node N (Next).

            // 1. Link NewNode to Node N and vice versa.
            newNode.Next = current!.Next;
            current!.Next!.Prev = newNode; // Use '!' to assert current.Next is not null (since index < count).

            // 2. Link NewNode to Node P and vice versa.
            newNode.Prev = current;
            current.Next = newNode;

            count++;
        }

        // ========== Remove ==========
        public void RemoveAll()
        {
            // Dereferencing head and tail allows the Garbage Collector to reclaim memory.
            head = tail = null;
            count = 0;
        }
        public void RemoveFirst()
        {
            if (IsEmpty()) throw new Exception("Can not remove an empty linked-list!");

            // Move head to the next node.
            head = head!.Next;

            // If the list is now empty (old Head was also Tail).
            if (head == null) tail = null;
            // Otherwise, sever the back-link from the new Head.
            else head.Prev = null;

            count--;
        }
        public void RemoveLast()
        {
            if (IsEmpty()) throw new Exception("Can not remove an empty linked-list!");

            // Move tail to the previous node.
            tail = tail!.Prev;

            // If the list is now empty (old Tail was also Head).
            if (tail == null) head = null;
            // Otherwise, sever the forward-link from the new Tail.
            else tail.Next = null;

            count--;
        }
        public void RemoveAt(int index)
        {
            if (IsEmpty() || index < 0 || index >= count)
            {
                throw new Exception("Can not remove at this index!");
            }

            // Handle edge cases using existing O(1) methods.
            if (index == 0) { RemoveFirst(); return; }
            if (index == count - 1) { RemoveLast(); return; }

            Node<T>? current = head;
            // Traverse to the node to be removed (position 'index').
            for (int i = 0; i < index; i++)
            {
                current = current!.Next;
            }

            // Bypass the current node (O(1) operation).
            // Node P (current.Prev) points to Node N (current.Next).
            current!.Prev!.Next = current.Next;
            // Node N (current.Next) points back to Node P (current.Prev).
            current.Next!.Prev = current.Prev;

            count--;
        }
        public void RemoveAll(T data)
        {
            if (IsEmpty()) return;

            Node<T>? current = head;
            EqualityComparer<T> comparer = EqualityComparer<T>.Default;

            while (current != null)
            {
                // Save the next node BEFORE potential deletion and pointer change.
                Node<T>? nextNode = current.Next;

                if (comparer.Equals(current.Data, data))
                {
                    // Case 1: Node is Head
                    if (current == head)
                    {
                        head = head.Next;
                        if (head == null) tail = null;
                        else head.Prev = null;
                    }
                    // Case 2: Node is Tail
                    else if (current == tail)
                    {
                        tail = tail.Prev;
                        if (tail == null) head = null;
                        else tail.Next = null;
                    }
                    // Case 3: Node is in the middle
                    else
                    {
                        // Bypass current node (O(1) operation)
                        current.Prev!.Next = current.Next;
                        current.Next!.Prev = current.Prev;
                    }
                    count--;
                }

                // Move to the next saved node to continue the loop.
                current = nextNode;
            }
        }
        public void RemoveFirst(T data)
        {
            if (IsEmpty()) return;

            Node<T>? current = head;
            EqualityComparer<T> comparer = EqualityComparer<T>.Default;
            while (current != null)
            {
                if (comparer.Equals(current.Data, data))
                {
                    // Case 1: Node is Head
                    if (current == head)
                    {
                        head = head.Next;
                        if (head == null) tail = null;
                        else head.Prev = null;
                    }
                    // Case 2: Node is Tail
                    else if (current == tail)
                    {
                        tail = tail.Prev;
                        if (tail == null) head = null;
                        else tail.Next = null;
                    }
                    // Case 3: Node is in the middle
                    else
                    {
                        // Bypass current node (O(1) operation)
                        current.Prev!.Next = current.Next;
                        current.Next!.Prev = current.Prev;
                    }
                    count--;
                    return; // Stop after removing the first occurrence found.
                }
                current = current.Next;
            }
        }

        // ========== Replace ==========
        public void ReplaceAt(T data, int index)
        {
            if (IsEmpty() || index < 0 || index >= count)
            {
                throw new Exception("Can not replace at this index!");
            }
            // Get(index) takes O(n) or O(n/2) time to find the node, then O(1) to update.
            Node<T> target = Get(index);
            target.Data = data;
        }
        public void ReplaceAll(T _old, T _new)
        {
            if (IsEmpty()) return;
            Node<T>? current = head;
            while (current != null)
            {
                if (EqualityComparer<T>.Default.Equals(_old, current.Data))
                {
                    current.Data = _new;
                }
                current = current.Next;
            }
        }
        public void ReplaceFirst(T _old, T _new)
        {
            if (IsEmpty()) return;
            Node<T>? current = head;
            while (current != null)
            {
                if (EqualityComparer<T>.Default.Equals(_old, current.Data))
                {
                    current.Data = _new;
                    return; // Stop immediately after the first replacement.
                }
                current = current.Next;
            }
        }

        // ========== Traverse ==========
        public Node<T> Get(int index)
        {
            if (index < 0 || index >= count)
                throw new Exception("Index out of range!");

            Node<T>? current;

            // Optimization: Start from Head or Tail based on index proximity (O(n/2)).
            if (index < count / 2)
            {
                current = head;
                for (int i = 0; i < index; i++)
                {
                    current = current!.Next;
                }
            }
            // Traverse backwards from the Tail.
            else
            {
                current = tail;
                // Loop backward from count - 1 down to index.
                for (int i = count - 1; i > index; i--)
                {
                    current = current!.Prev;
                }
            }
            return current!; // Assert current is not null since index was validated.
        }
        public void Display()
        {
            if (IsEmpty())
            {
                Console.WriteLine("List is empty.");
                return;
            }
            Console.WriteLine("Double Linked-list:");
            Node<T>? current = head;
            int index = 0;
            while (current != null)
            {
                Console.WriteLine($"{index}. {current.Data}");
                current = current.Next;
                index++;
            }
        }
    }
}