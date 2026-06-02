import { useState, type FormEvent } from 'react';
import { useNavigate, useParams, useLocation, Link } from 'react-router-dom';
import { atualizarEscola } from '../api/escolas';
import type { EscolaResponse } from '../types';

interface LocationState {
  escola: EscolaResponse;
}

export default function EditarEscola() {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const location = useLocation();
  const state = location.state as LocationState | null;

  const escola = state?.escola;

  const [nome, setNome] = useState(escola?.nome ?? '');
  const [endereco, setEndereco] = useState(escola?.endereco ?? '');
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  if (!escola || !id) {
    return (
      <div className="page">
        <div className="alert alert-error">
          Dados da escola não encontrados. <Link to="/escolas">Voltar para escolas</Link>
        </div>
      </div>
    );
  }

  async function handleSubmit(e: FormEvent) {
    e.preventDefault();
    setLoading(true);
    setError(null);

    try {
      await atualizarEscola(id!, nome, endereco);
      navigate(`/escolas/${id}`);
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Erro ao atualizar escola');
      setLoading(false);
    }
  }

  return (
    <div className="page">
      <div className="breadcrumb">
        <Link to="/escolas">Escolas</Link> / <Link to={`/escolas/${id}`}>{escola.nome}</Link> / Editar
      </div>

      <div className="page-header">
        <h1>Editar Escola</h1>
      </div>

      <div className="card" style={{ maxWidth: '480px' }}>
        {error && <div className="alert alert-error">{error}</div>}

        <form onSubmit={handleSubmit}>
          <div className="form-group">
            <label>Nome da escola</label>
            <input
              type="text"
              value={nome}
              onChange={e => setNome(e.target.value)}
              required
              autoFocus
            />
          </div>
          <div className="form-group">
            <label>Endereço</label>
            <input
              type="text"
              value={endereco}
              onChange={e => setEndereco(e.target.value)}
              required
            />
          </div>
          <div style={{ display: 'flex', gap: '12px', marginTop: '8px' }}>
            <button
              type="button"
              className="btn btn-secondary"
              onClick={() => navigate(`/escolas/${id}`)}
            >
              Cancelar
            </button>
            <button type="submit" className="btn btn-primary" disabled={loading}>
              {loading ? 'Salvando...' : 'Salvar alterações'}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}
