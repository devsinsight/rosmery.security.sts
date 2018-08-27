using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rosmery.Security.ApiModule.IdentityServer
{
    public interface IApiResources
    {
        string ApiName { get; }
        string ApiSecret { get; }
        string Authority { get; }
    }
}
