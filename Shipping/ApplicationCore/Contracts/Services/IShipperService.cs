using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services;

public interface IShipperService
{
    Task<int> AddShipperAsync(ShipperRequestModel model);
    Task<int> UpdateShipperAsync(ShipperRequestModel model);
    Task<int> DeleteShipperAsync(int id);
    Task<IEnumerable<ShipperResponseModel>> GetAllShippers();
    Task<ShipperResponseModel> GetShipperByIdAsync(int id);
    Task<IEnumerable<ShipperResponseModel>> GetShipperByRegion(string region);

}