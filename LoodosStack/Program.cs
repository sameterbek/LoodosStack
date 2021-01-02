using System;
using System.Collections.Generic;
using System.Linq;

namespace LoodosStack
{
    static class Program
    {

        public static char[] stack;
        public static int stackCount;
        static void Main(string[] args)
        {
            var arraySize = 12;
            //var arraySize = Convert.ToInt32(Console.ReadLine());
            //stackCount = Convert.ToInt32(Console.ReadLine());
            stack = new char[arraySize];

            stackCount = 2;

            FlagStack(stack, stackCount);
            Push(2, 't');
            Push(2, 'e');
            Push(2, 'm');
            Push(2, 'a');
            Push(2, 's');
            Push(2, '1');
            Push(2, '1');

            Push(1, 's');
            Push(1, 'a');
            Push(1, 'm');
            Push(1, 'e');
            Push(1, 't');

            Pop(1);
            Pop(1);
            Pop(2);
            Pop(2);
        }

        public static void FlagStack(char[] stack, int count)
        {
            var divide = stack.Length / count;
            for (int i = 1; i < count; i++)
            {
                stack[i * divide] = '?';
            }
        }

        public static void Push(int sCount, char c)
        {
            if (c == '?')
                Console.WriteLine("Özel karakter girdiniz. Farklı karakter giriniz.");

            var index = stack.FindIndexof('?', sCount);
            if (index == -1)
                Console.WriteLine("Stack bulunamadı");

            if (CheckEmptyRightSlot(sCount))
            {
                var startEndIndex = StackStartEndIndex(sCount);
                var firstEmptySlot = Array.IndexOf(stack.Skip(startEndIndex[0]).ToArray(), '\0') + startEndIndex[0];

                for (int i = firstEmptySlot; i > startEndIndex[0]; i--)
                {
                    if (stack[i - 1] != '\0')
                        stack[i] = stack[i - 1];
                }
                stack[startEndIndex[0]] = c;
            }
            else if (CheckEmptyLeftSlot(sCount))
            {
                var startEndIndex = StackStartEndIndex(sCount);
                var lastEmptySlot = Array.LastIndexOf(stack.Take(startEndIndex[0]).ToArray(), '\0');

                for (int i = lastEmptySlot; i < startEndIndex[0]; i++)
                {
                    stack[i] = stack[i + 1];
                }
                stack[startEndIndex[0] - 1] = c;
            }
        }

        public static void Pop(int sCount)
        {
            var startEndIndex = StackStartEndIndex(sCount);
            for (int i = startEndIndex[0]; i < startEndIndex[1]; i++)
            {
                stack[i] = stack[i + 1];
            }
            stack[startEndIndex[1]] = '\0';
        }

        public static int[] FindAllIndexof<T>(this IEnumerable<T> values, T val)
        {
            return values.Select((b, i) => object.Equals(b, val) ? i : -1).Where(i => i != -1).ToArray();
        }

        public static int FindIndexof<T>(this IEnumerable<T> values, T val, int index)
        {
            var allIndex = values.Select((b, i) => object.Equals(b, val) ? i : -1).Where(i => i != -1).ToArray();
            if (allIndex.Length >= index)
                return allIndex[index - 1];
            else
                return -1;
        }

        public static bool CheckEmptyRightSlot(int sCount)
        {
            var startEndIndex = StackStartEndIndex(sCount);
            int startIndex = startEndIndex[0];
            int finishIndex = startEndIndex[1];

            var stackElements = stack.Skip(startIndex);

            return stackElements.Contains('\0');
        }

        public static bool CheckEmptyLeftSlot(int sCount)
        {
            var startEndIndex = StackStartEndIndex(sCount);
            int startIndex = startEndIndex[0];
            int finishIndex = startEndIndex[1];

            var stackElements = stack.Take(startIndex);

            return stackElements.Contains('\0');
        }

        public static int[] StackStartEndIndex(int sCount)
        {
            int startIndex, finishIndex = 0;
            if (sCount == 1)
            {
                startIndex = 0;
                finishIndex = stack.FindIndexof('?', sCount) - 1;
            }
            else if (sCount == stackCount)
            {
                finishIndex = stack.Length-1;
                startIndex = stack.FindIndexof('?', sCount - 1) + 1;
            }
            else
            {
                startIndex = stack.FindIndexof('?', sCount - 1) + 1;
                finishIndex = stack.FindIndexof('?', sCount) - 1;
            }

            return new int[] { startIndex, finishIndex };
        }
    }
}
