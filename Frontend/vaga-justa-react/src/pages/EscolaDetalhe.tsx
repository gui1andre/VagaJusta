import { useState, useEffect } from 'react';
import { useParams, useNavigate, Link } from 'react-router-dom';
import { buscarEscola } from '../api/escolas';
import { deletarTurma } from '../api/turmas';
import type { EscolaResponse } from '../types';

export default function EscolaDetalhe() {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const [escola, setEscola] = useState<EscolaResponse | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [deletando, setDeletando] = useState<string | null>(null);

  useEffect(() => {
    if (!id) return;
    buscarEscola(id)
      .then(setEscola)
      .catch(err => setError(err instanceof Error ? err.message : 'Erro ao carregar escola'))
      .finally(() => setLoading(false));
  }, [id]);

  async function handleDeletarTurma(turmaId: string, serie: string) {
    if (!window.confirm(`Excluir a turma "${serie}"?`)) return;
    setDeletando(turmaId);
    try {
      await deletarTurma(turmaId);
      setEscola(prev =>
        prev ? { ...prev, turmas: prev.turmas.filter(t => t.id !== turmaId) } : null
      );
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Erro ao excluir turma');
    } finally {
      setDeletando(null);
    }
  }

  if (loading) return <div className="page"><div className="loading">Carregando...</div></div>;
  if (error) return <div className="page"><div className="alert alert-error">{error}</div></div>;
  if (!escola) return null;

  return (
    <div className="page">
      <div className="breadcrumb">
        <Link to="/escolas">Escolas</Link> / {escola.nome}
      </div>

      <div className="page-header">
        <div>
          <h1>{escola.nome}</h1>
          <p style={{ color: 'var(--text-muted)', fontSize: '0.875rem', marginTop: '4px' }}>
            {escola.endereco}
          </p>
        </div>
        <button
          className="btn btn-secondary"
          onClick={() => navigate(`/escolas/${id}/editar`, { state: { escola } })}
        >
          Editar escola
        </button>
      </div>

      <div className="section">
        <div className="section-header">
          <h2>Turmas ({escola.turmas.length})</h2>
          <button
            className="btn btn-primary"
            onClick={() => navigate(`/escolas/${id}/turmas/nova`)}
          >
            + Nova Turma
          </button>
        </div>

        {escola.turmas.length === 0 ? (
          <div className="card">
            <div className="empty-state">Nenhuma turma cadastrada para esta escola.</div>
          </div>
        ) : (
          <div className="turma-grid">
            {escola.turmas.map(turma => (
              <div key={turma.id} className="turma-card">
                <h3>{turma.serie}</h3>
                <p className="turma-meta">
                  {turma.categoriaSerie} &bull; {turma.idadeMinima}–{turma.idadeMaxima} anos
                </p>
                <div className="turma-vagas">
                  <span>
                    <strong>{turma.quantidadeAlunos}</strong>/{turma.capacidadeMaxima} alunos
                  </span>
                  <span style={{ color: turma.vagasDisponiveis > 0 ? 'var(--success)' : 'var(--danger)' }}>
                    {turma.vagasDisponiveis > 0 ? `${turma.vagasDisponiveis} vagas` : 'Sem vagas'}
                  </span>
                </div>
                <div style={{ display: 'flex', gap: '8px', marginTop: '12px' }}>
                  <button
                    className="btn btn-secondary"
                    style={{ flex: 1, justifyContent: 'center' }}
                    onClick={() => navigate(`/turmas/${turma.id}`)}
                  >
                    Ver turma
                  </button>
                  <button
                    className="btn btn-danger"
                    onClick={() => handleDeletarTurma(turma.id, turma.serie)}
                    disabled={deletando === turma.id}
                  >
                    {deletando === turma.id ? '...' : 'Excluir'}
                  </button>
                </div>
              </div>
            ))}
          </div>
        )}
      </div>
    </div>
  );
}
