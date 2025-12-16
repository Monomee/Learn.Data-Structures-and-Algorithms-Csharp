namespace MyLinkedList
{
    public class LinkedList<T> where T : IComparable<T>
    {
        private Node<T>? head;
        private Node<T>? tail;
        private int count;

        public LinkedList()
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
                // Usage of EqualityComparer.Default is best practice for Generics.
                // It handles null checks and value comparison correctly for type T.
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
                // If list is empty, head and tail refer to the same single node.
                head = newNode;
                tail = newNode;
            }
            else
            {
                // Link current tail to new node, then update tail reference.
                tail.Next = newNode;
                tail = newNode;
            }
            count++;
        }

        public void AddFirst(T data)
        {
            Node<T> newNode = new Node<T>(data);
            if (IsEmpty())
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                // New node points to old head, then head reference moves to new node.
                newNode.Next = head;
                head = newNode;
            }
            count++;
        }

        public void AddAt(T data, int index)
        {
            if (index < 0 || index > count) throw new Exception("Index out of range!");

            // Handle edge cases efficiently to avoid traversing.
            if (index == 0) { AddFirst(data); return; }
            if (index == count) { AddLast(data); return; }

            Node<T> newNode = new Node<T>(data);
            Node<T> current = head;

            // Stop at (index - 1). We need the node immediately BEFORE the insertion point.
            for (int k = 0; k < index - 1; k++)
            {
                current = current.Next;
            }

            // CRITICAL: Order matters here to prevent losing the list reference.
            // 1. Point new node to the rest of the list.
            newNode.Next = current.Next;
            // 2. Link the previous node to the new node.
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

        public void RemoveLast()
        {
            if (IsEmpty()) throw new Exception("Can not remove an empty linked-list!");

            // Special case: List has only one element.
            if (head == tail)
            {
                head = tail = null;
                count--;
                return;
            }

            // Traversal: Find the second-to-last node (the new tail).
            Node<T> current = head;
            Node<T> previous = null;
            while (current != tail)
            {
                previous = current;
                current = current.Next;
            }

            // Remove reference to the old tail.
            previous.Next = null;
            tail = previous;
            count--;
        }

        public void RemoveFirst()
        {
            if (IsEmpty()) throw new Exception("Can not remove an empty linked-list!");

            head = head.Next;
            // If the list becomes empty after removal, ensure tail is also null.
            if (head == null)
            {
                tail = null;
            }
            count--;
        }

        public void RemoveAt(int index)
        {
            if (IsEmpty() || index < 0 || index >= count)
            {
                throw new Exception("Can not remove at this index!");
            }

            if (index == 0) { RemoveFirst(); return; }
            if (index == count - 1) { RemoveLast(); return; }

            Node<T> previous = head;

            // Traverse to the node immediately BEFORE the one we want to remove.
            for (int k = 0; k < index - 1; k++)
            {
                previous = previous.Next;
            }

            // "Skip" the target node by linking previous directly to next.next.
            // The target node is now unreachable and will be garbage collected.
            previous.Next = previous.Next.Next;
            count--;
        }

        public void RemoveAll(T data)
        {
            if (IsEmpty()) return;

            // STRATEGY: Use a "Dummy Node" (Sentinel Node).
            // This simplifies logic by treating the Head like any other node (having a previous node).
            Node<T> dummy = new Node<T>(default(T));
            dummy.Next = head;

            Node<T> previous = dummy;
            Node<T> current = head;
            EqualityComparer<T> comparer = EqualityComparer<T>.Default;

            while (current != null)
            {
                if (comparer.Equals(data, current.Data))
                {
                    // Found match: bypass the current node.
                    previous.Next = current.Next;

                    // If we removed the last node, we must update the Tail reference.
                    // If 'previous' is dummy, it means the list is now empty, so tail becomes null.
                    if (current == tail)
                    {
                        tail = previous == dummy ? null : previous;
                    }

                    count--;
                    // Move 'current' forward, but keep 'previous' where it is 
                    // (because 'previous' is now pointing to a new 'next' that hasn't been checked yet).
                    current = current.Next;
                }
                else
                {
                    // No match: advance both pointers.
                    previous = current;
                    current = current.Next;
                }
            }

            // Restore the real Head from the dummy node.
            head = dummy.Next;

            // Safety check: if all items were removed, ensure Tail is null.
            if (head == null) tail = null;
        }

        public void RemoveFirst(T data)
        {
            if (IsEmpty()) throw new Exception("Can not remove an empty linked-list!");

            Node<T> current = head;
            Node<T> previous = null;

            while (current != null)
            {
                if (EqualityComparer<T>.Default.Equals(data, current.Data))
                {
                    // Case 1: The node to remove is the Head.
                    if (current == head)
                    {
                        head = head.Next;
                        // If list becomes empty, update tail.
                        if (head == null) tail = null;

                        count--;
                        return;
                    }

                    // Case 2: The node is in the middle or end.
                    previous.Next = current.Next;

                    // If we removed the Tail, update the Tail reference to the previous node.
                    if (current == tail)
                        tail = previous;

                    count--;
                    return;
                }

                previous = current;
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
            Node<T> target = Get(index);
            target.Data = data;
        }
        public void ReplaceAll(T _old, T _new)
        {
            if (IsEmpty()) return;
            Node<T> current = head;
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
            Node<T> current = head;
            while (current != null)
            {
                if (EqualityComparer<T>.Default.Equals(_old, current.Data))
                {
                    current.Data = _new;
                    return;
                }
                current = current.Next;
            }
        }

        // ========== Traverse ==========
        public Node<T> Get(int index)
        {
            // Validate index range (0 to count - 1).
            if (index < 0 || index >= count)
            {
                throw new Exception("Can not get at this index!");
            }

            Node<T> current = head;
            // Simple traversal to the specific index.
            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }
            return current;
        }

        public void Display()
        {
            if (IsEmpty())
            {
                Console.WriteLine("List is empty.");
                return;
            }
            Console.WriteLine("Linked-list:");
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
