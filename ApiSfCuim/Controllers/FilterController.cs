using ApiSfCuim.Data.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSfCuim.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilterController : ControllerBase
    {
        private readonly FilterContext _context;

        public FilterController (FilterContext context)
        {
            _context = context;
        }

        [HttpGet("{reference}")]


    }
}
