using System;
using System.IO;

namespace Dasha_OOP_4._3
{
 
    class Program
    {
        public static Arkhiv[] arkh = new Arkhiv[1000000];
        public static bool[] delete = new bool[1000000];


        static void Main(string[] args)
        {

            Input.Key();



        }

    }

    class Arkhiv
    {
        private string name;
        private string system;
    
        private int size;
        private DateTime time;

        public string Name
        {
            get { return name; }
            set { name = value; }

        }
        public string System
        {
            get { return system; }
            set { system = value; }
        }
       
        public int Size
        {
            get { return size; }
            set
            {
                if (value > 0) size = value;
                else throw new FormatException();
            }
        }
        public DateTime Time
        {
            get { return time; }
            set { time = value; }
        }



        public Arkhiv(string name, string system, int size, DateTime time)
        {
            this.name = name;
            this.system = system;
            this.size = size;
            this.time = time;
        }
    }
    class Output
    {
        public static void Write(Arkhiv[] m)
        {
            Console.WriteLine("{0,-30} {1,-20} {2,-30} {3,-15}", "Назва", "OS", "Розмір", "Дата");

            for (int i = 0; i < m.Length; ++i)
            {
                if (m[i] != null)
                {
                    Console.WriteLine("{0,-30} {1,-20} {2,-30} {3,-15}", Program.arkh[i].Name, Program.arkh[i].System, Program.arkh[i].Size,  Program.arkh[i].Time);
                }
            }
        }

        public static void Write1(Arkhiv[] m, bool[] write)
        {
            Console.WriteLine("{0,-30} {1,-20} {2,-30} {3,-15}", "Назва", "OS", "Розмір", "Дата");

            for (int i = 0; i < m.Length; ++i)
            {
                if ((write[i]) && (!Program.delete[i]))
                {
                    Console.WriteLine("{0,-30} {1,-20} {2,-30} {3,-15}", Program.arkh[i].Name, Program.arkh[i].System, Program.arkh[i].Size, Program.arkh[i].Time);
                }
            }
        }
    }

    class Input
    {


