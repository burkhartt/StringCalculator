using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StringCalculator {
    public class Calculator {
        string customDelimiter = "";
         
        public int Add(string numbers) {
             if (string.IsNullOrEmpty(numbers)) {
                 return 0;
             }
             
             var startingPoint = 0;
             customDelimiter = SetCustomDelimiter(numbers, ref startingPoint);

             List<int> negativeNumbers;
             var sum = DoSum(numbers, startingPoint, customDelimiter, out negativeNumbers);
             if (negativeNumbers.Count > 0) {
                 throw new Exception(string.Format("Negative numbers are not allows. The negative numbers are {0}", GetStringList(negativeNumbers)));
             }

             return sum;
         }

        private static int DoSum(string numbers, int startingPoint, string customDelimiter, out List<int> negativeNumbers) {
            var sum = 0;
            negativeNumbers = new List<int>();
            foreach (var numberString in numbers.Substring(startingPoint).Split(new[] {",", "\n", customDelimiter }, StringSplitOptions.None)) {
                if(string.IsNullOrEmpty(numberString)){continue;}
                var number = int.Parse(numberString);
                if (number < 0) {
                    negativeNumbers.Add(number);
                } else if (number > 1000) {
                    continue;
                } 
                sum += number;
            }
            return sum;
        }

        private static string SetCustomDelimiter(string numbers, ref int startingPoint) {
            string delimiter = string.Empty;
            if (!numbers.StartsWith("//")) {
                return delimiter;
            }

            if (numbers.StartsWith("//[")) {
                var startIndexDelimiter = numbers.IndexOf("//[") + 3;
                var endIndexDelimiter = numbers.IndexOf("]");
                delimiter = numbers.Substring(startIndexDelimiter, endIndexDelimiter - startIndexDelimiter);
                var indexOfNewLine = numbers.IndexOf("\n");
                startingPoint = indexOfNewLine;
                return delimiter;
            }
            if (numbers.StartsWith("//")) {
                var indexOfNewLine = numbers.IndexOf("\n");
                delimiter = numbers.Substring(2, indexOfNewLine - 2);
                startingPoint = indexOfNewLine + delimiter.Length;
            }
            return delimiter;
        }

        private string GetStringList(IEnumerable<int> negativeNumbers) {
            var stringList = negativeNumbers.Aggregate("", (current, negativeNumber) => current + (negativeNumber + ","));
            return stringList.TrimEnd(',');
        }
    }
}
