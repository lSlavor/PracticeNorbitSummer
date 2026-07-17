using System;
using System.Collections;
using System.Collections.Generic;

namespace Task4CollectionsSmartStack
{
    /// <summary>
    /// Представляет собой собственную реализацию стека на базе массива.
    /// </summary>
    /// <typeparam name="T">Тип элементов стека.</typeparam>
    public class SmartStack<T> : IEnumerable<T>
    {
        private T[] items;
        private int count;

        /// <summary>
        /// Создает стек емкостью 4 элемента.
        /// </summary>
        public SmartStack()
        {
            items = new T[4];
            count = 0;
        }

        /// <summary>
        /// Создает стек указанной емкости.
        /// </summary>
        /// <param name="capacity">Начальная емкость стека.</param>
        public SmartStack(int capacity)
        {
            if (capacity <= 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));

            items = new T[capacity];
            count = 0;
        }

        /// <summary>
        /// Создает стек на основе коллекции.
        /// Последний элемент коллекции становится вершиной стека.
        /// </summary>
        /// <param name="collection">Коллекция элементов.</param>
        public SmartStack(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            int size = 0;

            foreach (T item in collection)
                size++;

            items = new T[Math.Max(size, 4)];
            count = 0;

            foreach (T item in collection)
            {
                Push(item);
            }
        }

        /// <summary>
        /// Количество элементов в стеке.
        /// </summary>
        public int Count
        {
            get { return count; }
        }

        /// <summary>
        /// Емкость внутреннего массива.
        /// </summary>
        public int Capacity
        {
            get { return items.Length; }
        }

        /// <summary>
        /// Добавляет элемент на вершину стека.
        /// </summary>
        /// <param name="item">Добавляемый элемент.</param>
        public void Push(T item)
        {
            if (count == items.Length)
            {
                Resize(items.Length * 2);
            }

            items[count++] = item;
        }

        /// <summary>
        /// Добавляет на вершину стека элементы коллекции.
        /// Последний элемент коллекции становится вершиной стека.
        /// </summary>
        /// <param name="collection">Добавляемая коллекция.</param>
        public void PushRange(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            int size = 0;

            foreach (T item in collection)
                size++;

            T[] buffer = new T[size];

            int index = 0;

            foreach (T item in collection)
            {
                buffer[index++] = item;
            }

            if (count + size > items.Length)
            {
                int newCapacity = items.Length;

                while (newCapacity < count + size)
                {
                    newCapacity *= 2;
                }

                Resize(newCapacity);
            }

            for (int i = 0; i < buffer.Length; i++)
            {
                items[count++] = buffer[i];
            }
        }

        /// <summary>
        /// Возвращает верхний элемент стека и удаляет его.
        /// </summary>
        /// <returns>Верхний элемент.</returns>
        public T Pop()
        {
            if (count == 0)
                throw new InvalidOperationException("Стек пуст.");

            T item = items[count - 1];

            items[count - 1] = default(T);

            count--;

            return item;
        }

        /// <summary>
        /// Возвращает верхний элемент без удаления.
        /// </summary>
        /// <returns>Верхний элемент.</returns>
        public T Peek()
        {
            if (count == 0)
                throw new InvalidOperationException("Стек пуст.");

            return items[count - 1];
        }

        /// <summary>
        /// Проверяет наличие элемента в стеке.
        /// </summary>
        /// <param name="item">Искомый элемент.</param>
        /// <returns>true, если элемент найден.</returns>
        public bool Contains(T item)
        {
            EqualityComparer<T> comparer = EqualityComparer<T>.Default;

            for (int i = 0; i < count; i++)
            {
                if (comparer.Equals(items[i], item))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Индексатор по глубине стека.
        /// 0 — вершина.
        /// Count-1 — основание.
        /// </summary>
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= count)
                    throw new ArgumentOutOfRangeException(nameof(index));

                return items[count - 1 - index];
            }
        }

        private void Resize(int newCapacity)
        {
            T[] newArray = new T[newCapacity];

            for (int i = 0; i < count; i++)
            {
                newArray[i] = items[i];
            }

            items = newArray;
        }

        /// <summary>
        /// Перечисление элементов от вершины к основанию.
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = count - 1; i >= 0; i--)
            {
                yield return items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}