﻿using PUC.LDSI.Domain.Entities;
using PUC.LDSI.Domain.Exception;
using PUC.LDSI.Domain.Interfaces.Repository;
using PUC.LDSI.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PUC.LDSI.Domain.Services
{
    public class AvaliacaoService : IAvaliacaoService
    {
        private readonly IAvaliacaoRepository _avaliacaoRepository;
        private readonly IQuestaoAvaliacaoRepository _questaoAvaliacaoRepository;
        private readonly IOpcaoAvaliacaoRepository _opcaoAvaliacaoRepository;

        public AvaliacaoService(IAvaliacaoRepository avaliacaoRepository,
                                IQuestaoAvaliacaoRepository questaoAvaliacaoRepository,
                                IOpcaoAvaliacaoRepository opcaoAvaliacaoRepository)
        {
            _avaliacaoRepository = avaliacaoRepository;
            _questaoAvaliacaoRepository = questaoAvaliacaoRepository;
            _opcaoAvaliacaoRepository = opcaoAvaliacaoRepository;
        }

        public async Task<int> AdicionarAvaliacaoAsync(int professorId, string disciplina, string materia, string descricao)
        {
            var avaliacao = new Avaliacao() { ProfessorId = professorId, Disciplina = disciplina, Materia = materia, Descricao = descricao };

            var erros = avaliacao.Validate();

            if (erros.Length == 0)
            {
                await _avaliacaoRepository.AdicionarAsync(avaliacao);

                _avaliacaoRepository.SaveChanges();

                return avaliacao.Id;
            }
            else throw new DomainException(erros);
        }

        public async Task<int> AdicionarOpcaoAvaliacaoAsync(int questaoId, string descricao, bool verdadeira)
        {
            var opcaoAvaliacao = new OpcaoAvaliacao() { QuestaoId = questaoId, Descricao = descricao, Verdadeira = verdadeira };

            var erros = opcaoAvaliacao.Validate();

            if (erros.Length == 0)
            {
                await _opcaoAvaliacaoRepository.AdicionarAsync(opcaoAvaliacao);

                _opcaoAvaliacaoRepository.SaveChanges();

                return opcaoAvaliacao.Id;
            }
            else throw new DomainException(erros);
        }

        public async Task<int> AdicionarQuestaoAvaliacaoAsync(int avaliacaoId, int tipo, string enunciado)
        {
            var questaoAvaliacao = new Questao() { AvaliacaoId = avaliacaoId, Tipo = tipo, Enunciado = enunciado };

            var erros = questaoAvaliacao.Validate();

            if (erros.Length == 0)
            {
                await _questaoAvaliacaoRepository.AdicionarAsync(questaoAvaliacao);

                _questaoAvaliacaoRepository.SaveChanges();

                return questaoAvaliacao.Id;
            }
            else throw new DomainException(erros);
        }

        public async Task<int> AlterarAvaliacaoAsync(int id, string disciplina, string materia, string descricao)
        {
            var avaliacao = await _avaliacaoRepository.ObterAsync(id);

            avaliacao.Disciplina = disciplina;
            avaliacao.Materia = materia;
            avaliacao.Descricao = descricao;

            var erros = avaliacao.Validate();

            if (erros.Length == 0)
            {
                _avaliacaoRepository.Modificar(avaliacao);

                return _avaliacaoRepository.SaveChanges();
            }
            else throw new DomainException(erros);
        }

        public async Task<int> AlterarOpcaoAvaliacaoAsync(int id, string descricao, bool verdadeira)
        {
            var opcaoAvaliacao = await _opcaoAvaliacaoRepository.ObterAsync(id);

            opcaoAvaliacao.Verdadeira = verdadeira;
            opcaoAvaliacao.Descricao = descricao;

            var erros = opcaoAvaliacao.Validate();

            if (erros.Length == 0)
            {
                _opcaoAvaliacaoRepository.Modificar(opcaoAvaliacao);

                return _opcaoAvaliacaoRepository.SaveChanges();
            }
            else throw new DomainException(erros);
        }

        public async Task<int> AlterarQuestaoAvaliacaoAsync(int id, int tipo, string enunciado)
        {
            var questaoAvaliacao = await _questaoAvaliacaoRepository.ObterAsync(id);

            questaoAvaliacao.Tipo = tipo;
            questaoAvaliacao.Enunciado = enunciado;

            var erros = questaoAvaliacao.Validate();

            if (erros.Length == 0)
            {
                _questaoAvaliacaoRepository.Modificar(questaoAvaliacao);

                return _questaoAvaliacaoRepository.SaveChanges();
            }
            else throw new DomainException(erros);
        }

        public async Task<int> ExcluirAvaliacaoAsync(int id)
        {
            var avaliacao = await _avaliacaoRepository.ObterAsync(id);

            if (avaliacao.Publicacoes?.Count > 0)
                throw new DomainException("Não é possível excluir uma avaliação que já foi publicada ou realizada!");

            _avaliacaoRepository.Excluir(id);

            _avaliacaoRepository.SaveChanges();

            return 1;
        }

        public async Task<int> ExcluirOpcaoAvaliacaoAsync(int id)
        {
            var opcaoAvaliacao = await _opcaoAvaliacaoRepository.ObterAsync(id);

            if (opcaoAvaliacao.OpcoesProvas?.Count > 0)
                throw new DomainException("Não é possível excluir a opção de uma avaliação que já foi realizada!");

            _opcaoAvaliacaoRepository.Excluir(id);

            _opcaoAvaliacaoRepository.SaveChanges();

            return 1;
        }

        public async Task<int> ExcluirQuestaoAvaliacaoAsync(int id)
        {
            var questaoAvaliacao = await _questaoAvaliacaoRepository.ObterAsync(id);

            if (questaoAvaliacao.QuestoesProvas?.Count > 0)
                throw new DomainException("Não é possível excluir a questão de uma avaliação que já foi realizada!");

            _questaoAvaliacaoRepository.Excluir(id);

            _questaoAvaliacaoRepository.SaveChanges();

            return 1;
        }
    }
}