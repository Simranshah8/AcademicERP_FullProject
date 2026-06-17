using System.IO;
using System.Web;

namespace ClosedXML.Extensions
{
    internal static class HttpExtensions
    {

        public static Stream BodyStream(this HttpResponse httpResponse)
        {
            return httpResponse.OutputStream;
        }
    }
}