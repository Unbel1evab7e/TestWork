using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWork.Logic;
using Xunit;

namespace UnitTests
{
    public class LogicTest
    {
        CurrencyConverter converter = new CurrencyConverter();
        [Fact]
        public void  DivideByZeroExceptionPass()
        {
            decimal reuslt = converter.CalculateConvert(0, "RUB", "EUR");
            Assert.Equal(0, reuslt);
        }
    }
}
