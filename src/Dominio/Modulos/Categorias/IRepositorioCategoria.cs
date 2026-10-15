using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.Modulos.Categorias;

namespace eAgenda.Dominio.Modulos.Categorias;

public interface IRepositorioCategoria : IRepositorio<Categoria>
{
    bool ExisteCategoriaPorTitulo(string TituloCategoria, Guid? idignorado = null);
}
