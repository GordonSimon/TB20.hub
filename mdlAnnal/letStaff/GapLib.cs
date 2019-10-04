using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;
using System.Reflection;
using System.Net;

using System.DirectoryServices;


// GS171105 - Add FindKeyInCollection
// GS180907 - Add GapLib
// GS180907 - Modified by removing some WebServices methods


namespace GAPP
{
    class GapLib
    {
        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public static string StrVal(object s) { return (s.Equals(DBNull.Value) ? string.Empty : ((string)s).Trim()); }

        public static int IntVal(object i) { return Convert.ToInt32(i); }

        public static DateTime DayVal(object d)
        {
            if (d.Equals(DBNull.Value)) return DateTime.MaxValue;

            string s = (string)d;

            DateTime result;
            if (!DateTime.TryParse(s, out result)) return DateTime.MaxValue;

            return result;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public static bool ValidName(string n, bool initial = false)
        {
            string t = n.Trim();

            if (t.Equals(string.Empty)) return false;
            if (initial && t.Length == 1 && t.All(char.IsLetter)) return true;

            if (t.Length >= 2)
            {
                if (!char.IsLetter(t.First())) return false;

                if (t.All(char.IsLetter)) return true;
                if (t.Any(char.IsPunctuation)) return true;
                if (t.Any(char.IsSeparator)) return true;
            }

            if (t.ToUpper().Equals("O")) return true;

            return false;
        }

        public static bool ValidGender(string g)
        {
            string check = g.ToUpper();

            if (check.Equals(string.Empty)) return false;
            if (!check.All(char.IsLetter)) return false;
            if (check.Equals("F")) return true;
            if (check.Equals("M")) return true;
            if (check.Equals("FEMALE")) return true;
            if (check.Equals("MALE")) return true;
            if (check.Equals("U")) return true;

            return false;
        }

        public static bool ValidBday(DateTime d)
        {
            if (d.Equals(DateTime.MaxValue)) return false;

            if (d.CompareTo(DateTime.Now) != -1) return false;
            if (d.CompareTo(DateTime.Now.AddYears(-130)) != 1) return false;

            return true;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        static public string FindKeyInCollection(System.Collections.Specialized.StringCollection col, string key)
        {
            string a = string.Join(";", col.Cast<string>().ToArray());
            var dic = a
                .Split(';')
                .Select(part => part.Split(':'))
                .Where(part => part.Length == 2)
                .ToDictionary(sp => sp[0], sp => sp[1]);

            if (dic.ContainsKey(key)) return dic[key];
            return "";
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public static string AssemblyDesc
        {
            get
            {
                var attribute = ((AssemblyDescriptionAttribute)Attribute.GetCustomAttribute(
                   Assembly.GetExecutingAssembly(), typeof(AssemblyDescriptionAttribute), false)).Description;

                return attribute;
            }
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public static string AssemblyVersion
        {
            get
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                string version = fvi.FileVersion;

                return version;
            }
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public static string GetExternalIPAddress()
        {
            string result = string.Empty;
            try
            {
                using (var client = new WebClient())
                {
                    client.Headers["User-Agent"] =
                    "Mozilla/4.0 (Compatible; Windows NT 5.1; MSIE 6.0) " +
                    "(compatible; MSIE 6.0; Windows NT 5.1; " +
                    ".NET CLR 1.1.4322; .NET CLR 2.0.50727)";

                    try
                    {
                        byte[] arr = client.DownloadData("http://checkip.amazonaws.com/");

                        string response = System.Text.Encoding.UTF8.GetString(arr);

                        result = response.Trim();
                    }
                    catch (WebException)
                    {
                    }
                }
            }
            catch
            {
            }

            if (string.IsNullOrEmpty(result))
            {
                try
                {
                    result = new WebClient().DownloadString("https://ipinfo.io/ip").Replace("\n", "");
                }
                catch
                {
                }
            }

            if (string.IsNullOrEmpty(result))
            {
                try
                {
                    result = new WebClient().DownloadString("https://api.ipify.org").Replace("\n", "");
                }
                catch
                {
                }
            }

            if (string.IsNullOrEmpty(result))
            {
                try
                {
                    result = new WebClient().DownloadString("https://icanhazip.com").Replace("\n", "");
                }
                catch
                {
                }
            }

            if (string.IsNullOrEmpty(result))
            {
                try
                {
                    result = new WebClient().DownloadString("https://wtfismyip.com/text").Replace("\n", "");
                }
                catch
                {
                }
            }

            if (string.IsNullOrEmpty(result))
            {
                try
                {
                    result = new WebClient().DownloadString("http://bot.whatismyipaddress.com/").Replace("\n", "");
                }
                catch
                {
                }
            }

            if (string.IsNullOrEmpty(result))
            {
                try
                {
                    string url = "http://checkip.dyndns.org";
                    System.Net.WebRequest req = System.Net.WebRequest.Create(url);
                    System.Net.WebResponse resp = req.GetResponse();
                    System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
                    string response = sr.ReadToEnd().Trim();
                    string[] a = response.Split(':');
                    string a2 = a[1].Substring(1);
                    string[] a3 = a2.Split('<');
                    result = a3[0];
                }
                catch (Exception)
                {
                }
            }

            return result;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        static private string mktable_list(List<string> list)
        {

            if (list.Count < 1)
                return "<h3>NO Data</h3>";


            /* Early Exit */

            string table = "<table border='1'>";

            table += "<tr>";
            table += string.Format("<th>{0}</th>", list.First());
            table += "</tr>";
            foreach (var s in list)
            {
                if (list.IndexOf(s) == 0) continue;

                table += "<tr>";
                table += string.Format("<td>{0}</td>", s);
                table += "</tr>";
            }

            table += "</table>";

            return table;
        }

        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public static string StdTableList(string title, List<string> methods)
        {
            string html;

            html = "<html>";
            html += "<head>" + "</head>";

            html += "<body>";

            html += string.Format("<h1>{0}</h1>", title);

            html += mktable_list(methods);

            html += "</body>";
            html += "</html>";

            return html;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

    }
}
