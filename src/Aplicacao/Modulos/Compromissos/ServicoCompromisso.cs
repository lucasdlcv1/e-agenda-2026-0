using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.Modulos.Compromissos;
using eAgenda.Dominio.Modulos.Contatos;
using FluentResults;

namespace eAgenda.Aplicacao.Modulos.Compromissos;

public sealed class ServicoCompromisso(
    IRepositorioCompromisso repositorioCompromisso,
    IRepositorioContato repositorioContato)
{
    public Result<Guid> Cadastrar(SalvarCompromissoDto dto)
    {
        Compromisso compromisso = CriarCompromisso(dto);

        List<Error> erros = ValidarCompromisso(compromisso);

        if (erros.Count > 0)
            return Result.Fail<Guid>(erros);

        repositorioCompromisso.Cadastrar(compromisso);

        return Result.Ok(compromisso.Id);
    }

    public Result Editar(SalvarCompromissoDto dto)
    {
        if (dto.Id is null)
            return Result.Fail("O compromisso não foi identificado.");

        Compromisso compromisso = CriarCompromisso(dto);

        List<Error> erros = ValidarCompromisso(compromisso, dto.Id);

        if (erros.Count > 0)
            return Result.Fail(erros);

        if (!repositorioCompromisso.Editar(dto.Id.Value, compromisso))
            return Result.Fail("Não foi possível editar o compromisso.");

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        if (!repositorioCompromisso.Excluir(id))
            return Result.Fail("Não foi possível excluir o compromisso.");

        return Result.Ok();
    }

    public Result<CompromissoDto> SelecionarPorId(Guid id)
    {
        Compromisso? compromisso = repositorioCompromisso.SelecionarPorId(id);

        return compromisso is null
            ? Result.Fail<CompromissoDto>("Compromisso não encontrado.")
            : Result.Ok(Mapear(compromisso));
    }

    public List<CompromissoDto> SelecionarTodos()
    {
        return repositorioCompromisso.SelecionarTodos()
            .Select(Mapear)
            .ToList();
    }

    public List<ContatoCompromissoDto> SelecionarContatos()
    {
        return repositorioContato.SelecionarTodos()
            .OrderBy(contato => contato.Nome)
            .Select(contato => new ContatoCompromissoDto(contato.Id, contato.Nome))
            .ToList();
    }

    private List<Error> ValidarCompromisso(Compromisso compromisso, Guid? idIgnorado = null)
    {
        List<Error> erros = compromisso.Validar()
            .Select(erro => new Error(erro.Mensagem).WithMetadata("Campo", erro.Campo))
            .ToList();

        if (compromisso.ContatoId.HasValue &&
            repositorioContato.SelecionarPorId(compromisso.ContatoId.Value) is null)
        {
            erros.Add(new Error("O contato selecionado não foi encontrado.")
                .WithMetadata("Campo", nameof(Compromisso.ContatoId)));
        }

        if (compromisso.DataOcorrencia.HasValue &&
            compromisso.HoraInicio.HasValue &&
            compromisso.HoraTermino.HasValue &&
            compromisso.HoraTermino > compromisso.HoraInicio)
        {
            bool possuiConflito = repositorioCompromisso.SelecionarTodos().Any(outro =>
                outro.Id != idIgnorado &&
                outro.DataOcorrencia == compromisso.DataOcorrencia &&
                outro.HoraInicio.HasValue &&
                outro.HoraTermino.HasValue &&
                compromisso.HoraInicio < outro.HoraTermino &&
                compromisso.HoraTermino > outro.HoraInicio);

            if (possuiConflito)
            {
                erros.Add(new Error("Já existe um compromisso nesse intervalo de horário.")
                    .WithMetadata("Campo", nameof(Compromisso.HoraInicio)));
            }
        }

        return erros;
    }

    private static Compromisso CriarCompromisso(SalvarCompromissoDto dto)
    {
        return new Compromisso(
            dto.Assunto,
            dto.DataOcorrencia,
            dto.HoraInicio,
            dto.HoraTermino,
            dto.Tipo,
            dto.LinkOuLocal,
            dto.ContatoId);
    }

    private static CompromissoDto Mapear(Compromisso compromisso)
    {
        return new CompromissoDto(
            compromisso.Id,
            compromisso.Assunto,
            compromisso.DataOcorrencia,
            compromisso.HoraInicio,
            compromisso.HoraTermino,
            compromisso.Tipo,
            compromisso.LinkOuLocal,
            compromisso.ContatoId,
            compromisso.Contato?.Nome);
    }
}
