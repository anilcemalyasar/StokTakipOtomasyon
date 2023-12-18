using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StokTakipOtomasyon.Models.Domain;
using StokTakipOtomasyon.Models.DTO;
using StokTakipOtomasyon.Repositories.Abstracts;

namespace StokTakipOtomasyon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WareHouseController : ControllerBase
    {
        private readonly IWareHouseRepository _wareHouseRepository;
        private readonly IMapper _mapper;

        public WareHouseController(
            IWareHouseRepository wareHouseRepository,
            IMapper mapper)
        {
            _wareHouseRepository = wareHouseRepository;
            _mapper = mapper;
        }


        // GET ALL: /api/WareHouse/filterOn=Name&filterQuery=Fabrik&sortBy=Name&isAscending=true&pageNumber=1&pageSize=3
        [HttpGet]
        public async Task<IActionResult> GetAllWareHouses([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber=1, [FromQuery] int pageSize=3)
        {
            // Get All WareHouses From Database
            var wareHouses = await _wareHouseRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);

            // Map Domain Models To DTOs 
            return Ok(_mapper.Map<List<WareHouseDto>>(wareHouses));
            // return Ok(wareHouses);
        }


        // GET By Id: /api/WareHouse/{id}
        [HttpGet]
        [Route("{id=int}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            // Find domain model with given ID
            var wareHouse = await _wareHouseRepository.GetByIdAsync(id);
            
            // Check if domain model exists
            if (wareHouse is null)
            {
                return NotFound();
            }

            // Map Domain Model To DTO and Return
            return Ok(_mapper.Map<WareHouseDto>(wareHouse));

        }

        // POST: /api/WareHouse
        [HttpPost]
        public async Task<IActionResult> CreateWarehouse([FromBody] AddWareHouseRequestDto addWareHouseRequestDto)
        {
            // Map DTO to Domain Model
            var warehouseDomain = _mapper.Map<WareHouse>(addWareHouseRequestDto);

            warehouseDomain = await _wareHouseRepository.CreateAsync(warehouseDomain, addWareHouseRequestDto.CompanyId);

            if (warehouseDomain is null)
            {
                return NotFound("Sorry, the company with given ID not found!");
            }

            return Ok(_mapper.Map<WareHouseDto>(warehouseDomain));
        }



        // PUT: /api/WareHouse/{id}
        [HttpPut]
        [Route("{id=int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateWareHouseRequestDto updateWareHouseRequestDto)
        {
            // Map DTO to Domain Model
            var warehouseDomainModel = _mapper.Map<WareHouse>(updateWareHouseRequestDto);

            // Check if domain model with given Id exists
            warehouseDomainModel = await _wareHouseRepository.UpdateAsync(id, warehouseDomainModel);

            if (warehouseDomainModel is null)
            {
                return NotFound();
            }

            // Map Domain Model back to DTO
            return Ok(_mapper.Map<WareHouseDto>(warehouseDomainModel));

        }


        // DELETE: /api/WareHouse/{id}
        [HttpDelete]
        [Route("{id=int}")]
        public async Task<IActionResult> DeleteWarehouse([FromRoute] int id)
        {
            var wareHouse = await _wareHouseRepository.DeleteAsync(id);

            if (wareHouse is null)
            {
                return NotFound("Sorry, the warehouse with given ID not found!");
            }

            // Map Domain Model To DTO
            return Ok(_mapper.Map<WareHouseDto>(wareHouse));
        }

    }
}
