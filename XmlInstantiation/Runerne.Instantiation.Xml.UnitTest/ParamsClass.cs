using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runerne.Instantiation.Xml.UnitTest
{
    public class ParamsClass
    {
        private int _i;
        private string[] _messages;
        public ParamsClass(int i, params string[] messages)
        {
            _i = i;
            _messages = messages;
        }

        public string GetText()
        {
            return $"{_i}: {string.Join(", ", _messages)}";
        }
    }
}
