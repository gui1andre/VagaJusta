import { Link, useNavigate } from 'react-router-dom';

export default function Navbar() {
  const navigate = useNavigate();

  function handleLogout() {
    localStorage.removeItem('token');
    navigate('/login');
  }

  return (
    <nav className="navbar">
      <Link to="/escolas" className="navbar-brand">VagaJusta</Link>
      <div className="navbar-actions">
        <Link to="/escolas" className="navbar-link">Escolas</Link>
        <button onClick={handleLogout}>Sair</button>
      </div>
    </nav>
  );
}
