import { apiFetch } from './client';
import type { AlunoResponse } from '../types';

interface AtualizarAlunoData {
  id: string;
  nome: string;
  dataNascimento: string;
}

export function atualizarAluno(data: AtualizarAlunoData): Promise<AlunoResponse> {
  return apiFetch<AlunoResponse>('/api/aluno', {
    method: 'PUT',
    body: JSON.stringify(data),
  });
}
