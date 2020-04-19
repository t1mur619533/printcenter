using System.Net;
using Microsoft.AspNetCore.Http;

namespace PrintCenter.Infrastructure.Extensions
{
    public static class HttpContextExtensions
    {
        public const string NullIPv6 = "::1";

        public static bool IsLocal(this ConnectionInfo connectionInfo)
        {
            if (!connectionInfo.RemoteIpAddress.IsSet())
                return true;

            // we have a remote address set up
            // is local is same as remote, then we are local
            if (connectionInfo.LocalIpAddress.IsSet())
                return connectionInfo.RemoteIpAddress.Equals(connectionInfo.LocalIpAddress);

            // else we are remote if the remote IP address is not a loopback address
            return connectionInfo.RemoteIpAddress.IsLoopback();
        }

        public static bool IsLocal(this HttpContext ctx)
        {
            return ctx.Connection.IsLocal();
        }

        public static bool IsLocal(this HttpRequest req)
        {
            return req.HttpContext.IsLocal();
        }

        public static bool IsSet(this IPAddress address)
        {
            return address != null && address.ToString() != NullIPv6;
        }

        public static bool IsLoopback(this IPAddress address)
        {
            return IPAddress.IsLoopback(address);
        }
    }
}
