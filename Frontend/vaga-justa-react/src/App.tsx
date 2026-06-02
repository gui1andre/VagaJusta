import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import PrivateRoute from './components/PrivateRoute';
import Login from './pages/Login';
import Escolas from './pages/Escolas';
import EscolaDetalhe from './pages/EscolaDetalhe';
import CriarEscola from './pages/CriarEscola';
import CriarTurma from './pages/CriarTurma';
import TurmaDetalhe from './pages/TurmaDetalhe';
import SolicitarMatricula from './pages/SolicitarMatricula';
import EditarAluno from './pages/EditarAluno';
import EditarEscola from './pages/EditarEscola';

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/login" element={<Login />} />
        <Route element={<PrivateRoute />}>
          <Route path="/escolas" element={<Escolas />} />
          <Route path="/escolas/nova" element={<CriarEscola />} />
          <Route path="/escolas/:id" element={<EscolaDetalhe />} />
          <Route path="/escolas/:id/editar" element={<EditarEscola />} />
          <Route path="/escolas/:escolaId/turmas/nova" element={<CriarTurma />} />
          <Route path="/turmas/:id" element={<TurmaDetalhe />} />
          <Route path="/turmas/:turmaId/matricular" element={<SolicitarMatricula />} />
          <Route path="/alunos/:alunoId/editar" element={<EditarAluno />} />
        </Route>
        <Route path="*" element={<Navigate to="/escolas" replace />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
