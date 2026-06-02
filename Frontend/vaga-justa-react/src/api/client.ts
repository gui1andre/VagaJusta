const BASE_URL = import.meta.env.VITE_API_URL ?? 'http://localhost:5000';

interface ApiErrorBody {
  title?: string;
  errors?: string[];
}

function extractErrorMessage(body: ApiErrorBody): string {
  if (body.errors && body.errors.length > 0) {
    return body.errors.join('\n');
  }
  return body.title ?? 'Erro desconhecido';
}

export async function apiFetch<T>(path: string, options: RequestInit = {}): Promise<T> {
  const token = localStorage.getItem('token');

  const headers: HeadersInit = {
    'Content-Type': 'application/json',
    ...(token ? { Authorization: `Bearer ${token}` } : {}),
    ...(options.headers ?? {}),
  };

  const response = await fetch(`${BASE_URL}${path}`, { ...options, headers });

  if (!response.ok) {
    let errorMessage = `Erro ${response.status}`;
    try {
      const body: ApiErrorBody = await response.json();
      errorMessage = extractErrorMessage(body);
    } catch {
    }

    if (response.status === 401 && !window.location.pathname.startsWith('/login')) {
      localStorage.removeItem('token');
      window.location.href = '/login';
    }

    throw new Error(errorMessage);
  }

  if (response.status === 204) {
    return undefined as T;
  }

  return response.json() as Promise<T>;
}
