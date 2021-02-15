using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VatTestApp.GuiObjects
{
    internal class ListElement<T>
    {
        public string Name { get; set; }
        public T Value { get; set; }
    }
}
