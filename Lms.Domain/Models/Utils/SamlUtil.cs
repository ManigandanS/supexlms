using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Lms.Domain.Models.Utils
{
    public class SamlUtil
    {

        public static string EncodeRequest(string request)
        {
            var saml = string.Format(request, Guid.NewGuid());
            var bytes = Encoding.UTF8.GetBytes(saml);
            using (var output = new MemoryStream())
            {
                using (var zip = new DeflateStream(output, CompressionMode.Compress))
                {
                    zip.Write(bytes, 0, bytes.Length);
                }
                var base64 = Convert.ToBase64String(output.ToArray());
                return HttpUtility.UrlEncode(base64);
            }
        }

        public static string DecodeResponse(string response)
        {
            var utf8 = Encoding.UTF8;
            var bytes = utf8.GetBytes(response);
            using (var output = new MemoryStream())
            {
                using (new DeflateStream(output, CompressionMode.Decompress))
                {
                    output.Write(bytes, 0, bytes.Length);
                }
                var base64 = utf8.GetString(output.ToArray());
                return utf8.GetString(Convert.FromBase64String(base64));
            }
        }
    }
}
