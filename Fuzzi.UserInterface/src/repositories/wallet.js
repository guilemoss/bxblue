function getWalletCombo() {
  return fetch('api/wallet/combo')
    .then(async (response) => {
      if (response.ok) {
        return await response.json();
      }

      throw new Error('Coulnd\'t get data');
    });
}

function getWallet(id) {
  return fetch(`../api/wallet/${id}`)
    .then(async (response) => {
      if (response.ok) {
        return await response.json();
      }

      throw new Error('Coulnd\'t get data');
    });
}

function purchaseAsset(objectAsset) {
  return fetch('../api/wallet/purchase', {
    method: 'POST',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(objectAsset),
  })
    .then(async (response) => {
      if (response.ok) {
        return response.ok;
      }
      throw new Error('Couldn\'t purchase the asset.');
    });
}

export default {
  getWalletCombo,
  getWallet,
  purchaseAsset
};
