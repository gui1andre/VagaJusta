import { apiFetch } from './client';
import type { TokenResponse } from '../types';

export function login(email: string, senha: string): Promise<TokenResponse> {
  return apiFetch<TokenResponse>('/api/auth/login', {
    method: 'POST',
    body: JSON.stringify({ email, senha }),
  });
}
