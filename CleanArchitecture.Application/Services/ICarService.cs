using CleanArchitecture.Application.Features.CarFeatures.Commands.CreateCar;
using CleanArchitecture.Application.Features.CarFeatures.Queries.GetAllCars;
using CleanArchitecture.Domain.Entities;
using EntityFrameworkCorePagination.Nuget.Pagination;

namespace CleanArchitecture.Application.Services;

public interface ICarService
{
    Task CreateAsync(CreateCarCommand request, CancellationToken cancellationToken);
    Task<PaginationResult<Car>> GetAllAsync(GetAllCarsQuery request, CancellationToken cancellationToken);
}
