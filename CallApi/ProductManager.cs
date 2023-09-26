using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CallApi
{
    public class ProductManager
    {
        public string link = "http://localhost:5289/api/Product";
        internal async Task deleteProduct(string? id)
        {
            try
            {
                if (int.TryParse(id, out int result))
                {
                    using (HttpClient client = new HttpClient())
                    {
                        using (HttpResponseMessage resp = await client.DeleteAsync(link + "?id=" + result))
                        {
                            if (resp.IsSuccessStatusCode)
                            {
                                Console.WriteLine("Delete successfully!");
                            }
                            else
                            {
                                if (resp.StatusCode == System.Net.HttpStatusCode.NotFound)
                                {
                                    Console.WriteLine("Product Id not found.");
                                }
                                else
                                {
                                    Console.WriteLine("Failed to delete");
                                }
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Id!");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        internal async void addProduct(string? name, int price, int quantity, string? image, int cateId)
        {
            try
            {
                Product prod = new Product()
                {
                    ProductName = name,
                    UnitPrice = price,
                    UnitsInStock = quantity,
                    Image = image,
                    CategoryId = cateId
                };
                using (HttpClient client = new HttpClient())
                {
                    // Serialize the object to JSON
                    //string jsonContent = JsonConvert.SerializeObject(cate);

                    // Create a StringContent object with the JSON data
                    //var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

                    //Cách 1: Add object using PostAsJsonAsync
                    using (HttpResponseMessage resp = await client.PostAsJsonAsync(link, prod))
                    //Cách 2: Convert object to json data
                    //using(HttpResponseMessage resp = await client.PostAsync(link + "endpoint", content))
                    {

                        if (resp.IsSuccessStatusCode)
                        {
                            Console.WriteLine("Add successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Add failed!");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Insert error: " + e.Message);
            }
        }

        internal async void updateProduct(string id, string? name, int price, int quantity, string? image, int cateId)
        {
            if(int.TryParse(id, out var productId))
            {
                try
                {
                    Product updateProduct = new Product()
                    {
                        ProductId = productId,
                        ProductName=name,
                        UnitPrice=price,
                        UnitsInStock=quantity,
                        Image = image,
                        CategoryId=cateId
                    };
                    using (HttpClient client = new HttpClient())
                    {
                        using (HttpResponseMessage resp = await client.PutAsJsonAsync(link, updateProduct))
                        {
                            if (resp.IsSuccessStatusCode)
                            {
                                Console.WriteLine("Update successfully!");
                            }
                            else
                            {
                                if (resp.StatusCode == System.Net.HttpStatusCode.NotFound)
                                {
                                    Console.WriteLine("Product Id not found.");
                                }
                                else
                                {
                                    Console.WriteLine("Failed to update");
                                }
                            }
                        }
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("Invalid product id");
            }
        }
    }
}