        public static void Key()
        {
            Work.Parse(Read(), false);

            Console.WriteLine("Додавання записiв: +");
            Console.WriteLine("Редагування записiв: E");
            Console.WriteLine("Знищення записiв: -");
            Console.WriteLine("Виведення записiв: Enter");
            Console.WriteLine("Пошук записiв: F");
            Console.WriteLine("Сортуванн записiв: S");
            Console.WriteLine("Вихiд: Esc");

            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.OemPlus:
                    Console.WriteLine();
                    Work.Add();
                    break;

                case ConsoleKey.E:
                    Console.WriteLine();
                    Work.Edit();
                    break;

                case ConsoleKey.OemMinus:
                    Console.WriteLine();
                    Work.Remove();
                    break;

                case ConsoleKey.Enter:
                    Console.WriteLine();
                    Output.Write(Program.arkh);
                    Key();
                    break;

                case ConsoleKey.F:
                    Console.WriteLine();
                    Work.Find();
                    break;

                case ConsoleKey.S:
                    Console.WriteLine();
                    Work.Sort();
                    break;

                case ConsoleKey.Escape:
                    return;
            }
        }
        public static string[] Read()
        {
            StreamReader fromFile = new StreamReader("text.txt");

            return fromFile.ReadToEnd().Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
        }
    }
    class Work
    {
        public static void Add()
        {
            Console.WriteLine("Введiть данi");

            string str = Console.ReadLine();

            string[] elements = str.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            Parse(elements, true);

            Output.Write(Program.arkh);
            Input.Key();
        }

        public static void Remove()
        {
            Console.Write("Назва: ");

            string singer = Console.ReadLine();

            bool[] write = new bool[Program.arkh.Length];

            for (int i = 0; i < Program.arkh.Length; ++i)
            {
                if (Program.arkh[i] != null)
                {
                    if (Program.arkh[i].Name == singer)
                    {
                        Console.WriteLine("{0,-30} {1,-20} {2,-30} {3,-15}", Program.arkh[i].Name, Program.arkh[i].System, Program.arkh[i].Size, Program.arkh[i].Time);

                        Console.WriteLine("Видалити? (Y/N)");

                        var key = Console.ReadKey().Key;

                        if (key == ConsoleKey.Y)
                        {
                            Program.arkh[i] = null;
                            Program.delete[i] = true;
                        }
                        else
                        {
                            Program.delete[i] = false;
                        }
                    }
                }
            }
            Output.Write(Program.arkh);
        }

        public static void Edit()
        {
            Console.Write("Назва: ");

            string singer = Console.ReadLine();

            bool[] write = new bool[Program.arkh.Length];

            for (int i = 0; i < Program.arkh.Length; ++i)
            {
                if (Program.arkh[i] != null)
                {
                    if (Program.arkh[i].Name == singer)
                    {
                        Console.WriteLine("{0,-30} {1,-20} {2,-30} {3,-15}", Program.arkh[i].Name, Program.arkh[i].System, Program.arkh[i].Size, Program.arkh[i].Time);

                        Console.WriteLine("Введiть нову iнформацiю");

                        string str = Console.ReadLine();

                        string[] elements = str.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                        Program.arkh[i] = new Arkhiv(elements[0], elements[1], int.Parse(elements[2]), DateTime.Parse(elements[3]));
                    }
                }
            }
            Output.Write(Program.arkh);
        }

        public static void Find()
        {
            Console.Write("OS: ");

            string singer = Console.ReadLine();

            bool[] write = new bool[Program.arkh.Length];

            for (int i = 0; i < Program.arkh.Length - 1; ++i)
            {
                if (Program.arkh[i] != null)
                {
                    if (Program.arkh[i].System == singer)
                    {
                        write[i] = true;

                    }
                    else
                    {
                        write[i] = false;

                    }
                }

            }

            Output.Write1(Program.arkh, write);

            Input.Key();
        }

        public static void Sort()
        {
            int index = 0;

            while (Program.arkh[index + 1] != null)
            {
                for (int i = 0; i < Program.arkh.Length - 1; ++i)
                {
                    if (Program.arkh[i + 1] != null)
                    {

                        for (int j = 0; j < Program.arkh.Length - 1; j++)
                        {
                            if (needToReOrder(Program.arkh[j], Program.arkh[j + 1]))
                            {
                                Arkhiv s = Program.arkh[j];
                                Program.arkh[j] = Program.arkh[j + 1];
                                Program.arkh[j + 1] = s;
                            }
                        }




                    }
                }
            }

            Output.Write(Program.arkh);

            Input.Key();
        }

        private static bool needToReOrder(Arkhiv musicCollection1, Arkhiv musicCollection2)
        {
            throw new NotImplementedException();
        }

        private static void Save(Arkhiv m)
        {
            StreamWriter save = new StreamWriter("text.txt", true);

            save.WriteLine(m.Name);
            save.WriteLine(m.System);
            save.WriteLine(m.Size);
            
            save.WriteLine(m.Time);

            save.Close();
        }

        public static void Parse(string[] elements, bool save)
        {
            int counter = 0;

            while (Program.arkh[counter] != null)
            {
                ++counter;
            }

            for (int i = 0; i < elements.Length; i += 4)
            {
                Program.arkh[counter + i / 4] = new Arkhiv(elements[i], elements[i + 1],int.Parse(elements[i + 2]), DateTime.Parse(elements[i + 3]));

                if (save)
                {
                    Save(Program.arkh[counter + i / 5]);
                }
            }
        }
        public static string[] Read()
        {
            StreamReader fromFile = new StreamReader("text.txt");

            return fromFile.ReadToEnd().Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
        }




    }
}


