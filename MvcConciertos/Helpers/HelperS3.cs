using Microsoft.Extensions.Configuration;

namespace MvcConciertos.Helpers
{
    public class HelperS3
    {
        private readonly IConfiguration _configuration;

        public HelperS3(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string BucketName => _configuration["AWS:BucketName"] ?? string.Empty;
        public string BucketUrl => _configuration["AWS:BucketUrl"] ?? string.Empty;
    }
}
