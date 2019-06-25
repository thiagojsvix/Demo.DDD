using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Demo.DDD.Domain;
using Demo.DDD.Domain.Entitys;
using Demo.DDD.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace DDD.Domain.Tests.Entitys
{
    [ExcludeFromCodeCoverage]
    public class PessoaFisicaTests
    {
        private readonly Documento documento;
        private readonly Email email;

        private readonly Endereco enderecoEntrega;
        private readonly Endereco enderecoComercial;
        private readonly Endereco enderecoResidencial;

        public PessoaFisicaTests()
        {
            this.documento = new Documento("29028611096", Enums.TipoDocumento.Cpf);
            this.email = new Email("foo@bar.com");
            this.enderecoEntrega = new Endereco("Rua Foo", "Bairro Bar", "127-A", "Cidade ASD", Enums.TipoEndereco.Entrega);
            this.enderecoComercial = new Endereco("Rua Foo", "Bairro Bar", "127-A", "Cidade ASD", Enums.TipoEndereco.Comercial);
            this.enderecoResidencial = new Endereco("Rua Foo", "Bairro Bar", "127-A", "Cidade ASD", Enums.TipoEndereco.Residencial);
        }

        [Fact]
        public void DeveRetornaValidoQuandoForValida()
        {
            //act
            var value = new Cliente("Bruce Wayner", 1, documento, email, Enums.Sexo.Masculino, new DateTime(2000, 1, 1));

            //assert
            value.Valid.Should().BeTrue();
        }

        [Fact]
        public void DeveRetornaAtivoQuandoAtivarComEnderecoResidencial()
        {
            //arrange
            var result = new Cliente("Bruce Wayner", 1, this.documento, this.email, Enums.Sexo.Masculino, DateTime.Now.AddYears(-20));
            result.AdicionarEndereco(this.enderecoResidencial);

            //act
            result.Ativar();

            //assert
            result.Situacao.Should().Be(Enums.Situacao.Ativo);
        }

        [Fact]
        public void DeveRetornaErroQuandoPessoaMenor18Anos()
        {
            //act
            var value = new Cliente("Bruce Wayner", 1, documento, email, Enums.Sexo.Masculino, DateTime.Now.AddYears(-17));

            //assert
            value.Invalid.Should().BeTrue();
        }

        [Fact]
        public void DeveRetornaErroQuandoDocumentoNulo()
        {
            //act
            var value = new Cliente("Bruce Wayner", 0, null, email, Enums.Sexo.Masculino, DateTime.Now.AddYears(-17));

            //assert
            value.Invalid.Should().BeTrue();
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void DeveRetornaErroQuandoNome(string value)
        {
            //act
            var result = new Cliente(value, 1, null, email, Enums.Sexo.Masculino, DateTime.Now.AddYears(-17));

            //assert
            result.Invalid.Should().BeTrue();
        }

        [Fact]
        public void DeveRetornaErroQuandoAtivarSemEndereco()
        {
            //arrange
            var result = new Cliente("Bruce Wayner", 1, null, email, Enums.Sexo.Masculino, DateTime.Now.AddYears(-20));

            //act
            result.Ativar();

            //assert
            result.ValidationResult.Errors.Any(x => x.ErrorCode == "NotEmptyValidator").Should().BeTrue();
        }

        [Fact]
        public void DeveRetornaErroQuandoAtivarSemEnderecoResidencial()
        {
            //arrange
            var result = new Cliente("Bruce Wayner", 1, this.documento, this.email, Enums.Sexo.Masculino, DateTime.Now.AddYears(-20));
            result.AdicionarEndereco(this.enderecoComercial);

            //act
            result.Ativar();

            //assert
            result.ValidationResult.Errors.Any(x => x.ErrorCode == "PessoaFisicaEnderecoResidencial").Should().BeTrue();
        }

        [Fact]
        public void DeveRetornaErroQuandoIncluirDoisEnderecoResidencial()
        {
            //arrange
            var result = new Cliente("Bruce Wayner", 1, this.documento, email, Enums.Sexo.Masculino, DateTime.Now.AddYears(-20));

            //act
            result.AdicionarEndereco(this.enderecoResidencial);
            result.AdicionarEndereco(this.enderecoResidencial);

            //assert
            result.ValidationResult.Errors.Should().OnlyContain(x => x.ErrorCode == "EnderecoResidenciaDuplicado");
        }

        [Fact]
        public void DeveRetornaErroQuandoIncluirDoisEnderecoComercial()
        {
            //arrange
            var result = new Cliente("Bruce Wayner", 1, this.documento, email, Enums.Sexo.Masculino, DateTime.Now.AddYears(-20));

            //act
            result.AdicionarEndereco(this.enderecoComercial);
            result.AdicionarEndereco(this.enderecoComercial);

            //assert
            result.ValidationResult.Errors.Should().OnlyContain(x => x.ErrorCode == "EnderecoComercialDuplicado");
        }

        [Fact]
        public void DeveRetornaErroQuandoIncluirDoisEnderecoEntrega()
        {
            //arrange
            var result = new Cliente("Bruce Wayner", 1, this.documento, email, Enums.Sexo.Masculino, DateTime.Now.AddYears(-20));

            //act
            result.AdicionarEndereco(this.enderecoEntrega);
            result.AdicionarEndereco(this.enderecoEntrega);

            //assert
            result.ValidationResult.Errors.Should().OnlyContain(x => x.ErrorCode == "EnderecoEntregaDuplicado");
        }
    }
}