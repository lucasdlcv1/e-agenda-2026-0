using eAgenda.Dominio.Compartilhado;

namespace eAgenda.Dominio.Modulos.Contatos;

public sealed class Contato : EntidadeBase<Contato>
{
    public string Nome { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Telefone { get; set; } = string.Empty;

    public string Cargo { get; set; } = string.Empty;

    public string Empresa { get; set; } = string.Empty;

    public Contato() { }

    public Contato(string nome, string telefone, string email, string cargo, string empresa) : this()
    {
        Nome = nome;
        Email = email;
        Telefone = telefone;
        Cargo = cargo;
        Empresa = empresa;
    }




    public override List<ErroValidacao> Validar()
    {
        List<ErroValidacao> erros = [];

        if (string.IsNullOrWhiteSpace(Nome))
        {
            erros.Add(new ErroValidacao
            {
                Campo = nameof(Nome),
                Mensagem = "O campo \"Nome\" deve ser preenchido"
            });
        }

        if (Nome?.Length < 2 || Nome?.Length > 100)
        {
            erros.Add(new ErroValidacao
            {
                Campo = nameof(Nome),
                Mensagem = "O campo \"Nome\" deve conter entre 2 e 100 caracteres"
            });
        }

        if (string.IsNullOrWhiteSpace(Email))
        {
            erros.Add(new ErroValidacao
            {
                Campo = nameof(Email),
                Mensagem = "O campo \"E-mail\" deve ser preenchido"
            });
        }

        if (string.IsNullOrWhiteSpace(Telefone))
        {
            erros.Add(new ErroValidacao
            {
                Campo = nameof(Telefone),
                Mensagem = "O campo \"Telefone\" deve ser preenchido"
            });
        }

        return erros;
    }

    public override void Atualizar(Contato entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;
        Email = entidadeAtualizada.Email;
        Telefone = entidadeAtualizada.Telefone;
        Cargo = entidadeAtualizada.Cargo;
        Empresa = entidadeAtualizada.Empresa;
    }
}
