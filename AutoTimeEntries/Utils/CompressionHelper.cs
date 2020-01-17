using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.SharpZipLib.Zip;

namespace GSAutoTimeEntries.Utils
{
    public static class CompressionHelper
    {
        public static void DescompacteZip(string filePath, string fileName)
        {
            var fastZip = new FastZip();

            // Will always overwrite if target filenames already exist
            fastZip.ExtractZip(fileName, filePath, null);
        }
    }
}
