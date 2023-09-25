namespace CallApi
{
    class Program
    {
        static void Main(string[] args)
        {
            Manager m = new Manager();
            while(true)
            {
                Console.WriteLine("");
                Console.WriteLine("1. Show List Category");
                Console.WriteLine("2. Search Category by Id");
                Console.WriteLine("3. Search Category by Name");
                Console.WriteLine("4. Update category");
                Console.WriteLine("5. Delete category");
                Console.WriteLine("0. Exit the program");
                Console.WriteLine("======================");
                Console.Write("Enter your choice: ");
                int opt = Int32.Parse(Console.ReadLine());
                Console.WriteLine("");
                switch (opt)
                {
                    case 0: Console.WriteLine("Bye bye!");
                        return;
                    case 1:
                        {
                            m.ShowAll();
                            Console.ReadKey();
                            break;
                        }
                    case 2:
                        {
                            m.SearchById();
                            Console.ReadKey();
                            break;
                        }
                    case 3:
                        {
                            m.SearchByName();
                            Console.ReadKey();
                            break;
                        }
                    case 4:
                        {
                            break;
                        }
                    case 5:
                        {
                            break;
                        }
                }
            }
        }
    }
}
