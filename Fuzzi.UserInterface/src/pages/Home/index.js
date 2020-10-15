import React, { useEffect } from 'react';
import PageDefault from '../../components/PageDefault';
import FormField from '../../components/FormField';
import useForm from '../../hooks/useForm';
import walletRepository from '../../repositories/wallet';

function Home() {
    const quantityAssetsForecastDefault = 3;
    const quantityAssetsForecast = (localStorage.getItem('quantityAssetsForecast')
        ? localStorage.getItem('quantityAssetsForecast') : quantityAssetsForecastDefault);

    localStorage.setItem('quantityAssetsForecast', quantityAssetsForecast);
    const walletName = localStorage.getItem('walletName') ? localStorage.getItem('walletName') : '';

    const initialValues = {
        walletName: walletName,
        quantityAssetsForecast: quantityAssetsForecast
    };

    const { values, handleChange } = useForm(initialValues);

    useEffect(() => {
        walletRepository
            .getWalletCombo()
            .then((response) => {
                localStorage.setItem('walletId', response[0].id);
                localStorage.setItem('walletName', response[0].name);
            });
    }, []);

    function handleBur() {
        localStorage.setItem('quantityAssetsForecast', values.quantityAssetsForecast);
    }

    return (
        <PageDefault>
            <h1>Settings</h1>
            <form>
                <FormField
                    label="Quantity assets forecast"
                    type="number"
                    name="quantityAssetsForecast"
                    value={values.quantityAssetsForecast.toString()}
                    onChange={handleChange}
                    onBlur={handleBur}
                    pattern="[0-9]*"
                    min="3"
                    max="20"
                    step="5"
                />
            </form>
        </PageDefault >
    );
}

export default Home;