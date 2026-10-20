using eAgenda.Aplicacao.Modulos.Compromissos;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace eAgenda.WebApp.Modulos.Compromissos;

public sealed class CompromissoController(ServicoCompromisso servicoCompromisso) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        return View(servicoCompromisso.SelecionarTodos());
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        return View(CriarViewModel());
    }

    [HttpPost]
    public ActionResult Cadastrar(SalvarCompromissoViewModel viewModel)
    {
        Result<Guid> resultado = servicoCompromisso.Cadastrar(CriarDto(viewModel));

        if (resultado.IsFailed)
        {
            AdicionarErrosNoModelState(resultado.Errors);
            return View(CriarViewModel(viewModel));
        }

        TempData["MensagemSucesso"] = "Compromisso cadastrado com sucesso!";
        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(Guid id)
    {
        Result<CompromissoDto> resultado = servicoCompromisso.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData["MensagemErro"] = resultado.Errors.First().Message;
            return RedirectToAction(nameof(Listar));
        }

        CompromissoDto dto = resultado.Value;
        SalvarCompromissoViewModel viewModel = new(
            dto.Id,
            dto.Assunto,
            dto.DataOcorrencia,
            dto.HoraInicio,
            dto.HoraTermino,
            dto.Tipo,
            dto.LinkOuLocal,
            dto.ContatoId);

        return View(CriarViewModel(viewModel));
    }

    [HttpPost]
    public ActionResult Editar(SalvarCompromissoViewModel viewModel)
    {
        Result resultado = servicoCompromisso.Editar(CriarDto(viewModel));

        if (resultado.IsFailed)
        {
            AdicionarErrosNoModelState(resultado.Errors);
            return View(CriarViewModel(viewModel));
        }

        TempData["MensagemSucesso"] = "Compromisso atualizado com sucesso!";
        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(Guid id)
    {
        Result<CompromissoDto> resultado = servicoCompromisso.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData["MensagemErro"] = resultado.Errors.First().Message;
            return RedirectToAction(nameof(Listar));
        }

        CompromissoDto dto = resultado.Value;
        ExcluirCompromissoViewModel viewModel = new(
            dto.Id,
            dto.Assunto,
            dto.DataOcorrencia,
            dto.HoraInicio,
            dto.HoraTermino,
            dto.Tipo,
            dto.LinkOuLocal,
            dto.ContatoNome);

        return View(viewModel);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirCompromissoViewModel viewModel)
    {
        Result resultado = servicoCompromisso.Excluir(viewModel.Id);

        if (resultado.IsFailed)
            TempData["MensagemErro"] = resultado.Errors.First().Message;
        else
            TempData["MensagemSucesso"] = "Compromisso excluído com sucesso!";

        return RedirectToAction(nameof(Listar));
    }

    private SalvarCompromissoViewModel CriarViewModel(SalvarCompromissoViewModel? viewModel = null)
    {
        viewModel ??= new SalvarCompromissoViewModel(
            null,
            string.Empty,
            null,
            null,
            null,
            null,
            string.Empty,
            null);

        return viewModel with { ContatosDisponiveis = servicoCompromisso.SelecionarContatos() };
    }

    private static SalvarCompromissoDto CriarDto(SalvarCompromissoViewModel viewModel)
    {
        return new SalvarCompromissoDto(
            viewModel.Id,
            viewModel.Assunto,
            viewModel.DataOcorrencia,
            viewModel.HoraInicio,
            viewModel.HoraTermino,
            viewModel.Tipo,
            viewModel.LinkOuLocal,
            viewModel.ContatoId);
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
