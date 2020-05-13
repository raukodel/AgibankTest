using System;
using Xunit;

namespace FileWatcher.Tests
{
    public class FileWatcher_DataExtractor
    {
        private readonly DataExtractor _dataExtractor;
        private readonly string _data = @"001ç1234567891234çPedroç50000
                                        001ç3245678865434çPauloç40000.99
                                        002ç2345675434544345çJose da SilvaçRural
                                        002ç2345675433444345çEduardo PereiraçRural
                                        003ç10ç[1-10-100,2-30-2.50,3-40-3.10]çPedro
                                        003ç08ç[1-34-10,2-33-1.50,3-40-0.10]çPaulo";

        public FileWatcher_DataExtractor()
        {
            _dataExtractor = new DataExtractor();
        }

        [Fact]
        public void SalesmansCount()
        {
            int count =_dataExtractor.GetSalesmansCount(_data);
            bool result = count >= 0 ? true : false; 

            Assert.True(result, "Salesmans count should not be less then 0");
        }

        [Fact]
        public void ClientsCount()
        {
            int count =_dataExtractor.GetClientsCount(_data);
            bool result = count >= 0 ? true : false; 

            Assert.True(result, "Clients count should not be less then 0");
        }

        [Fact]
        public void SalesIDHighestPrice()
        {
            int count =_dataExtractor.GetIDSalesHighestPrice(_data);
            bool result = count >= 1 ? true : false; 

            Assert.True(result, "Sales ID for highest price should not be less then 1");
        }

        [Fact]
        public void WorstSalesman()
        {
            string name =_dataExtractor.GetWorstSalesman(_data);
            bool result = name.Length >= 1 ? true : false; 

            Assert.True(result, "Worst salesman name length should not be less then 1");
        }
    }
}