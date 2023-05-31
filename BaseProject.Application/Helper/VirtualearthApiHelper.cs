using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BaseProject.Application.Helper
{
    
    public static class VirtualearthApiHelper
    {
        
        public async static Task<VirtualearthApiModelResponse> GetLatLongFromAddress(string address,string key)
        {
            var client = new HttpClient();

            var url = $"https://dev.virtualearth.net/REST/v1/Locations?addressLine={address}&key={key}";
            var httpResponse = await client.GetAsync(url);
            var content = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<VirtualearthApiModelResponse>(content);
            return response;

        }
        public async static Task<VirtualearthApiModelResponse> GetLatLongFromAddress(string address, string zipCode, string key)
        {
            var client = new HttpClient();

            var url = $"https://dev.virtualearth.net/REST/v1/Locations?postalCode={zipCode}addressLine={address}&key={key}";
            var httpResponse = await client.GetAsync(url);
            var content = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<VirtualearthApiModelResponse>(content);
            return response;

        }
    }
}


public class VirtualearthApiModelResponse
{
    public string StatusCode { get; set; }
   
    public List<ResourceSets> ResourceSets { get; set; }
}

public class ResourceSets
{
    public List<Resource> Resources { get; set; } 

}

public class Resource
{
    public Point Point { get; set; }
    public string Name { get; set; }
    public Adrress Address { get; set; }
}


public class Adrress
{
    public string FormattedAddress { get; set; }
    public string AdminDistrict { get; set; }
    public string AdminDistrict2 { get; set; }
    public string Locality { get; set; }
    public string PostalCode { get; set; }
    public string CountryRegion { get; set; }

}

public class Point
{
    public double[] Coordinates { get; set; }

}