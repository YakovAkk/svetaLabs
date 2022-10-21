using System;
using System.Collections.Generic;

namespace SvetaLabs.AStarSearchAsync.Laba6
{
    public class PriorityQueueAsync<T>
    {
        // В этом примере я использую несортированный массив, но в идеале
        // это должна быть двоичная куча. 

        private List<Tuple<T, double>> elements = new List<Tuple<T, double>>();

        public int Count // кількість елеменів у черзі
        {
            get { return elements.Count; }
        }

        public void Enqueue(T item, double priority) // Метод для добавлення обьектів у чергу
        {
            elements.Add(Tuple.Create(item, priority));
        }

        public T Dequeue() // метод для отримання обьектів з черги
        {
            int bestIndex = 0;

            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].Item2 < elements[bestIndex].Item2)
                {
                    bestIndex = i;
                }
            }

            T bestItem = elements[bestIndex].Item1;
            elements.RemoveAt(bestIndex);
            return bestItem;
        }
    }
}
