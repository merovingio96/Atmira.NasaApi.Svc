using Atmira.NasaApi.Svc.Data.DTOs;
using System.Collections.Generic;

namespace Atmira.NasaApi.Svc.Services.V1.Asteroids
{
    public interface IAsteroidsService
    {
        public List<Asteroid> GetAsteroids(int days);
    }
}
