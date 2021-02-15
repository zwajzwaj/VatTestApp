using BO;
using Exchanges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    internal abstract class CalculatorBase
    {
        public OperationTypes OperationType { get; protected set; }

        public abstract CalculateResponseBase Calculate(CalculateRequestBase request);

        protected T2 CalculateCore<T1,T2>(CalculateRequestBase request)
            where T1: CalculateRequestBase, new()
            where T2: CalculateResponseBase, new()
        {
            T2 result = new T2();
            result.Messages.AddRange(Validate<T1>(request));
            result.IsValid = !result.ErrorMessages.Any();
            if (result.IsValid)
            {
                result.Result = CalculateResult<T1>(request);
            }
            return result;
        }

        // tu zakładam że dodatkowe przyszłe wyliczenia będą zwracać decimal - jeśli np miałyby zwracać jakiś obiekt 
        // to można np rozszerzać konkretne response i dawać zwracanie obiektu już w konkretnych klasach kalkulatorów
        protected abstract decimal CalculateResult<T1>(CalculateRequestBase request) where T1 : CalculateRequestBase, new();
        protected abstract IEnumerable<Message> Validate<T1>(CalculateRequestBase request) where T1 : CalculateRequestBase, new();

        protected Message CreateError(string text)
        {
            return new Message() { Text = text, Type = MessageTypes.Error };
        }


    }
}
