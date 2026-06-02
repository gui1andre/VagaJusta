import { apiFetch } from './client';
import type { MatriculaResponse, FilaEsperaResponse } from '../types';

interface SolicitarMatriculaData {
  nomeAluno: string;
  cpfAluno: string;
  dataNascimento: string;
  turmaId: string;
}

export function solicitarMatricula(data: SolicitarMatriculaData): Promise<MatriculaResponse> {
  return apiFetch<MatriculaResponse>('/api/matricula', {
    method: 'POST',
    body: JSON.stringify(data),
  });
}

export function listarFilaEspera(turmaId: string): Promise<FilaEsperaResponse[]> {
  return apiFetch<FilaEsperaResponse[]>(`/api/matricula/${turmaId}/fila`);
}
