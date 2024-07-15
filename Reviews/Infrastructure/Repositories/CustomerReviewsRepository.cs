using System.Data;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Dapper;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class CustomerReviewsRepository: IBaseRepository<CustomerReview>, ICustomerReviewsRepository
{
    private readonly DbConnection _dbConnection;

    public CustomerReviewsRepository(DbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }
    public async Task<int> DeleteAsync(int id)
    {
        using (IDbConnection conn = _dbConnection.GetConnection())
        {
            return await conn.ExecuteAsync("DELETE FROM CustomerReviews WHERE Id = @CustomerReviewsId",
                new { CustomerReviewsId = id });
        }
    }
   
//Automapper

    public async Task<IEnumerable<CustomerReview>> GetAllAsync()
    {
        using(IDbConnection conn = _dbConnection.GetConnection())
        {
            return await conn.QueryAsync<CustomerReview>("SELECT * FROM CustomerReviews");
        }
    }

    public async Task<CustomerReview> GetByIdAsync(int id)
    {
        using (IDbConnection conn = _dbConnection.GetConnection())
        {
            return await conn.QueryFirstOrDefaultAsync<CustomerReview>
                ("SELECT * FROM CustomerReviews WHERE Id = @CustomerReviewId", new { CustomerReviewId = id });
        }
    }

    public  async Task<int> AddAsync(CustomerReview entity)
    {
        using (IDbConnection conn = _dbConnection.GetConnection())
        {
            return await conn.ExecuteAsync("INSERT INTO CustomerReviews " +
                                           "(CustomerId, CustomerName, OrderId, OrderDate, ProductId, ProductName, RatingValue, Comment, ReviewDate, Status) " +
                                           "VALUES (@CustomerId, @CustomerName, @OrderId, @OrderDate, @ProductId, @ProductName, @RatingValue, @Comment, @ReviewDate, @Status)",
                entity);

        }
    }
    public async Task<int> UpdateAsync(CustomerReview entity)
    {
        using (IDbConnection conn = _dbConnection.GetConnection())
        {
            return await conn.ExecuteAsync("UPDATE CustomerReviews SET CustomerId=@CustomerId," +
                                     "CustomerName = @CustomerName,OrderId= @OrderId,OrderDate= @OrderDate," +
                                     " ProductId = @ProductId, ProductName=@ProductName, RatingValue=@RatingValue, " +
                                     "Comment= @Comment, ReviewDate=@ReviewDate, Status=@Status WHERE Id=@Id", entity);
        }
    }
    
    public async Task<IEnumerable<CustomerReview>> GetReviewsByUserAsync(int userId)
    {
        using (IDbConnection conn = _dbConnection.GetConnection())
        {
            return await conn.QueryAsync<CustomerReview>(
                "SELECT * FROM CustomerReviews WHERE CustomerId = @UserId",
                new { UserId = userId });
        }
    }
    
    public async Task<IEnumerable<CustomerReview>> GetReviewsByProductAsync(string productId)
    {
        using (IDbConnection conn = _dbConnection.GetConnection())
        {
            return await conn.QueryAsync<CustomerReview>(
                "SELECT * FROM CustomerReviews WHERE ProductId = @ProductId",
                new { ProductId = productId });
        }
    }

    public async Task<IEnumerable<CustomerReview>> GetReviewsByYearAsync(int year)
    {
        using (IDbConnection conn = _dbConnection.GetConnection())
        {
            return await conn.QueryAsync<CustomerReview>(
                "SELECT * FROM CustomerReviews WHERE YEAR(ReviewDate) = @Year",
                new { Year = year });
        }
    }
    
}