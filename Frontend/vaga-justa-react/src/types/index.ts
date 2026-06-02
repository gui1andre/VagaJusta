export interface TokenResponse {
  acessToken: string;
}

export interface TurmaResponse {
  id: string;
  serie: string;
  categoriaSerie: string;
  capacidadeMaxima: number;
  quantidadeAlunos: number;
  idadeMinima: number;
  idadeMaxima: number;
  vagasDisponiveis: number;
}

export interface EscolaResponse {
  id: string;
  nome: string;
  endereco: string;
  turmas: TurmaResponse[];
}

export interface AlunoResponse {
  id: string;
  nome: string;
  cpf: string;
  dataNascimento: string;
  idade: number;
}

export interface MatriculaResponse {
  id: string;
  alunoId: string;
  nomeAluno: string;
  cpfAluno: string;
  turma: string;
  escola: string;
  status: string;
  solicitacao: string;
}

export interface FilaEsperaResponse {
  posicao: number;
  matriculaId: string;
  aluno: AlunoResponse;
  dataSolicitacao: string;
}
