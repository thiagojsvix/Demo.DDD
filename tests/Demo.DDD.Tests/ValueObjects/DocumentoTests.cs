using System.Diagnostics.CodeAnalysis;
using Demo.DDD.Domain;
using Demo.DDD.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace DDD.Domain.Tests.ValueObjects
{
    [ExcludeFromCodeCoverage]
    public class DocumentoTests
    {
        [Fact]
        public void cnpj_should_valid()
        {
            //act
            var result = new Documento("87213238000183", Enums.TipoDocumento.Cnpj);

            //assert
            result.Valid.Should().BeTrue();
        }

        [Fact]
        public void cnpj_should_invalid()
        {
            //act
            var result = new Documento("87213238000983", Enums.TipoDocumento.Cnpj);

            //assert
            result.Invalid.Should().BeTrue();
        }

        [Fact]
        public void cpf_should_valid()
        {
            //act
            var result = new Documento("67123303008", Enums.TipoDocumento.Cpf);

            //assert
            result.Valid.Should().BeTrue();
        }

        [Fact]
        public void cpf_should_invalid()
        {
            //act
            var result = new Documento("67123303000", Enums.TipoDocumento.Cpf);

            //assert
            result.Invalid.Should().BeTrue();
        }
    }
}
