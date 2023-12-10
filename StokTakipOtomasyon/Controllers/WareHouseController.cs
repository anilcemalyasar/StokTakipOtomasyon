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
            var wareHouses = await _wareHouseRepository.GetAllWareHousesAsync();

            // Map Domain Models To DTOs 
            // return Ok(_mapper.Map<List<WareHouseDto>>(wareHouses));
            return Ok(wareHouses);
        }

    }
}
