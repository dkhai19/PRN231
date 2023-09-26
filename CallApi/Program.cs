namespace CallApi
{
    class Program
    {
        static void Main(string[] args)
        {
            ProductManager m = new ProductManager();
            while(true)
            {
                Console.WriteLine("");
                Console.WriteLine("1. Add product");
                Console.WriteLine("2. Update product");
                Console.WriteLine("3. Delete product");
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
                            Console.Write("Enter name of product: ");
                            string name = Console.ReadLine();
                            Console.Write("Enter price of product: ");
                            int price = Int32.Parse(Console.ReadLine());
                            Console.Write("Enter quantity of product: ");
                            int quantity = Int32.Parse(Console.ReadLine());
                            Console.Write("Enter image link of product: ");
                            string image = Console.ReadLine();
                            Console.Write("Enter category id of product: ");
                            int cateId = Int32.Parse(Console.ReadLine());
                            m.addProduct(name, price, quantity, image, cateId);
                            Console.ReadKey();
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("Enter product id: ");
                            string id = Console.ReadLine();
                            Console.Write("Enter name of product: ");
                            string name = Console.ReadLine();
                            Console.Write("Enter price of product: ");
                            int price = Int32.Parse(Console.ReadLine());
                            Console.Write("Enter quantity of product: ");
                            int quantity = Int32.Parse(Console.ReadLine());
                            Console.Write("Enter image link of product: ");
                            string image = Console.ReadLine();
                            Console.Write("Enter category id of product: ");
                            int cateId = Int32.Parse(Console.ReadLine());
                            m.updateProduct(id, name, price, quantity, image, cateId);
                            Console.ReadKey();
                            break;
                        }
                    case 3:
                        {
                            Console.Write("Enter product Id: ");
                            string id = Console.ReadLine();
                            m.deleteProduct(id);
                            Console.ReadKey();
                            break;
                        }
                }
            }
        }
    }
}
