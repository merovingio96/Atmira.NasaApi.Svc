using Atmira.NasaApi.Svc.Configuration;
using Atmira.NasaApi.Svc.Data.DTOs;
using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Atmira.NasaApi.Svc.Services.V1.Asteroids
{
    public class AsteroidsService : IAsteroidsService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper _mapper;

        public AsteroidsService(IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;
        }

        public List<Asteroid> GetAsteroids(int days)
        {
            try
            {
                DateTime startDate = DateTime.UtcNow, endDate = DateTime.UtcNow.AddDays(days);
                List<Asteroid> asteroidsList = new List<Asteroid>();

                using (HttpClient httpClient = _httpClientFactory.CreateClient())
                {
                    string requestUri = Configurations.ApiNasaUrl + "?start_date=" + startDate.ToString("yyyy-MM-dd") + "&end_date=" + endDate.ToString("yyyy-MM-dd") + "&api_key=" + Configurations.ApiNasaKey;
                    HttpResponseMessage nasaResponse = httpClient.GetAsync(requestUri).Result;

                    string jsonResponse = nasaResponse.Content.ReadAsStringAsync().Result;

                    if (!string.IsNullOrEmpty(jsonResponse))
                    {
                        object nearEarthObjectsBasic = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonResponse).GetValueOrDefault("near_earth_objects");

                        if (nearEarthObjectsBasic != null)
                        {
                            Dictionary<string, List<NearEarthObject>> nearEarthObjectsParsed = JsonConvert.DeserializeObject<Dictionary<string, List<NearEarthObject>>>(nearEarthObjectsBasic.ToString());

                            foreach (List<NearEarthObject> nearEarthObjectsPerDayList in nearEarthObjectsParsed.Values)
                            {
                                foreach (NearEarthObject nearEarthObject in nearEarthObjectsPerDayList)
                                {
                                    asteroidsList.Add(_mapper.Map<Asteroid>(nearEarthObject));
                                }
                            }
                        }
                    }
                }

                return asteroidsList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex?.Message, ex?.InnerException);
            }
        }
    }
}
