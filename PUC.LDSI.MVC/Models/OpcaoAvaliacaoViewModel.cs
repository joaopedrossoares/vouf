﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PUC.LDSI.MVC.Models
{
    public class OpcaoAvaliacaoViewModel
    {
        [Key]
        public int Id { get; set; }

        public int QuestaoId { get; set; }

        [Required(ErrorMessage = "A Descricao é requerido.")]
        [MaxLength(100, ErrorMessage = "Informe no maximo {0} caracteres.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "A Verdadeira é requerido.")]
        public bool Verdadeira { get; set; }

        public QuestaoAvaliacaoViewModel Questao { get; set; }
    }
}