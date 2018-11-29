using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace ExemploStorage
{

    public class Aluno : TableEntity
    {
        public Aluno(string ra, string cidade) : base(cidade, ra)
        {

        }

        public string Nome { get; set; }
        public string Email { get; set; }

    }
}
