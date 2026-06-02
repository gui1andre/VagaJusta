import { useState, type FormEvent } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { criarEscola } from '../api/escolas';

export default function CriarEscola() {
  const navigate = useNavigate();
  const [nome, setNome] = useState('');
  const [endereco, setEndereco] = useState('');
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  async function handleSubmit(e: FormEvent) {
    e.preventDefault();
    setLoading(true);
    setError(null);

    try {
      const escola = await criarEscola(nome, endereco);
      navigate(`/escolas/${escola.id}`);
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Erro ao criar escola');
      setLoading(false);
    }
  }

  return (
    <div className="page">
      <div className="breadcrumb">
        <Link to="/escolas">Escolas</Link> / Nova Escola
      </div>

      <div className="page-header">
        <h1>Nova Escola</h1>
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
              placeholder="Ex: Escola Municipal João Silva"
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
              placeholder="Ex: Rua das Flores, 123 - Centro"
              required
            />
          </div>
          <div style={{ display: 'flex', gap: '12px', marginTop: '8px' }}>
            <button type="button" className="btn btn-secondary" onClick={() => navigate('/escolas')}>
              Cancelar
            </button>
            <button type="submit" className="btn btn-primary" disabled={loading}>
              {loading ? 'Salvando...' : 'Criar Escola'}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}
