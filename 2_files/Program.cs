using System.IO;
using System.Threading;

namespace _2_files
{//Создайте консольное приложение, которое в различных потоках сможет получить доступ к 2-м
 //   файлам.Считайте из этих файлов содержимое и попытайтесь записать полученную
 //  информацию в третий файл.Чтение/запись должны осуществляться одновременно в каждом
//из дочерних потоков.Используйте блокировку потоков для того, чтобы добиться корректной
//записи в конечный файл.
    class Program
    {
        
        static StreamReader stream1 = File.OpenText("read1.txt");
        static StreamReader stream2 = File.OpenText("read2.txt");
        static StreamWriter stream3 = File.CreateText("write.txt");

        static object bloсk = new object();

        static void ReadFile1()
        {
            string str = stream1.ReadToEnd();
            stream1.Close();

            lock (bloсk)
            {
                stream3.WriteLine(str);
            }
        }

        static void ReadFile2()
        {
            string str = stream2.ReadToEnd();
            stream2.Close();
            
            lock (bloсk)
            {
                stream3.WriteLine(str);
            }
        }

        static void Main(string[] args)
        {
            //var sr = new StreamWriter("read1.txt");
            //sr.WriteLine("Исла́м — самая молодая и вторая по численности приверженцев, после христианства, ");
            //sr.Close();
            //sr = new StreamWriter("read2.txt");
            //sr.WriteLine("мировая монотеистическая авраамическая религия.");
            //sr.Close();
            File.AppendAllText("read1.txt", string.Format("Исла́м — самая молодая и вторая по численности приверженцев, после христианства, "));
            
            File.AppendAllText("read2.txt", string.Format("мировая монотеистическая авраамическая религия."));


            var thread1 = new Thread(ReadFile1);
            var thread2 = new Thread(ReadFile2);

            thread1.Start();
            thread2.Start();

            thread1.Join(); // чтоб поток не закрывался раньше, чем отработали методы
            thread2.Join();

            stream3.Close();
        }
    }
}
