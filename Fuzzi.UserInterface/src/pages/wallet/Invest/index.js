import React, { useState } from 'react';
import { Link, useHistory } from 'react-router-dom';
import PageDefault from '../../../components/PageDefault';
import FormField from '../../../components/FormField';
import Button from '../../../components/Button';
import useForm from '../../../hooks/useForm';
import assetRepository from '../../../repositories/assets';
import walletRepository from '../../../repositories/wallet';

function WalletInvestiment() {
    const quantityAssetsForecast = localStorage.getItem('quantityAssetsForecast');
    const walletId = localStorage.getItem('walletId');
    const history = useHistory();
    const initialValues = {
        walletId: walletId,
        assetName: '',
        valueToApply: ''
    };

    const [assets, setAssets] = useState([]);
    const assetsNames = assets.map(({ asset }) => asset.name);

    const { values, handleChange } = useForm(initialValues);

    function handleGetAssetsForecast() {
        if (parseInt(values.valueToApply) >= 0) {
            assetRepository
                .getForecast(values.valueToApply, quantityAssetsForecast)
                .then((response) => {
                    setAssets(response);
                });
        }
    }

    function _formatPriceUSD(valuePrice) {
        return Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD' }).format(valuePrice);
    }

    return (
        <PageDefault>
            <h1>
                Would you like to apply?
                {` ${values.valueToApply}`}
            </h1>

            <form onSubmit={function handleSubmit(event) {
                event.preventDefault();

                const chosenAsset = assets.find((item) => item.asset.name === values.assetName);

                walletRepository.purchaseAsset({
                    value: parseInt(values.valueToApply),
                    walletId: initialValues.walletId,
                    assetForecastId: chosenAsset.id,
                })
                    .then(() => {
                        console.log('Investment made successfully!');
                        history.push('/wallet/dashboard');
                    });
            }}
            >

                <FormField
                    label="USD"
                    type="number"
                    name="valueToApply"
                    value={values.valueToApply}
                    onChange={handleChange}
                    onBlur={handleGetAssetsForecast}
                    pattern="[0-9]*"
                    min="100"
                    step="100"
                />

                <FormField
                    label="Asset"
                    name="assetName"
                    value={values.assetName}
                    onChange={handleChange}
                    suggestions={assetsNames}
                />
                <Button type="submit">
                    Purchase
                </Button>
            </form>
            {
                assets.length > 0 && (
                    <div id="assetsStatistics">
                        <h1>Statistics </h1>
                        <table>
                            <thead>
                                <tr>
                                    <th>Score</th>
                                    <th>Name</th>
                                    <th>Symbol</th>
                                    <th>Price</th>
                                    <th>Amount</th>
                                </tr>
                            </thead>
                            <tbody>
                                {assets.length &&
                                    assets.map((item, index) => (
                                        <tr key={item.id}>
                                            <td>{item.asset.symbol==="BTC" ? "-" : `${(index + 1)}º`}</td>
                                            <td>{item.asset.name}</td>
                                            <td id="symbol"><span className={item.asset.symbol==="BTC" ? "BTC" :""}>{item.asset.symbol}</span></td>
                                            <td className="currency">{_formatPriceUSD(item.price)}</td>
                                            <td id="amount" className="number">{parseInt(item.value / item.price)}</td>
                                        </tr>
                                    ))
                                }
                            </tbody>
                        </table>
                        <br />
                        <small className="text-muted">We're using moving average for the last 30 days based on the closing value.</small>
                        <br />
                        <small className="text-muted">
                            {`Showing bitcoin and ${quantityAssetsForecast} assets based on your `}
                            <Link to="/">settings.</Link>
                        </small>
                    </div>)
            }
            <Link to="/">Go Homepage</Link>
        </PageDefault >
    );
}

export default WalletInvestiment;
