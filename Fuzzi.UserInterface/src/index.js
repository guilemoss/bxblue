import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import * as serviceWorker from './serviceWorker';

import { BrowserRouter, Switch, Route } from 'react-router-dom';

import Home from './pages/Home';
import WalletInvestiment from './pages/wallet/Invest';
import WalletDashboard from './pages/wallet/Dashboard';

const Page404 = () => (
  <div style={{ display: 'flex', alignItems: 'center', justifyContent: 'center', flexDirection: 'column' }}>
    <h1>Sorry, we couldn’t find that page</h1>
    <p>
      <a href="/">Go Homepage</a>
    </p>
  </div>
)

ReactDOM.render(
  <BrowserRouter>
    <Switch>
      <Route path="/" component={Home} exact />
      <Route path="/wallet/dashboard" component={WalletDashboard} />
      <Route path="/wallet/apply" component={WalletInvestiment} />
      <Route component={Page404} />
    </Switch>
  </BrowserRouter>,
  document.getElementById('root')
);

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();
