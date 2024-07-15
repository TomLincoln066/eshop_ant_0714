using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ShipperRepository: BaseRepository<Shipper>, IShipperRepository
{
    private IShipperRepository _shipperRepositoryImplementation;

    public ShipperRepository(ShippingDbContext context) : base(context)
    {
    }

}