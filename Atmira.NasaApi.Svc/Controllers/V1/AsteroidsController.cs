using Atmira.NasaApi.Svc.Data.DTOs;
using Atmira.NasaApi.Svc.Services.V1.Asteroids;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Atmira.NasaApi.Svc.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AsteroidsController : ControllerBase
    {
        private readonly IAsteroidsService _asteroidsService;

        public AsteroidsController(IAsteroidsService asteroidsService)
        {
            _asteroidsService = asteroidsService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Asteroid>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult GetAsteroids(int days)
        {
            try
            {
                if (!_asteroidsService.IsValidDay(days))
                    return StatusCode(StatusCodes.Status400BadRequest, "Query param value of 'days' must be between 1 and 7");

                return Ok(_asteroidsService.GetAsteroids(days));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex?.Message);
            }
        }
    }
}
