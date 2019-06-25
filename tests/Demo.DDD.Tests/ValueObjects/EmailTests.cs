using System.Diagnostics.CodeAnalysis;
using Demo.DDD.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace DDD.Domain.Tests.ValueObjects
{
    [ExcludeFromCodeCoverage]
    public class EmailTests
    {
        [Fact]
        public void email_should_valid()
        {
            //act
            var email = new Email("foo@bar.com");

            //assert
            email.Valid.Should().BeTrue();
        }

        [Fact]
        public void email_should_invalid()
        {
            //act
            var email = new Email("foo.bar.com");

            //assert
            email.Valid.Should().BeFalse();
        }
    }
}
