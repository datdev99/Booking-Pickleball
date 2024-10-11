using AutoMapper;
using BookingApi.Models.Domain;
using BookingApi.Models.DTO.NewDto;
using BookingApi.Repositories.NewRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly INewRepository _newRepository;

        public NewsController(IMapper mapper ,INewRepository newRepository)
        {
            this.mapper = mapper;
            _newRepository = newRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _newRepository.GetAllAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddNewRequestDto addNewRequestDto)
        {
            var newDomainModel = mapper.Map<News>(addNewRequestDto);
            await _newRepository.CreateAsync(newDomainModel);
            return Ok(mapper.Map<GetNewDto>(newDomainModel));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateNewRequestDto updateNewRequestDto)
        {
            // Ánh xạ từ DTO sang Domain Model
            var newDomainModel = mapper.Map<News>(updateNewRequestDto);

            // Gọi phương thức UpdateAsync
            var response = await _newRepository.UpdateAsync(id, newDomainModel);

            // Kiểm tra trạng thái thành công
            if (!response.Success)
            {
                // Nếu không thành công, trả về thông báo lỗi
                return NotFound(response.Message);
            }

            // Nếu cập nhật thành công, trả về đối tượng DTO đã cập nhật
            return Ok(mapper.Map<GetNewDto>(response.Data));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var response = await _newRepository.DeleteAsync(id);
            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response);
        }
    }
}
