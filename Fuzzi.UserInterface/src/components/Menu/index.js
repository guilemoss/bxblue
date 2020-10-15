import React from 'react';
import { Link } from 'react-router-dom';
import './Menu.css';
import Button from '../Button';

function Menu() {
  return (
    <nav className="Menu">
      <Link to="/">
        <span className="Logo">Fuzzy Trader</span>
      </Link>

      <div>
        <Button as={Link} className="ButtonLink" to="/wallet/dashboard">
          Wallet
      </Button>
        <Button as={Link} className="ButtonLink" to="/wallet/apply">
          Apply
      </Button>
      </div>
    </nav>
  );
}

export default Menu;