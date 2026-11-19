using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.Modulos.Categorias;

namespace eAgenda.Dominio.Modulos.Categorias;

public interface IRepositorioCategoria : IRepositorio<Categoria>
{
    bool ExisteCategoriaPorTitulo(string TituloCategoria, Guid? idignorado = null);

    Categoria? SelecionarPorIdDoUsuario(Guid idUsuario, Guid idCategoria);

    List<Categoria> SelecionarTodosDoUsuario(Guid idUsuario);
}
