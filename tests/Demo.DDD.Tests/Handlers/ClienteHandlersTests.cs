
using System;
using Demo.DDD.Domain;
using Demo.DDD.Domain.Commands;
using Demo.DDD.Domain.Handlers;
using Demo.DDD.Domain.Repositories;
using Demo.DDD.Domain.Services;
using Demo.DDD.Shared.Notifications;
using FluentAssertions;
using Moq;
using Xunit;

namespace DDD.Domain.Tests.Handlers
{
    public class ClienteHandlersTests
    {
        private readonly Mock<IEmailService> emailService;
        private readonly Mock<IClienteRepository> clienteRepositoryMock;

        public ClienteHandlersTests()
        {
            this.emailService = new Mock<IEmailService>();
            this.clienteRepositoryMock = new Mock<IClienteRepository>();
        }

        [Fact]
        public void DeveRetornarErroValidacaoFailFastValidation()
        {
            //arrange
            var notificacao = new NotificationList();
            var command = new CriarClienteCommand("Nome", "Email", "62347448005", Enums.TipoDocumento.Cpf,
                Enums.Sexo.Masculino, DateTime.Now, "Rua", "Bairro", "127-A", "Cidade", Enums.TipoEndereco.Comercial);

            this.emailService.Setup(service => service.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            //act
            var handler = new ClienteHandler(this.clienteRepositoryMock.Object, this.emailService.Object, notificacao);
            var result = handler.Handle(command);

            //assert
            result.Sucess.Should().BeFalse();
        }

        [Fact]
        public void DeveRetornarSucesso()
        {
            //arrange
            var notificacao = new NotificationList();
            var command = new CriarClienteCommand("Nome", "foo@bar.com", "62347448005", Enums.TipoDocumento.Cpf,
                Enums.Sexo.Masculino, DateTime.Now, "Rua", "Bairro", "127-A", "Cidade", Enums.TipoEndereco.Comercial);

            this.emailService.Setup(service => service.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            //act
            var handler = new ClienteHandler(this.clienteRepositoryMock.Object, this.emailService.Object, notificacao);
            var result = handler.Handle(command);

            //assert
            result.Sucess.Should().BeTrue();
        }
    }
}
