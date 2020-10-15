import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import PageDefault from '../../../components/PageDefault';
import walletRepository from '../../../repositories/wallet';

function WalletDashBoard() {
    const walletId = localStorage.getItem('walletId');
    const [wallet, setWallet] = useState({ id: '', name: '', walletAssets: [] });

    useEffect(() => {
        walletRepository
            .getWallet(walletId)
            .then((response) => {
                setWallet(response);
            });
    }, [walletId]);

    const _formatPriceUSD = (value) =>
        new Intl.NumberFormat('en-US', {
            style: 'currency',
            currency: 'USD'
        }).format(value);

    const _appendLeadingZeroes = (number) =>
        (number <= 9) ? "0" + number : number;

    const _formatDate = (inputDate) => {
        const date = new Date(Date.parse(inputDate));
        const formattedDate = _appendLeadingZeroes(date.getDate()) + "-" + _appendLeadingZeroes(date.getMonth() + 1) + "-" + date.getFullYear();
        const formattedTime = _appendLeadingZeroes(date.getHours()) + ":" + _appendLeadingZeroes(date.getMinutes()) + ":" + _appendLeadingZeroes(date.getSeconds());
        return `${formattedDate} ${formattedTime}`;
    };

    return (
        <PageDefault>
            <h1>
                Your wallet {wallet.name} has
        {` ${wallet && wallet.walletAssets?.length} `}
        assets
      </h1>
            <Link to="/">
                Go Homepage
            </Link>
            {
                wallet.walletAssets.length > 0 && (
                    <div id="assetsStatistics">
                        <h1>Wallet Assets</h1>
                        <table>
                            <thead>
                                <tr>
                                    <th>Date</th>
                                    <th>Name</th>
                                    <th>Symbol</th>
                                    <th>Purchased</th>
                                    <th>Applied</th>
                                    <th>Amount</th>
                                </tr>
                            </thead>
                            <tbody>
                                {
                                    wallet.walletAssets.map((item, index) => (
                                        <tr key={item.id}>
                                            <td>{_formatDate(item.date)}</td>
                                            <td>{item.asset.name}</td>
                                            <td id="symbol"><span className={item.asset.symbol==="BTC" ? "BTC" :""}>{item.asset.symbol}</span></td>
                                            <td className="currency">{_formatPriceUSD(item.price)}</td>
                                            <td id="applied" className="currency">{_formatPriceUSD(item.value)}</td>
                                            <td id="amount" className="number">{parseInt(item.value / item.price)}</td>
                                        </tr>
                                    ))
                                }
                            </tbody>
                            <tfoot>
                                <tr>
                                    <td colSpan="4" align="right">Total</td>
                                    <td>{_formatPriceUSD(wallet.walletAssets.reduce((sum, item) => sum + item.value, 0))}</td>
                                </tr>
                            </tfoot>
                        </table>
                        <br /><br />

                    </div>)
            }
        </PageDefault>
    );
}

export default WalletDashBoard;