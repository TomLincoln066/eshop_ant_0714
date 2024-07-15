using System.Linq.Expressions;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;


namespace Infrastructure.Services;

public class ShipperService: IShipperService
{
    private readonly IShipperRepository _shipperRepository;
    public ShipperService(IShipperRepository shipperRepository)
    {
        _shipperRepository = shipperRepository;
    }

    public async Task<int> AddShipperAsync(ShipperRequestModel model)
    {
        Expression<Func<Shipper, bool>> filter = shipper => shipper.Email == model.Email;
        var existingShipper = await _shipperRepository.GetByConditionAsync(filter);
        if (existingShipper.Any())
        {
            throw new Exception("Email is already in use.");
        }
        
        var newShipper = new Shipper {
            Name = model.Name,
            Email = model.Email,
            Phone = model.Phone,
            ContactPerson = model.ContactPerson
        };
       await _shipperRepository.AddAsync(newShipper);
       return newShipper.Id;
    }

    public async Task<int> UpdateShipperAsync(ShipperRequestModel model)
    {
        //Expression<Func<Shipper, bool>> filter = shipper => shipper.Email == model.Email;
        //var existingShipper = await _shipperRepository.GetByConditionAsync(filter);
        var existingShipper = await  _shipperRepository.GetByIdAsync(model.Id);
        if (existingShipper == null)
        {
            throw new Exception("Shipper does not exist");
           
        }
        existingShipper.Name = model.Name;
        existingShipper.Email = model.Email;
        existingShipper.Phone = model.Phone;
        existingShipper.ContactPerson = model.ContactPerson;

        return await _shipperRepository.UpdateAsync(existingShipper);
    }

    public Task<int> DeleteShipperAsync(int id)
    {
        return _shipperRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<ShipperResponseModel>> GetAllShippers()
    {
        var shippers = await _shipperRepository.GetAllAsync();

        return shippers.Select(shipper => new ShipperResponseModel
        {
            Id = shipper.Id,
            Name = shipper.Name,
            Email = shipper.Email,
            Phone = shipper.Phone,
            ContactPerson = shipper.ContactPerson
        }).ToList();
    }

    public async Task<ShipperResponseModel> GetShipperByIdAsync(int id)
    {
        var shipper = await _shipperRepository.GetByIdAsync(id);
        if (shipper == null)
        {
            return null;

        }

        var shipperResponseModel = new ShipperResponseModel
            {
                Id = shipper.Id,
                Name = shipper.Name,
                Email = shipper.Email,
                Phone = shipper.Phone,
                ContactPerson = shipper.ContactPerson
            };
        return shipperResponseModel;

    }

    public async Task<IEnumerable<ShipperResponseModel>> GetShipperByRegion(string region)
    {
        Expression<Func<Shipper, bool>> filter = shipper => shipper.ShipperRegions
            .Any(sr => sr.Region.Name == region);
        
        
        var shippersInRegion = await _shipperRepository.GetByConditionAsync(filter);

        if (shippersInRegion == null || !shippersInRegion.Any())
        {
            return null;
        }
        
        return shippersInRegion.Select(shipper => new ShipperResponseModel
        {
            Id = shipper.Id,
            Name = shipper.Name,
            Email = shipper.Email,
            Phone = shipper.Phone,
            ContactPerson = shipper.ContactPerson
        });
        
    }
}