using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Timeout;
using System.Net.Http;

namespace Atmira.NasaApi.Svc
{
    public partial class Startup
    {
        private static void AddHttpClients(IServiceCollection services)
        {
            IAsyncPolicy<HttpResponseMessage> pNoOp = Policy.NoOpAsync().AsAsyncPolicy<HttpResponseMessage>();
            IAsyncPolicy<HttpResponseMessage> pTimeout = Policy.TimeoutAsync<HttpResponseMessage>(2);
            IAsyncPolicy<HttpResponseMessage> pRetry = Policy<HttpResponseMessage>.Handle<TimeoutRejectedException>().RetryAsync(3);
            IAsyncPolicy<HttpResponseMessage> pRetryTimeout = pRetry.WrapAsync(pTimeout);

            services.AddHttpClient(NamedClient.RetryTimeout)
                .AddPolicyHandler(r => r.Method == HttpMethod.Get ? pRetryTimeout : pNoOp);
        }
    }

    public static class NamedClient
    {
        public const string RetryTimeout = "RetryTimeout";
    }
}
