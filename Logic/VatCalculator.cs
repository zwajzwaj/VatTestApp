using BO;
using Exchanges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    internal class VatCalculator : CalculatorBase
    {
        private decimal[] validRates = { 0.08M, 0.23M };
        private decimal arbitraryMaximumSensibleNetto = 10000000000M;

        public VatCalculator()
        {
            OperationType = OperationTypes.CalculateVat;
        }

        public override CalculateResponseBase Calculate(CalculateRequestBase request)
        {
            return CalculateCore<CalculateVatRequest, CalculateVatResponse>(request);
        }

        protected override decimal CalculateResult<T1>(CalculateRequestBase request)
        {
            CalculateVatRequest innerRequest = (CalculateVatRequest)request;
            decimal result = (innerRequest.TaxRate + 1) * innerRequest.Netto;
            return result;
        }

        protected override IEnumerable<Message> Validate<T1>(CalculateRequestBase request)
        {
            CalculateVatRequest innerRequest = (CalculateVatRequest)request;
            if (!validRates.Contains(innerRequest.TaxRate))
            {
                yield return CreateError("Stawka podatku nieprawidłowa.");
            }
            if (innerRequest.Netto < 0 || innerRequest.Netto > arbitraryMaximumSensibleNetto)
            {
                yield return CreateError("Kwota netto nieprawidłowa.");
            }
        }
    }
}
