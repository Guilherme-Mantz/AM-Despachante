﻿using AMDespachante.Domain.Core.Data;
using AMDespachante.Domain.Models;

namespace AMDespachante.Domain.Interfaces;
public interface IRecursoRepository : IRepository<Recurso>
{
    Task<IEnumerable<Recurso>> GetAll();
    Task<Recurso?> GetById(Guid Id);

    void Add(Recurso recurso);
    void Update(Recurso recurso);
    void Delete(Recurso recurso);
}
