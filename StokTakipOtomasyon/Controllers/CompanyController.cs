using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StokTakipOtomasyon.Models.Domain;
using StokTakipOtomasyon.Models.DTO;
using StokTakipOtomasyon.Repositories.Abstracts;
using StokTakipOtomasyon.Repositories.Concretes;

namespace StokTakipOtomasyon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository companyRepository;
        private readonly IWareHouseRepository wareHouseRepository;
        private readonly IMapper mapper;

        public CompanyController(ICompanyRepository companyRepository, 
            IMapper mapper, IWareHouseRepository wareHouseRepository)
        {
            this.companyRepository = companyRepository;
            this.mapper = mapper;
            this.wareHouseRepository = wareHouseRepository;
        }

        // GET ALL : /api/Company/
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Get All Companies From Database
            var companies = await companyRepository.GetAllAsync();

            var companyDtos = new List<CompanyDto>();

            // Convert them to DTOs
            foreach (var company in companies)
            {
                var wareHouseDtos = mapper.Map<List<WareHouseDto>>(company.WareHouses);
                var companyDto = mapper.Map<CompanyDto>(company);
                companyDto.WareHouses = wareHouseDtos;
                companyDtos.Add(companyDto);
            }

            // Return DTO
            return Ok(companyDtos);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            // Find company with given Id
            var company = await companyRepository.GetByIdAsync(id);

            // Check if exists
            if (company is null)
            {
                return NotFound("Sorry, company couldn't be found!");
            }

            return Ok(mapper.Map<CompanyDto>(company));
        }

        
        // POST : /api/Company
        [HttpPost]
        public async Task<IActionResult> AddCompany([FromBody] AddCompanyDto addCompanyDto)
        {
            // Map DTO to Domain Model
            var companyDomainModel = mapper.Map<Company>(addCompanyDto);

            // Call Create Company Method in repository
            companyDomainModel = await companyRepository.AddCompanyAsync(companyDomainModel);

            // Check if created
            if (companyDomainModel is null)
            {
                return BadRequest("Company couldn't be created");
            }

            // Map created Domain Model back to DTO
            return Ok(mapper.Map<CompanyDto>(companyDomainModel));

        }


        // PUT : /api/Company/id={}&wareHouseId={}
        [HttpPut]
        [Route("Update/{id:int}")]
        public async Task<IActionResult> AssignWarehouseToCompany([FromRoute] int id, [FromQuery] int wareHouseId)
        {
            var wareHouseDomain = await wareHouseRepository.GetByIdAsync(wareHouseId);
            
            if (wareHouseDomain is null)
            {
                return NotFound("Sorry, Warehouse with given ID not found!");
            }

            var companyDomainModel = await companyRepository.GetByIdAsync(id);

            if (companyDomainModel is null)
            {
                return NotFound("Sorry, Company with given ID not found!");
            }

            companyDomainModel = await companyRepository.AssignWareHouseToCompany(companyDomainModel, wareHouseDomain);
            return Ok(mapper.Map<CompanyDto>(companyDomainModel));
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateCompany([FromRoute] int id, [FromBody] UpdateCompanyRequestDto updateCompanyRequestDto)
        {
            // Map DTO to Domain Model
            var companyDomainModel = mapper.Map<Company>(updateCompanyRequestDto);

            // Update Domain Model in Database
            companyDomainModel = await companyRepository.UpdateCompanyAsync(id, companyDomainModel);

            // Check if exists
            if (companyDomainModel is null)
            {
                return NotFound("Sorry, Company with given ID not found!");
            }

            // Map Domain Model Back To Dto
            return Ok(mapper.Map<CompanyDto>(companyDomainModel));

        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteCompany([FromRoute] int id)
        {
            var companyDomainModel = await companyRepository.GetByIdAsync(id);

            // Check if exists
            if (companyDomainModel is null)
            {
                return NotFound("Sorry, Company with given ID not found!");
            }

            companyDomainModel = await companyRepository.DeleteCompanyByIdAsync(id);
            return Ok(mapper.Map<CompanyDto>(companyDomainModel));

        }


    }
}
