using System;
using Library;

namespace Library
{   
// non resident 2017-2018 test cases
public class UnitTestResident1718
    {
        private Random random = new Random();
        private string inputYear = "2017-18";
        private bool status =  false;
        private int[] ranges = { 0, 87000, 180000 };

        //method for generating random values
        public double GenerateValues(int minValue, int maxValue)
        {
            return random.Next(minValue,maxValue);
        }
        //generate different ranges of values and then test all of them

        public Unit TestMethod()
        {
            Unit unit = new Unit();
            //count correct answers and calculate correct rate
            double correctAnswer = 0.0;
            unit.setYear(inputYear);
            unit.setStatus("resident");
            //step 1 : generate random testing values
            for (int index = 0;index <= ranges.Length;index++)
            {
                double testAmount;
                if (index == 0)
                {
                    testAmount = 0;
                }
                else if(index == ranges.Length)
                {
                    testAmount = GenerateValues(ranges[index-1] + 1,Int32.MaxValue/1000);
                }
                else
                {
                    testAmount = GenerateValues(ranges[index-1] + 1,ranges[index]);
                }
                //get expected values and the values from the tax calculator
                double expectedAmount = expectedResult(index, testAmount);
                double actualAmount = Calculate.taxByYear(testAmount, Calculate.inputYearIndex(inputYear), status);
                //step 3 - Assert
                if (expectedAmount == actualAmount)
                {
                    correctAnswer += 1;
                    unit.addIncomeValues(testAmount);
                    unit.addExpectedValues("		Your expected income after tax is $" + expectedAmount);
                    unit.addActualValues("		Through the tax calculator, Your income value after tax is $" + actualAmount);
                }
                else
                {
                    unit.addFIncomeValues(testAmount);
                    unit.addFExpectedValues("		Your expected income after tax should be $" + expectedAmount);
                    unit.addFActualValues("		Through the tax calculator, Your actual income after tax is $" + actualAmount);
                }
            }
            unit.setTotalTest(ranges.Length + 1);
            unit.setPassedTest(correctAnswer);
            return unit;
        }
        //get the expected result
        public double expectedResult(int indexValue, double testAmount)
        {
            switch(indexValue)
            {
                case 0:
                    return 0;
                case 1:
                    return (testAmount - ranges[indexValue-1]) * 0.325;
                case 2:
                    return ((testAmount - ranges[indexValue-1]) * 0.37) + 28275;
                case 3:
                    return ((testAmount - ranges[indexValue-1]) * 0.45) + 62685;
            
                default:
                    return 0;
            }
        }
    }
}