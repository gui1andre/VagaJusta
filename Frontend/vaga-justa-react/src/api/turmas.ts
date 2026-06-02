import { apiFetch } from './client';
import type { TurmaResponse, AlunoResponse } from '../types';

interface CriarTurmaData {
  escolaId: string;
  serie: string;
  categoriaSerie: string;
  capacidadeMaxima: number;
  idadeMinima: number;
  idadeMaxima: number;
}

export function criarTurma(data: CriarTurmaData): Promise<TurmaResponse> {
  return apiFetch<TurmaResponse>('/api/turma', {
    method: 'POST',
    body: JSON.stringify(data),
  });
}

export function deletarTurma(turmaId: string): Promise<void> {
  return apiFetch<void>(`/api/turma/${turmaId}`, { method: 'DELETE' });
}

export function listarAlunosDaTurma(turmaId: string): Promise<AlunoResponse[]> {
  return apiFetch<AlunoResponse[]>(`/api/turma/${turmaId}/alunos`);
}
