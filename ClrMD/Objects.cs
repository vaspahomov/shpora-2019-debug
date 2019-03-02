using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace ClrMD
{
    public static class Objects
    {   
        public static List<object> CreateObjects()
        {
            var objects = new List<object>();
            
            var dict1 = new Dictionary<string, string>();
            SetupDictionary1(dict1);
            
            var dict2 = new Dictionary<string, string>();
            SetupDictionary2(dict2);
            
            objects.Add(dict1);
            objects.Add(dict2);

            var cdict1 = new ConcurrentDictionary<string, string>();
            SetupDictionary1(cdict1);
            
            var cdict2 = new ConcurrentDictionary<string, string>();
            SetupDictionary2(cdict2);
            
            var list1 = new List<string> {"alpha", "beta", "gamma", "delta"};
            objects.Add(list1);

            var immutableList1 = list1.ToImmutableList();
            var immutableList2 = immutableList1.Add("kappa");
            var immutableList3 = immutableList2.Add("theta");
            var immutableList4 = immutableList3.Add("epsilon");
            var immutableList5 = immutableList4.Remove("gamma");

            
            objects.Add(dict1);
            objects.Add(dict2);
            objects.Add(immutableList1);
            objects.Add(immutableList2);
            objects.Add(immutableList3);
            objects.Add(immutableList4);
            objects.Add(immutableList5);

            return objects;
        }
        
        private static void SetupDictionary1(IDictionary<string, string> dict)
        {
            dict["aa"] = "11";
            dict["bb"] = "22";
            dict["cc"] = "33";
            dict["dd"] = "44";
            dict["ee"] = "55";
            dict.Remove("bb");
        }
        
        private static void SetupDictionary2(IDictionary<string, string> dict)
        {
            for (var i = 'a'; i <= 'z'; ++i)
                dict[i.ToString()] = i.ToString();
            dict.Remove("a");
            dict.Remove("e");
            dict.Remove("i");
            dict.Remove("o");
            dict.Remove("u");
        }
    }
}