import { useState, useEffect } from 'react';
import { useParams, useNavigate, Link } from 'react-router-dom';
import { listarAlunosDaTurma } from '../api/turmas';
import { listarFilaEspera } from '../api/matriculas';
import type { AlunoResponse, FilaEsperaResponse } from '../types';

function formatDate(dateStr: string) {
  return new Date(dateStr).toLocaleDateString('pt-BR');
}

type Aba = 'alunos' | 'fila';

export default function TurmaDetalhe() {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const [alunos, setAlunos] = useState<AlunoResponse[]>([]);
  const [fila, setFila] = useState<FilaEsperaResponse[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [aba, setAba] = useState<Aba>('alunos');

  useEffect(() => {
    if (!id) return;

    Promise.all([listarAlunosDaTurma(id), listarFilaEspera(id)])
      .then(([alunosData, filaData]) => {
        setAlunos(alunosData);
        setFila(filaData);
      })
      .catch(err => setError(err instanceof Error ? err.message : 'Erro ao carregar dados'))
      .finally(() => setLoading(false));
  }, [id]);

  if (loading) return <div className="page"><div className="loading">Carregando...</div></div>;
  if (error) return <div className="page"><div className="alert alert-error">{error}</div></div>;

  return (
    <div className="page">
      <div className="breadcrumb">
        <Link to="/escolas">Escolas</Link> / Turma
      </div>

      <div className="page-header">
        <h1>Detalhes da Turma</h1>
        <button
          className="btn btn-primary"
          onClick={() => navigate(`/turmas/${id}/matricular`)}
        >
          + Solicitar Matrícula
        </button>
      </div>

      <div style={{ display: 'flex', gap: '8px', marginBottom: '16px' }}>
        <button
          className={`btn ${aba === 'alunos' ? 'btn-primary' : 'btn-secondary'}`}
          onClick={() => setAba('alunos')}
        >
          Alunos matriculados ({alunos.length})
        </button>
        <button
          className={`btn ${aba === 'fila' ? 'btn-primary' : 'btn-secondary'}`}
          onClick={() => setAba('fila')}
        >
          Fila de espera ({fila.length})
        </button>
      </div>

      <div className="card">
        {aba === 'alunos' ? (
          alunos.length === 0 ? (
            <div className="empty-state">Nenhum aluno matriculado nesta turma.</div>
          ) : (
            <table className="table">
              <thead>
                <tr>
                  <th>Nome</th>
                  <th>CPF</th>
                  <th>Nascimento</th>
                  <th>Idade</th>
                  <th></th>
                </tr>
              </thead>
              <tbody>
                {alunos.map(aluno => (
                  <tr key={aluno.id}>
                    <td>{aluno.nome}</td>
                    <td>{aluno.cpf}</td>
                    <td>{formatDate(aluno.dataNascimento)}</td>
                    <td>{aluno.idade} anos</td>
                    <td>
                      <button
                        className="btn btn-secondary"
                        style={{ padding: '4px 12px', fontSize: '0.8rem' }}
                        onClick={() => navigate(`/alunos/${aluno.id}/editar`, { state: { aluno, turmaId: id } })}
                      >
                        Editar
                      </button>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          )
        ) : (
          fila.length === 0 ? (
            <div className="empty-state">Nenhum aluno na fila de espera.</div>
          ) : (
            <table className="table">
              <thead>
                <tr>
                  <th>Posição</th>
                  <th>Aluno</th>
                  <th>CPF</th>
                  <th>Solicitado em</th>
                </tr>
              </thead>
              <tbody>
                {fila.map(item => (
                  <tr key={item.matriculaId}>
                    <td><strong>{item.posicao}º</strong></td>
                    <td>{item.aluno.nome}</td>
                    <td>{item.aluno.cpf}</td>
                    <td>{formatDate(item.dataSolicitacao)}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          )
        )}
      </div>
    </div>
  );
}
