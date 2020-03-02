using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ClubBaist.CustomAtrritutes;
using ClubBaist.Models;
using Microsoft.Data.SqlClient;

namespace ClubBaist.Managers
{
    public class StandingTeeTimes
    {
        public bool CreateStandingTeeTimeRequest(StandingTeeTime request)
        {
            bool Success = false;

            SqlConnection clubbaistConnection = new SqlConnection();
            clubbaistConnection.ConnectionString = Startup.ConnectionString;

            SqlCommand CreateStandingTeeTimeRequestCommand = new SqlCommand();
            CreateStandingTeeTimeRequestCommand.CommandType = CommandType.StoredProcedure;
            CreateStandingTeeTimeRequestCommand.Connection = clubbaistConnection;
            CreateStandingTeeTimeRequestCommand.CommandText = "CreateStandingTeeTimeRequest";

            SqlParameter memberNumber1Parameter = new SqlParameter();
            memberNumber1Parameter.ParameterName = "@MemberNumber1";
            memberNumber1Parameter.SqlDbType = SqlDbType.Int;
            memberNumber1Parameter.Value = request.MemberNumber1;

            SqlParameter memberNumber2Parameter = new SqlParameter();
            memberNumber2Parameter.ParameterName = "@MemberNumber2";
            memberNumber2Parameter.SqlDbType = SqlDbType.Int;
            memberNumber2Parameter.Value = request.MemberNumber2;

            SqlParameter memberNumber3Parameter = new SqlParameter();
            memberNumber3Parameter.ParameterName = "@MemberNumber3";
            memberNumber3Parameter.SqlDbType = SqlDbType.Int;
            memberNumber3Parameter.Value = request.MemberNumber3;

            SqlParameter memberNumber4Parameter = new SqlParameter();
            memberNumber4Parameter.ParameterName = "@MemberNumber4";
            memberNumber4Parameter.SqlDbType = SqlDbType.Int;
            memberNumber4Parameter.Value = request.MemberNumber4;

            SqlParameter memberName1Parameter = new SqlParameter();
            memberName1Parameter.ParameterName = "@MemberName1";
            memberName1Parameter.SqlDbType = SqlDbType.NVarChar;
            memberName1Parameter.Value = request.MemberName1;

            SqlParameter memberName2Parameter = new SqlParameter();
            memberName2Parameter.ParameterName = "@MemberName2";
            memberName2Parameter.SqlDbType = SqlDbType.NVarChar;
            memberName2Parameter.Value = request.MemberName2;

            SqlParameter memberName3Parameter = new SqlParameter();
            memberName3Parameter.ParameterName = "@MemberName3";
            memberName3Parameter.SqlDbType = SqlDbType.NVarChar;
            memberName3Parameter.Value = request.MemberName3;

            SqlParameter memberName4Parameter = new SqlParameter();
            memberName4Parameter.ParameterName = "@MemberName4";
            memberName4Parameter.SqlDbType = SqlDbType.NVarChar;
            memberName4Parameter.Value = request.MemberName4;

            SqlParameter dayOfWeekParameter = new SqlParameter();
            dayOfWeekParameter.ParameterName = "@DayOfWeek";
            dayOfWeekParameter.SqlDbType = SqlDbType.DateTime;
            dayOfWeekParameter.Value = request.DayOfWeek;

            SqlParameter timeParameter = new SqlParameter();
            timeParameter.ParameterName = "@Time";
            timeParameter.SqlDbType = SqlDbType.Time;
            timeParameter.Value = request.Time;

            SqlParameter startDateParameter = new SqlParameter();
            startDateParameter.ParameterName = "@StartDate";
            startDateParameter.SqlDbType = SqlDbType.DateTime;
            startDateParameter.Value = request.StartDate;

            SqlParameter endDateParameter = new SqlParameter();
            endDateParameter.ParameterName = "@EndDate";
            endDateParameter.SqlDbType = SqlDbType.DateTime;
            endDateParameter.Value = request.EndDate;

            SqlParameter ReturnCodeParameter = new SqlParameter();
            ReturnCodeParameter.ParameterName = "@ReturnCode";
            ReturnCodeParameter.SqlDbType = SqlDbType.Int;
            ReturnCodeParameter.Direction = ParameterDirection.ReturnValue;

            CreateStandingTeeTimeRequestCommand.Parameters.Add(memberNumber1Parameter);
            CreateStandingTeeTimeRequestCommand.Parameters.Add(memberNumber2Parameter);
            CreateStandingTeeTimeRequestCommand.Parameters.Add(memberNumber3Parameter);
            CreateStandingTeeTimeRequestCommand.Parameters.Add(memberNumber4Parameter);
            CreateStandingTeeTimeRequestCommand.Parameters.Add(memberName1Parameter);
            CreateStandingTeeTimeRequestCommand.Parameters.Add(memberName2Parameter);
            CreateStandingTeeTimeRequestCommand.Parameters.Add(memberName3Parameter);
            CreateStandingTeeTimeRequestCommand.Parameters.Add(memberName4Parameter);
            CreateStandingTeeTimeRequestCommand.Parameters.Add(dayOfWeekParameter);
            CreateStandingTeeTimeRequestCommand.Parameters.Add(timeParameter);
            CreateStandingTeeTimeRequestCommand.Parameters.Add(startDateParameter);
            CreateStandingTeeTimeRequestCommand.Parameters.Add(endDateParameter);
            CreateStandingTeeTimeRequestCommand.Parameters.Add(ReturnCodeParameter);

            try
            {
                clubbaistConnection.Open();
                CreateStandingTeeTimeRequestCommand.ExecuteNonQuery();

                int returnValue = (int) ReturnCodeParameter.Value;
                if (returnValue == 0)
                {
                    Success = true;
                }

                clubbaistConnection.Close();
            }
            catch
            {
                Success = false;
            }


            return Success;
        }

