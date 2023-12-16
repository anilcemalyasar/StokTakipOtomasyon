using AutoMapper;
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

        [HttpGet]
        public async Task<IActionResult> GetAllWareHouses()
        {
            // Get All WareHouses From Database
            var wareHouses = await _wareHouseRepository.GetAllAsync();

            // Map Domain Models To DTOs 
            return Ok(_mapper.Map<List<WareHouseDto>>(wareHouses));
            // return Ok(wareHouses);
        }

        [HttpGet]
        [Route("{id=int}")]
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
            return Ok(_mapper.Map<WareHouse>(wareHouse));

        }

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
        

    }
}
