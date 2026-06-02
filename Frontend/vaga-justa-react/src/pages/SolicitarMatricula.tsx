import { useState, type FormEvent } from 'react';
import { useNavigate, useParams, Link } from 'react-router-dom';
import { solicitarMatricula } from '../api/matriculas';
import type { MatriculaResponse } from '../types';

function maskCPF(value: string) {
  return value
    .replace(/\D/g, '')
    .replace(/(\d{3})(\d)/, '$1.$2')
    .replace(/(\d{3})(\d)/, '$1.$2')
    .replace(/(\d{3})(\d{1,2})$/, '$1-$2')
    .slice(0, 14);
}

export default function SolicitarMatricula() {
  const { turmaId } = useParams<{ turmaId: string }>();
  const navigate = useNavigate();

  const [nome, setNome] = useState('');
  const [cpf, setCpf] = useState('');
  const [dataNascimento, setDataNascimento] = useState('');
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [sucesso, setSucesso] = useState<MatriculaResponse | null>(null);

  async function handleSubmit(e: FormEvent) {
    e.preventDefault();
    if (!turmaId) return;

    setLoading(true);
    setError(null);

    try {
      const resultado = await solicitarMatricula({
        nomeAluno: nome,
        cpfAluno: cpf.replace(/\D/g, ''),
        dataNascimento: `${dataNascimento}T00:00:00Z`,
        turmaId,
      });
      setSucesso(resultado);
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Erro ao solicitar matrícula');
    } finally {
      setLoading(false);
    }
  }

  if (sucesso) {
    return (
      <div className="page">
        <div className="card" style={{ maxWidth: '480px', textAlign: 'center', padding: '40px 32px' }}>
          <div style={{ fontSize: '3rem', marginBottom: '16px', color: 'var(--success)' }}>✓</div>
          <h2 style={{ marginBottom: '12px' }}>Matrícula solicitada!</h2>
          <p style={{ color: 'var(--text-muted)', marginBottom: '4px' }}>
            Aluno: <strong>{sucesso.nomeAluno}</strong>
          </p>
          <p style={{ color: 'var(--text-muted)', marginBottom: '4px' }}>
            Turma: <strong>{sucesso.turma}</strong>
          </p>
          <p style={{ color: 'var(--text-muted)', marginBottom: '24px' }}>
            Escola: <strong>{sucesso.escola}</strong>
          </p>
          <span className="badge badge-blue" style={{ fontSize: '0.875rem', padding: '4px 14px' }}>
            {sucesso.status}
          </span>
          <div style={{ display: 'flex', gap: '8px', justifyContent: 'center', marginTop: '24px' }}>
            <button
              className="btn btn-secondary"
              onClick={() => navigate(`/turmas/${turmaId}`)}
            >
              Ver turma
            </button>
            <button
              className="btn btn-primary"
              onClick={() => { setNome(''); setCpf(''); setDataNascimento(''); setSucesso(null); }}
            >
              Nova solicitação
            </button>
          </div>
        </div>
      </div>
    );
  }

  return (
    <div className="page">
      <div className="breadcrumb">
        <Link to={`/turmas/${turmaId}`}>Turma</Link> / Solicitar Matrícula
      </div>

      <div className="page-header">
        <h1>Solicitar Matrícula</h1>
      </div>

      <div className="card" style={{ maxWidth: '480px' }}>
        {error && <div className="alert alert-error">{error}</div>}

        <form onSubmit={handleSubmit}>
          <div className="form-group">
            <label>Nome do aluno</label>
            <input
              type="text"
              value={nome}
              onChange={e => setNome(e.target.value)}
              placeholder="Nome completo"
              required
              autoFocus
            />
          </div>
          <div className="form-group">
            <label>CPF</label>
            <input
              type="text"
              value={cpf}
              onChange={e => setCpf(maskCPF(e.target.value))}
              placeholder="000.000.000-00"
              required
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
              onClick={() => navigate(-1)}
            >
              Cancelar
            </button>
            <button type="submit" className="btn btn-primary" disabled={loading}>
              {loading ? 'Enviando...' : 'Solicitar Matrícula'}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}
