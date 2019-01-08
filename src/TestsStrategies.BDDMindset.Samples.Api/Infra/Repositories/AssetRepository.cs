﻿using System;
using System.Collections.Generic;
using Dapper;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Commands.Request;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Entities;
using TestsStrategies.BDDMindset.Samples.Api.Domain.Queries.Response;
using TestsStrategies.BDDMindset.Samples.Api.Infra.Database;
using TestsStrategies.BDDMindset.Samples.Api.Infra.QueryBuilders;

namespace TestsStrategies.BDDMindset.Samples.Api.Infra.Repositories
{
	public class AssetRepository : IAssetRepository
	{
		private readonly IDatabaseContext context;
		public AssetRepository(IDatabaseContext context) => this.context = context;

		public IEnumerable<AssetQueryResult> GetAll()
		{
			var (query, parameters) = new AssetQueryBuilder().GetAll().Build();

			var assets = this.context.Connection.Query<AssetQueryResult>(query);

			return assets;
		}

		public AssetQueryResult GetById(Guid assetId)
		{
			var (query, parameters) = new AssetQueryBuilder().GetById(assetId).Build();

			var asset = this.context.Connection.QueryFirstOrDefault<AssetQueryResult>(query, parameters);

			return asset;
		}

		public IEnumerable<AssetQueryResult> GetAllByBrandId(Guid brandId)
		{
			var (query, parameters) = new AssetQueryBuilder().GetAllByBrandId(brandId).Build();

			var assets = this.context.Connection.Query<AssetQueryResult>(query, parameters);

			return assets;
		}

		public void Save(Asset asset)
		{
			var (query, parameters) = new AssetQueryBuilder().CreateAsset(asset).Build();

			this.context.Connection.Execute(query, parameters);
		}

		public bool Update(Guid id, AssetCommand assetCommand)
		{
			var (query, parameters) = new AssetQueryBuilder().UpdateAsset(id, assetCommand).Build();

			var result = this.context.Connection.Execute(query, parameters);
			return Convert.ToBoolean(result);
		}

		public bool Delete(Guid assetId)
		{
			var (query, parameters) = new AssetQueryBuilder().DeleteAsset(assetId).Build();

			var result = this.context.Connection.Execute(query, parameters);
			return Convert.ToBoolean(result);
		}
	}
}
