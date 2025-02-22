using AMDespachante.Application.Interfaces;
using AMDespachante.Application.ViewModels;
using AMDespachante.Domain.Commands.RecursoCommands;
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
        public async Task<PagedResult<RecursoViewModel>> GetPagedAsync(int page, int pageSize, string sortOrder, string searchTerm = null, string sortField = null)
        {
            var pagedResult = await _repository.GetPagedAsync(page, pageSize, sortOrder, searchTerm, sortField);

            return new PagedResult<RecursoViewModel>
            {
                Queryable = _mapper.Map<IEnumerable<RecursoViewModel>>(pagedResult.Queryable).AsQueryable(),
                PageCount = pagedResult.PageCount
            };
        }

        public async Task<IEnumerable<RecursoViewModel>> GetAll() => _mapper.Map<IEnumerable<RecursoViewModel>>(await _repository.GetAll());

        public async Task<RecursoViewModel?> GetById(Guid Id) => _mapper.Map<RecursoViewModel>(await _repository.GetById(Id));

        public async void Add(RecursoViewModel recurso)
        {
            var addCommand = _mapper.Map<NovoRecursoCommand>(recurso);
            await _mediatorHandler.SendCommand(addCommand);
        }

        public async void Update(RecursoViewModel recurso)
        {
            var updateCommand = _mapper.Map<AtualizarRecursoCommand>(recurso);
            await _mediatorHandler.SendCommand(updateCommand);
        }

        public async void Delete(Guid id)
        {
            await _mediatorHandler.SendCommand(new RemoverRecursoCommand(id));
        }
    }
}
