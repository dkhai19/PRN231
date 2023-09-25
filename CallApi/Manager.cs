using CallApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
