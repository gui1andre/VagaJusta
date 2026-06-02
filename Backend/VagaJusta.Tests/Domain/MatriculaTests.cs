using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VagaJusta.Domain.Entities;
using VagaJusta.Domain.Enums;
using VagaJusta.Domain.Exceptions;

namespace VagaJusta.Tests.Domain
{
    public class MatriculaTests
    {
        [Fact]
        public void Rejeitar_Sem_Motivo_Deve_Lancar_Excecao()
        {
            var matricula = Matricula.Criar(Guid.NewGuid(), Guid.NewGuid());
            var ex = Assert.Throws<DomainException>(() => matricula.Rejeitar(string.Empty));
            Assert.Equal("O motivo da rejeição é obrigatorio.", ex.Message);
        }
        [Fact]
        public void Aprovar_Matricula_Rejeitada_Deve_Lancar_Excecao()
        {
            var matricula = Matricula.Criar(Guid.NewGuid(), Guid.NewGuid());
            matricula.Rejeitar("Documentação incompleta.");
            var ex = Assert.Throws<DomainException>(() => matricula.Aprovar());
            Assert.Equal("A matrícula só pode ser aprovada se estiver aguardando processamento.", ex.Message);
        }

        [Fact]
        public void Criar_Deve_Ter_Status_Aguardando_Processamento()
        {
            var matricula = Matricula.Criar(Guid.NewGuid(), Guid.NewGuid());
            Assert.Equal(StatusMatriculaEnum.AguardandoProcessamento, matricula.Status);
        }

        [Fact]
        public void Aprovar_Quando_Aguardando_Processamento_Deve_Aprovar()
        {
            var matricula = Matricula.Criar(Guid.NewGuid(), Guid.NewGuid());
            matricula.Aprovar();
            Assert.Equal(StatusMatriculaEnum.Aprovada, matricula.Status);
        }

        [Fact]
        public void Aprovar_Quando_Ja_Aprovada_Deve_Lancar_Excecao()
        {
            var matricula = Matricula.Criar(Guid.NewGuid(), Guid.NewGuid());
            matricula.Aprovar();
            var ex = Assert.Throws<DomainException>(() => matricula.Aprovar());
            Assert.Equal("A matrícula já está aprovada.", ex.Message);
        }

        [Fact]
        public void Colocar_Em_Espera_Deve_Alterar_Status()
        {
            var matricula = Matricula.Criar(Guid.NewGuid(), Guid.NewGuid());
            matricula.ColocarEmEspera();
            Assert.Equal(StatusMatriculaEnum.AguardandoVaga, matricula.Status);
        }

        [Fact]
        public void Rejeitar_ComMotivo_Deve_Rejeitar_E_Registrar_Motivo()
        {
            var matricula = Matricula.Criar(Guid.NewGuid(), Guid.NewGuid());
            matricula.Rejeitar("Documentação incompleta.");
            Assert.Equal(StatusMatriculaEnum.Rejeitada, matricula.Status);
            Assert.Equal("Documentação incompleta.", matricula.MotivoRejeicao);
        }


    }
}
