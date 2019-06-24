using System;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class Program
    {
        
        static void Main(string[] args)
        {
            Test(new[] {"22:12:12"}, new [] { "10 hours ago" }, "08:12:12");
            Test(new[] { "23:23:23", "23:23:23" }, new[] { "59 minutes ago", "59 minutes ago" }, "00:22:23");
            Test(new[] { "00:10:10", "00:10:10" }, new[] { "59 minutes ago", "1 hours ago" }, "impossible");
            Test(new[] { "11:59:13", "11:13:23", "12:25:15" }, new[] { "few seconds ago", "46 minutes ago", "23 hours ago" }, "11:59:23");
           
        }

        private static void Test(string[] postTimes, string[] showTimes, string expected)
        {
            var result = GetCurrentTime(postTimes, showTimes).Equals(expected) ? "PASS" : "FAIL";
            var postTimesCsv = string.Join(", ", postTimes);
            var showTimesCsv = string.Join(", ", showTimes);
            Console.WriteLine($"[{postTimesCsv}], [{showTimesCsv}] => {result}");
        }

        public static string GetCurrentTime(string[] exactPostTime, string[] showPostTime)
        {       

            for (int i = 0; i < exactPostTime.Length; i++) {
                for (int j = i + 1; j < exactPostTime.Length; j++) {
                    if (exactPostTime[i] == exactPostTime[j])
                        if (showPostTime[i] != showPostTime[j])
                            return "impossible";
                     }
                }
                var resString = new string[exactPostTime.Length];
                    for(int i=0; i<exactPostTime.Length;i++)
                    {
                        DateTime dt=Convert.ToDateTime(exactPostTime[i]);
                        
                        if(showPostTime[i].Contains("seconds"))
                        {
                            resString[i] = exactPostTime[i];
                            
                        }
                        else if(showPostTime[i].Contains("minutes"))
                        {
                         string min = showPostTime[i].Split(" ")[0];
                         TimeSpan ts=new TimeSpan(0,Int32.Parse(min),0);
                         DateTime ndt = dt.Add(ts);
                         string s = ndt.ToString("HH:mm:ss");
                         resString[i]=s;
                        }
                        else if(showPostTime[i].Contains("hours"))
                        {
                            string hour = showPostTime[i].Split(" ")[0];
                            TimeSpan ts = new TimeSpan(Int32.Parse(hour),0,0);    
                            DateTime ndt = dt.Add(ts);
                            string s = ndt.ToString("HH:mm:ss");
                            resString[i] =s;
                        }
                        else{
                            resString[i] = "error";
                        }
                        
                    }
                    Array.Sort(resString);
                    Array.Reverse(resString);
           
             return resString[0];
        }
    }
}
