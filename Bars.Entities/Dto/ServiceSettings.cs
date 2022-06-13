using System;

namespace Bars.Entities.Dto
{
    public class ServiceSettings
    {
        private const string DefaultAddress = "net.tcp://localhost:8088/nettcp";
        public Uri NetTcpAddress { get; set; }

        private Uri AddPathPrefix(Uri source, string prefix)
        {
            if (string.IsNullOrEmpty(prefix))
                return source;
            var builder = new UriBuilder(source);
            builder.Path = prefix + builder.Path;
            return builder.Uri;
        }

        public ServiceSettings SetDefaults(string servicePrefix = default)
        {
            NetTcpAddress = AddPathPrefix(new Uri(DefaultAddress), servicePrefix);
            return this;
        }
    }
}