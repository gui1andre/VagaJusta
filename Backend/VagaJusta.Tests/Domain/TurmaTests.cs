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
    public class TurmaTests
    {
        private static Turma CriarTurmaComVagas(int capacidade, int vagasOcupadas = 0)
        {
            var turma = Turma.Criar(Guid.NewGuid(), SerieEnum.PrimeiroAno, CategoriaSerieEnum.A, capacidade, 5, 7);
            for (var i = 0; i < vagasOcupadas; i++)
                turma.IncrementarVagasOcupadas();
            return turma;
        }

        [Fact]
        public void Criar_Com_Capacidade_Zero_Deve_Lancar_Excecao()
        {
            var ex = Assert.Throws<DomainException>(() =>
                Turma.Criar(Guid.NewGuid(), SerieEnum.PrimeiroAno, CategoriaSerieEnum.A, 0, 5, 7));
            Assert.Equal("A capacidade máxima deve ser maior que zero.", ex.Message);
        }
        [Fact]
        public void Criar_Com_IdadeMinima_Menor_Que_Zero_Deve_Lancar_Excecao()
        {
            var ex = Assert.Throws<DomainException>(() =>
                Turma.Criar(Guid.NewGuid(), SerieEnum.PrimeiroAno, CategoriaSerieEnum.A, 30, 0, 7));
            Assert.Equal("A idade mínima deve ser maior que zero.", ex.Message);
        }

        [Fact]
        public void Criar_Com_Idade_Maxima_Menor_Que_Idade_Minima_Deve_Lancar_Excecao()
        {
            var ex = Assert.Throws<DomainException>(() =>
                Turma.Criar(Guid.NewGuid(), SerieEnum.PrimeiroAno, CategoriaSerieEnum.A, 30, 8, 7));
            Assert.Equal("A idade máxima deve ser maior ou igual à idade mínima.", ex.Message);
        }

        [Fact]
        public void Turma_Com_30_Vagas_Nao_Deve_Aceitar_O_31_Aluno()
        {
            var turma = CriarTurmaComVagas(30, 30);
            Assert.True(turma.TurmaLotada);
            var ex = Assert.Throws<DomainException>(() => turma.IncrementarVagasOcupadas());
            Assert.Equal("Turma sem vagas disponíveis.", ex.Message);
        }

        [Fact]
        public void Incrementar_Vagas_Ocupadas_Quando_A_Vaga_Deve_Incrementar()
        {
            var turma = CriarTurmaComVagas(30);
            turma.IncrementarVagasOcupadas();
            Assert.Equal(1, turma.QuantidadeAlunos);
        }

        [Fact]
        public void Incrementar_Vagas_Ocupadas_Quando_Lotada_Deve_Lancar_Excecao()
        {
            var turma = CriarTurmaComVagas(30, 30);
            var ex = Assert.Throws<DomainException>(() => turma.IncrementarVagasOcupadas());
            Assert.Equal("Turma sem vagas disponíveis.", ex.Message);
        }

        [Fact]
        public void Esta_Lotada_Quando_Vagas_Ocupadas_Igual_Capacidade_Deve_Retornar_True()
        {
            var turma = CriarTurmaComVagas(30, 30);
            Assert.True(turma.TurmaLotada);
        }

        [Fact]
        public void Esta_Lotada_Quando_Ainda_A_Vaga_Deve_Retornar_False()
        {
            var turma = CriarTurmaComVagas(30, 29);
            Assert.False(turma.TurmaLotada);
        }

        [Fact]
        public void Vagas_Disponiveis_Deve_Calcular_Corretamente()
        {
            var turma = CriarTurmaComVagas(30, 10);
            Assert.Equal(20, turma.VagasDisponiveis);
        }

        [Theory]
        [InlineData(5, true)]
        [InlineData(6, true)]
        [InlineData(7, true)]
        [InlineData(4, false)]
        [InlineData(8, false)]
        public void Idade_Aluno_Compativel_Deve_Validar_Faixa_Etaria(int idade, bool esperado)
        {
            var turma = Turma.Criar(Guid.NewGuid(), SerieEnum.PrimeiroAno, CategoriaSerieEnum.A, 30, 5, 7);
            Assert.Equal(esperado, turma.IdadeAlunoCompativel(idade));
        }
    }
}
