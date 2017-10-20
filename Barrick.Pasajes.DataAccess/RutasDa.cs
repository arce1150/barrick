using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Barrick.Pasajes.BusinessEntity;
using Barrick.Pasajes.DataAccess.Contracts;
namespace Barrick.Pasajes.DataAccess
{
    public class RutasDa : IRutasDa
    {
        
        public List<RutasBe> listarRutasPorEmpresas ( SqlConnection con, int idempresa )
        {
            List<RutasBe> lista = null;
            try
            {
                SqlCommand cmd = new SqlCommand("USP_PA_LISTAR_RUTA_ORIGEN", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                var parametros = new[]
                {
                    new SqlParameter("@empresa" , SqlDbType.Int) {Value =  (object)idempresa ?? DBNull.Value ,Direction = ParameterDirection.Input},
                };
                cmd.Parameters.AddRange(parametros);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleResult);
                if (dr != null)
                {
                    int pOrigen = dr.GetOrdinal("origen"); 
                    RutasBe objRutas = null;
                    lista = new List<RutasBe>();
                    while (dr.Read())
                    {
                        objRutas = new RutasBe();
                        objRutas.Origen = dr.GetValue(pOrigen) == DBNull.Value ? default(string) : dr.GetString(pOrigen); 
                        lista.Add(objRutas);
                    }
                    dr.Close();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<RutasBe> listarDestinos ( SqlConnection con, string origen, int idempresa )
        {
            List<RutasBe> lista = null;
            try
            {
                SqlCommand cmd = new SqlCommand("USP_PA_LISTAR_DESTINOS", con);
                cmd.CommandType = CommandType.StoredProcedure;
                var parametros = new[]
                {
                    new SqlParameter("@origen" , SqlDbType.VarChar) {Value =  (object)origen ?? DBNull.Value ,Direction = ParameterDirection.Input},
                    new SqlParameter("@idemp" , SqlDbType.Int) {Value =  (object)idempresa ?? DBNull.Value ,Direction = ParameterDirection.Input},
                };
                cmd.Parameters.AddRange(parametros);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleResult);
                if (dr != null)
                {
                    int pDestino = dr.GetOrdinal("destino");
                    RutasBe objRutas = null;
                    lista = new List<RutasBe>();
                    while (dr.Read())
                    {
                        objRutas = new RutasBe();
                        objRutas.Destino = dr.GetValue(pDestino) == DBNull.Value ? default(string) : dr.GetString(pDestino);
                        lista.Add(objRutas);
                    }
                    dr.Close();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return lista;
        }

        public List<RutasBe> listarHorarioDeSalida ( SqlConnection con, string origen, string destino, int idempresa )
        {
            List<RutasBe> lista = null;
            try
            {
                SqlCommand cmd = new SqlCommand("USP_PA_HORASALIDARETORNO", con);
                cmd.CommandType = CommandType.StoredProcedure;
                var parametros = new[]
                {
                    new SqlParameter("@origen" , SqlDbType.VarChar) {Value =  (object)origen ?? DBNull.Value ,Direction = ParameterDirection.Input},
                    new SqlParameter("@destino" , SqlDbType.VarChar) {Value =  (object)destino ?? DBNull.Value ,Direction = ParameterDirection.Input},
                    new SqlParameter("@empresa" , SqlDbType.Int) {Value =  (object)idempresa ?? DBNull.Value ,Direction = ParameterDirection.Input},
                };
                cmd.Parameters.AddRange(parametros);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleResult);
                if (dr != null)
                {
                    int pHoraStringValor = dr.GetOrdinal("horaStringValor");
                    int pHoraString = dr.GetOrdinal("HoraString");
                    int pNota = dr.GetOrdinal("nota");
                    RutasBe objRutas = null;
                    lista = new List<RutasBe>();
                    while (dr.Read())
                    {
                        objRutas = new RutasBe();
                        objRutas.HoraStringValor = dr.GetValue(pHoraStringValor) == DBNull.Value ? default(string) : dr.GetString(pHoraStringValor);
                        objRutas.HoraString = dr.GetValue(pHoraString) == DBNull.Value ? default(string) : dr.GetString(pHoraString);
                        objRutas.Nota = dr.GetValue(pNota) == DBNull.Value ? default(string) : dr.GetString(pNota);
                        lista.Add(objRutas);
                    }
                    dr.Close();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return lista;
        }
    }
}
