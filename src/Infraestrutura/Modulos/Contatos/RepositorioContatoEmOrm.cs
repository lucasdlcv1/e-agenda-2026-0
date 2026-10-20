using eAgenda.Infraestrutura.Compartilhado.Orm;
using eAgenda.Dominio.Modulos.Contatos;
using Microsoft.EntityFrameworkCore;

public sealed class RepositorioContatoEmOrm(EAgendaDbContext dbContext) : RepositorioBaseEmOrm<Contato>(dbContext), IRepositorioContato
{
    public override Contato? SelecionarPorId(Guid idSelecionado)
    {
        Contato? contato = registros
            .SingleOrDefault(c => c.Id == idSelecionado);

        OrdenarItens(contato);

        return contato;
    }

    public override List<Contato> SelecionarTodos()
    {
        List<Contato> contatos = registros.ToList();

        foreach (Contato contato in contatos)
            OrdenarItens(contato);

        return contatos;
    }

    private static void OrdenarItens(Contato? contato)
    {
        if (contato == null)
            return;
    }

}
