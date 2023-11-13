using Application.Services.Interfaces.Province;
using Domain.Entities;
using Domain.Repositories;
using Shared.Dtos.Province;

namespace Application.Services
{
    public class ProvinceService : IProvinceService
    {
        private readonly IRepository<Province, ProvinceResponse> _provinceRepository;

        public ProvinceService(IRepository<Province, ProvinceResponse> provinceRepository)
        {
            this._provinceRepository = provinceRepository;
        }

        public async Task<IEnumerable<ProvinceResponse>> GetProvincesAsync()
        {
            return await _provinceRepository.GetAllAsync();
        }

        public async Task AddProvinceAsync(ProvinceRequest ProvinceRequest)
        {
            if (ProvinceRequest == null)
            {
                return;
            }

            Province province = new Province()
            {
                ProvinceName = ProvinceRequest.ProvinceName,
                CountryId = ProvinceRequest.CountryId
            };

            await _provinceRepository.AddAsync(province);
        }
    }
}
