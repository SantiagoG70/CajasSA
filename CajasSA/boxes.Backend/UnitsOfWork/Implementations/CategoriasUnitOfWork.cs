using boxes.Backend.Repositories.Interfaces;
using boxes.Backend.UnitsOfWork.Interfaces;
using Boxes.Shared.DTOs;
using Boxes.Shared.Entites;
using Boxes.Shared.Responses;

namespace boxes.Backend.UnitsOfWork.Implementations
{
    public class CategoriasUnitOfWork: GenericUnitOfWork<Categoria>, ICategoriasUnitOfWork
    {
        private readonly ICategoriasRepository _categoriesRepository;

        public CategoriasUnitOfWork(IGenericRepository<Categoria> repository, ICategoriasRepository categoriesRepository) : base(repository)
        {
            _categoriesRepository = categoriesRepository;
        }

        public override async Task<ActionResponse<IEnumerable<Categoria>>> GetAsync(PaginationDTO pagination) => await _categoriesRepository.GetAsync(pagination);

        public  async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _categoriesRepository.GetTotalPagesAsync(pagination);

        public async Task<IEnumerable<Categoria>> GetComboAsync() => await _categoriesRepository.GetComboAsync();
    }
}
