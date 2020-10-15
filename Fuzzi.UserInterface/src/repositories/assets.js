function getAll() {
  return fetch('../api/asset')
    .then(async (response) => {
      if (response.ok) {
        const resposta = await response.json();
        return resposta;
      }

      throw new Error('Coulnd\'t get data');
    });
}

function getForecast(valueToApply, pageSize, pageIndex) {
  let queryString = '';

  if (parseInt(pageSize) > 0)
    queryString = `?pageSize=${pageSize}`;

  if (parseInt(pageIndex) > 0)
    queryString += `&pageIndex=${pageIndex}`;

    return fetch(`../api/asset/forecast/${valueToApply}${queryString}`)
    .then(async (response) => {
      if (response.ok) {
        const resposta = await response.json();
        return resposta;
      }
    });
}

export default {
  getAll,
  getForecast
};
