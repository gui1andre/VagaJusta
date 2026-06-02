import { apiFetch } from './client';
import type { EscolaResponse } from '../types';

export function listarEscolas(pagina = 1, tamanhoPagina = 20): Promise<EscolaResponse[]> {
  return apiFetch<EscolaResponse[]>(`/api/escola?pagina=${pagina}&tamanhoPagina=${tamanhoPagina}`);
}

export function buscarEscola(escolaId: string): Promise<EscolaResponse> {
  return apiFetch<EscolaResponse>(`/api/escola/${escolaId}`);
}

export function criarEscola(nome: string, endereco: string): Promise<EscolaResponse> {
  return apiFetch<EscolaResponse>('/api/escola', {
    method: 'POST',
    body: JSON.stringify({ nome, endereco }),
  });
}

export function atualizarEscola(id: string, nome: string, endereco: string): Promise<EscolaResponse> {
  return apiFetch<EscolaResponse>('/api/escola', {
    method: 'PUT',
    body: JSON.stringify({ id, nome, endereco }),
  });
}

export function deletarEscola(escolaId: string): Promise<void> {
  return apiFetch<void>(`/api/escola/${escolaId}`, { method: 'DELETE' });
}
