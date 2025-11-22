using boxes.Backend.Repositories.Interfaces;
using boxes.Backend.UnitsOfWork.Interfaces;
using Boxes.Shared.DTOs;
using Boxes.Shared.Entites;
using Boxes.Shared.Responses;

namespace boxes.Backend.UnitsOfWork.Implementations
{
    public class OrdenesTemporalesUnitOfWork: GenericUnitOfWork<OrdenTemporal>, IOrdenesTemporalesUnitOfWork
    {
        private readonly IOrdenesTemporalesRepository _temporalOrdersRepository;

        public OrdenesTemporalesUnitOfWork(IGenericRepository<OrdenTemporal> repository, IOrdenesTemporalesRepository temporalOrdersRepository) : base(repository)
        {
            _temporalOrdersRepository = temporalOrdersRepository;
        }

        public async Task<ActionResponse<OrdenTemporalDTO>> AddFullAsync(string email, OrdenTemporalDTO temporalOrderDTO) => await _temporalOrdersRepository.AddFullAsync(email, temporalOrderDTO);

        public async Task<ActionResponse<IEnumerable<OrdenTemporal>>> GetAsync(string email) => await _temporalOrdersRepository.GetAsync(email);

        public async Task<ActionResponse<int>> GetCountAsync(string email) => await _temporalOrdersRepository.GetCountAsync(email);

        public async Task<ActionResponse<OrdenTemporal>> PutFullAsync(OrdenTemporalDTO temporalOrderDTO) => await _temporalOrdersRepository.PutFullAsync(temporalOrderDTO);

        public override async Task<ActionResponse<OrdenTemporal>> GetAsync(int id) => await _temporalOrdersRepository.GetAsync(id);

    }
}
