using Sicv1.Domain.Contracts.Repositories;
using Sicv1.Domain.Entities;
using Sicv1.Infrastructure.Cross;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sicv1.Infrastructure.Repositories
{
	public class UploadsRepository : Connection, IUploadsRepository
	{
		public async Task Insert(Upload upload)
		{
			try
			{
				using (SqlConnection conn = new SqlConnection(SqlConnectionString))
				{
					conn.Open();
					SqlTransaction transaction = conn.BeginTransaction();

					SqlCommand cmd_upload = new SqlCommand("SP_UPLOADS_INSERT", conn, transaction);
					cmd_upload.CommandType = CommandType.StoredProcedure;
					cmd_upload.Parameters.AddWithValue("@CREATED_USER", upload.CREATED_USER);
					
					var id = await cmd_upload.ExecuteScalarAsync();
					upload.ID = Convert.ToInt32(id);
					if (upload.ID > 0)
					{
						for (int i = 0; i < upload.CODES.Count; i++)
						{
							upload.CODES[i].ID_UPLOAD = upload.ID;
							try
							{
								SqlCommand cmd_code = new SqlCommand("SP_CODES_INSERT", conn, transaction);
								cmd_code.CommandType = CommandType.StoredProcedure;
								cmd_code.Parameters.AddWithValue("@ID_UPLOAD", upload.CODES[i].ID_UPLOAD);
								cmd_code.Parameters.AddWithValue("@CODE", upload.CODES[i].CODE);
								int detail = await cmd_code.ExecuteNonQueryAsync();

								upload.CODES[i].STATUS = true;
								upload.CODES[i].MESSAGE = "Registrado Correctamente";
							}
							catch (Exception ex)
							{
								upload.CODES[i].STATUS = false;
								upload.CODES[i].MESSAGE = ex.Message;
								upload.CODES[i].TRACE = ex.StackTrace;
							}
						}
						transaction.Commit();
						upload.STATUS = true;
						upload.MESSAGE = "Registro Correcto";
					}
					else
					{
						transaction.Rollback();
						upload.STATUS = false;
						upload.MESSAGE = "Error al insertar la carga";
					}
				}
			}
			catch (Exception ex)
			{
				upload.STATUS = false;
				upload.MESSAGE = ex.Message;
				upload.TRACE = ex.StackTrace;
			}
		}

		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}
	}
}
