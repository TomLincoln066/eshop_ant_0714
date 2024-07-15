using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shipping.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipperController : ControllerBase
    {
        private readonly IShipperService shipperService;
        public ShipperController(IShipperService _shipperService)
        {
            shipperService = _shipperService;
        }

        [HttpGet]
        [Route("")]
        [Authorize]
        public async Task<IActionResult> GetAllShipper()
        {
            var shippers = await shipperService.GetAllShippers(); 
            if (!shippers.Any())
            {
                return NotFound(new { error = "No shippers found, please try later"});
            }
            return Ok(shippers);
        }
        
        [HttpGet]
        [Route("{id:int}", Name = "GetShipperDetails")]
        [Authorize]
        public async Task<IActionResult> GetShipperDetails(int id)
        {
            var shipper = await shipperService.GetShipperByIdAsync(id); 
            if (shipper == null)
            {
                return NotFound(new { errorMessage = "No shipper found for this id" });
            }

            return Ok(shipper);
        }
        
        [HttpPost]
        [Route("")]
        [Authorize]
        public async Task<IActionResult> CreateShipper(ShipperRequestModel model)
        {
            if (!ModelState.IsValid)
                // 400 status code
                return BadRequest();

            var shipper = await shipperService.AddShipperAsync(model);
            return CreatedAtAction
                ("GetShipperDetails", new { controller = "Shipper", id = shipper }, "Shipper Created");
        }
        
        
        [HttpDelete]
        [Route("delete-{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteShipper(int id)
        {
            var shipper = await shipperService.GetShipperByIdAsync(id);
            if (shipper == null)
            {
                return NotFound(new { errorMessage = "No shipper found for this id" });
            }
            
            var response = new { Message = "Shipper is deleted" };
            if (await shipperService.DeleteShipperAsync(id) > 0)
                return Ok(response);
            return NoContent();
        }
        
        [HttpPut]
        [Route("")]
        [Authorize]
        public async Task<IActionResult> UpdateShipper(ShipperRequestModel model)
        {
            if (!ModelState.IsValid)
                // 400 status code
                return BadRequest();
            var response = new { Message = "Shipper is updated" };
            if (await shipperService.UpdateShipperAsync(model) > 0)
                return Ok(response);
            return NoContent();
        }

        [HttpGet]
        [Route("region/{region}")]
        [Authorize]
        public async Task<IActionResult> GetShipperByRegion(string region)
        {
            var shippers = await shipperService.GetShipperByRegion(region);
            if (shippers == null)
            {
                return NotFound(new { errorMessage = "No shipper found for this id" });
            }
            return Ok(shippers);
        }
        
        
    }
}
