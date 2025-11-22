using boxes.Backend.Repositories.Interfaces;
using boxes.Backend.UnitsOfWork.Interfaces;
using Boxes.Shared.DTOs;
using Boxes.Shared.Entites;
using Boxes.Shared.Responses;

namespace boxes.Backend.UnitsOfWork.Implementations
{
    public class OrdenesUnitOfWork: GenericUnitOfWork<Orden> , IOrdenesUnitOfWork
    {
        private readonly IOrdenesRepository _ordenesRepository;

        public OrdenesUnitOfWork(IGenericRepository<Orden> Repository ,IOrdenesRepository ordenesRepository) : base(Repository)
        {
            _ordenesRepository = ordenesRepository;
        }
        public async Task<ActionResponse<IEnumerable<Orden>>> GetAsync(string email, PaginationDTO pagination) => await _ordenesRepository.GetAsync(email, pagination);

        public async Task<ActionResponse<int>> GetTotalPagesAsync(string email, PaginationDTO pagination) => await _ordenesRepository.GetTotalPagesAsync(email, pagination);

        public async Task<ActionResponse<Orden>> UpdateFullAsync(string email, OrdenDTO orderDTO) => await _ordenesRepository.UpdateFullAsync(email, orderDTO);

        public override async Task<ActionResponse<Orden>> GetAsync(int id) => await _ordenesRepository.GetAsync(id);


    }
}
