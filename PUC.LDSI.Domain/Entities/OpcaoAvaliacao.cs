﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PUC.LDSI.Domain.Entities
{
    public class OpcaoAvaliacao : Entity
    {
        public int QuestaoId { get; set; }
        public string Descricao { get; set; }
        public bool Verdadeira { get; set; }
        public List<OpcaoProva> OpcoesProva { get; set; }
        public Questao Questao { get; set; }

        public override string[] Validate()
        {
            var erros = new List<string>();

            if (QuestaoId == 0)
            {
                erros.Add(" A questão presisa ser informada ");
            }
            if (string.IsNullOrEmpty(Descricao))
            {
                erros.Add("A Descrição deve ser informado ");

            }
            return erros.ToArray();
        }

    }
}