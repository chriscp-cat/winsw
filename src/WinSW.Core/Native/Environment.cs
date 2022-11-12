using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WinSW.Native
{
    internal class EnvironmentUtils
    {
        internal static Dictionary<string, string> ParserEnvironmentString(IntPtr environmentString)
        {
            var environment = new Dictionary<string, string>();
            var pEnvBlock = environmentString;
            var length = 0;
            while (true)
            {
                var chr = Marshal.ReadInt16(pEnvBlock + (checked(++length) * 2));
                if (chr == 0)
                {
                    if (length <= 1)
                    {
                        break;
                    }

                    var entry = Marshal.PtrToStringUni(pEnvBlock, length);

                    var kv = entry.Split("=".ToCharArray(), 2);

                    if (kv.Length == 2)
                    {
                        environment[kv[0]] = kv[1];
                    }

                    pEnvBlock += (length + 1) * 2;
                    length = 0;
                }
            }

            return environment;
        }

        internal static char[] EnvironmentToString(Dictionary<string, string> environments)
        {
            List<char> buffer = new List<char>();
            foreach (var environment in environments)
            {
                string entry = $"{environment.Key}={environment.Value}";
                buffer.AddRange(entry.ToCharArray());

                buffer.Add('\0');
            }

            buffer.Add('\0');
            return buffer.ToArray();
        }
    }
}
