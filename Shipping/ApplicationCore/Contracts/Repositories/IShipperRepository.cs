using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repositories;

public interface IShipperRepository: IBaseRepository<Shipper>
{
    /*Task<List<Shipper>> GetShipperByRegion(int regionId);
    Task<Shipper> GetShipperByEmail(string email);*/
}