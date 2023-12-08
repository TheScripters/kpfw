using kpfw.DataModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kpfw.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataApiController : ControllerBase
    {
        private readonly DataContext _context;
        public DataApiController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route(nameof(GetEpisodes))]
        public dynamic GetEpisodes()
        {
            var eps = _context.Episodes.Select(x => new { x.Id, x.Title, x.AirDate, x.ProductionNumber, Season = Convert.ToInt32(x.ProductionNumber[0].ToString()) });
            return eps;
        }
    }
}
