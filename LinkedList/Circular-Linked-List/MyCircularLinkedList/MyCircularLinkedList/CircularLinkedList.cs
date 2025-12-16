using System;
using System.Collections.Generic;

namespace MyCircularLinkedList
{
    internal class CircularLinkedList<T> where T : IComparable<T>
    {
        private Node<T>? last;
        private int count;

        public CircularLinkedList()
        {
            last = null;
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

            Node<T> current = last!;
            do
            {
                // Usage of EqualityComparer.Default is best practice for Generics.
                if (EqualityComparer<T>.Default.Equals(data, current.Data)) return true;
                current = current.Next!;
            } while (current != last);
            return false;
        }

        // ========== Add ==========
        public void AddLast(T data)
        {
            Node<T> newNode = new Node<T>(data);
            if (IsEmpty())
            {
                // List is empty. New node points to itself.
                last = newNode;
                newNode.Next = last;
            }
            else
            {
                // List is not empty.
                // 1. New node points to Head (last.Next).
                newNode.Next = last!.Next;
                // 2. Old Last points to New node.
                last.Next = newNode;
                // 3. Update Last to be the New node.
                last = newNode;
            }
            count++;
        }
        public void AddFirst(T data)
        {
            Node<T> newNode = new Node<T>(data);
            if (IsEmpty())
            {
                last = newNode;
                newNode.Next = last;
            }
            else
            {
                // List is not empty.
                // 1. New node points to old Head (last.Next).
                newNode.Next = last!.Next;
                // 2. Last points to New node (making it the new Head).
                last.Next = newNode;
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
            Node<T> current = last!.Next!; // Start at Head (index 0)

            // Traverse to the node immediately BEFORE the insertion point.
            for (int k = 0; k < index - 1; k++)
            {
                current = current.Next!;
            }

            // Insert newNode between current and current.Next.
            newNode.Next = current.Next;
            current.Next = newNode;
            count++;
        }

        // ========== Remove ==========
        public void RemoveAll()
        {
            // Dereferencing 'last' allows GC to collect the entire circular structure.
            last = null;
            count = 0;
        }
        public void RemoveLast()
        {
            if (IsEmpty()) throw new Exception("Can not remove an empty linked-list!");

            // Case: Only one node in the list.
            if (count == 1) last = null;
            else
            {
                Node<T> current = last!;
                // Traverse to find the second-to-last node.
                while (current.Next != last)
                {
                    current = current.Next!;
                }

                // Bypass the old Last node.
                current.Next = last.Next;
                // Update Last reference.
                last = current;
            }
            count--;
        }
        public void RemoveFirst()
        {
            if (IsEmpty()) throw new Exception("Can not remove an empty linked-list!");

            if (count == 1) last = null;
            else
            {
                // Last points to the second node (skipping the old Head).
                last!.Next = last.Next!.Next;
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

            Node<T> previous = last!.Next!; // Start at Head

            // Traverse to the node immediately BEFORE the one to remove.
            for (int k = 0; k < index - 1; k++)
            {
                previous = previous.Next!;
            }

            // Bypass the node at 'index'.
            previous.Next = previous.Next!.Next;
            count--;
        }
        public void RemoveAll(T data)
        {
            if (IsEmpty()) return;

            Node<T> current = last!.Next!; // Start at Head
            Node<T> previous = last!;
            EqualityComparer<T> comparer = EqualityComparer<T>.Default;

            // Save initial count to control loop iterations, as count changes during deletion.
            int itemsToCheck = count;

            while (itemsToCheck > 0)
            {
                if (comparer.Equals(data, current.Data))
                {
                    // Case: List has only 1 node.
                    if (count == 1)
                    {
                        last = null;
                        count = 0;
                        return;
                    }
                    else
                    {
                        // Bypass current node.
                        previous.Next = current.Next;

                        // If the removed node was Last, update Last pointer to Previous.
                        if (current == last)
                        {
                            last = previous;
                        }
                        count--;
                    }
                }
                else
                {
                    // Only advance 'previous' if we didn't delete the node.
                    // If we deleted, 'previous' is already pointing to the new 'next'.
                    previous = current;
                }

                // Always advance 'current' to check the next node.
                current = current.Next!;

                itemsToCheck--;
            }
        }
        public void RemoveFirst(T data)
        {
            if (IsEmpty()) return;

            Node<T> current = last!.Next!; // Start at Head
            Node<T> previous = last!;
            EqualityComparer<T> comparer = EqualityComparer<T>.Default;

            // Use a for loop based on current 'count' because we return immediately after one deletion.
            for (int i = 0; i < count; i++)
            {
                if (comparer.Equals(data, current.Data))
                {
                    if (count == 1)
                    {
                        last = null;
                    }
                    else
                    {
                        previous.Next = current.Next;
                        // Update Last if we removed the tail node.
                        if (current == last)
                        {
                            last = previous;
                        }
                    }
                    count--;
                    return; // Stop immediately after first removal.
                }
                previous = current;
                current = current.Next!;
            }
        }

        // ========== Replace =========
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
            Node<T> current = last!;
            do
            {
                if (EqualityComparer<T>.Default.Equals(_old, current.Data)) current.Data = _new;
                current = current.Next!;
            } while (current != last);
        }
        public void ReplaceFirst(T _old, T _new)
        {
            if (IsEmpty()) return;
            Node<T> current = last!;
            do
            {
                if (EqualityComparer<T>.Default.Equals(_old, current.Data))
                {
                    current.Data = _new;
                    return;
                }
                current = current.Next!;
            } while (current != last);
        }

        // ========== Traverse ==========
        public Node<T> Get(int index)
        {
            if (index < 0 || index >= count)
            {
                throw new Exception("Can not get at this index!");
            }
            Node<T> current = last!.Next!; // Start at Head
            for (int i = 0; i < index; i++)
            {
                current = current.Next!;
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

            Node<T> head = last!.Next!;
            Node<T>? current = head;
            int index = 0;
            // Use do-while to ensure the loop runs at least once and handles the circular condition correctly.
            do
            {
                Console.WriteLine($"{index}. {current!.Data}");
                current = current.Next;
                index++;
            } while (current != head);
        }
    }
}