using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Added_Sugar_Finder
{
    class SugarDetector
    {
        public static bool CheckConcentrateForColor(List<string> list, int index)
        {
            if (index + 1 < list.Count && list[index + 1] != "for color") return true;
            return false;
        }
        public static bool CheckLastItem(List<string> list, int index)
        {
            if (index + 1 == list.Count) return true;
            return false;
        }
        public static List<string> GetSugarInSugarList(List<string> listOfIngredients, List<string> listOfAddedSugar)
        {
            List<string> result = new List<string>();
            bool waterChecker = !listOfIngredients.Contains("water");  //check if any juice concentrate comes right after water
            for(var i = 0; i < listOfIngredients.Count; i++)
            {
                //Not juice concentrate
                if (listOfAddedSugar.Contains(listOfIngredients[i]) && !listOfIngredients[i].Contains("juice concentrate"))
                    result.Add(listOfIngredients[i]);
                //Juice concentrate
                else if (listOfAddedSugar.Contains(listOfIngredients[i]) && listOfIngredients[i].Contains("juice concentrate") && waterChecker)
                {
                    if (CheckConcentrateForColor(listOfIngredients, i)) result.Add(listOfIngredients[i]);
                    else if (i + 1 == listOfIngredients.Count) result.Add(listOfIngredients[i]);
                }
            }
            return result;
        }
        public static List<string> GetPossibleSugar(List<string> otherIngredients, List<string> listOfKeywords)
        {
            List<string> result = new List<string>();
            foreach (string k in listOfKeywords)
            {
                CheckIndividualIngredient(otherIngredients, k, ref result);
            }
            return result.Distinct().ToList();
        }
        public static bool CheckNonSugarJuiceKeyword(string item)
        {
            List<string> nonSugarJuiceKeyword = new List<string>() { "powder", "solid", "dry" };
            foreach(string k in nonSugarJuiceKeyword)
            {
                if (item.Contains(k)) return true;
            }
            return false;
        }
        public static void CheckIndividualIngredient(List<string> list, string keyword, ref List<string> result)
        {
            bool waterChecker = !list.Contains("water");
            
            for (var i = 0; i < list.Count; i++)
            {
                if (list[i].Contains(keyword) && keyword == "juice concentrate" && waterChecker)
                {
                    if (i + 1 < list.Count && list[i + 1] != "for color") result.Add(list[i]);
                    else if (CheckLastItem(list, i)) result.Add(list[i]);
                }
                
                else if (list[i].Contains(keyword) && keyword == "juice" && !list[i].Contains("juice concentrate") && !CheckNonSugarJuiceKeyword(list[i])) result.Add(list[i]);
                
                else if (list[i].Contains(keyword) && keyword != "juice concentrate" && keyword != "juice") result.Add(list[i]);
                
            }
        }
    }
}
