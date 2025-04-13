using AMDespachante.Application.Interfaces;
using AMDespachante.Application.ViewModels;
using AMDespachante.Domain.Commands.VeiculoCommands;
using AMDespachante.Domain.Core.Communication.Mediator;
using AMDespachante.Domain.Interfaces;
using AutoMapper;
using FluentValidation.Results;
using System.Linq.Dynamic.Core;

namespace AMDespachante.Application.Services
{
    public class VeiculoAppService : IVeiculoAppService
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IMapper _mapper;
        private readonly IVeiculoRepository _veiculoRepository;

        public VeiculoAppService(IMediatorHandler mediatorHandler, IMapper mapper, IVeiculoRepository veiculoRepository)
        {
            _mediatorHandler = mediatorHandler;
            _mapper = mapper;
            _veiculoRepository = veiculoRepository;
        }

        public async Task<PagedResult<VeiculoViewModel>> GetPagedAsync(int page, int pageSize, string sortOrder, string searchTerm = null, string sortField = null)
        {
            var pagedResult = await _veiculoRepository.GetPagedAsync(page, pageSize, sortOrder, searchTerm, sortField);

            return new PagedResult<VeiculoViewModel>
            {
                Queryable = _mapper.Map<IEnumerable<VeiculoViewModel>>(pagedResult.Queryable).AsQueryable(),
                PageCount = pagedResult.PageCount
            };
        }

        public async Task<IEnumerable<VeiculoViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<VeiculoViewModel>>(await _veiculoRepository.GetAll());
        }

        public async Task<VeiculoViewModel> GetById(Guid id)
        {
            return _mapper.Map<VeiculoViewModel>(await _veiculoRepository.GetById(id));
        }

        public async Task<IEnumerable<VeiculoViewModel>> GetByClienteIdAsync(Guid clienteId)
        {
            return _mapper.Map<IEnumerable<VeiculoViewModel>>(await _veiculoRepository.GetByClienteIdAsync(clienteId));
        }

        public async Task<ValidationResult> Add(VeiculoViewModel veiculo)
        {
            var addCommand = _mapper.Map<NovoVeiculoCommand>(veiculo);
            return await _mediatorHandler.SendCommand(addCommand);
        }

        public async Task<ValidationResult> Update(VeiculoViewModel veiculo)
        {
            var updateCommand = _mapper.Map<AtualizarVeiculoCommand>(veiculo);
            return await _mediatorHandler.SendCommand(updateCommand);
        }

        public async Task<ValidationResult> Delete(Guid id)
        {
            var deleteCommand = new RemoverVeiculoCommand(id);
            return await _mediatorHandler.SendCommand(deleteCommand);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }
}
