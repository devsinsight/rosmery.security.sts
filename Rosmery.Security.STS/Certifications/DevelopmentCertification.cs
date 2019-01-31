using Microsoft.Extensions.FileProviders;
using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace Rosmery.Security.STS
{
    public class DevelopmentCertification
    {
        public static X509Certificate2 GetFromContainer() {

            using (FileStream certificateStream = File.Open(Environment.GetEnvironmentVariable("IdentityServer4_Certificate_Token_Path"), FileMode.Open))
            {
                byte[] certificatePayload;
                using (var memoryStream = new MemoryStream())
                {
                    certificateStream.CopyTo(memoryStream);
                    certificatePayload = memoryStream.ToArray();
                }
                return new X509Certificate2(certificatePayload, Environment.GetEnvironmentVariable("IdentityServer4_Certificate_Token_Password"));
            }

        }
    }
}
