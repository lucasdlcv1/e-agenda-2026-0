using eAgenda.Dominio.Modulos.Compromissos;
using eAgenda.Infraestrutura.Compartilhado.Orm;
using Microsoft.EntityFrameworkCore;

namespace eAgenda.Infraestrutura.Modulos.Compromissos;

public sealed class RepositorioCompromissoEmOrm(EAgendaDbContext dbContext)
    : RepositorioBaseEmOrm<Compromisso>(dbContext), IRepositorioCompromisso
{
    public override Compromisso? SelecionarPorId(Guid idSelecionado)
    {
        return registros
            .Include(compromisso => compromisso.Contato)
            .SingleOrDefault(compromisso => compromisso.Id == idSelecionado);
    }

    public override List<Compromisso> SelecionarTodos()
    {
        return registros
            .Include(compromisso => compromisso.Contato)
            .OrderBy(compromisso => compromisso.DataOcorrencia)
            .ThenBy(compromisso => compromisso.HoraInicio)
            .ToList();
    }
}
