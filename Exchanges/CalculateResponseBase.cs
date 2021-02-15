using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchanges
{
    public abstract class CalculateResponseBase
    {
        public decimal Result { get; set; }
        public bool IsValid { get; set; }

        public List<Message> Messages { get; private set; }

        public IEnumerable<Message> ErrorMessages
        {
            get
            {
                return Messages.Where(x => x.Type == MessageTypes.Error);
            }
        }

        public IEnumerable<Message> InfoMessages
        {
            get
            {
                return Messages.Where(x => x.Type == MessageTypes.Info);
            }
        }

        public CalculateResponseBase()
        {
            Messages = new List<Message>();
        }
    }
}
