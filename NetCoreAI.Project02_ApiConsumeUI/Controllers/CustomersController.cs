using Microsoft.AspNetCore.Mvc;
using NetCoreAI.Project02_ApiConsumeUI.Dtos;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace NetCoreAI.Project02_ApiConsumeUI.Controllers
{
	public class CustomersController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public CustomersController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public async Task<IActionResult> CustomerList()
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync("https://localhost:44382/api/Customers");
			if(responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<List<ResultCustomerDto>>(jsonData);
				return View(values);
			}
			return View();
			
		}

		[HttpGet]
		public IActionResult CreateCustomer()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreateCustomer(CreateCustomerDto customerDto)
		{
			var client = _httpClientFactory.CreateClient();
			var jsonData = JsonConvert.SerializeObject(customerDto);
			StringContent content = new StringContent(jsonData,Encoding.UTF8,"application/json");
			var responseMessage = await client.PostAsync("https://localhost:44382/api/Customers", content);
			if(responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("CustomerList");
			}
			return View();
		}

		public async Task<IActionResult> DeleteCustomer(int id)
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.DeleteAsync("https://localhost:44382/api/Customers?id=" + id);
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("CustomerList");
			}
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> UpdateCustomer(int id)
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync("https://localhost:44382/api/Customers/Getcustomer/?id=" + id);
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<GetByIdCustomerDto>(jsonData);
				return View(values);
			}
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> UpdateCustomer(UpdateCustomerDto updateCustomerDto)
		{
			var client = _httpClientFactory.CreateClient();
			var jsonData = JsonConvert.SerializeObject(updateCustomerDto);
			StringContent stringContent = new StringContent(jsonData,Encoding.UTF8,"application/json");
			var responseMessage = await client.PutAsync("https://localhost:44382/api/Customers", stringContent);
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("CustomerList");
			}
			return View();
		}
	}
}
