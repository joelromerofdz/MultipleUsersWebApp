using Shared.Dtos.Province;

namespace Application.Services.Interfaces.Province
{
    public interface IProvinceService
    {
        Task<IEnumerable<ProvinceResponse>> GetProvincesAsync();
        Task AddProvinceAsync(ProvinceRequest provinceRequests);
    }
}
