import { useState, type FormEvent } from 'react';
import { useNavigate, useLocation, Link } from 'react-router-dom';
import { atualizarAluno } from '../api/alunos';
import type { AlunoResponse } from '../types';

interface LocationState {
  aluno: AlunoResponse;
  turmaId: string;
}

export default function EditarAluno() {
  const navigate = useNavigate();
  const location = useLocation();
  const state = location.state as LocationState | null;

  const aluno = state?.aluno;
  const turmaId = state?.turmaId;

  const [nome, setNome] = useState(aluno?.nome ?? '');
  const [dataNascimento, setDataNascimento] = useState(
    aluno ? aluno.dataNascimento.slice(0, 10) : ''
  );
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  if (!aluno || !turmaId) {
    return (
      <div className="page">
        <div className="alert alert-error">
          Dados do aluno não encontrados. <Link to="/escolas">Voltar para escolas</Link>
        </div>
      </div>
    );
  }

  async function handleSubmit(e: FormEvent) {
    e.preventDefault();
    setLoading(true);
    setError(null);

    try {
      await atualizarAluno({ id: aluno!.id, nome, dataNascimento: `${dataNascimento}T00:00:00Z` });
      navigate(`/turmas/${turmaId}`);
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Erro ao atualizar aluno');
      setLoading(false);
    }
  }

  return (
    <div className="page">
      <div className="breadcrumb">
        <Link to={`/turmas/${turmaId}`}>Turma</Link> / Editar Aluno
      </div>

      <div className="page-header">
        <h1>Editar Aluno</h1>
      </div>

      <div className="card" style={{ maxWidth: '480px' }}>
        <p style={{ fontSize: '0.8rem', color: 'var(--text-muted)', marginBottom: '20px' }}>
          CPF: <strong>{aluno.cpf}</strong> &bull; Aluno ID: {aluno.id.slice(0, 8)}...
        </p>

        {error && <div className="alert alert-error">{error}</div>}

        <form onSubmit={handleSubmit}>
          <div className="form-group">
            <label>Nome</label>
            <input
              type="text"
              value={nome}
              onChange={e => setNome(e.target.value)}
              required
              autoFocus
            />
          </div>
          <div className="form-group">
            <label>Data de nascimento</label>
            <input
              type="date"
              value={dataNascimento}
              onChange={e => setDataNascimento(e.target.value)}
              required
            />
          </div>
          <div style={{ display: 'flex', gap: '12px', marginTop: '8px' }}>
            <button
              type="button"
              className="btn btn-secondary"
              onClick={() => navigate(`/turmas/${turmaId}`)}
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
