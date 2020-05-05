using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace KenoFormatterForm
{
    class Util
    {

        public static String KenoFormatterUtilFunc(String inputText){return Regex.Replace(inputText.Trim(), "\\s+", ", ");}

          



    }
}
