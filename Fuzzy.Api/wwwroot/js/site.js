const uri = 'api/';
const quantityAssetsForecastDefault = 3;
const valueToApplyDefault = 100;

let assets = [];
let quantityAssetsForecastTextbox = document.getElementById('quantityAssetsForecast');
quantityAssetsForecastTextbox.setAttribute('value', quantityAssetsForecastDefault);
quantityAssetsForecastTextbox.setAttribute('onblur', 'getItems()');

let valueToApplyTextbox = document.getElementById('valueToApply');
valueToApplyTextbox.setAttribute('onblur', 'getItems()');

let walletSelect = document.getElementById('walletId');
walletSelect.setAttribute('onchange', 'getWallet(this.value);');

function setWalletCombo() {
    fetch(`${uri}wallet/combo`)
        .then(response => response.json())
        .then(data => _displayWallets(data))
        .catch(error => console.error('Unable to get items.', error));

}

function getWallet(id) {
    fetch(`${uri}wallet/${id}`)
        .then(response => response.json())
        .then(data => _displayWalletDashboard(data))
        .catch(error => console.error('Unable to get items.', error));
}

function getItems() {
    let quantityAssetsForecastValue = parseInt(quantityAssetsForecastTextbox.value);
    quantityAssetsForecast = quantityAssetsForecastValue > 0 ? quantityAssetsForecastValue : quantityAssetsForecastDefault;
    
    let valueToApplyValue = parseInt(valueToApplyTextbox.value);
    valueToApplyValue = valueToApplyValue > 100 ? valueToApplyValue : valueToApplyDefault;
    valueToApplyTextbox.value = valueToApplyValue;

    fetch(`${uri}asset/forecast/${valueToApplyValue}?pageSize=${quantityAssetsForecast}`)
        .then(response => response.json())
        .then(data => _displayItems(data))
        .catch(error => console.error('Unable to get items.', error));
}

