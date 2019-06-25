using System;
using System.Collections.Generic;
using Demo.DDD.Domain.Validators;
using Demo.DDD.Domain.ValueObjects;
using Demo.DDD.Shared.Entitys;

namespace Demo.DDD.Domain.Entitys
{
    public abstract class Pessoa : Entity
    {
        #region Construtores

        private readonly List<Endereco> enderecos;

        protected Pessoa(string nome, long codigo,Documento documento, Email email)
        {
            this.Nome = nome;
            this.Codigo = codigo;
            this.Documento = documento;
            this.Email = email;
            this.Situacao = Enums.Situacao.Inativo;
            this.enderecos = new List<Endereco>();

            this.Validate(this, new PessoaValidator());
        }

        protected Pessoa(string nome, long codigo, Documento documento, Email email, Endereco endereco)
        {
            this.Nome = nome;
            this.Codigo = codigo;
            this.Documento = documento;
            this.Email = email;
            this.Situacao = Enums.Situacao.Inativo;
            this.enderecos = new List<Endereco>() { endereco };

            //valida se ocorrer algum erro de validação
            this.Validate(this, new PessoaAdicionarEnderecoValidator());
        }

        #endregion

        #region Propriedades

        public string Nome { get; private set; }
        /// <summary>
        /// Codigo para contrar sequencialmente as Pessoas, ou seja, esse código não pode se repetir ao incluir qualquer pessoa
        /// </summary>
        public long Codigo { get; private set; }
        public Documento Documento { get; private set; }
        public Email Email { get; private set; }
        public Enums.Situacao Situacao { get; protected set; }
        public IReadOnlyCollection<Endereco> Endereco => this.enderecos;

        #endregion

        #region Métodos

        public abstract void Ativar();

        public void AdicionarEndereco(Endereco endereco)
        {
            if (endereco == null)
                throw new ArgumentNullException(nameof(endereco), "Parametro de Preenchimento Obrigatório");

            //adiciona endereço
            this.enderecos.Add(endereco);

            //valida se ocorrer algum erro de validação
            this.Validate(this, new PessoaAdicionarEnderecoValidator());
        }

        #endregion
    }
}
