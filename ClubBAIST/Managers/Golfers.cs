using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ClubBaist.Models;
using Microsoft.Data.SqlClient;

namespace ClubBaist.Managers
{
    public class Golfers
    {
        public Golfer GetGolfer(int MemberNumber)
        {
            SqlConnection clubbaistConnection = new SqlConnection();
            clubbaistConnection.ConnectionString = Startup.ConnectionString;

            SqlCommand getGolfer = new SqlCommand();
            getGolfer.CommandType = CommandType.StoredProcedure;
            getGolfer.Connection = clubbaistConnection;
            getGolfer.CommandText = "GetGolfer";

            SqlParameter memberNumberParameter = new SqlParameter();
            memberNumberParameter.ParameterName = "@MemberNumber";
            memberNumberParameter.SqlDbType = SqlDbType.Int;
            memberNumberParameter.Value = MemberNumber;
            memberNumberParameter.Direction = ParameterDirection.Input;

            SqlParameter returnCodeParameter = new SqlParameter();
            returnCodeParameter.ParameterName = "@ReturnCode";
            returnCodeParameter.SqlDbType = SqlDbType.Int;
            returnCodeParameter.Direction = ParameterDirection.ReturnValue;

            getGolfer.Parameters.Add(memberNumberParameter);
            getGolfer.Parameters.Add(returnCodeParameter);

            Golfer golfer = new Golfer();

            try
            {
                clubbaistConnection.Open();
                SqlDataReader reader;

                reader = getGolfer.ExecuteReader();

                while (reader.Read())
                {
                    golfer.MemberNumber = MemberNumber;
                    golfer.MembershipLevel = (int) reader["MembershipLevel"];
                    golfer.Password = (string) reader["Password"];
                    golfer.FirstName = (string) reader["FirstName"];
                    golfer.LastName = (string) reader["LastName"];
                    golfer.Email = (string) reader["Email"];
                }

            }
            catch
            {
                
            }

            return golfer;

        }


    }
}
