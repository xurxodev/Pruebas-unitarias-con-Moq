using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTestWithMoqExample
{
    public interface IHtmlValidator
    {
        bool IsValid(string html);
    }
}
