using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchanges
{
    public abstract class CalculateRequestBase
    {
        public OperationTypes OperationType { get; protected set; }
    }
}
