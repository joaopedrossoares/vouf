﻿using PUC.LDSI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PUC.LDSI.Application.Interfaces
{
    public interface ITurmaAppService
    {
        Task<DataResult<int>> AdicionarTurmaAsync(string descricao);
        Task<DataResult<int>> AlterarTurmaAsync(int id, string descricao);
        DataResult<List<Turma>> ListarTurmasAsync();
        Task<DataResult<Turma>> ObterAsync(int id);
        Task<DataResult<int>> ExcluirAsync(int id);
    }
}