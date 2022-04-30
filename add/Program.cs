using System;
using System.Threading;

namespace add
{//Используя конструкции блокировки, создайте метод, который будет в цикле for (допустим, на 10
//итераций) увеличивать счетчик на единицу и выводить на экран счетчик и текущий поток.
//Метод запускается в трех потоках. Каждый поток должен выполниться поочередно, т.е.в
//результате на экран должны выводиться числа (значения счетчика) с 1 до 30 по порядку, а не в
//произвольном порядке.
    class Program
    {
        static int counter = 0;
        static object block = new object(); //эстафетная палочка
        static void IncCounter()
        {
            lock (block) 
            {
                for (int i = 0; i < 10; ++i)
                {
                    //Interlocked.Increment(ref counter);// пока новое значение не записалось в RAM, не пускает к counter другой поток 
                                                       // здесь немного коряво работает
                    Console.WriteLine("Счетчик- {0, -7}, из потока- {1}", ++counter, Thread.CurrentThread.GetHashCode());

                    //Thread.Sleep(1000);
                }
            }
        }
        
        static void Main(string[] args)
        {
            

            for (int i = 0; i < 3; i++)
            {
                new Thread(IncCounter).Start();
            }

        }
    }
}
