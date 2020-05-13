using System;
using System.Text.RegularExpressions;

namespace FileWatcher
{
    public class DataExtractor
    {
        #region Variables
        private string _regexSalesman = @"001";
        private string _regexClient = @"002";
        private string _regexSale = @"003\S*";
        private string _regexSaleID = @"ç(\d*)ç";
        private string _regexSalePrice = @"\d*\.\d*";
        private string _regexWorstSalesman = @"001\w*";
        private string _regexWorstSalesmanName = @"ç(\D*)ç";
        private string _regexWorstSalesmanSalary = @"ç(\d+)";
        #endregion

        public DataExtractor() { }

        #region Methods
        public int GetSalesmansCount(string data) 
        {
            return Regex.Matches(data, _regexSalesman).Count;
        }

        public int GetClientsCount(string data)
        {
            return Regex.Matches(data, _regexClient).Count;
        }

        public int GetIDSalesHighestPrice(string data)
        {
            int saleIDHighest = 0;
            decimal saleHighestPrice = 0;
            MatchCollection matchesSales = Regex.Matches(data, _regexSale);

            foreach (Match matchSale in matchesSales)
            {
                string matchSaleText = matchSale.ToString();
                int matchID = Convert.ToInt32(Regex.Match(matchSaleText, _regexSaleID).Groups[1].Value);

                Console.WriteLine($"ID: {matchID}");

                MatchCollection matchPrices = Regex.Matches(matchSaleText, _regexSalePrice);

                foreach (Match matchPrice in matchPrices)
                {
                    decimal salePrice = Convert.ToDecimal(matchPrice.Value);

                    Console.WriteLine($"    Price: {salePrice}");

                    if (salePrice > saleHighestPrice)
                    {
                        saleIDHighest = matchID;
                        saleHighestPrice = salePrice;
                    }
                }
            }

            return saleIDHighest;
        }

        public string GetWorstSalesman(string data)
        {
            string worstSalesman = "";
            decimal worstSalary = decimal.MaxValue;
            MatchCollection matchesSalesmans = Regex.Matches(data, _regexWorstSalesman);
            
            foreach (Match matchesSalesman in matchesSalesmans)
            {
                string matchSalesmanText = matchesSalesman.ToString();
                string salesmanName = Regex.Match(matchSalesmanText, _regexWorstSalesmanName).Groups[1].Value;

                Console.WriteLine($"Salesman: {salesmanName}");

                decimal salary = Convert.ToDecimal(Regex.Matches(matchSalesmanText, _regexWorstSalesmanSalary)[1].Groups[1].Value);
                Console.WriteLine($"    Salary: {salary}");

                if (salary < worstSalary)
                {
                    worstSalesman = salesmanName;
                    worstSalary = salary;
                }
            }

            return worstSalesman;
        }
    }
    #endregion
}