using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rosmery.Security.STS.Services
{
    public class AuthMessageSenderOptions
    {
        public string EmailSendgridUser { get; set; }
        public string EmailSendgridKey { get; set; }
    }
}
