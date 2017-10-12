using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace TourCore.Controllers
{
    [Authorize("Permission")]
    [EnableCors("MyDomain")]
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            string currentDirectory = ApplicationEnvironment.ApplicationBasePath;
            string dbPath = currentDirectory + @"tour.db";
            bool ret = System.IO.File.Exists(dbPath);
            return new string[] { ret.ToString(), dbPath };
        }
      
    }
}
