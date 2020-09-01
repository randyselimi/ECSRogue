using System;
using System.Collections;
using System.Collections.Generic;

namespace ECSRogue.PriorityQueues
{
    public sealed class BinaryHeap<TItem, TPriority> : IPriorityQueue<TItem, TPriority>
    {
        private const int InitialSize = 16;
        private const int Degree = 2;

        private readonly Func<TPriority, TPriority, int> Compare;

        private readonly Guid identifier;

        private BinaryHeapNode[] heap;

        public BinaryHeap(PriorityQueueType type, IComparer<TPriority> comparer)
        {
            if (comparer == null) throw new ArgumentNullException("comparer");
            switch (type)
            {
                case PriorityQueueType.Minimum:
                    Compare = (x, y) => comparer.Compare(x, y);
                    break;
                case PriorityQueueType.Maximum:
                    Compare = (x, y) => comparer.Compare(y, x);
                    break;
                default: throw new ArgumentException(string.Format("Unknown priority queue type: {0}", type));
            }

            identifier = Guid.NewGuid();
            heap = new BinaryHeapNode[InitialSize];
        }

        public BinaryHeap(PriorityQueueType type)
            : this(type, Comparer<TPriority>.Default)
        {
        }

        public TItem Peek
        {
            get
            {
                if (Count == 0) throw new InvalidOperationException("Binary heap does not contain elements");
                return heap[1].Item;
            }
        }

        public TPriority PeekPriority
        {
            get
            {
                if (Count == 0) throw new InvalidOperationException("Binary heap does not contain elements");
                return heap[1].Priority;
            }
        }

        public int Count { get; private set; }

        public IEnumerator<TItem> GetEnumerator()
        {
            for (var i = 1; i <= Count; i++) yield return heap[i].Item;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IPriorityQueueEntry<TItem> Enqueue(TItem item, TPriority priority)
        {
            if (item == null) throw new ArgumentNullException("item");
            if (priority == null) throw new ArgumentNullException("priority");
            if (Count == heap.Length - 1) Array.Resize(ref heap, heap.Length * Degree);
            var node = new BinaryHeapNode(item, priority, ++Count, identifier);
            heap[Count] = node;
            HeapifyUp(node);
            return node;
        }

        public TItem Dequeue()
        {
            if (Count == 0) throw new InvalidOperationException("Binary heap is empty!");
            var head = heap[1];
            Remove(heap[1]);
            return head.Item;
        }

        public void UpdatePriority(IPriorityQueueEntry<TItem> entry, TPriority priority)
        {
            if (entry == null) throw new ArgumentNullException("entry");
            if (priority == null) throw new ArgumentNullException("priority");
            var node = entry as BinaryHeapNode;
            if (node == null) throw new InvalidCastException("Invalid heap entry format!");
            if (node.HeapIdentifier != identifier) throw new ArgumentException("Heap does not contain this node!");
            node.Priority = priority;
            HeapifyUp(node);
        }

        public void Remove(IPriorityQueueEntry<TItem> entry)
        {
            if (entry == null) throw new ArgumentNullException("entry");
            var temp = entry as BinaryHeapNode;
            if (temp == null) throw new InvalidCastException("Invalid heap entry format!");
            if (temp.HeapIdentifier != identifier) throw new ArgumentException("Heap does not contain this node!");
            if (temp.Index == Count)
            {
                temp.HeapIdentifier = Guid.Empty;
                heap[Count--] = null;
                return;
            }

            MoveNode(heap[Count], temp.Index);
            heap[Count--] = null;
            HeapifyUp(heap[HeapifyDown(heap[temp.Index])]);
            temp.HeapIdentifier = Guid.Empty;
        }

        public void Clear()
        {
            for (var i = 1; i <= Count; i++) heap[i].HeapIdentifier = Guid.Empty;
            heap = new BinaryHeapNode[InitialSize];
            Count = 0;
        }

        private void HeapifyUp(BinaryHeapNode node)
        {
            var parent = Parent(node.Index);
            var to = node.Index;

            while (parent != null && Compare(parent.Priority, node.Priority) > 0)
            {
                var grandParent = parent.Index / Degree;
                var temp = parent.Index;
                MoveNode(parent, to);
                to = temp;
                parent = heap[grandParent];
            }

            MoveNode(node, to);
        }

        private int HeapifyDown(BinaryHeapNode node)
        {
            var child = BestChild(node.Index);
            var index = node.Index;

            while (child != null)
            {
                index = child.Index;
                MoveNode(child, child.Index / Degree);
                child = BestChild(index);
            }

            MoveNode(node, index);
            return index;
        }

        private void MoveNode(BinaryHeapNode node, int to)
        {
            heap[to] = node;
            node.Index = to;
        }

        private BinaryHeapNode Parent(int index)
        {
            return index == 1 ? null : heap[index / Degree];
        }

        private BinaryHeapNode BestChild(int index)
        {
            var temp = index * Degree;

            if (Count < temp)
                return null;
            if (Count == temp)
                return heap[Count];
            return Compare(heap[temp + 1].Priority, heap[temp].Priority) > 0 ? heap[temp] : heap[temp + 1];
        }

        private sealed class BinaryHeapNode : IPriorityQueueEntry<TItem>
        {
            public BinaryHeapNode(TItem item, TPriority priority, int index, Guid heapIdentifier)
            {
                Item = item;
                Priority = priority;
                Index = index;
                HeapIdentifier = heapIdentifier;
            }

            public TPriority Priority { get; internal set; }
            public int Index { get; set; }
            public Guid HeapIdentifier { get; set; }
            public TItem Item { get; }
        }
    }
}