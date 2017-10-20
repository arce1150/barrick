using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Barrick.Pasajes.BusinessEntity;
using Barrick.Pasajes.DataAccess.Contracts;
using System.Transactions;
using System.Data;

namespace Barrick.Pasajes.DataAccess
{
    public class PasajesTerrestresDa : IPasajesTerrestresDa
    {
        public int insertarPasajesTerrestres ( SqlConnection con, PasajesTerrestresBe pasaje, List<DetallePasajesBe> detallePasaje )
        {
            int filasAfectadas = 0;
            try
            {
                using (var tnxsInsert = new TransactionScope())
                {
                    int idRegistroOut = 0;
                    var oParam = new IDataParameter[25];
                    SqlCommand cmd = new SqlCommand("USP_INSERT_PASAJE", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oParam[0] = new SqlParameter("@idcc", SqlDbType.Int) { Value = (object)pasaje.Idcc ?? DBNull.Value, Direction = ParameterDirection.Input };
                    oParam[1] = new SqlParameter("@cuenta", SqlDbType.Int) { Value = (object)pasaje.Cuenta ?? DBNull.Value, Direction = ParameterDirection.Input };
                    oParam[2] = new SqlParameter("@job", SqlDbType.VarChar) { Value = (object)pasaje.Job ?? DBNull.Value, Direction = ParameterDirection.Input };
                    oParam[3] = new SqlParameter("@empresa", SqlDbType.Int) { Value = (object)pasaje.Empresa ?? DBNull.Value, Direction = ParameterDirection.Input };
                    oParam[4] = new SqlParameter("@origen", SqlDbType.VarChar) { Value = (object)pasaje.Origen ?? DBNull.Value, Direction = ParameterDirection.Input };
                    oParam[5] = new SqlParameter("@destino", SqlDbType.VarChar) { Value = (object)pasaje.Destino ?? DBNull.Value, Direction = ParameterDirection.Input };
                    oParam[6] = new SqlParameter("@fecsalida", SqlDbType.DateTime) { Value = (object)pasaje.FechaSalida ?? DBNull.Value, Direction = ParameterDirection.Input };
                    oParam[7] = new SqlParameter("@horsalida", SqlDbType.DateTime) { Value = (object)pasaje.HorSalida ?? DBNull.Value, Direction = ParameterDirection.Input };
                    oParam[8] = new SqlParameter("@fecretorno", SqlDbType.DateTime) { Value = (object)pasaje.FechaRetorno ?? DBNull.Value, Direction = ParameterDirection.Input };
                    oParam[9] = new SqlParameter("@horretorno", SqlDbType.DateTime) { Value = (object)pasaje.HorRetorno ?? DBNull.Value, Direction = ParameterDirection.Input };
                    oParam[10] = new SqlParameter("@motivo", SqlDbType.VarChar) { Value = (object)pasaje.Motivo ?? DBNull.Value, Direction = ParameterDirection.Input };
                    oParam[11] = new SqlParameter("@solicitante", SqlDbType.VarChar) { Value = (object)pasaje.Solicitante ?? DBNull.Value, Direction = ParameterDirection.Input };
                    oParam[12] = new SqlParameter("@mailsolicitante", SqlDbType.VarChar) { Value = (object)pasaje.MailSolicitante ?? DBNull.Value, Direction = ParameterDirection.Input };
                    oParam[13] = new SqlParameter("@codjefe", SqlDbType.VarChar) { Value = (object)pasaje.CodigoJefe ?? DBNull.Value, Direction = ParameterDirection.Input };
                    oParam[14] = new SqlParameter("@codcontabilidad", SqlDbType.VarChar) { Value = (object)pasaje.CodigoContabilidad ?? DBNull.Value, Direction = ParameterDirection.Input };
                    oParam[15] = new SqlParameter("@solicitante_login", SqlDbType.VarChar) { Value = (object)pasaje.SolicitanteLogin ?? DBNull.Value, Direction = ParameterDirection.Input };
                    oParam[16] = new SqlParameter("@solicitante_nombre", SqlDbType.VarChar) { Value = (object)pasaje.SolicitanteNombre ?? DBNull.Value, Direction = ParameterDirection.Input };
                    oParam[17] = new SqlParameter("@solicitante_email", SqlDbType.VarChar) { Value = (object)pasaje.SolicitanteEmail ?? DBNull.Value, Direction = ParameterDirection.Input };
                    oParam[18] = new SqlParameter("@aprobador_login", SqlDbType.VarChar) { Value = (object)pasaje.AprobadorLogin ?? DBNull.Value, Direction = ParameterDirection.Input };
                    oParam[19] = new SqlParameter("@aprobador_nombre", SqlDbType.VarChar) { Value = (object)pasaje.AprobadorNombre ?? DBNull.Value, Direction = ParameterDirection.Input };
                    oParam[20] = new SqlParameter("@aprobador_email", SqlDbType.VarChar) { Value = (object)pasaje.AprobadorEmail ?? DBNull.Value, Direction = ParameterDirection.Input };
                    oParam[21] = new SqlParameter("@aprobado", SqlDbType.Int) { Value = (object)pasaje.Aprobado ?? DBNull.Value, Direction = ParameterDirection.Input };
                    oParam[22] = new SqlParameter("@condicion", SqlDbType.Int) { Value = (object)pasaje.Condicion ?? DBNull.Value, Direction = ParameterDirection.Input };
                    oParam[23] = new SqlParameter("@nota", SqlDbType.Text) { Value = (object)pasaje.Nota ?? DBNull.Value, Direction = ParameterDirection.Input };
                    oParam[24] = new SqlParameter("@OutIdRegistro", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    cmd.Parameters.AddRange(oParam);
                    cmd.ExecuteNonQuery();

                    idRegistroOut = int.Parse(cmd.Parameters["@OutIdRegistro"].Value.ToString());
                    cmd = new SqlCommand("USP_PA_INSERT_DETALLE_PASAJE", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    var oParamDetalle = new IDataParameter[3];
                    foreach (var item in detallePasaje)
                    {
                        cmd.Parameters.Clear();
                        item.IdRegistro = idRegistroOut;
                        oParamDetalle[0] = new SqlParameter("@idregistroPasajero", SqlDbType.Int) { Value = (object)item.IdRegistro ?? DBNull.Value, Direction = ParameterDirection.Input };
                        oParamDetalle[1] = new SqlParameter("@nompasajero", SqlDbType.VarChar) { Value = (object)item.NomPasajero ?? DBNull.Value, Direction = ParameterDirection.Input };
                        oParamDetalle[2] = new SqlParameter("@docidentidad", SqlDbType.VarChar) { Value = (object)item.DocIdentidad ?? DBNull.Value, Direction = ParameterDirection.Input };
                        cmd.Parameters.AddRange(oParamDetalle);
                        cmd.ExecuteNonQuery();
                    }
                    tnxsInsert.Complete();
                    filasAfectadas = idRegistroOut;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return filasAfectadas;
        }
        public PasajesTerrestresBe listarPasajesTerrestres ( SqlConnection con, int IdRegistro )
        {
            PasajesTerrestresBe pasaje = null;
            List<DetallePasajesBe> lista = null;
            try
            {
                var oParam = new SqlParameter[1];
                SqlCommand cmd = new SqlCommand("USP_PA_LISTAR_PASAJES_PORIDREGISTRO", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                oParam[0] = new SqlParameter("@idregistro", SqlDbType.Int) { Value = (object)IdRegistro ?? DBNull.Value, Direction = ParameterDirection.Input };
                cmd.Parameters.AddRange(oParam);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr != null)
                {
                    int pRegistro = dr.GetOrdinal("registro");
                    int pFecha = dr.GetOrdinal("fecha");
                    int pCcosto = dr.GetOrdinal("ccosto");
                    int pcuenta = dr.GetOrdinal("cuenta");
                    int pjob = dr.GetOrdinal("job");
                    int pSeccion = dr.GetOrdinal("seccion");
                    int pArea = dr.GetOrdinal("area");
                    int pCodempresa = dr.GetOrdinal("codempresa");
                    int pNomempresa = dr.GetOrdinal("nomempresa");
                    int pOrigen = dr.GetOrdinal("origen");
                    int pDestino = dr.GetOrdinal("destino");
                    int pFecsalida = dr.GetOrdinal("fecsalida");
                    int pHorsalida = dr.GetOrdinal("horsalida");
                    int pFecretorno = dr.GetOrdinal("fecretorno");
                    int pHorretorno = dr.GetOrdinal("horretorno");
                    int pMotivo = dr.GetOrdinal("motivo");
                    int pSolicitante = dr.GetOrdinal("solicitante");
                    int pMailsolicitante = dr.GetOrdinal("mailsolicitante");
                    int pCodigojefe = dr.GetOrdinal("codigojefe");
                    int pLoginjefe = dr.GetOrdinal("loginjefe");
                    int pJefe = dr.GetOrdinal("jefe");
                    int pMailjefe = dr.GetOrdinal("mailjefe");
                    int pCodigoconta = dr.GetOrdinal("codigoconta");
                    int pLoginconta = dr.GetOrdinal("loginconta");
                    int pConta = dr.GetOrdinal("conta");
                    int pMailconta = dr.GetOrdinal("mailconta");
                    int pAprobado = dr.GetOrdinal("aprobado");
                    int pCondicion = dr.GetOrdinal("condicion");
                    int pNota = dr.GetOrdinal("nota");
                    pasaje = new PasajesTerrestresBe();
                    if (dr.Read())
                    {
                        pasaje.Idregistro = dr.GetValue(pRegistro) == DBNull.Value ? default(int) : dr.GetInt32(pRegistro);
                        pasaje.Fecha = dr.GetValue(pFecha) == DBNull.Value ? default(DateTime) : dr.GetDateTime(pFecha);
                        pasaje.Costo = dr.GetValue(pCcosto) == DBNull.Value ? default(int) : dr.GetInt32(pCcosto);
                        pasaje.Cuenta = dr.GetValue(pcuenta) == DBNull.Value ? default(int) : dr.GetInt32(pcuenta);
                        pasaje.Job = dr.GetValue(pjob) == DBNull.Value ? default(string) : dr.GetString(pjob);
                        pasaje.Seccion = dr.GetValue(pSeccion) == DBNull.Value ? default(string) : dr.GetString(pSeccion);
                        pasaje.Area = dr.GetValue(pArea) == DBNull.Value ? default(string) : dr.GetString(pArea);
                        pasaje.IdEmpresa = dr.GetValue(pCodempresa) == DBNull.Value ? default(int) : dr.GetInt32(pCodempresa);
                        pasaje.NombreEmpresa = dr.GetValue(pNomempresa) == DBNull.Value ? default(string) : dr.GetString(pNomempresa);
                        pasaje.Origen = dr.GetValue(pOrigen) == DBNull.Value ? default(string) : dr.GetString(pOrigen);
                        pasaje.Destino = dr.GetValue(pDestino) == DBNull.Value ? default(string) : dr.GetString(pDestino);
                        pasaje.FechaSalida = dr.GetValue(pFecsalida) == DBNull.Value ? default(DateTime) : dr.GetDateTime(pFecsalida);
                        pasaje.HorSalida = dr.GetValue(pHorsalida) == DBNull.Value ? default(DateTime) : dr.GetDateTime(pHorsalida);
                        pasaje.FechaRetorno = dr.GetValue(pFecretorno) == DBNull.Value ? default(DateTime) : dr.GetDateTime(pFecretorno);
                        pasaje.HorRetorno = dr.GetValue(pHorretorno) == DBNull.Value ? default(DateTime) : dr.GetDateTime(pHorretorno);
                        pasaje.Motivo = dr.GetValue(pMotivo) == DBNull.Value ? default(string) : dr.GetString(pMotivo);
                        pasaje.Solicitante = dr.GetValue(pSolicitante) == DBNull.Value ? default(string) : dr.GetString(pSolicitante);
                        pasaje.MailSolicitante = dr.GetValue(pMailsolicitante) == DBNull.Value ? default(string) : dr.GetString(pMailsolicitante);
                        pasaje.CodigoJefe = dr.GetValue(pCodigojefe) == DBNull.Value ? default(string) : dr.GetString(pCodigojefe);
                        pasaje.LoginJefe = dr.GetValue(pLoginjefe) == DBNull.Value ? default(string) : dr.GetString(pLoginjefe);
                        pasaje.Jefe = dr.GetValue(pJefe) == DBNull.Value ? default(string) : dr.GetString(pJefe);
                        pasaje.MailJefe = dr.GetValue(pMailjefe) == DBNull.Value ? default(string) : dr.GetString(pMailjefe);
                        pasaje.CodigoContabilidad = dr.GetValue(pCodigoconta) == DBNull.Value ? default(string) : dr.GetString(pCodigoconta);
                        pasaje.LoginConta = dr.GetValue(pLoginconta) == DBNull.Value ? default(string) : dr.GetString(pLoginconta);
                        pasaje.NombreConta = dr.GetValue(pConta) == DBNull.Value ? default(string) : dr.GetString(pConta);
                        pasaje.MailConta = dr.GetValue(pMailconta) == DBNull.Value ? default(string) : dr.GetString(pMailconta);
                        pasaje.Aprobado = dr.GetValue(pAprobado) == DBNull.Value ? default(int) : dr.GetInt32(pAprobado);
                        pasaje.Condicion = dr.GetValue(pCondicion) == DBNull.Value ? default(int) : dr.GetInt32(pCondicion);
                        pasaje.Nota = dr.GetValue(pNota) == DBNull.Value ? default(string) : dr.GetString(pNota);
                    }

                    if (dr.NextResult())
                    {
                        cmd.Parameters.Clear();
                        int pIdregistro = dr.GetOrdinal("idregistro");
                        int pIdpasajero = dr.GetOrdinal("idpasajero");
                        int pNompasajero = dr.GetOrdinal("nompasajero");
                        int pDocidentidad = dr.GetOrdinal("docidentidad");
                        DetallePasajesBe objDetalle = null;
                        lista = new List<DetallePasajesBe>();
                        while (dr.Read())
                        {
                            objDetalle = new DetallePasajesBe();
                            objDetalle.IdRegistro = dr.GetValue(pIdregistro) == DBNull.Value ? default(int) : dr.GetInt32(pIdregistro);
                            objDetalle.IdPasajero = dr.GetValue(pIdpasajero) == DBNull.Value ? default(int) : dr.GetInt32(pIdpasajero);
                            objDetalle.NomPasajero = dr.GetValue(pNompasajero) == DBNull.Value ? default(string) : dr.GetString(pNompasajero);
                            objDetalle.DocIdentidad = dr.GetValue(pDocidentidad) == DBNull.Value ? default(string) : dr.GetString(pDocidentidad);
                            lista.Add(objDetalle);
                        }
                        pasaje.detallePasaje = lista;
                    }
                    dr.Close();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return pasaje;
        }
        public List<PasajesTerrestresBe> paginarPasajesTerrestres ( SqlConnection con, bool condicion,string mailsolicitante )
        {

            List<PasajesTerrestresBe> lista = null;
            try
            {
                var oParam = new SqlParameter[2];
                SqlCommand cmd = new SqlCommand("USP_PA_LISTAR_PASAJES_TERRESTRES", con);
                cmd.CommandType = CommandType.StoredProcedure;
                oParam[0] = new SqlParameter("@conCorreo", SqlDbType.Bit) { Value = (object)condicion ?? DBNull.Value, Direction = ParameterDirection.Input };
                oParam[1] = new SqlParameter("@mailsolicitante", SqlDbType.VarChar) { Value = (object)mailsolicitante ?? DBNull.Value, Direction = ParameterDirection.Input };
                cmd.Parameters.AddRange(oParam);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr != null)
                {
                    int pIdRegistro = dr.GetOrdinal("idregistro");
                    int pFecha = dr.GetOrdinal("fecha");
                    int pIdcc = dr.GetOrdinal("idCc");
                    int pCuenta = dr.GetOrdinal("cuenta");
                    int pEmPresa = dr.GetOrdinal("nombreEmpresa");
                    int pOrigen = dr.GetOrdinal("origen");
                    int pDestino = dr.GetOrdinal("destino");
                    int pFecsalida = dr.GetOrdinal("fecsalida");
                    int pFecRetorno = dr.GetOrdinal("fecRetorno");
                    int pSolicitante = dr.GetOrdinal("solicitante");
                    int pAprobador = dr.GetOrdinal("nombreAprobador");
                    int pConta = dr.GetOrdinal("conta");
                    int pAprobacionArea = dr.GetOrdinal("aprobacionArea");
                    int pAprobacionConta = dr.GetOrdinal("aprobacionConta");
                    lista = new List<PasajesTerrestresBe>();
                    PasajesTerrestresBe objPasaje = null;
                    while (dr.Read())
                    {
                        objPasaje = new PasajesTerrestresBe();
                        objPasaje.Idregistro = dr.GetValue(pIdRegistro) == DBNull.Value ? default(int) : dr.GetInt32(pIdRegistro);
                        objPasaje.Fecha = dr.GetValue(pFecha) == DBNull.Value ? default(DateTime) : dr.GetDateTime(pFecha);
                        objPasaje.Idcc = dr.GetValue(pIdcc) == DBNull.Value ? default(int) : dr.GetInt32(pIdcc);
                        objPasaje.Cuenta = dr.GetValue(pCuenta) == DBNull.Value ? default(int) : dr.GetInt32(pCuenta);
                        objPasaje.NombreEmpresa = dr.GetValue(pEmPresa) == DBNull.Value ? default(string) : dr.GetString(pEmPresa);
                        objPasaje.Origen = dr.GetValue(pOrigen) == DBNull.Value ? default(string) : dr.GetString(pOrigen);
                        objPasaje.Destino = dr.GetValue(pDestino) == DBNull.Value ? default(string) : dr.GetString(pDestino);
                        objPasaje.FechaSalida = dr.GetValue(pFecsalida) == DBNull.Value ? default(DateTime) : dr.GetDateTime(pFecsalida);
                        objPasaje.FechaRetorno = dr.GetValue(pFecRetorno) == DBNull.Value ? default(DateTime) : dr.GetDateTime(pFecRetorno);
                        objPasaje.Solicitante = dr.GetValue(pSolicitante) == DBNull.Value ? default(string) : dr.GetString(pSolicitante);
                        objPasaje.AprobadorNombre = dr.GetValue(pAprobador) == DBNull.Value ? default(string) : dr.GetString(pAprobador);
                        objPasaje.NombreConta = dr.GetValue(pConta) == DBNull.Value ? default(string) : dr.GetString(pConta);
                        objPasaje.Area = dr.GetValue(pAprobacionArea) == DBNull.Value ? default(string) : dr.GetString(pAprobacionArea);
                        objPasaje.AprobacionConta = dr.GetValue(pAprobacionConta) == DBNull.Value ? default(string) : dr.GetString(pAprobacionConta);
                        lista.Add(objPasaje);
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
