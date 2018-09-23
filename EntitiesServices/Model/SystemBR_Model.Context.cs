﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EntitiesServices.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SystemBRDatabaseEntities : DbContext
    {
        public SystemBRDatabaseEntities()
            : base("name=SystemBRDatabaseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AGENDA> AGENDA { get; set; }
        public virtual DbSet<ASSINANTE> ASSINANTE { get; set; }
        public virtual DbSet<ASSINANTE_PLANO> ASSINANTE_PLANO { get; set; }
        public virtual DbSet<ATENDIMENTO_ACOMPANHAMENTO> ATENDIMENTO_ACOMPANHAMENTO { get; set; }
        public virtual DbSet<ATENDIMENTO_ANEXO> ATENDIMENTO_ANEXO { get; set; }
        public virtual DbSet<BANCO> BANCO { get; set; }
        public virtual DbSet<CARGO> CARGO { get; set; }
        public virtual DbSet<CATEGORIA_ATENDIMENTO> CATEGORIA_ATENDIMENTO { get; set; }
        public virtual DbSet<CATEGORIA_CLIENTE> CATEGORIA_CLIENTE { get; set; }
        public virtual DbSet<CATEGORIA_EQUIPAMENTO> CATEGORIA_EQUIPAMENTO { get; set; }
        public virtual DbSet<CATEGORIA_FORNECEDOR> CATEGORIA_FORNECEDOR { get; set; }
        public virtual DbSet<CATEGORIA_MATERIA> CATEGORIA_MATERIA { get; set; }
        public virtual DbSet<CATEGORIA_PATRIMONIO> CATEGORIA_PATRIMONIO { get; set; }
        public virtual DbSet<CATEGORIA_PRODUTO> CATEGORIA_PRODUTO { get; set; }
        public virtual DbSet<CATEGORIA_SERVICO> CATEGORIA_SERVICO { get; set; }
        public virtual DbSet<CENTRO_CUSTO> CENTRO_CUSTO { get; set; }
        public virtual DbSet<CLIENTE> CLIENTE { get; set; }
        public virtual DbSet<CLIENTE_ANEXO> CLIENTE_ANEXO { get; set; }
        public virtual DbSet<COLABORADOR> COLABORADOR { get; set; }
        public virtual DbSet<COLABORADOR_ANEXO> COLABORADOR_ANEXO { get; set; }
        public virtual DbSet<COLABORADOR_FREQUENCIA> COLABORADOR_FREQUENCIA { get; set; }
        public virtual DbSet<COMISSAO_VENDA> COMISSAO_VENDA { get; set; }
        public virtual DbSet<CONFIGURACAO> CONFIGURACAO { get; set; }
        public virtual DbSet<CONTA_BANCARIA> CONTA_BANCARIA { get; set; }
        public virtual DbSet<CONTA_PAGAR> CONTA_PAGAR { get; set; }
        public virtual DbSet<CONTA_PAGAR_ANEXO> CONTA_PAGAR_ANEXO { get; set; }
        public virtual DbSet<CONTA_RECEBER> CONTA_RECEBER { get; set; }
        public virtual DbSet<CONTA_RECEBER_ANEXO> CONTA_RECEBER_ANEXO { get; set; }
        public virtual DbSet<CONTRATO> CONTRATO { get; set; }
        public virtual DbSet<DEPENDENTE> DEPENDENTE { get; set; }
        public virtual DbSet<EQUIPAMENTO> EQUIPAMENTO { get; set; }
        public virtual DbSet<ESCALA_TRABALHO> ESCALA_TRABALHO { get; set; }
        public virtual DbSet<ESCOLARIDADE> ESCOLARIDADE { get; set; }
        public virtual DbSet<ESTADO_CIVIL> ESTADO_CIVIL { get; set; }
        public virtual DbSet<EXAME_PERIODICO> EXAME_PERIODICO { get; set; }
        public virtual DbSet<FILIAL> FILIAL { get; set; }
        public virtual DbSet<FORNECEDOR> FORNECEDOR { get; set; }
        public virtual DbSet<FORNECEDOR_ANEXO> FORNECEDOR_ANEXO { get; set; }
        public virtual DbSet<FUNCIONALIDADE> FUNCIONALIDADE { get; set; }
        public virtual DbSet<INVENTARIO> INVENTARIO { get; set; }
        public virtual DbSet<INVENTARIO_ITEM> INVENTARIO_ITEM { get; set; }
        public virtual DbSet<ITEM_AGENDA> ITEM_AGENDA { get; set; }
        public virtual DbSet<ITEM_PEDIDO_COMPRA> ITEM_PEDIDO_COMPRA { get; set; }
        public virtual DbSet<ITEM_PEDIDO_COMPRA_COTACAO> ITEM_PEDIDO_COMPRA_COTACAO { get; set; }
        public virtual DbSet<ITEM_PEDIDO_SERVICO> ITEM_PEDIDO_SERVICO { get; set; }
        public virtual DbSet<ITEM_PEDIDO_VENDA> ITEM_PEDIDO_VENDA { get; set; }
        public virtual DbSet<ITEM_PROPOSTA_SERVICO> ITEM_PROPOSTA_SERVICO { get; set; }
        public virtual DbSet<ITEM_PROPOSTA_VENDA> ITEM_PROPOSTA_VENDA { get; set; }
        public virtual DbSet<LICENSA_ATESTADO> LICENSA_ATESTADO { get; set; }
        public virtual DbSet<LOG> LOG { get; set; }
        public virtual DbSet<MATERIA_PRIMA> MATERIA_PRIMA { get; set; }
        public virtual DbSet<MATRIZ> MATRIZ { get; set; }
        public virtual DbSet<MOVIMENTO_ESTOQUE_MATERIA_PRIMA> MOVIMENTO_ESTOQUE_MATERIA_PRIMA { get; set; }
        public virtual DbSet<MOVIMENTO_ESTOQUE_PRODUTO> MOVIMENTO_ESTOQUE_PRODUTO { get; set; }
        public virtual DbSet<NIVEL_CONTA> NIVEL_CONTA { get; set; }
        public virtual DbSet<OPORTUNIDADE_NEGOCIO> OPORTUNIDADE_NEGOCIO { get; set; }
        public virtual DbSet<OPORTUNIDADE_NEGOCIO_ANEXO> OPORTUNIDADE_NEGOCIO_ANEXO { get; set; }
        public virtual DbSet<PATRIMONIO> PATRIMONIO { get; set; }
        public virtual DbSet<PEDIDO_COMPRA> PEDIDO_COMPRA { get; set; }
        public virtual DbSet<PEDIDO_COMPRA_ANEXO> PEDIDO_COMPRA_ANEXO { get; set; }
        public virtual DbSet<PEDIDO_SERVICO> PEDIDO_SERVICO { get; set; }
        public virtual DbSet<PEDIDO_SERVICO_ANEXO> PEDIDO_SERVICO_ANEXO { get; set; }
        public virtual DbSet<PEDIDO_VENDA> PEDIDO_VENDA { get; set; }
        public virtual DbSet<PEDIDO_VENDA_ANEXO> PEDIDO_VENDA_ANEXO { get; set; }
        public virtual DbSet<PERFIL> PERFIL { get; set; }
        public virtual DbSet<PLANO_ASSINATURA> PLANO_ASSINATURA { get; set; }
        public virtual DbSet<PLANO_CONTA> PLANO_CONTA { get; set; }
        public virtual DbSet<PLANO_PERMISSAO> PLANO_PERMISSAO { get; set; }
        public virtual DbSet<PRACA_ATENDIMENTO> PRACA_ATENDIMENTO { get; set; }
        public virtual DbSet<PRECO_PRODUTO> PRECO_PRODUTO { get; set; }
        public virtual DbSet<PRECO_SERVICO> PRECO_SERVICO { get; set; }
        public virtual DbSet<PRODUTO> PRODUTO { get; set; }
        public virtual DbSet<PROPOSTA_SERVICO> PROPOSTA_SERVICO { get; set; }
        public virtual DbSet<PROPOSTA_SERVICO_ANEXO> PROPOSTA_SERVICO_ANEXO { get; set; }
        public virtual DbSet<PROPOSTA_VENDA> PROPOSTA_VENDA { get; set; }
        public virtual DbSet<PROPOSTA_VENDA_ANEXO> PROPOSTA_VENDA_ANEXO { get; set; }
        public virtual DbSet<SERVICO> SERVICO { get; set; }
        public virtual DbSet<TAREFA> TAREFA { get; set; }
        public virtual DbSet<TEMPLATE> TEMPLATE { get; set; }
        public virtual DbSet<TICKET_ATENDIMENTO> TICKET_ATENDIMENTO { get; set; }
        public virtual DbSet<TIPO_CONTA> TIPO_CONTA { get; set; }
        public virtual DbSet<TIPO_DOCUMENTO> TIPO_DOCUMENTO { get; set; }
        public virtual DbSet<TIPO_FAVORECIDO> TIPO_FAVORECIDO { get; set; }
        public virtual DbSet<TIPO_SALARIAL> TIPO_SALARIAL { get; set; }
        public virtual DbSet<TRANSPORTADORA> TRANSPORTADORA { get; set; }
        public virtual DbSet<TURNO_TRABALHO> TURNO_TRABALHO { get; set; }
        public virtual DbSet<UNIDADE> UNIDADE { get; set; }
        public virtual DbSet<USUARIO> USUARIO { get; set; }
        public virtual DbSet<VALOR_COMISSAO> VALOR_COMISSAO { get; set; }
        public virtual DbSet<VINCULO_EMPREGATICIO> VINCULO_EMPREGATICIO { get; set; }
        public virtual DbSet<PRODUTO_ANEXO> PRODUTO_ANEXO { get; set; }
        public virtual DbSet<MATERIA_PRIMA_ANEXO> MATERIA_PRIMA_ANEXO { get; set; }
        public virtual DbSet<SERVICO_ANEXO> SERVICO_ANEXO { get; set; }
        public virtual DbSet<TRANSPORTADORA_ANEXO> TRANSPORTADORA_ANEXO { get; set; }
        public virtual DbSet<EQUIPAMENTO_ANEXO> EQUIPAMENTO_ANEXO { get; set; }
        public virtual DbSet<PATRIMONIO_ANEXO> PATRIMONIO_ANEXO { get; set; }
        public virtual DbSet<TIPO_COMISSAO> TIPO_COMISSAO { get; set; }
    }
}
