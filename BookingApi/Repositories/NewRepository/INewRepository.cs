using BookingApi.Models.Domain;
using BookingApi.Services;

namespace BookingApi.Repositories.NewRepository
{
    public interface INewRepository
    {
        Task<ServiceResponse<List<News>>> GetAllAsync();
        Task<ServiceResponse<News>> CreateAsync(News @new);
        Task<ServiceResponse<News?>> UpdateAsync(Guid id, News @new);
        Task<ServiceResponse<News?>> DeleteAsync(Guid id);
    }
}
