using System.Collections.Generic;
using System.Linq;
using Fuzzy.Api.Domain.Entities;
using Fuzzy.Api.Domain.Models;

namespace Fuzzy.Api.Shared.Mapper
{
    public static class AssetMapper
    {
        public static AssetModel ConvertToAssetModel(this Asset asset) =>
            new AssetModel(asset.Id, asset.Symbol, asset.Name, asset.Type);

        public static IEnumerable<AssetModel> ConvertToAssetsModel(this IList<Asset> assets) =>
            new List<AssetModel>(assets.Select(asset => ConvertToAssetModel(asset)));

        public static Asset ConvertToAssetEntity(this AssetModel assetModel) =>
            new Asset(assetModel.Symbol, assetModel.Name);
    }
}
