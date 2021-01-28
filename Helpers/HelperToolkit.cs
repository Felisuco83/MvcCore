using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Helpers
{
    public class HelperToolkit
    {
        public static bool CompararArrayBytes(byte[] a, byte[]b)
        {
            bool iguales = true;
            if (a.Length != b.Length)
            {
                iguales = false;
            }
            for (int i = 0; i < a.Length; i++)
            {
                if(a[i].Equals(b[i]) == false)
                {
                    iguales = false;
                    break;
                }
            }
            return iguales;
        }

        public string SanitizeFileName(string filename)
        {
            var removeChar = false;
            for (var i=0; i< filename.Length; i++)
            {
                if(Char.IsSymbol(filename[i])){
                    removeChar = true;
                }
                if (Char.IsWhiteSpace(filename[i]))
                {
                    removeChar = true;
                }
                if (Char.IsPunctuation(filename[i]))
                {
                    if (filename[i] != '.' || (filename[i] == '.' && filename.LastIndexOf('.') != i))
                    {
                        removeChar = true;
                    }
                }
                if(removeChar)
                    filename.Remove(i, 1);
            }
            return filename;
        }
    }
}
