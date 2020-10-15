using System.Collections.Generic;
using System.Linq;
using Fuzzy.Api.Domain.Entities;
using Fuzzy.Api.Domain.Models;

namespace Fuzzy.Api.Shared.Mapper
{
    public static class AssetForecastMapper
    {
        public static AssetForecastModel ConvertToAssetForecastModel(this AssetForecast assetForecast, int valueToApply) =>
           new AssetForecastModel(assetForecast.Id, assetForecast.Date, assetForecast.Price, valueToApply,
               new AssetModel(assetForecast.AssetId, assetForecast.Asset.Symbol, assetForecast.Asset.Name, assetForecast.Asset.Type)
           );

        public static IEnumerable<AssetForecastModel> ConvertToAssetsForecastModel(this IList<AssetForecast> assetForecasts, int valueToApply) =>
            new List<AssetForecastModel>(assetForecasts.Select(assetForecast => ConvertToAssetForecastModel(assetForecast, valueToApply)));

        public static AssetForecast ConvertToAssetForecastEntity(this AssetForecastModel assetForecastModel) =>
           new AssetForecast(assetForecastModel.Asset.Id, assetForecastModel.Price);

        public static IEnumerable<AssetForecast> ConvertToAssetsForecastEntity(this IList<AssetForecastModel> assetForecastsModel) =>
            new List<AssetForecast>(assetForecastsModel.Select(assetForecastModel => ConvertToAssetForecastEntity(assetForecastModel)));

    }
}
