namespace Docker.App.Services
{
    public class ProductService
    {
        private readonly HttpClient _client;

        public ProductService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<string>?> GetList()
        {

            var response = await _client.GetAsync("/api/Products/GetList");
          
            if (response.IsSuccessStatusCode)
            {

                var responseAsBody = await response.Content.ReadFromJsonAsync<List<string>>();

                return responseAsBody;
            }
            else
            {
                //logging
                return null;
            }


        }
    }
}
