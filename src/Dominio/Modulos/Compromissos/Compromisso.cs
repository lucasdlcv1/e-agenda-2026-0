using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.Modulos.Contatos;

namespace eAgenda.Dominio.Modulos.Compromissos;

public sealed class Compromisso : EntidadeBase<Compromisso>
{
    public string Assunto { get; set; } = string.Empty;
    public DateOnly? DataOcorrencia { get; set; }
    public TimeOnly? HoraInicio { get; set; }
    public TimeOnly? HoraTermino { get; set; }
    public TipoCompromisso? Tipo { get; set; }
    public string LinkOuLocal { get; set; } = string.Empty;
    public Guid? ContatoId { get; set; }
    public Contato? Contato { get; set; }

    public Compromisso() { }

    public Compromisso(
        string assunto,
        DateOnly? dataOcorrencia,
        TimeOnly? horaInicio,
        TimeOnly? horaTermino,
        TipoCompromisso? tipo,
        string linkOuLocal,
        Guid? contatoId)
    {
        Assunto = assunto;
        DataOcorrencia = dataOcorrencia;
        HoraInicio = horaInicio;
        HoraTermino = horaTermino;
        Tipo = tipo;
        LinkOuLocal = linkOuLocal;
        ContatoId = contatoId;
    }

    public override List<ErroValidacao> Validar()
    {
        List<ErroValidacao> erros = [];

        if (string.IsNullOrWhiteSpace(Assunto) || Assunto.Trim().Length is < 2 or > 100)
        {
            erros.Add(new ErroValidacao
            {
                Campo = nameof(Assunto),
                Mensagem = "O assunto deve conter entre 2 e 100 caracteres."
            });
        }

        if (DataOcorrencia is null || DataOcorrencia == default)
        {
            erros.Add(new ErroValidacao
            {
                Campo = nameof(DataOcorrencia),
                Mensagem = "A data de ocorrência deve ser preenchida."
            });
        }

        if (HoraInicio is null)
        {
            erros.Add(new ErroValidacao
            {
                Campo = nameof(HoraInicio),
                Mensagem = "A hora de início deve ser preenchida."
            });
        }

        if (HoraTermino is null)
        {
            erros.Add(new ErroValidacao
            {
                Campo = nameof(HoraTermino),
                Mensagem = "A hora de término deve ser preenchida."
            });
        }

        if (HoraInicio.HasValue && HoraTermino.HasValue && HoraTermino <= HoraInicio)
        {
            erros.Add(new ErroValidacao
            {
                Campo = nameof(HoraTermino),
                Mensagem = "A hora de término deve ser posterior à hora de início."
            });
        }

        if (Tipo is null || !Enum.IsDefined(Tipo.Value))
        {
            erros.Add(new ErroValidacao
            {
                Campo = nameof(Tipo),
                Mensagem = "Selecione o tipo do compromisso."
            });
        }

        if (string.IsNullOrWhiteSpace(LinkOuLocal) || LinkOuLocal.Trim().Length > 500)
        {
            erros.Add(new ErroValidacao
            {
                Campo = nameof(LinkOuLocal),
                Mensagem = "Informe um link ou local com até 500 caracteres."
            });
        }

        return erros;
    }

    public override void Atualizar(Compromisso entidadeAtualizada)
    {
        Assunto = entidadeAtualizada.Assunto;
        DataOcorrencia = entidadeAtualizada.DataOcorrencia;
        HoraInicio = entidadeAtualizada.HoraInicio;
        HoraTermino = entidadeAtualizada.HoraTermino;
        Tipo = entidadeAtualizada.Tipo;
        LinkOuLocal = entidadeAtualizada.LinkOuLocal;
        ContatoId = entidadeAtualizada.ContatoId;
        Contato = entidadeAtualizada.Contato;
    }
}
