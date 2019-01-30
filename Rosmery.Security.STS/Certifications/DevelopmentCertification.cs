using Microsoft.Extensions.FileProviders;
using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace Rosmery.Security.STS
{
    public class DevelopmentCertification
    {
        public static X509Certificate2 GetFromLocal()
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

        public static X509Certificate2 GetFromStore()
        {
            X509Store computerCaStore = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            computerCaStore.Open(OpenFlags.ReadOnly);
            var devCert = computerCaStore.Certificates.Find(X509FindType.FindBySubjectName, "development-dummy-cert", false);
            computerCaStore.Close();
            return devCert[0];
        }

        public static X509Certificate2 GetFromContainer() {

            using (FileStream certificateStream = File.Open(@"/root/.dotnet/https/token.pfx", FileMode.Open))
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
