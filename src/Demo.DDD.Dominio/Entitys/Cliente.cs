using System;
using Demo.DDD.Domain.Validators;
using Demo.DDD.Domain.ValueObjects;
using Demo.DDD.Shared.Entitys;

namespace Demo.DDD.Domain.Entitys
{
    public class Cliente : Pessoa, IAggregateRoot
    {
        public Cliente(string nome, long codigo, Documento documento, Email email, Enums.Sexo sexo, DateTime dataNascimento) : base(nome, codigo, documento, email)
        {
            this.Sexo = sexo;
            this.DataNascimento = dataNascimento;
            this.Validate(this, new PessoaFisicaValidator());
        }

        public Enums.Sexo Sexo { get; private set; }
        public DateTime DataNascimento { get; private set; }

        public override void Ativar()
        {
            //Chama validação especifica para ativação de Pessoa
            this.Validate(this, new PessoaFisicaAtivarPessoaValidator());

            //Situação só pode Ativo, caso a Pessoa esteja valida
            this.Situacao = this.Valid ? Enums.Situacao.Ativo : Enums.Situacao.Inativo;
        }
    }

}
