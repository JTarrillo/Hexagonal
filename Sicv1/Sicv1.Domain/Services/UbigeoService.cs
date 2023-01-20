using Sicv1.Domain.Contracts.Repositories;
using Sicv1.Domain.Contracts.Services;
using Sicv1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sicv1.Domain.Services
{
	public class UbigeoService : IUbigeoService
	{
		private readonly IUbigeoRepository _ubigeoRepository;
		public UbigeoService(IUbigeoRepository ubigeoRepository)
		{
			this._ubigeoRepository = ubigeoRepository;
		}

		public async Task<IEnumerable<Ubigeo>> GetDptos()
		{
			try
			{
				return await _ubigeoRepository.GetDptos();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<IEnumerable<Ubigeo>> GetProvs(string DptoId = null)
		{
			try
			{
				return await _ubigeoRepository.GetProvs(DptoId);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public Task<IEnumerable<Ubigeo>> GetDists(string DptoId = null, string ProvId = null)
		{
			try
			{
				return _ubigeoRepository.GetDists(DptoId, ProvId);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public void Dispose()
		{
			_ubigeoRepository.Dispose();
			GC.SuppressFinalize(this);
		}
	}
}
