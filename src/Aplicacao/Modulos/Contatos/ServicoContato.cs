using eAgenda.Aplicacao.Modulos.Contatos;
using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.Modulos.Contatos;
using FluentResults;

namespace eAgenda.WebApp.Modulos.Contatos;

public sealed class ServicoContato(
    IRepositorioContato repositorioContato
)
{
    public Result<Guid> Cadastrar(CadastrarContatoDto dto)
    {
        Contato contato = new Contato(
            dto.Nome,
            dto.Telefone,
            dto.Email,
            dto.Cargo,
            dto.Empresa
        );

        List<Error> erros = ValidarContato(contato);

        if (erros.Count > 0)
            return Result.Fail(erros);

        repositorioContato.Cadastrar(contato);

        return Result.Ok(contato.Id);
    }

    public Result Editar(EditarContatoDto dto)
    {
        Contato contatoAtualizado = new Contato(
            dto.Nome,
            dto.Telefone,
            dto.Email,
            dto.Cargo,
            dto.Empresa
        );

        List<Error> erros = ValidarContato(contatoAtualizado, dto.Id);

        if (erros.Count > 0)
            return Result.Fail(erros);

        repositorioContato.Editar(dto.Id, contatoAtualizado);

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        bool conseguiuExcluir = repositorioContato.Excluir(id);

        if (!conseguiuExcluir)
            return Result.Fail("Não foi possível excluir o contato.");

        return Result.Ok();
    }

    public Result<ContatoDto> SelecionarPorId(Guid id)
    {
        Contato? contato = repositorioContato.SelecionarPorId(id);

        if (contato == null)
            return Result.Fail<ContatoDto>("Contato não encontrado.");

        ContatoDto dto = new(
            contato.Id,
            contato.Nome,
            contato.Email,
            contato.Telefone,
            contato.Cargo,
            contato.Empresa
        );

        return Result.Ok(dto);
    }

    public List<ContatoDto> SelecionarTodos()
    {
        List<Contato> contatos = repositorioContato.SelecionarTodos();

        return contatos.Select(c => new ContatoDto(
            c.Id,
            c.Nome,
            c.Email,
            c.Telefone,
            c.Cargo,
            c.Empresa
        )).ToList();
    }

    private List<Error> ValidarContato(Contato contato, Guid? idIgnorado = null)
    {
        List<Error> erros = contato.Validar()
            .Select(e => new Error(e.Mensagem).WithMetadata("Campo", e.Campo))
            .ToList();

        List<Contato> contatosExistentes = repositorioContato.SelecionarTodos();

        bool emailDuplicado = !string.IsNullOrWhiteSpace(contato.Email) &&
            contatosExistentes.Any(c =>
                c.Id != idIgnorado &&
                string.Equals(c.Email, contato.Email, StringComparison.OrdinalIgnoreCase));

        if (emailDuplicado)
        {
            erros.Add(new Error("Já existe um contato cadastrado com este e-mail.")
                .WithMetadata("Campo", nameof(Contato.Email)));
        }

        bool telefoneDuplicado = !string.IsNullOrWhiteSpace(contato.Telefone) &&
            contatosExistentes.Any(c =>
                c.Id != idIgnorado &&
                string.Equals(c.Telefone, contato.Telefone, StringComparison.OrdinalIgnoreCase));

        if (telefoneDuplicado)
        {
            erros.Add(new Error("Já existe um contato cadastrado com este telefone.")
                .WithMetadata("Campo", nameof(Contato.Telefone)));
        }

        return erros;
    }
}
