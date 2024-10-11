using Bogus;
using BookingApi.Data;
using BookingApi.Migrations;
using BookingApi.Models.Domain;
using BookingApi.Models.DTO;
using BookingApi.Services;
using Microsoft.EntityFrameworkCore;

namespace BookingApi.Repositories.NewRepository
{
    public class SQLNewRepository : INewRepository
    {
        private readonly BookingDbContext dbContext;

        public SQLNewRepository(BookingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ServiceResponse<News>> CreateAsync(News @new)
        {
            var response = new ServiceResponse<News>();
            try
            {
                await dbContext.New.AddRangeAsync(@new);  // Sử dụng AddRangeAsync để thêm dữ liệu bất đồng bộ
                await dbContext.SaveChangesAsync();  // Sử dụng SaveChangesAsync để lưu dữ liệu xuống DB

                response.Data = @new;
                response.Message = "Created news successfully";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error fetching news: {ex.Message}";
            }

            return response;  // Trả về danh sách các bản ghi đã tạo
        }

        public async Task<ServiceResponse<News?>> DeleteAsync(Guid id)
        {
            var response = new ServiceResponse<News?>();
            try
            {
                var existingNew = await dbContext.New.FirstOrDefaultAsync(x => x.Id == id);

                if (existingNew == null)
                {
                    response.Success = false;
                    response.Message = "News not found";
                    return response;
                }

                dbContext.New.Remove(existingNew);
                await dbContext.SaveChangesAsync();

                // Thiết lập thông tin thành công
                response.Data = existingNew; // Trả về đối tượng đã xóa
                response.Success = true;
                response.Message = "News deleted successfully"; // Thông báo thành công
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error deleted news: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<List<News>>> GetAllAsync()
        {
            var response = new ServiceResponse<List<News>>();
            try
            {
                var newsList = await dbContext.New.ToListAsync();

                // Gán dữ liệu vào ServiceResponse
                response.Data = newsList;
                response.Message = "Fetched news successfully";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error fetching news: {ex.Message}";
            }
            return response;// await dbContext.New.ToListAsync();
        }

        public async Task<ServiceResponse<News?>> UpdateAsync(Guid id, News @new)
        {
            var response = new ServiceResponse<News?>();

            try
            {
                var existingNew = await dbContext.New.FirstOrDefaultAsync(x => x.Id == id);
                if (existingNew == null)
                {
                    response.Success = false;
                    response.Message = "News not found";
                    return response;
                }

                // Cập nhật thông tin
                existingNew.Title = @new.Title;
                existingNew.Description = @new.Description;
                existingNew.Type = @new.Type;

                // Lưu thay đổi
                await dbContext.SaveChangesAsync();

                // Thiết lập thông tin thành công vào response
                response.Data = existingNew;
                response.Success = true; // Đánh dấu là thành công
                response.Message = "News updated successfully";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error updating news: {ex.Message}";
            }

            return response;
        }

    }
}
