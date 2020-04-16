using System;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;
using DevopsGenie.Reponook.HelperClasses;
using DevopsGenie.Reponook.Models;
using DevopsGenie.Reponook.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System.Threading.Tasks;
using MongoDB.Bson;
using System.Collections.Generic;

namespace DevopsGenie.Reponook.Controllers
{
    [Route("/")]
    public class RepositoryNookController : Controller
    {
        // GET all databases
        [HttpGet]   
        public async Task<IActionResult> GetDatabases([FromServices]IRepositoryService repositoryService)
        {
            try
            {
                List<string> found = await repositoryService.GetDatabases();
                return Ok(found);
            }
            catch (Exception exc)
            {
                return BadRequest("Get databases failed. " + exc.Message);
            }
        }
        // GET all collections
        [HttpGet("{database}")]   
        public async Task<IActionResult> GetCollections([FromServices]IRepositoryService repositoryService, string database)
        {
            try
            {
                List<string> found = await repositoryService.GetCollections(database);
                return Ok(found);
            }
            catch (Exception exc)
            {
                return BadRequest("Get collections failed. " + exc.Message);
            }
        }
        // POST (C)reate Repository object - CRUD operation: Create
        [HttpPost("{database}/{collection}")]  
        public async Task<IActionResult> CreateRepositoryObject([FromServices]IRepositoryService repositoryService, string database, string collection, [FromBody]Repository repoObject)
        {
            try
            {
                return Ok("Created." + await repositoryService.Create(database, collection, repoObject));
            }
            catch(Exception exc)
            {
                return BadRequest("Create failed." + exc.ToString());
            }

        }
        // GET (R)ead All Repository objects (Query by "*" wildcard operation, or default: all records API call) - CRUD operation: Read (All)
        [HttpGet("{database}/{collection}")]   
        public async Task<IActionResult> GetAllRepositoryObjects([FromServices]IRepositoryService repositoryService, string database, string collection, string _id)
        {
            try
            {
                List<Repository> found = await repositoryService.ReadAll(database, collection);

                return Ok(found);
            }
            catch (Exception exc)
            {
                return BadRequest("Read All failed." + exc.ToString());
            }

        }
        // GET Repository (Query by Id)      
        [HttpGet("{database}/{collection}/{_id}")]   
        public async Task<IActionResult> GetRepositoryObject([FromServices]IRepositoryService repositoryService, string database, string collection, string _id)
        {
            try
            {
                Repository found = await repositoryService.Read(_id, database, collection);

                return Ok(found);
            }
            catch (Exception exc)
            {
                return BadRequest("Read failed." + exc.ToString());
            }

        }
        // GET query by key (Key should be main index, NOT a PRIMARY KEY.no guarantee of uniqueness by reponook service)
        [HttpGet("{database}/{collection}/key/{key}")]   
        public async Task<IActionResult> QueryByKeyRepositoryObject([FromServices]IRepositoryService repositoryService, string database, string collection, string key)
        {
            try
            {
                List<Repository> found = await repositoryService.QueryByKey(database, collection, key);

                if( found.Count == 0)
                {
                    return NotFound(string.Format("check query string argument key={0}",key));
                }

                return Ok(found);
            }
            catch (Exception exc)
            {
                return BadRequest("Query exception." + exc.ToString());
            }

        }
        [HttpGet("{database}/{collection}/tag/{tag}")]   // query by tagName = tagValue
        public async Task<IActionResult> QueryByTagRepositoryObject([FromServices]IRepositoryService repositoryService, string database, string collection, string tag)
        {
            try
            {
                List<Repository> found = await repositoryService.QueryByTag(database, collection, tag);

                if (found.Count == 0)
                {
                    return NotFound(string.Format("check query string argument tag={0}", tag));
                }

                return Ok(found);
            }
            catch (Exception exc)
            {
                return BadRequest("Query failed." + exc.ToString());
            }

        }
        [HttpPut("{database}/{collection}")]  // update
        public async Task<IActionResult> UpdateRepositoryObject([FromServices]IRepositoryService repositoryService, string database, string collection, [FromBody]Repository repoObject)
        {
            try
            {
                await repositoryService.Update( database, collection, repoObject);
            }
            catch (Exception exc)
            {
                return BadRequest("Update failed. " + exc.ToString());
            }
            try
            {
                await repositoryService.Update(database, collection, repoObject);

                return Ok( "Updated. " + repoObject.ToString());
            }
            catch (Exception exc)
            {
                return BadRequest("Retreiving Update failed. Record may still have been written. " + exc.ToString());
            }

        }
        [HttpDelete("{database}/{collection}/{_id}")]    // delete
        public async Task<IActionResult> DeleteRepositoryObject([FromServices]IRepositoryService repositoryService, string database, string collection, string _id, [FromBody]Repository repoObject)
        {
            try
            {
                await repositoryService.Delete(database, collection, _id);

                return Ok($"_id: {_id} deleted.");
            }
            catch (Exception exc)
            {
                return BadRequest( $"Delete failed for _id: {_id}." + exc.ToString());
            }

        }
    }
}
