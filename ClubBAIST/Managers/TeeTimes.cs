using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ClubBaist.Models;
using Microsoft.Data.SqlClient;

namespace ClubBaist.Managers
{
    public class TeeTimes
    {
        public DailyTeeSheet FindDailyTeeSheet(DateTime date)
        {



            SqlConnection clubbaistConnection = new SqlConnection();
            clubbaistConnection.ConnectionString = @"Server=(localdb)\MSSQLLocalDB;" + "Integrated Security = true; Database = CLUBBAIST";

            SqlCommand findDailyTeeSheetCommand = new SqlCommand();
            findDailyTeeSheetCommand.CommandType = CommandType.StoredProcedure;
            findDailyTeeSheetCommand.Connection = clubbaistConnection;
            findDailyTeeSheetCommand.CommandText = "FindDailyTeeSheet";

            SqlParameter dailyTeeSheetdate = new SqlParameter();
            dailyTeeSheetdate.ParameterName = "@Date";
            dailyTeeSheetdate.SqlDbType = SqlDbType.Date;
            dailyTeeSheetdate.Value = date;
            dailyTeeSheetdate.Direction = ParameterDirection.Input;

            SqlParameter returnCodeParameter = new SqlParameter();
            returnCodeParameter.ParameterName = "@ReturnCode";
            returnCodeParameter.SqlDbType = SqlDbType.Int;
            returnCodeParameter.Direction = ParameterDirection.ReturnValue;

            findDailyTeeSheetCommand.Parameters.Add(returnCodeParameter);
            findDailyTeeSheetCommand.Parameters.Add(dailyTeeSheetdate);

            DailyTeeSheet dailyTeeSheet = new DailyTeeSheet();
            List<TeeTime> teeTimes = new List<TeeTime>();

            try
            {
                clubbaistConnection.Open();

                SqlDataReader reader;

                reader = findDailyTeeSheetCommand.ExecuteReader();

                var time = TimeSpan.FromHours(7);
                for (var i = 0; i < 97; i++)
                {
                    var teeTime = new TeeTime();
                    teeTime.Date = date;
                    teeTime.Time = time;
                    teeTime.Golfer1 = null;
                    teeTime.Golfer2 = null;
                    teeTime.Golfer3 = null;
                    teeTime.Golfer4 = null;
                    teeTimes.Add(teeTime);
                    if (i % 2 == 0)
                        time += TimeSpan.FromMinutes(7);
                    else
                        time += TimeSpan.FromMinutes(8);
                }


                while (reader.Read())
                    for (var i = 0; i < teeTimes.Count; i++)
                        if (teeTimes[i].Time.Equals(reader["Time"]))
                        {
                            teeTimes[i].Date = (DateTime) reader["Date"];
                            teeTimes[i].Time = (TimeSpan) reader["Time"];
                            teeTimes[i].Golfer1 = reader["Golfer1"] == DBNull.Value
                                ? string.Empty
                                : reader["Golfer1"].ToString();
                            teeTimes[i].Golfer2 = reader["Golfer2"] == DBNull.Value
                                ? string.Empty
                                : reader["Golfer2"].ToString();
                            teeTimes[i].Golfer3 = reader["Golfer3"] == DBNull.Value
                                ? string.Empty
                                : reader["Golfer3"].ToString();
                            teeTimes[i].Golfer4 = reader["Golfer4"] == DBNull.Value
                                ? string.Empty
                                : reader["Golfer4"].ToString();
                        }

                dailyTeeSheet.TeeTimes = teeTimes;
                dailyTeeSheet.Date = date;
                
                clubbaistConnection.Close();
            }
            catch
            {
                
            }



            return dailyTeeSheet;
        }

        public bool CreateTeeTime(TeeTime selectedTeeTime)
        {
            bool Success = false;
            SqlConnection clubbaistConnection = new SqlConnection();
            clubbaistConnection.ConnectionString = @"Server=(localdb)\MSSQLLocalDB;" + "Integrated Security = true; Database = CLUBBAIST";

            SqlCommand CreateTeeTimeCommand = new SqlCommand();
            CreateTeeTimeCommand.CommandType = CommandType.StoredProcedure;
            CreateTeeTimeCommand.Connection = clubbaistConnection;
            CreateTeeTimeCommand.CommandText = "CreateTeeTime";

            SqlParameter dateParameter = new SqlParameter();
            dateParameter.ParameterName = "@Date";
            dateParameter.SqlDbType = SqlDbType.Date;
            dateParameter.Value = selectedTeeTime.Date;
            dateParameter.Direction = ParameterDirection.Input;

            SqlParameter timeParameter = new SqlParameter();
            timeParameter.ParameterName = "@Time";
            timeParameter.SqlDbType = SqlDbType.Time;
            timeParameter.Value = selectedTeeTime.Time;
            timeParameter.Direction = ParameterDirection.Input;

            SqlParameter golfer1Parameter = new SqlParameter();
            golfer1Parameter.ParameterName = "@Golfer1";
            golfer1Parameter.SqlDbType = SqlDbType.NVarChar;
            golfer1Parameter.Value = selectedTeeTime.Golfer1;
            golfer1Parameter.Direction = ParameterDirection.Input;

            SqlParameter golfer2Parameter = new SqlParameter();
            golfer2Parameter.ParameterName = "@Golfer2";
            golfer2Parameter.SqlDbType = SqlDbType.NVarChar;
            golfer2Parameter.Value = selectedTeeTime.Golfer2;
            golfer2Parameter.Direction = ParameterDirection.Input;

            SqlParameter golfer3Parameter = new SqlParameter();
            golfer3Parameter.ParameterName = "@Golfer3";
            golfer3Parameter.SqlDbType = SqlDbType.NVarChar;
            golfer3Parameter.Value = selectedTeeTime.Golfer3;
            golfer3Parameter.Direction = ParameterDirection.Input;

            SqlParameter golfer4Parameter = new SqlParameter();
            golfer4Parameter.ParameterName = "@Golfer4";
            golfer4Parameter.SqlDbType = SqlDbType.NVarChar;
            golfer4Parameter.Value = selectedTeeTime.Golfer4;
            golfer4Parameter.Direction = ParameterDirection.Input;

            SqlParameter ReturnCodeParameter = new SqlParameter();
            ReturnCodeParameter.ParameterName = "@ReturnCode";
            ReturnCodeParameter.SqlDbType = SqlDbType.Int;
            ReturnCodeParameter.Direction = ParameterDirection.ReturnValue;

            CreateTeeTimeCommand.Parameters.Add(dateParameter);
            CreateTeeTimeCommand.Parameters.Add(timeParameter);
            CreateTeeTimeCommand.Parameters.Add(golfer1Parameter);
            CreateTeeTimeCommand.Parameters.Add(golfer2Parameter);
            CreateTeeTimeCommand.Parameters.Add(golfer3Parameter);
            CreateTeeTimeCommand.Parameters.Add(golfer4Parameter);
            CreateTeeTimeCommand.Parameters.Add(ReturnCodeParameter);

            try
            {
                clubbaistConnection.Open();
                CreateTeeTimeCommand.ExecuteNonQuery();

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
    }
}
