using System;
using System.Data;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace JWTDemo
{
    public class TokenDataHandler
    {
        private bool response;

        private readonly IConfiguration config;

        public TokenDataHandler(IConfiguration config)
        {
            this.config = config;
        }

        /// <summary>
        /// Verifies the refresh token.
        /// </summary>
        /// <returns><c>true</c>, if refresh token was verifyed, <c>false</c> otherwise.</returns>
        /// <param name="userId">User identifier.</param>
        /// <param name="token">Token.</param>
        public bool VerifyRefreshToken(string userId, string token)
        {
            response = false;

            //string connectionString = config.GetConnectionString("DBConnectionString");
            //MySqlConnection conn = new MySqlConnection(connectionString);
            //MySqlCommand mySqlCommand = new MySqlCommand();

            //RefreshToken refreshToken = new RefreshToken();

            //try
            //{
            //    conn.Open();    //opening DB connection
            //    mySqlCommand.Connection = conn;

            //    mySqlCommand.CommandText = "RetrieveToken";
            //    mySqlCommand.CommandType = CommandType.StoredProcedure;

            //    mySqlCommand.Parameters.Add(new MySqlParameter("_EmailAddress", userId));
            //    mySqlCommand.Parameters.Add(new MySqlParameter("_Token", token));
            //    mySqlCommand.Parameters.Add(new MySqlParameter("_Response", 0));
            //    mySqlCommand.Parameters["_Response"].Direction = ParameterDirection.Output;

            //    mySqlCommand.ExecuteNonQuery();

            //    var result = mySqlCommand.Parameters["_Response"].Value;

            //    //if result is 1, it means token matched and session is valid 
            //    if (Convert.ToInt32(result) == 1)
            //    {
            //        response = true;
            //    }

            //}
            //catch (Exception ex)
            //{
            //    //Log exception
            //    LogHandler.LogError("TokenDataHandler.VerifyRefreshToken()", ex.Message);
            //    refreshToken = null;
            //    return response;
            //}
            //finally
            //{
            //    conn.Close();           //closing DB connection
            //}


            response = true;    //hardcoded .... this should be true/false based on success/failure in DB retrieve


            return response;
        }


        /// <summary>
        /// Updates the token.
        /// </summary>
        /// <returns><c>true</c>, if token was updated, <c>false</c> otherwise.</returns>
        /// <param name="refreshToken">Refresh token.</param>
        public bool UpdateToken(RefreshToken refreshToken)
        {
            response = false;   //default value

            //string connectionString = config.GetConnectionString("DBConnectionString");
            //MySqlConnection conn = new MySqlConnection(connectionString);
            //MySqlCommand mySqlCommand = new MySqlCommand();

            //try
            //{
            //    conn.Open();    //opening DB connection
            //    mySqlCommand.Connection = conn;

            //    mySqlCommand.CommandText = "UpdateToken";
            //    mySqlCommand.CommandType = CommandType.StoredProcedure;

            //    mySqlCommand.Parameters.Add(new MySqlParameter("_EmailAddress", refreshToken.UserName));
            //    mySqlCommand.Parameters.Add(new MySqlParameter("_Id", refreshToken.Id));
            //    mySqlCommand.Parameters.Add(new MySqlParameter("_Token", refreshToken.Token));
            //    mySqlCommand.Parameters.Add(new MySqlParameter("_IsStop", refreshToken.IsStop));
            //    mySqlCommand.Parameters.Add(new MySqlParameter("_ClientId", refreshToken.ClientId));
            //    mySqlCommand.Parameters.Add(new MySqlParameter("_Response", 0));
            //    mySqlCommand.Parameters["_Response"].Direction = ParameterDirection.Output;

            //    mySqlCommand.ExecuteNonQuery();

            //    var result = mySqlCommand.Parameters["_Response"].Value;

            //    //if result is 1, it means stored procedure ran successfully without any error 
            //    if (Convert.ToInt32(result) == 1)
            //    {
            //        response = true;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    //Log exception
            //    LogHandler.LogError("VendorDataHandler.UpdateVendor()", ex.Message);
            //    return response;
            //}
            //finally
            //{
            //    conn.Close();           //closing DB connection
            //}


            response = true;    //hardcoded .... this should be true/false based on success/failure in DB save
            return response;
        }

    }
}
