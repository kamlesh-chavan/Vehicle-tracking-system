using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTrackingSystem.Core.Http
{
    public interface IHttpClientAdapter
    {
        //Task getGeocode();

        public Task<TResult> GetAsync<TResult>(string url)
            where TResult : new();

        public Task<HttpResponseMessage> GetAsync(string url);
    }
}
