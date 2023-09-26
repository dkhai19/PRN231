using CallApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CallApi
{
    internal class Manager
    {
        public string link = "http://localhost:5289/api/Category";
        public Manager()
        {}
        public async void ShowAll()
        {
            try
            {
                using(HttpClient client = new HttpClient())
                {
                    using(HttpResponseMessage resp = await client.GetAsync(link))
                    {
                        using(HttpContent content = resp.Content)
                        {
                            string data = content.ReadAsStringAsync().Result;
                            //Console.WriteLine(data);
                            List<Category> list = JsonConvert.DeserializeObject<List<Category>>(data);
                            foreach(var item in list)
                            {
                                Console.WriteLine(item);
                            }

                        }
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Show error: " + e.Message);
            } 
        }

        internal async void SearchById()
        {
            try
            {
                Console.Write("Enter the category id: ");
                string input = Console.ReadLine();
                if(int.TryParse(input, out int result))
                {
                    using (HttpClient client = new HttpClient())
                    {
                        using (HttpResponseMessage rsp = await client.GetAsync(link+"/id?id=" + result))
                        {
                            using (HttpContent content = rsp.Content)
                            {
                                string data = content.ReadAsStringAsync().Result;
                                
                                Category cate = JsonConvert.DeserializeObject<Category>(data);
                                
                                if(cate != null)
                                {
                                    Console.WriteLine(cate);
                                } else
                                {
                                    Console.WriteLine("No such that category");
                                }
                            }
                        }
                    }
                }
                
            }catch(Exception e)
            {
                Console.WriteLine("Show error: " + e.Message);
            }
        }

        internal async Task deleteCategoryAsync(string? id)
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
                                    Console.WriteLine("Category Id not found.");
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

        internal async void addCategory(string? name)
        {
            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Invalid name!");
                return;
            }
            try
            {
                Category cate = new Category()
                {
                    CategoryName = name
                };
                using (HttpClient client = new HttpClient())
                {
                    // Serialize the object to JSON
                    //string jsonContent = JsonConvert.SerializeObject(cate);

                    // Create a StringContent object with the JSON data
                    //var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

                    //Cách 1: Add object using PostAsJsonAsync
                    using (HttpResponseMessage resp = await client.PostAsJsonAsync(link,cate))
                    //Cách 2: Convert object to json data
                    //using(HttpResponseMessage resp = await client.PostAsync(link + "endpoint", content))
                    {

                        if (resp.IsSuccessStatusCode)
                        {
                            Console.WriteLine("Add successfully!");
                        } else
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

        internal async void SearchByName()
        {
            try
            {
                Console.Write("Enter the category name: ");
                string input = Console.ReadLine();
                    using (HttpClient client = new HttpClient())
                    {
                        using (HttpResponseMessage rsp = await client.GetAsync(link))
                        {
                            using (HttpContent content = rsp.Content)
                            {
                                string data = content.ReadAsStringAsync().Result;
                                List<Category> cate = JsonConvert.DeserializeObject<List<Category>>(data);

                                if (cate != null)
                                {
                                    foreach(var item in cate)
                                    {
                                        if(item.CategoryName.Contains(input))
                                        {
                                            Console.WriteLine(item);
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("No such that category");
                                }
                            }
                        }
                    }
                }
            catch (Exception e)
            {
                Console.WriteLine("Show error: " + e.Message);
            }
        }

        internal async void updateCategory(string id, string? name)
        {
            try
            {
                if(string.IsNullOrEmpty(name))
                {
                    return;
                }
                if(int.TryParse(id, out int result))
                {
                    Category updateCate = new Category()
                    {
                        CategoryId = result,
                        CategoryName = name
                    };
                    using (HttpClient client = new HttpClient())
                    {
                        using (HttpResponseMessage resp = await client.PutAsJsonAsync(link, updateCate))
                        {
                            if (resp.IsSuccessStatusCode)
                            {
                                Console.WriteLine("Update successfully!");
                            }
                            else
                            {
                                if (resp.StatusCode == System.Net.HttpStatusCode.NotFound)
                                {
                                    Console.WriteLine("Category Id not found.");
                                }
                                else
                                {
                                    Console.WriteLine("Failed to update");
                                }
                            }
                        }
                    }
                } else
                {
                    Console.WriteLine("Invalid Id!");
                }
                
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
