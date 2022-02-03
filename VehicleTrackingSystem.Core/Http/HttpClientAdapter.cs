using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;
using System.Net.Http.Headers;
using VehicleTrackingSystem.Core.Helper;
using VehicleTrackingSystem.Core.Model;

namespace VehicleTrackingSystem.Core.Http
{
    public sealed class HttpClientAdapter : IHttpClientAdapter, IDisposable
    {
        private static readonly TimeSpan ExpirationTimeSpan = new TimeSpan(24, 0, 0); // 24 hours

        private readonly IMemoryCache memoryCache;
        private readonly AppSettings constants;
        private static readonly HttpClient Client;
        static HttpClientAdapter()
        {
            Client = new HttpClient();
            Client.DefaultRequestHeaders.Add("User-Agent", "VehicleTrackingSystem");
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public HttpClientAdapter(IMemoryCache memoryCache, IOptions<AppSettings> constants)
        {
            this.memoryCache = memoryCache;
            this.constants = constants.Value;
        }

        public async Task<TResult> GetAsync<TResult>(string url)
            where TResult : new()
        {
            HttpResponseMessage response = await this.GetAsync(this.constants.GoogleGeocodeReverseApi + url);

            return this.GetResultFromResponse<TResult>(response).Result;
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            var cacheKey = ("GetAsync|HttpResponseMessage|" + this.constants.GoogleGeocodeReverseApi + url).ToMD5();
            if (this.memoryCache != null && this.memoryCache.TryGetValue(cacheKey, out HttpResponseMessage cachedResult))
            {
                return cachedResult;
            }
            else
            {
                var result = await Client.GetAsync(url);

                if (!result.IsSuccessStatusCode)
                {
                    Log.Information($"Non-2xx responses from API  | {this.constants.GoogleGeocodeReverseApi + url}");
                }

                if (this.memoryCache != null && result.IsSuccessStatusCode && this.HasContent(result))
                {
                    this.memoryCache.Set(cacheKey, result, ExpirationTimeSpan);
                }

                return result;
            }
        }

        private async Task<TResult> GetResultFromResponse<TResult>(HttpResponseMessage response)
             where TResult : new()
        {
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<TResult>(
                    await response.Content.ReadAsStringAsync(),
                    new JsonSerializerSettings
                    {
                        Error = (object sender, Newtonsoft.Json.Serialization.ErrorEventArgs args) =>
                        {
                            Log.Error(args.ErrorContext.Error, args.ErrorContext.Error.Message);
                            args.ErrorContext.Handled = true;
                        },
                    }) ?? new TResult();

                return result;
            }
            else
            {
                var request = response.RequestMessage;

                Log.Error("Error:" +
                    "\nFor Request: " + (request != null ? request.Method + " " + request.RequestUri : string.Empty) +
                    "\nStatus Code: " + response.StatusCode.ToString() +
                    "\nReason: " + response.ReasonPhrase);

                return new TResult();
            }
        }

        private bool HasContent(HttpResponseMessage result)
        {
            var stringVal = result.Content.ReadAsStringAsync().Result;
            if (stringVal.Length == 0 || stringVal.Contains("\"result\":[]"))
            {
                return false;
            }

            return true;
        }

        public void Dispose()
        {
            Client?.Dispose();
        }

    }
}