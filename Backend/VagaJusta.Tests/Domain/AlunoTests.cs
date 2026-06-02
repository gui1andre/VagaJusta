using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VagaJusta.Domain.Entities;
using VagaJusta.Domain.Exceptions;

namespace VagaJusta.Tests.Domain
{
    public class AlunoTests
    {
        private static readonly DateTime DataNascimento2020 = new(2020, 6, 15);

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void Criar_Com_Nome_Vazio_Deve_Lancar_Excecao(string nome)
        {
            var ex = Assert.Throws<DomainException>(() =>
                Aluno.Criar(nome, "529.982.247-25", DataNascimento2020));
            Assert.Equal("O nome do aluno é obrigatório.", ex.Message);
        }

        [Fact]
        public void Criar_Com_CPF_Invalido_Deve_Lancar_Excecao()
        {
            var cpfInvalido = "000.000.000-00";
            var ex = Assert.Throws<DomainException>(() =>
                Aluno.Criar("Joao", cpfInvalido, DataNascimento2020));
            Assert.Equal($"CPF {cpfInvalido} é inválido.", ex.Message);
        }

        [Fact]
        public void Criar_Com_Dados_Validos_Deve_Criar_Aluno()
        {
            var aluno = Aluno.Criar("Joao Silva", "529.982.247-25", DataNascimento2020);

            Assert.Equal("Joao Silva", aluno.Nome);
            Assert.Equal("52998224725", aluno.CPF.Numero);
            Assert.Equal(DataNascimento2020.Date, aluno.DataNascimento);
            Assert.NotEqual(Guid.Empty, aluno.Id);
        }

        [Fact]
        public void Idade_Deve_Calcular_Corretamente()
        {
            var nascimento = DateTime.Today.AddYears(-6);
            var aluno = Aluno.Criar("Ana Lima", "529.982.247-25", nascimento);

            Assert.Equal(6, aluno.Idade);
        }

        [Fact]
        public void Idade_Aniversario_Ainda_Nao_Ocorreu_Este_Ano_Deve_Retornar_Idade_Correta()
        {
            var nascimento = new DateTime(DateTime.Today.Year - 7, 12, 31);
            var aluno = Aluno.Criar("Lucas Pena", "529.982.247-25", nascimento);

            var idadeEsperada = DateTime.Today < nascimento.AddYears(7) ? 6 : 7;
            Assert.Equal(idadeEsperada, aluno.Idade);
        }
    }
}
