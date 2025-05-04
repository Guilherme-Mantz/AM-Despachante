using AMDespachante.Application.Interfaces;
using AMDespachante.Application.ViewModels;
using AMDespachante.Domain.Commands.AtendimentoCommands;
using AMDespachante.Domain.Core.Communication.Mediator;
using AMDespachante.Domain.Interfaces;
using AutoMapper;
using FluentValidation.Results;
using System.Linq.Dynamic.Core;

namespace AMDespachante.Application.Services
{
    public class AtendimentoAppService : IAtendimentoAppService
    {
        private readonly IAtendimentoRepository _repository;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IMapper _mapper;

        public AtendimentoAppService(IAtendimentoRepository repository, IMediatorHandler mediatorHandler, IMapper mapper)
        {
            _repository = repository;
            _mediatorHandler = mediatorHandler;
            _mapper = mapper;
        }

        public async Task<PagedResult<AtendimentoViewModel>> GetPagedAsync(int page, int pageSize, string sortOrder, string searchTerm = null, string sortField = null)
        {
            var pagedResult = await _repository.GetPagedAsync(page, pageSize, sortOrder, searchTerm, sortField);

            return new PagedResult<AtendimentoViewModel>
            {
                Queryable = _mapper.Map<IEnumerable<AtendimentoViewModel>>(pagedResult.Queryable).AsQueryable(),
                PageCount = pagedResult.PageCount
            };
        }

        public async Task<IEnumerable<AtendimentoViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<AtendimentoViewModel>>(await _repository.GetAll());
        }

        public async Task<IEnumerable<AtendimentoViewModel>> ObterAtendimentosPorPeriodo(DateTime dataInicio, DateTime dataFim)
        {
            return _mapper.Map<IEnumerable<AtendimentoViewModel>>(await _repository.ObterPorPeriodoAsync(dataInicio, dataFim));
        }

        public async Task<AtendimentoViewModel> GetById(Guid Id)
        {
            return _mapper.Map<AtendimentoViewModel>(await _repository.GetById(Id));
        }

        public async Task<AtendimentoViewModel> GetByIdWithIncludes(Guid Id)
        {
            return _mapper.Map<AtendimentoViewModel>(await _repository.GetByIdWithIncludes(Id));
        }

        public async Task<ValidationResult> Add(AtendimentoViewModel atendimento)
        {
            var addCommand = _mapper.Map<NovoAtendimentoCommand>(atendimento);
            return await _mediatorHandler.SendCommand(addCommand);
        }

        public async Task<ValidationResult> Update(AtendimentoViewModel atendimento)
        {
            var updateCommand = _mapper.Map<AtualizarAtendimentoCommand>(atendimento);
            return await _mediatorHandler.SendCommand(updateCommand);
        }

        public async Task<ValidationResult> Delete(Guid Id)
        {
            var deleteCommand = new RemoverAtendimentoCommand(Id);
            return await _mediatorHandler.SendCommand(deleteCommand);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
