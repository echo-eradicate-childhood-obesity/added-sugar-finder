using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Added_Sugar_Finder
{
    class DataController
    {
        public static void NormalizeList(ref List<string> list)
        {
            LowerLetters(ref list);
            TrimStringInList(ref list);
            list = list.ConvertAll(item => DataController.RemoveSubstringInParentheses(item));
        }
        public static string RemoveSubstringInParentheses(string s)
        {
            string regex = "(\\(.*\\))";
            s = Regex.Replace(s, regex, "");
            return s.Trim();
        }
        public static void ReplaceSpecialCharacters(ref string ingredients)
        {
            ingredients = Regex.Replace(ingredients, @"[^0-9a-zA-Z ]+", ",");
        }

        public static void LowerLetters(ref List<string> s)
        {
            s = s.ConvertAll(item => item.ToLower());
        }

        public static void TrimStringInList(ref List<string> list)
        {
            list = list.ConvertAll(item => item.Trim());
        }

        public static List<string> SplitStringByComma(string i)
        {
            return i.Split(',').Select(p => p.Trim()).Where(q => !string.IsNullOrWhiteSpace(q)).ToList();
        }
    }
}
