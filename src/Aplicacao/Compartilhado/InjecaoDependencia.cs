using System;
using eAgenda.Aplicacao.Modulos.Categorias;
using eAgenda.Aplicacao.Modulos.Compromissos;
using eAgenda.Aplicacao.Modulos.Despesas;
using eAgenda.Aplicacao.Modulos.Tarefas;
using eAgenda.WebApp.Modulos.Contatos;
using Microsoft.Extensions.DependencyInjection;

namespace eAgenda.Aplicacao.Compartilhado;

public static class InjecaoDependencia
{
    public static void AdicionarCamadaAplicacao(this IServiceCollection services)
    {
        services.AddScoped<ServicoCategoria>();
        services.AddScoped<ServicoDespesa>();
        services.AddScoped<ServicoTarefa>();
        services.AddScoped<ServicoContato>();
        services.AddScoped<ServicoCompromisso>();
    }
}
