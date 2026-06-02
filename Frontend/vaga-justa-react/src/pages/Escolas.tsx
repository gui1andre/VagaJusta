import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { listarEscolas, deletarEscola } from '../api/escolas';
import type { EscolaResponse } from '../types';

export default function Escolas() {
  const navigate = useNavigate();
  const [escolas, setEscolas] = useState<EscolaResponse[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [deletando, setDeletando] = useState<string | null>(null);

  useEffect(() => {
    fetchEscolas();
  }, []);

  async function fetchEscolas() {
    setLoading(true);
    setError(null);
    try {
      const data = await listarEscolas();
      setEscolas(data);
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Erro ao carregar escolas');
    } finally {
      setLoading(false);
    }
  }

  async function handleDeletar(id: string, nome: string) {
    if (!window.confirm(`Tem certeza que deseja excluir "${nome}"?`)) return;
    setDeletando(id);
    try {
      await deletarEscola(id);
      setEscolas(prev => prev.filter(e => e.id !== id));
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Erro ao excluir escola');
    } finally {
      setDeletando(null);
    }
  }

  return (
    <div className="page">
      <div className="page-header">
        <h1>Escolas</h1>
        <button className="btn btn-primary" onClick={() => navigate('/escolas/nova')}>
          + Nova Escola
        </button>
      </div>

      {error && <div className="alert alert-error">{error}</div>}

      <div className="card">
        {loading ? (
          <div className="loading">Carregando...</div>
        ) : escolas.length === 0 ? (
          <div className="empty-state">
            <p>Nenhuma escola cadastrada ainda.</p>
            <button
              className="btn btn-primary"
              style={{ marginTop: '12px' }}
              onClick={() => navigate('/escolas/nova')}
            >
              Cadastrar primeira escola
            </button>
          </div>
        ) : (
          escolas.map(escola => (
            <div key={escola.id} className="escola-item">
              <div className="escola-info">
                <h3>{escola.nome}</h3>
                <p>{escola.endereco}</p>
                <p style={{ fontSize: '0.75rem', marginTop: '2px' }}>
                  {escola.turmas?.length ?? 0} turma(s)
                </p>
              </div>
              <div className="escola-actions">
                <button
                  className="btn btn-secondary"
                  onClick={() => navigate(`/escolas/${escola.id}`)}
                >
                  Ver detalhes
                </button>
                <button
                  className="btn btn-danger"
                  onClick={() => handleDeletar(escola.id, escola.nome)}
                  disabled={deletando === escola.id}
                >
                  {deletando === escola.id ? '...' : 'Excluir'}
                </button>
              </div>
            </div>
          ))
        )}
      </div>
    </div>
  );
}