function createWallet() {
    const walletNameTextbox = document.getElementById('wallet-name');
    const item = {
        name: walletNameTextbox.value.trim()
    };

    fetch(`${uri}wallet`, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(response => response.json())
        .then(() => {
            setWalletCombo();
            walletNameTextbox.value = '';
        })
        .catch(error => console.error('Unable to add item.', error));
}


function purchaseAsset(id) {
    const valueToApply = parseInt(valueToApplyTextbox.value);
    const walletId = document.getElementById('walletId').value;

    if (Number.isNaN(valueToApply) || valueToApply == 0)
        return false;

    const item = {
        walletId: walletId,
        assetForecastId: id,
        value: parseInt(valueToApply)
    };
        
    fetch(`${uri}wallet/purchase`, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(response => response.json())
        .then(() => {
            valueToApplyTextbox.value = valueToApplyDefault;
            quantityAssetsForecastTextbox.value = quantityAssetsForecastDefault;
            getWallet(walletId);
        })
        .catch(error => console.error('Unable to delete item.', error));
}

function _displayCount(itemCount) {
    const name = (itemCount === 1) ? 'asset' : 'assets';

    document.getElementById('counter').innerText = `${itemCount} ${name}`;
}

function _displayWalletCount(itemCount) {
    const name = (itemCount === 1) ? 'asset' : 'assets';

    document.getElementById('counterWalletAssets').innerText = `${itemCount} ${name}`;
}

function _displayWallets(data) {
    var select = document.getElementById("walletId");
    var length = select.options.length;
    for (i = length - 1; i >= 0; i--) {
        select.options[i] = null;
    }

    var option = document.createElement("option");
    option.className = 'placeholder';
    option.value = '';
    option.text = 'Select wallet';
    option.selected = true;
    option.disabled = true;
    select.appendChild(option);

    data.forEach(item => {
        var option = document.createElement("option");
        option.value = item.id;
        option.text = item.name;
        select.appendChild(option);
    });

    if (select.options.length > 1) {
        //select.options[1].selected = true;
        let valueSelected = select.options[1].value;
        select.value = valueSelected;
        getWallet(valueSelected);
    }
}
    
function _displayWalletDashboard(data) {
    let sumWallet = 0;
    const walletAssets = data.walletAssets;
    const tBody = document.getElementById('walletAssets');
    tBody.innerHTML = '';

    _displayWalletCount(walletAssets.length);

    if (walletAssets.lenght === 0) {
        document.getElementById('walletInvestiment').style.display = 'none';
    }

    walletAssets.forEach(item => {
        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        let walletAssetDate = new Date(Date.parse(item.date));
        let formattedDate = _appendLeadingZeroes(walletAssetDate.getDate()) + "-" + _appendLeadingZeroes(walletAssetDate.getMonth() + 1) + "-" + walletAssetDate.getFullYear();
        let formattedTime = _appendLeadingZeroes(walletAssetDate.getHours()) + ":" + _appendLeadingZeroes(walletAssetDate.getMinutes()) + ":" + _appendLeadingZeroes(walletAssetDate.getSeconds());
        let textIndexNode = document.createTextNode(formattedDate + " " + formattedTime);
        td1.appendChild(textIndexNode);

        let td2 = tr.insertCell(1);
        let textNameNode = document.createTextNode(item.asset.name);
        td2.appendChild(textNameNode);

        let td3 = tr.insertCell(2);
        let textSymbolNode = document.createTextNode(item.asset.symbol);
        td3.appendChild(textSymbolNode);

        let td4 = tr.insertCell(3);
        td4.className = 'currency';
        let textPriceNode = document.createTextNode(_formatPriceUSD(item.price));
        td4.appendChild(textPriceNode);

        let td5 = tr.insertCell(4);
        td5.className = 'currency';
        let textValueAppliedNode = document.createTextNode(_formatPriceUSD(item.value));
        td5.appendChild(textValueAppliedNode);

        let td6 = tr.insertCell(5);
        td6.className = 'currency';
        let valueAmount = item.value > 0 ? parseInt((item.value / item.price)) : 0;
        let textAmountNode = document.createTextNode(`${valueAmount}x`);
        td6.appendChild(textAmountNode);

        sumWallet += item.value;
    });

    let tr = tBody.insertRow();
    let td1 = tr.insertCell(0);
    let td2 = tr.insertCell(1);
    let td3 = tr.insertCell(2);
    let td4 = tr.insertCell(3);
    let textTotalAppliedNode = document.createTextNode('Total');
    td4.appendChild(textTotalAppliedNode);

    let td5 = tr.insertCell(4);
    td5.className = 'currency';
    let textSumWalletValueNode = document.createTextNode(_formatPriceUSD(sumWallet));
    td5.appendChild(textSumWalletValueNode);
}

function _displayItems(data) {
    const tBody = document.getElementById('assets');
    tBody.innerHTML = '';

    _displayCount(data.length);

    const button = document.createElement('button');
    const valueToApply = parseInt(valueToApplyTextbox.value);
    let index = 0;
    data.forEach(item => {
        index++;
        let purchaseButton = button.cloneNode(false);
        purchaseButton.innerText = 'Apply';
        purchaseButton.setAttribute('onclick', `purchaseAsset('${item.id}')`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        let textIndexNode = document.createTextNode(`${index}º`);
        td1.appendChild(textIndexNode);

        let td2 = tr.insertCell(1);
        let textNameNode = document.createTextNode(item.asset.name);
        td2.appendChild(textNameNode);

        let td3 = tr.insertCell(2);
        let textSymbolNode = document.createTextNode(item.asset.symbol);
        td3.appendChild(textSymbolNode);

        let td4 = tr.insertCell(3);
        td4.className = 'currency';
        let textPriceNode = document.createTextNode(_formatPriceUSD(item.price));
        td4.appendChild(textPriceNode);

        let td5 = tr.insertCell(4);
        td5.className = 'currency';
        let valueAmount = valueToApply > 0 ? parseInt((valueToApply / item.price)) : 0;
        let textAmountNode = document.createTextNode(`${valueAmount}x`);
        td5.appendChild(textAmountNode);

        let td6 = tr.insertCell(5);
        td6.appendChild(purchaseButton);
    });

    assets = data;
}

function _appendLeadingZeroes(n) {
    return(n <= 9) ? "0" + n : n;
}

function _formatPriceUSD(valuePrice) {
    return Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD' }).format(valuePrice);
}