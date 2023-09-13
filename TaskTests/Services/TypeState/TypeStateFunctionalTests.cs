

using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Task;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;

namespace TaskTests.Services.TypeState
{
    public class TypeStateFunctionalTests 
    {
        private readonly HttpClient _httpClient;

        public TypeStateFunctionalTests()
        {
            // Configurar el cliente HTTP
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7228"); // Establece la URL base de tu aplicación ASP.NET Core en ejecución
        }

        [Fact]
        public async void FullTypeStateReturnsDataWhenDataIsAvailable()
        {
            // Realizar una solicitud HTTP GET a la ruta del controlador
            var response = await _httpClient.GetAsync("/api/v1/full-types-states");

            // Verificar el código de estado de la respuesta
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
