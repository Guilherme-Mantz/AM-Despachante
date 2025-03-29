using AMDespachante.Application.Interfaces;
using AMDespachante.Application.ViewModels;
using AMDespachante.Domain.Commands.ClienteCommands;
using AMDespachante.Domain.Commands.RecursoCommands;
using AMDespachante.Domain.Core.Communication.Mediator;
using AMDespachante.Domain.Interfaces;
using AutoMapper;
using FluentValidation.Results;
using System.Linq.Dynamic.Core;

namespace AMDespachante.Application.Services
{
    public class ClienteAppService : IClienteAppService
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IMapper _mapper;
        private readonly IClienteRepository _clienteRepository;

        public ClienteAppService(IMediatorHandler mediatorHandler, IMapper mapper, IClienteRepository clienteRepository)
        {
            _mediatorHandler = mediatorHandler;
            _mapper = mapper;
            _clienteRepository = clienteRepository;
        }

        public async Task<PagedResult<ClienteViewModel>> GetPagedAsync(int page, int pageSize, string sortOrder, string searchTerm = null, string sortField = null)
        {
            var pagedResult = await _clienteRepository.GetPagedAsync(page, pageSize, sortOrder, searchTerm, sortField);

            return new PagedResult<ClienteViewModel>
            {
                Queryable = _mapper.Map<IEnumerable<ClienteViewModel>>(pagedResult.Queryable).AsQueryable(),
                PageCount = pagedResult.PageCount
            };
        }

        public async Task<IEnumerable<ClienteViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<ClienteViewModel>>(await _clienteRepository.GetAll());
        }

        public async Task<ClienteViewModel> GetById(Guid Id)
        {
            return _mapper.Map<ClienteViewModel>(await _clienteRepository.GetById(Id));
        }
        
        public async Task<ValidationResult> Add(ClienteViewModel cliente)
        {
            var addCommand = _mapper.Map<NovoClienteCommand>(cliente);
            return await _mediatorHandler.SendCommand(addCommand);
        }

        public async Task<ValidationResult> Update(ClienteViewModel cliente)
        {
            var updateCommand = _mapper.Map<AtualizarClienteCommand>(cliente);
            return await _mediatorHandler.SendCommand(updateCommand);
        }

        public async Task<ValidationResult> Delete(Guid Id)
        {
            var removeCommand = new RemoverRecursoCommand(Id);
            return await _mediatorHandler.SendCommand(removeCommand);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
