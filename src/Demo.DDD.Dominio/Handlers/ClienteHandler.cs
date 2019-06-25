using Demo.DDD.Domain.Commands;
using Demo.DDD.Domain.Entitys;
using Demo.DDD.Domain.Repositories;
using Demo.DDD.Domain.Services;
using Demo.DDD.Domain.ValueObjects;
using Demo.DDD.Shared.Commands;
using Demo.DDD.Shared.Handlers;
using Demo.DDD.Shared.Notifications;

namespace Demo.DDD.Domain.Handlers
{
    public class ClienteHandler : IHandler<CriarClienteCommand>
    {
        private readonly IClienteRepository clienteRepository;
        private readonly IEmailService emailService;
        private readonly NotificationList notificationList;

        public ClienteHandler(IClienteRepository clienteRepository, IEmailService emailService, NotificationList notificationList)
        {
            this.emailService = emailService;
            this.notificationList = notificationList;
            this.clienteRepository = clienteRepository;
        }

        public ICommandResult Handle(CriarClienteCommand command)
        {
            //Fail Fast Validations
            command.Validate(command, new CriarClienteCommandValidator());
            if (command.Invalid)
                return new CommandResult(false, "Não foi possível realizar sua inscrição. Configra a lista de notificação");

            //Verifica se Cliente já foi cadastrado observando se o Documento já está cadastrado
            if (this.clienteRepository.ExisteDocumento(command.documento))
                this.notificationList.AddNotification("DocumentoJaCadastrado", "Já foi encontrado documento cadastrado");

            //Verifica se o e-mail inofrmado pelo cliente já está cadastrado no sistema
            if (this.clienteRepository.ExisteEmail(command.Email))
                this.notificationList.AddNotification("EmailJaCadastrado", "Já foi encontrado e-mail cadastrado");

            //Objter Novo Codigo de Pessoa
            var codigoCliente = this.clienteRepository.ObterNovoCodigoPessoa();

            //criar VOs
            var documento = new Documento(command.documento, command.tipoDocumento);
            var email = new Email(command.Email);
            var endereco = new Endereco(command.Rua, command.Bairro, command.Numero, command.Cidade, command.TipoEndereco);

            //Criar Entidades
            var cliente = new Cliente(command.Nome, codigoCliente, documento, email, command.Sexo, command.DataNascimento);
            cliente.AdicionarEndereco(endereco);

            //Agrupar validações
            this.notificationList.AddNotifications(documento, email, endereco, cliente);

            if (this.notificationList.HasNotifications)
                return new CommandResult(this.notificationList);

            //Salvar no Banco
            this.clienteRepository.Salvar(cliente);

            //Enviar E-mail de boas vindas
            this.emailService.Send(command.Nome, command.Email, "Cliente Cadastrado", "O cliente foi cadastrado com sucesso!");

            //Retorna informações de sucesso
            return new CommandResult(true, "Pessoa Cadastrada com Sucesso!");
        }
    }
}
