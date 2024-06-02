using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlantNurseryAssessment.Models;

namespace PlantNurseryAssessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NurseryController : ControllerBase
    {
        private readonly NurseryContext _context;

        public NurseryController(NurseryContext context)
        {
            this._context = context;
        }

        // GET: api/Plants
         [HttpGet]
        public async Task<ActionResult<List<Plant>>> Get()
        {

            try
            {
                List<Plant> plants = await _context.Plants.Where(p => p.SaleStatus == SaleStatus.AVAILABLE).ToListAsync();
                if (plants.Count == 0)
                {
                    return StatusCode(404, "NO available Plant in Inventory");
                }
                return Ok(await _context.Plants.ToListAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
           
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------

        
        // GET: api/Plants/5
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Plant>> GetPlant(int id)
        {
            try
            {
                if (_context.Plants == null)
                {
                    return NotFound();
                }
                var plant = await _context.Plants.FindAsync(id);

                if (plant == null)
                {
                    return NotFound("This Id plant is not available!");
                }

                return plant;
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        //----------------------------------------------------
        [HttpPut("{id}")]
        public async Task<ActionResult<List<Plant>>> UpdateSaleStatus(int id,SaleStatus saleStatus)
        {
            try
            {
                var edittedPlant = await _context.Plants.FindAsync(id);
                if (edittedPlant == null)
                    return BadRequest("Plant not found.");

                edittedPlant.SaleStatus = saleStatus;
                edittedPlant.UpdatedOn = DateTime.Now.ToUniversalTime();
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    message = $"Plant with ID {id}'s salestatus has been updated to sold.",
                    plant = edittedPlant
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

       
        //---------------------------------
        
        [HttpPost]
        public async Task<ActionResult<List<Plant>>> AddPlant(Plant newplant)
        {

            try
            {
                _context.Plants.Add(newplant);
                await _context.SaveChangesAsync();
                return Ok(await _context.Plants.ToListAsync());
            }
            catch(Exception ex) {
                return BadRequest();
                   }

        }
        //-----------------------------------------
        [HttpPut]
        public async Task<ActionResult<List<Plant>>> EditPlant(Plant edittedPlantRequest)
        {
            try
            {
                var edittedPlant = await _context.Plants.FindAsync(edittedPlantRequest.Id);
                if (edittedPlant == null)
                    return BadRequest("Plant not found.");
                edittedPlant.Id = edittedPlantRequest.Id;
                edittedPlant.Name = edittedPlantRequest.Name;
                edittedPlant.Price = edittedPlantRequest.Price;
                //edittedPlant.SaleStatus = edittedPlantRequest.SaleStatus;
                edittedPlant.UpdatedOn = DateTime.Now.ToUniversalTime();

                await _context.SaveChangesAsync();

                return Ok(await _context.Plants.ToListAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }
        //
        // GET api/plants/sold
        [HttpGet("sold")]
        public async Task<IActionResult> GetSoldPlants()
        {
            try
            {
                List<object> result = (from purchase in _context.Purchases
                                       join plant in _context.Plants on purchase.PlantId equals plant.Id
                                       join customer in _context.Customers on purchase.CustomerId equals customer.Id
                                       where purchase.IsActive == false
                                       select new
                                       {
                                           PlantName = plant.Name,
                                           PlantPrice = plant.Price,
                                           CustomerName = customer.Name,
                                           CustomerPhone = customer.PhoneNumber
                                       }).ToList<object>();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }


            
           

        }

    }
}
