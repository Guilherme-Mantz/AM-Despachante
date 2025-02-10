using AMDespachante.Application.Interfaces;
using AMDespachante.Application.ViewModels;
using AMDespachante.Domain.Core.Communication.Mediator;
using AMDespachante.Domain.Interfaces;
using AutoMapper;
using System.Linq.Dynamic.Core;

namespace AMDespachante.Application.Services
{
    public class RecursoAppService : IRecursoAppService
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IMapper _mapper;
        private readonly IRecursoRepository _repository;

        public RecursoAppService(IMediatorHandler mediatorHandler, IMapper mapper, IRecursoRepository repository)
        {
            _mediatorHandler = mediatorHandler;
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<PagedResult<RecursoViewModel>> GetPagedAsync(int page, int pageSize, string searchTerm = null)
        {
            var pagedResult = await _repository.GetPagedAsync(page, pageSize, searchTerm);

            return new PagedResult<RecursoViewModel>
            {
                Queryable = _mapper.Map<IEnumerable<RecursoViewModel>>(pagedResult.Queryable).AsQueryable(),
                PageCount = pagedResult.PageCount
            };
        }

        public async Task<IEnumerable<RecursoViewModel>> GetAll() => _mapper.Map<IEnumerable<RecursoViewModel>>(await _repository.GetAll());

        public Task<RecursoViewModel> GetById(Guid Id)
        {
            throw new NotImplementedException();
        }

        public void Add(RecursoViewModel recurso)
        {
            throw new NotImplementedException();
        }

        public void Update(RecursoViewModel recurso)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}
