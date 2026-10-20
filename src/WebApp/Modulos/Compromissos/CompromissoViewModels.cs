using eAgenda.Aplicacao.Modulos.Compromissos;
using eAgenda.Dominio.Modulos.Compromissos;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace eAgenda.WebApp.Modulos.Compromissos;

[ValidateNever]
public record SalvarCompromissoViewModel(
    Guid? Id,
    string Assunto,
    DateOnly? DataOcorrencia,
    TimeOnly? HoraInicio,
    TimeOnly? HoraTermino,
    TipoCompromisso? Tipo,
    string LinkOuLocal,
    Guid? ContatoId)
{
    public List<ContatoCompromissoDto> ContatosDisponiveis { get; set; } = [];
}

[ValidateNever]
public record ExcluirCompromissoViewModel(
    Guid Id,
    string Assunto,
    DateOnly? DataOcorrencia,
    TimeOnly? HoraInicio,
    TimeOnly? HoraTermino,
    TipoCompromisso? Tipo,
    string LinkOuLocal,
    string? ContatoNome);
