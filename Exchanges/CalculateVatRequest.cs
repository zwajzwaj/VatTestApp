using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchanges
{
    public class CalculateVatRequest : CalculateRequestBase
    {
        public decimal Netto { get; set; }
        public decimal TaxRate { get; set; }

        public CalculateVatRequest()
        {
            OperationType = BO.OperationTypes.CalculateVat;
        }
    }
}
