using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cache_Update
{
    class Program
    {
        static void Main(string[] args)
        {
            //Test Case where we have list of items to be added to cache
            List<int> listCache = new List<int>() { 1, 2, 3, 5, 7, 9, 10, 1, 10 };

            // Cache can be stored in a dictionary in the form of cache value as the key and value as updated date
            Dictionary<int, DateTime> cacheData = new Dictionary<int, DateTime>();

            //Call Cache function to process the latest cache to be stored
            cacheData = Cache(listCache);

            //Display the list of cache in the browser
            foreach (KeyValuePair<int, DateTime> data in cacheData)
            {
                Console.WriteLine("Cache Value : " + data.Key + " Date Logged : " + data.Value);
                Console.ReadLine();
            }
        }

        /// <summary>
        /// Cache method which will update based on the scenarios
        /// It will store latest 5 values and while retriving the previous value , it will update that particular value with
        /// the latest date
        /// </summary>
        /// <param name="listCache"></param>
        /// <returns></returns>
        private static Dictionary<int, DateTime> Cache(List<int> listCache)
        {
            Dictionary<int, DateTime> cacheData = new Dictionary<int, DateTime>();


            //Looping through each input value
            for (int i = 0; i < listCache.Count; i++)
            {

                //cache will be storing 5 value at max
                if (cacheData.Count < 5)
                {
                    //if cache already has the key then update the cache date 
                    if (cacheData.ContainsKey(listCache[i]))
                    {
                        cacheData[listCache[i]] = DateTime.Now;
                    }

                    //if cache does not have the cache key, then add that to cache with the latest date
                    else
                    {
                        cacheData.Add(listCache[i], DateTime.Now);
                    }
                }

                //I cache dictionary already have more then 5 (max limit) values
                else
                {
                    //if cache already has the key then update the cache date 
                    if (cacheData.ContainsKey(listCache[i]))
                    {
                        cacheData[listCache[i]] = DateTime.Now;
                    }

                    //if new cache value has to be added then remove the last used cache key based on the date
                    else
                    {
                        //sort the cache dictionary based on the date
                        var sortedDict = from entry in cacheData orderby entry.Value ascending select entry;

                        //retrieve the key of the cache dictionary which is least used
                        var first = sortedDict.OrderBy(kvp => kvp.Key).First();

                        //remove that key
                        cacheData.Remove(first.Key);

                        //Add new key to the cache dictionary
                        cacheData.Add(listCache[i], DateTime.Now);
                    }
                }
            }

            // return back the cache dictionary
            return cacheData;
        }
    }
}
