using Exchanges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Logic
    {
        private List<CalculatorBase> calculators;
        public Logic()
        {
            calculators = new List<CalculatorBase>();
            calculators.Add(new VatCalculator());
        }

        public CalculateResponseBase HandleCalculateRequest(CalculateRequestBase request)
        {
            CalculatorBase calculatorToUse = calculators.Where(x => x.OperationType == request.OperationType).SingleOrDefault();
            if (calculatorToUse != null)
            {
                return calculatorToUse.Calculate(request);
            }
            else
            {
                return null;
            }
        }
    }
}
