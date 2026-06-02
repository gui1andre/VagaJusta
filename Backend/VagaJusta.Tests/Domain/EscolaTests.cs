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
    public class EscolaTests
    {
        [Theory]
        [InlineData("", "Rua A, 1")]
        [InlineData("   ", "Rua A, 1")]
        public void Criar_Com_Nome_Vazio_Deve_Lancar_Excecao(string nome, string endereco)
        {
            var ex = Assert.Throws<DomainException>(() => Escola.Criar(nome, endereco));
            Assert.Equal("Nome da escola é obrigatório.", ex.Message);
        }

        [Theory]
        [InlineData("Escola SLG", "")]
        [InlineData("Escola SLG", "   ")]
        public void Criar_Com_Endereco_Vazio_Deve_Lancar_Excecao(string nome, string endereco)
        {
            var ex = Assert.Throws<DomainException>(() => Escola.Criar(nome, endereco));
            Assert.Equal("Endereço da escola é obrigatório.", ex.Message);
        }

        [Theory]
        [InlineData("", "Rua Nova, 5")]
        [InlineData("   ", "Rua Nova, 5")]
        public void Atualizar_Com_Nome_Vazio_Deve_Lancar_Excecao(string nome, string endereco)
        {
            var escola = Escola.Criar("Escola SLG", "Rua das Flores, 100");

            var ex = Assert.Throws<DomainException>(() => escola.Atualizar(nome, endereco));
            Assert.Equal("Nome da escola é obrigatório.", ex.Message);
        }

        [Theory]
        [InlineData("Escola Nova", "")]
        [InlineData("Escola Nova", "   ")]
        public void Atualizar_Com_Endereco_Vazio_Deve_Lancar_Excecao(string nome, string endereco)
        {
            var escola = Escola.Criar("Escola SLG", "Rua das Flores, 100");

            var ex = Assert.Throws<DomainException>(() => escola.Atualizar(nome, endereco));
            Assert.Equal("Endereço da escola é obrigatório.", ex.Message);
        }

        [Fact]
        public void Atualizar_Com_Dados_Validos_Deve_Atualizar_Propriedades()
        {
            var escola = Escola.Criar("Escola SLG", "Rua das Flores, 100");

            escola.Atualizar("Escola Nova", "Av. Brasil, 200");

            Assert.Equal("Escola Nova", escola.Nome);
            Assert.Equal("Av. Brasil, 200", escola.Endereco);
        }

        [Fact]
        public void Atualizar_Nao_Deve_Alterar_Id_Nem_Turmas()
        {
            var escola = Escola.Criar("Escola SLG", "Rua das Flores, 100");
            var turma = Turma.Criar(escola.Id, SerieEnum.PrimeiroAno, CategoriaSerieEnum.A, 30, 5, 7);
            escola.AdicionarTurma(turma);
            var idOriginal = escola.Id;

            escola.Atualizar("Escola Nova", "Av. Brasil, 200");

            Assert.Equal(idOriginal, escola.Id);
            Assert.Single(escola.Turmas);
        }

        [Fact]
        public void Adicionar_Turma_Com_Mesma_Serie_E_Categoria_Deve_Lancar_Excecao()
        {
            var escola = Escola.Criar("Escola SLG", "Rua das Flores, 100");
            var turma1 = Turma.Criar(escola.Id, SerieEnum.PrimeiroAno, CategoriaSerieEnum.A, 30, 5, 7);
            var turma2 = Turma.Criar(escola.Id, SerieEnum.PrimeiroAno, CategoriaSerieEnum.A, 25, 5, 7);

            escola.AdicionarTurma(turma1);

            var ex = Assert.Throws<DomainException>(() => escola.AdicionarTurma(turma2));
            Assert.Equal($"Já existe uma turma da serie {SerieEnum.PrimeiroAno} na categoria {CategoriaSerieEnum.A}.", ex.Message);
        }

        [Fact]
        public void Criar_Com_Dados_Validos_Deve_Criar_Escola()
        {
            var escola = Escola.Criar("Escola SLG", "Rua das Flores, 100");

            Assert.Equal("Escola SLG", escola.Nome);
            Assert.Equal("Rua das Flores, 100", escola.Endereco);
            Assert.NotEqual(Guid.Empty, escola.Id);
            Assert.Empty(escola.Turmas);
        }

        [Fact]
        public void Adicionar_Turma_Com_Turma_Valida_Deve_Adicionar_Na_Colecao()
        {
            var escola = Escola.Criar("Escola SLG", "Rua das Flores, 100");
            var turma = Turma.Criar(escola.Id, SerieEnum.PrimeiroAno, CategoriaSerieEnum.A, 30, 5, 7);

            escola.AdicionarTurma(turma);

            Assert.Single(escola.Turmas);
            Assert.Contains(turma, escola.Turmas);
        }

        [Fact]
        public void Adicionar_Multiplas_Turmas_Deve_Manter_Coleção_Correta()
        {
            var escola = Escola.Criar("Escola SLG", "Rua das Flores, 100");
            var turmaA = Turma.Criar(escola.Id, SerieEnum.PrimeiroAno, CategoriaSerieEnum.A, 30, 5, 7);
            var turmaB = Turma.Criar(escola.Id, SerieEnum.SegundoAno, CategoriaSerieEnum.A, 28, 6, 8);

            escola.AdicionarTurma(turmaA);
            escola.AdicionarTurma(turmaB);

            Assert.Equal(2, escola.Turmas.Count);
        }
    }
}
