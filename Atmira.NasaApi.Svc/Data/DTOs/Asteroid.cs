using System;

namespace Atmira.NasaApi.Svc.Data.DTOs
{
    public class Asteroid
    {
        public string Nombre { get; set; }
        public double? Diametro { get; set; }
        public double? Velocidad { get; set; }
        public DateTime? Fecha { get; set; }
        public string Planeta { get; set; }
    }
}
