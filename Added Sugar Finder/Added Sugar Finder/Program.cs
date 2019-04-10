using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Added_Sugar_Finder
{
    class Program
    {
        static void Main(string[] args)
        {
            const string PATHBASE = @"..\lib\";
            string inProgressSugarPath = PATHBASE + "In Progress.txt";
            string removedSugarPath = PATHBASE + "Removed Sugar.txt";
            string keywordsPath = PATHBASE + "SugarKeywords.txt";

            List<string> inProgressSugar = FileController.GetContentFromFile(inProgressSugarPath);
            List<string> removedSugar = FileController.GetContentFromFile(removedSugarPath);
            List<string> keywords = FileController.GetContentFromFile(keywordsPath);

            //Normalize concent: lower case, trim, remove string in parentheses
            DataController.NormalizeList(ref inProgressSugar);
            DataController.NormalizeList(ref removedSugar);
            DataController.NormalizeList(ref keywords);

            //Set console readline limit
            Console.SetIn(new StreamReader(Console.OpenStandardInput(),
                               Console.InputEncoding,
                               false,
                               bufferSize: 32768));

            while (true)
            {
                Console.WriteLine("Enter ingredients: ");
                string ingredients = Console.ReadLine();

                DataController.ReplaceSpecialCharacters(ref ingredients);  //Need to be called before Normalization
                List<string> listOfIngredients = DataController.SplitStringByComma(ingredients);
                DataController.NormalizeList(ref listOfIngredients);


                //Check
                List<string> inProgressSugarResult = SugarDetector.GetSugarInSugarList(listOfIngredients, inProgressSugar);
                List<string> removedSugarResult = SugarDetector.GetSugarInSugarList(listOfIngredients, removedSugar);
                List<string> otherIngredients = listOfIngredients.Except(inProgressSugarResult).ToList();
                List<string> possibleSugars = SugarDetector.GetPossibleSugar(otherIngredients, keywords);

                Console.WriteLine("-----------------------------------------------------------");

                Console.WriteLine("In Progress: " + String.Join(", ", inProgressSugarResult.Distinct().OrderBy(q => q).ToList()));
                Console.WriteLine("Removed Sugar: " + String.Join(", ", removedSugarResult.Distinct().OrderBy(q => q).ToList()));
                Console.WriteLine("Other Ingredients: " + String.Join(", ", possibleSugars.Distinct().ToList()));

                Console.WriteLine("-----------------------------------------------------------");

            }
        }
    }
}
