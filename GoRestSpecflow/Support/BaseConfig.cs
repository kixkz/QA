using Microsoft.Extensions.Configuration;

namespace GoRestSpecflow.Support
{
    public class BaseConfig
    {
        public HttpClientConfig HttpClientConfig { get; set; }

        public BaseConfig()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("testConfig.json")
                .Build();

            HttpClientConfig = config.GetSection("HttpClient").Get<HttpClientConfig>();
        }
    }
}
