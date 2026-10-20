using eAgenda.Dominio.Modulos.Compromissos;

namespace eAgenda.Aplicacao.Modulos.Compromissos;

public record CompromissoDto(
    Guid Id,
    string Assunto,
    DateOnly? DataOcorrencia,
    TimeOnly? HoraInicio,
    TimeOnly? HoraTermino,
    TipoCompromisso? Tipo,
    string LinkOuLocal,
    Guid? ContatoId,
    string? ContatoNome
);

public record ContatoCompromissoDto(Guid Id, string Nome);

public record SalvarCompromissoDto(
    Guid? Id,
    string Assunto,
    DateOnly? DataOcorrencia,
    TimeOnly? HoraInicio,
    TimeOnly? HoraTermino,
    TipoCompromisso? Tipo,
    string LinkOuLocal,
    Guid? ContatoId
);
