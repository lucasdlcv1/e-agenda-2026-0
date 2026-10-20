using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.Modulos.Contatos;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using eAgenda.Aplicacao.Modulos.Contatos;

namespace eAgenda.WebApp.Modulos.Contatos;

public sealed class ContatoController(
    ServicoContato servicoContato
) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        List<ContatoDto> dtos = servicoContato.SelecionarTodos();

        return View(dtos);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarContatoViewModel viewModel = new(
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty
        );

        return View(viewModel);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarContatoViewModel viewModel)
    {
        CadastrarContatoDto dto = new(
            viewModel.Nome,
            viewModel.Email,
            viewModel.Telefone,
            viewModel.Cargo,
            viewModel.Empresa
        );

        Result<Guid> resultado = servicoContato.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            AdicionarErrosNoModelState(resultado.Errors);

            return View(viewModel);
        }

        TempData["MensagemSucesso"] = "Contato cadastrado com sucesso!";

        return RedirectToAction("Listar");
    }

    [HttpGet]
    public ActionResult Editar(Guid id)
    {
        Result<ContatoDto> resultado = servicoContato.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData["MensagemErro"] = resultado.Errors.First().Message;

            return RedirectToAction(nameof(Listar));
        }

        ContatoDto dto = resultado.Value;

        EditarContatoViewModel viewModel = new(
            dto.Id,
            dto.Nome,
            dto.Telefone,
            dto.Email,
            dto.Cargo,
            dto.Empresa
        );

        return View(viewModel);
    }

    [HttpPost]
    public ActionResult Editar(EditarContatoViewModel viewModel)
    {
        EditarContatoDto dto = new(
            viewModel.Id,
            viewModel.Nome,
            viewModel.Email,
            viewModel.Telefone,
            viewModel.Cargo,
            viewModel.Empresa
        );

        Result resultado = servicoContato.Editar(dto);

        if (resultado.IsFailed)
        {
            AdicionarErrosNoModelState(resultado.Errors);

            return View(viewModel);
        }

        TempData["MensagemSucesso"] = "Contato atualizado com sucesso!";

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(Guid id)
    {
        Result<ContatoDto> resultado = servicoContato.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData["MensagemErro"] = resultado.Errors.First().Message;

            return RedirectToAction(nameof(Listar));
        }

        ContatoDto dto = resultado.Value;

        ExcluirContatoViewModel viewModel = new(
            dto.Id,
            dto.Nome,
            dto.Telefone,
            dto.Email,
            dto.Cargo,
            dto.Empresa
        );

        return View(viewModel);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirContatoViewModel viewModel)
    {
        Result resultado = servicoContato.Excluir(viewModel.Id);

        if (resultado.IsFailed)
            TempData["MensagemErro"] = resultado.Errors.First().Message;
        else
            TempData["MensagemSucesso"] = "Contato excluído com sucesso!";

        return RedirectToAction(nameof(Listar));
    }

    private void AdicionarErrosNoModelState(IEnumerable<IError> erros)
    {
        foreach (IError erro in erros)
        {
            string campo = erro.Metadata.TryGetValue("Campo", out object? campoMetadata)
                ? campoMetadata?.ToString() ?? string.Empty
                : string.Empty;

            ModelState.AddModelError(campo, erro.Message);
        }
    }
}
