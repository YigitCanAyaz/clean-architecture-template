using AutoMapper;
using CleanArchitecture.Application.Features.CarFeatures.Commands.CreateCar;
using CleanArchitecture.Application.Features.CarFeatures.Queries.GetAllCars;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Persistence.Context;
using EntityFrameworkCorePagination.Nuget.Pagination;
using GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Persistence.Services;

public sealed class CarService : ICarService
{
    private readonly AppDbContext _context;
    private readonly ICarRepository _carRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CarService(AppDbContext context, ICarRepository carRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _context = context;
        _carRepository = carRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task CreateAsync(CreateCarCommand request, CancellationToken cancellationToken)
    {
        Car car = _mapper.Map<Car>(request);

        //await _context.Set<Car>().AddAsync(car, cancellationToken);
        //await _context.SaveChangesAsync(cancellationToken);

        await _carRepository.AddAsync(car, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<PaginationResult<Car>> GetAllAsync(GetAllCarsQuery request, CancellationToken cancellationToken)
    {
        PaginationResult<Car> cars = await
            _carRepository
            .GetWhere(p => p.Name.ToLower().Contains(request.Search.ToLower()))
            .OrderBy(p => p.Name)
            .ToPagedListAsync(request.PageNumber, request.PageSize, cancellationToken);

        return cars;
    }
}
