using Microsoft.Extensions.FileProviders;
using System.IO;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace Rosmery.Security.ApiCore
{
    public class DevelopmentCertification
    {
        public static X509Certificate2 Get()
        {
            var embeddedFileProvider = new EmbeddedFileProvider(typeof(DevelopmentCertification).GetTypeInfo().Assembly);
            var certificateFileInfo = embeddedFileProvider.GetFileInfo("development.pfx");
            using (var certificateStream = certificateFileInfo.CreateReadStream())
            {
                byte[] certificatePayload;
                using (var memoryStream = new MemoryStream())
                {
                    certificateStream.CopyTo(memoryStream);
                    certificatePayload = memoryStream.ToArray();
                }

                return new X509Certificate2(certificatePayload, "Pass@w0rd1");
            }
        }
    }
}