        public StandingTeeTime FindStandingTeeTimeRequest(int MemberNumber)
        {
            SqlConnection clubbaistConnection = new SqlConnection();
            clubbaistConnection.ConnectionString = Startup.ConnectionString;

            SqlCommand FindStandingTeeTimeRequestCommand = new SqlCommand();
            FindStandingTeeTimeRequestCommand.CommandType = CommandType.StoredProcedure;
            FindStandingTeeTimeRequestCommand.Connection = clubbaistConnection;
            FindStandingTeeTimeRequestCommand.CommandText = "FindStandingTeeTimeRequest";

            SqlParameter memberNumberParameter = new SqlParameter();
            memberNumberParameter.ParameterName = "@MemberNumber1";
            memberNumberParameter.SqlDbType = SqlDbType.Int;
            memberNumberParameter.Value = MemberNumber;

            SqlParameter ReturnCodeParameter = new SqlParameter();
            ReturnCodeParameter.ParameterName = "@ReturnCode";
            ReturnCodeParameter.SqlDbType = SqlDbType.Int;
            ReturnCodeParameter.Direction = ParameterDirection.ReturnValue;

            FindStandingTeeTimeRequestCommand.Parameters.Add(memberNumberParameter);
            FindStandingTeeTimeRequestCommand.Parameters.Add(ReturnCodeParameter);

            StandingTeeTime Request = new StandingTeeTime();

            try
            {
                clubbaistConnection.Open();

                SqlDataReader reader;

                reader = FindStandingTeeTimeRequestCommand.ExecuteReader();

                while (reader.Read())
                {
                    Request.StandingTeeTimeID = (int) reader["StandingTeeTimeID"];
                    Request.DayOfWeek = (DateTime) reader["DayOfWeek"];
                    Request.EndDate = (DateTime) reader["EndDate"];
                    Request.MemberName1 = reader["MemberName1"].ToString();
                    Request.MemberName2 = reader["MemberName2"].ToString();
                    Request.MemberName3 = reader["MemberName3"].ToString();
                    Request.MemberName4 = reader["MemberName4"].ToString();
                    Request.MemberNumber1 = (int) reader["MemberNumber1"];
                    Request.MemberNumber2 = (int) reader["MemberNumber2"];
                    Request.MemberNumber3 = (int) reader["MemberNumber3"];
                    Request.MemberNumber4 = (int) reader["MemberNumber4"];
                    Request.StartDate = (DateTime) reader["StartDate"];
                    Request.EndDate = (DateTime) reader["EndDate"];
                    Request.Time = (TimeSpan) reader["Time"];

                }

                clubbaistConnection.Close();
            }
            catch
            {

            }

            return Request;
        }

        public bool CancelStandingTeeTimeRequest(int StandingTeeTimeID)
        {
            bool Success = false;
            SqlConnection clubbaistConnection = new SqlConnection();
            clubbaistConnection.ConnectionString = Startup.ConnectionString;

            SqlCommand CancelStandingTeeTimeRequestCommand = new SqlCommand();
            CancelStandingTeeTimeRequestCommand.CommandType = CommandType.StoredProcedure;
            CancelStandingTeeTimeRequestCommand.Connection = clubbaistConnection;
            CancelStandingTeeTimeRequestCommand.CommandText = "CancelStandingTeeTimeRequest";

            SqlParameter StandingTeeTimeIDParameter = new SqlParameter();
            StandingTeeTimeIDParameter.ParameterName = "@StandingTeeTimeID";
            StandingTeeTimeIDParameter.SqlDbType = SqlDbType.Int;
            StandingTeeTimeIDParameter.Value = StandingTeeTimeID;
            StandingTeeTimeIDParameter.Direction = ParameterDirection.Input;


            SqlParameter ReturnCodeParameter = new SqlParameter();
            ReturnCodeParameter.ParameterName = "@ReturnCode";
            ReturnCodeParameter.SqlDbType = SqlDbType.Int;
            ReturnCodeParameter.Direction = ParameterDirection.ReturnValue;

            CancelStandingTeeTimeRequestCommand.Parameters.Add(StandingTeeTimeIDParameter);
            CancelStandingTeeTimeRequestCommand.Parameters.Add(ReturnCodeParameter);

            try
            {
                clubbaistConnection.Open();
                CancelStandingTeeTimeRequestCommand.ExecuteNonQuery();

                int returnValue = (int) ReturnCodeParameter.Value;
                if (returnValue == 0)
                {
                    Success = true;
                }

                clubbaistConnection.Close();

            }
            catch
            {

            }

            return Success;
        }
    }
}
