import { useState, type FormEvent } from 'react';
import { useNavigate, useParams, Link } from 'react-router-dom';
import { criarTurma } from '../api/turmas';

const SERIES: { value: string; label: string }[] = [
  { value: 'PreI',        label: 'Pré I' },
  { value: 'PreII',       label: 'Pré II' },
  { value: 'PrimeiroAno', label: '1º Ano' },
  { value: 'SegundoAno',  label: '2º Ano' },
  { value: 'TerceiroAno', label: '3º Ano' },
  { value: 'QuartoAno',   label: '4º Ano' },
  { value: 'QuintoAno',   label: '5º Ano' },
];

const CATEGORIAS: { value: string; label: string }[] = [
  { value: 'A', label: 'A' },
  { value: 'B', label: 'B' },
  { value: 'C', label: 'C' },
];

export default function CriarTurma() {
  const { escolaId } = useParams<{ escolaId: string }>();
  const navigate = useNavigate();

  const [serie, setSerie] = useState('');
  const [categoriaSerie, setCategoriaSerie] = useState('');
  const [capacidadeMaxima, setCapacidadeMaxima] = useState('');
  const [idadeMinima, setIdadeMinima] = useState('');
  const [idadeMaxima, setIdadeMaxima] = useState('');
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  async function handleSubmit(e: FormEvent) {
    e.preventDefault();
    if (!escolaId) return;

    setLoading(true);
    setError(null);

    try {
      await criarTurma({
        escolaId,
        serie,
        categoriaSerie,
        capacidadeMaxima: parseInt(capacidadeMaxima),
        idadeMinima: parseInt(idadeMinima),
        idadeMaxima: parseInt(idadeMaxima),
      });
      navigate(`/escolas/${escolaId}`);
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Erro ao criar turma');
      setLoading(false);
    }
  }

  return (
    <div className="page">
      <div className="breadcrumb">
        <Link to="/escolas">Escolas</Link> / <Link to={`/escolas/${escolaId}`}>Escola</Link> / Nova Turma
      </div>

      <div className="page-header">
        <h1>Nova Turma</h1>
      </div>

      <div className="card" style={{ maxWidth: '480px' }}>
        {error && <div className="alert alert-error">{error}</div>}

        <form onSubmit={handleSubmit}>
          <div className="form-group">
            <label>Série</label>
            <select value={serie} onChange={e => setSerie(e.target.value)} required autoFocus>
              <option value="">Selecione...</option>
              {SERIES.map(s => (
                <option key={s.value} value={s.value}>{s.label}</option>
              ))}
            </select>
          </div>
          <div className="form-group">
            <label>Categoria</label>
            <select
              value={categoriaSerie}
              onChange={e => setCategoriaSerie(e.target.value)}
              required
            >
              <option value="">Selecione...</option>
              {CATEGORIAS.map(c => (
                <option key={c.value} value={c.value}>{c.label}</option>
              ))}
            </select>
          </div>
          <div className="form-group">
            <label>Capacidade máxima</label>
            <input
              type="number"
              value={capacidadeMaxima}
              onChange={e => setCapacidadeMaxima(e.target.value)}
              placeholder="Ex: 30"
              min="1"
              required
            />
          </div>
          <div className="form-row">
            <div className="form-group">
              <label>Idade mínima</label>
              <input
                type="number"
                value={idadeMinima}
                onChange={e => setIdadeMinima(e.target.value)}
                placeholder="Ex: 6"
                min="0"
                required
              />
            </div>
            <div className="form-group">
              <label>Idade máxima</label>
              <input
                type="number"
                value={idadeMaxima}
                onChange={e => setIdadeMaxima(e.target.value)}
                placeholder="Ex: 7"
                min="0"
                required
              />
            </div>
          </div>
          <div style={{ display: 'flex', gap: '12px', marginTop: '8px' }}>
            <button
              type="button"
              className="btn btn-secondary"
              onClick={() => navigate(`/escolas/${escolaId}`)}
            >
              Cancelar
            </button>
            <button type="submit" className="btn btn-primary" disabled={loading}>
              {loading ? 'Salvando...' : 'Criar Turma'}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}
