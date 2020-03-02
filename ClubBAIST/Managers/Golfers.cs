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
        public Golfer ViewMemberAccount(int MemberNumber)
        {
            SqlConnection clubbaistConnection = new SqlConnection();
            clubbaistConnection.ConnectionString = Startup.ConnectionString;

            SqlCommand getGolfer = new SqlCommand();
            getGolfer.CommandType = CommandType.StoredProcedure;
            getGolfer.Connection = clubbaistConnection;
            getGolfer.CommandText = "ViewMemberAccount";

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
                    golfer.MembershipLevel = (int)reader["MembershipLevel"];
                    golfer.Password = (string)reader["Password"];
                    golfer.FirstName = (string)reader["FirstName"];
                    golfer.LastName = (string)reader["LastName"];
                    golfer.Email = (string)reader["Email"];
                    golfer.Address = (string)reader["Address"];
                    golfer.PostalCode = (string)reader["PostalCode"];
                    golfer.Phone = (string)reader["Phone"];
                    golfer.AltPhone = (string)reader["AltPhone"];
                    golfer.DateOfBirth = (DateTime)reader["DateOfBirth"];
                    golfer.Occupation = (string)reader["Occupation"];
                    golfer.CompanyName = (string)reader["CompanyName"];
                    golfer.CompanyAddress = (string)reader["CompanyAddress"];
                    golfer.CompanyPostalCode = (string)reader["CompanyPostalCode"];
                    golfer.CompanyPhone = (string)reader["CompanyName"];
                    golfer.MembershipStartDate = (DateTime)reader["MembershipStartDate"];
                    golfer.Sponser1 = (int)reader["Sponser1"];
                    golfer.Sponser2 = (int)reader["Sponser2"];
                    golfer.Shareholder = (bool)reader["Shareholder"];
                    golfer.Approved = (string)reader["Approved"];
                }

                reader.NextResult();
                while (reader.Read())
                {
                    MemberAccountEntries entry = new MemberAccountEntries();
                    entry.PaymentAmount = (decimal)reader["PaymentAmount"];
                    entry.PaymentDate = (DateTime) reader["PaymentDate"];
                    entry.PaymentDescription = reader["PaymentDescription"].ToString();
                    golfer.AccountEntries.Add(entry);

                }

            }
            catch
            {
                
            }

            return golfer;

        }
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
                    golfer.MembershipLevel = (int)reader["MembershipLevel"];
                    golfer.Password = (string)reader["Password"];
                    golfer.FirstName = (string)reader["FirstName"];
                    golfer.LastName = (string)reader["LastName"];
                    golfer.Email = (string)reader["Email"];
                    golfer.Address = (string) reader["Address"];
                    golfer.PostalCode = (string) reader["PostalCode"];
                    golfer.Phone = (string) reader["Phone"];
                    golfer.AltPhone = (string) reader["AltPhone"];
                    golfer.DateOfBirth = (DateTime) reader["DateOfBirth"];
                    golfer.Occupation = (string) reader["Occupation"];
                    golfer.CompanyName = (string) reader["CompanyName"];
                    golfer.CompanyAddress = (string) reader["CompanyAddress"];
                    golfer.CompanyPostalCode = (string) reader["CompanyPostalCode"];
                    golfer.CompanyPhone = (string) reader["CompanyName"];
                    golfer.MembershipStartDate = (DateTime)reader["MembershipStartDate"];
                    golfer.Sponser1 = (int) reader["Sponser1"];
                    golfer.Sponser2 = (int) reader["Sponser2"];
                    golfer.Shareholder = (bool)reader["Shareholder"];
                    golfer.Approved = (string)reader["Approved"];
                }

            }
            catch
            {

            }

            return golfer;

        }

        public List<Golfer> GetGolfers()
        {
            List<Golfer> golfers = new List<Golfer>();
            SqlConnection clubbaistConnection = new SqlConnection();
            clubbaistConnection.ConnectionString = Startup.ConnectionString;

            SqlCommand GetGolfersCommand = new SqlCommand();
            GetGolfersCommand.CommandType = CommandType.StoredProcedure;
            GetGolfersCommand.Connection = clubbaistConnection;
            GetGolfersCommand.CommandText = "GetGolfers";

            SqlParameter ReturnCodeParameter = new SqlParameter();
            ReturnCodeParameter.ParameterName = "@ReturnCode";
            ReturnCodeParameter.SqlDbType = SqlDbType.Int;
            ReturnCodeParameter.Direction = ParameterDirection.ReturnValue;

            GetGolfersCommand.Parameters.Add(ReturnCodeParameter);

            try
            {
                clubbaistConnection.Open();
                SqlDataReader reader;
                reader = GetGolfersCommand.ExecuteReader();
                while (reader.Read())
                {
                    Golfer golfer = new Golfer();
                    golfer.MemberNumber = (int) reader["MemberNumber"];
                    golfer.FirstName = reader["FirstName"].ToString();
                    golfer.LastName = reader["LastName"].ToString();
                    golfer.Approved = reader["Approved"].ToString();
                    golfer.Shareholder = (bool) reader["Shareholder"];
                    golfers.Add(golfer);

                }

                clubbaistConnection.Close();
            }
            catch
            {

            }

            return golfers;
        }

        public bool RecordMembershipApplication(Golfer golfer)
        {
            bool Success = false;
            SqlConnection clubbaistConnection = new SqlConnection();
            clubbaistConnection.ConnectionString = Startup.ConnectionString;

            SqlCommand RecordMembershipApplicationCommand = new SqlCommand();
            RecordMembershipApplicationCommand.CommandType = CommandType.StoredProcedure;
            RecordMembershipApplicationCommand.Connection = clubbaistConnection;
            RecordMembershipApplicationCommand.CommandText = "RecordMembershipApplication";

            SqlParameter MembershipLevelParameter = new SqlParameter();
            MembershipLevelParameter.ParameterName = "@MembershipLevel";
            MembershipLevelParameter.SqlDbType = SqlDbType.Int;
            MembershipLevelParameter.Value = golfer.MembershipLevel;
            MembershipLevelParameter.Direction = ParameterDirection.Input;

            SqlParameter FirstNameParameter = new SqlParameter();
            FirstNameParameter.ParameterName = "@FirstName";
            FirstNameParameter.SqlDbType = SqlDbType.VarChar;
            FirstNameParameter.Value = golfer.FirstName;
            FirstNameParameter.Direction = ParameterDirection.Input;

            SqlParameter LastNameParameter = new SqlParameter();
            LastNameParameter.ParameterName = "@LastName";
            LastNameParameter.SqlDbType = SqlDbType.VarChar;
            LastNameParameter.Value = golfer.LastName;
            LastNameParameter.Direction = ParameterDirection.Input;

            SqlParameter PasswordParameter = new SqlParameter();
            PasswordParameter.ParameterName = "@Password";
            PasswordParameter.SqlDbType = SqlDbType.VarChar;
            PasswordParameter.Value = golfer.Password;
            PasswordParameter.Direction = ParameterDirection.Input;

            SqlParameter AddressParameter = new SqlParameter();
            AddressParameter.ParameterName = "@Address";
            AddressParameter.SqlDbType = SqlDbType.VarChar;
            AddressParameter.Value = golfer.Address;
            AddressParameter.Direction = ParameterDirection.Input;

            SqlParameter PostalCodeParameter = new SqlParameter();
            PostalCodeParameter.ParameterName = "@PostalCode";
            PostalCodeParameter.SqlDbType = SqlDbType.VarChar;
            PostalCodeParameter.Value = golfer.PostalCode;
            PostalCodeParameter.Direction = ParameterDirection.Input;

            SqlParameter PhoneParameter = new SqlParameter();
            PhoneParameter.ParameterName = "@Phone";
            PhoneParameter.SqlDbType = SqlDbType.VarChar;
            PhoneParameter.Value = golfer.Phone;
            PhoneParameter.Direction = ParameterDirection.Input;

            SqlParameter AltPhoneParameter = new SqlParameter();
            AltPhoneParameter.ParameterName = "@AltPhone";
            AltPhoneParameter.SqlDbType = SqlDbType.VarChar;
            AltPhoneParameter.Value = golfer.AltPhone;
            AltPhoneParameter.Direction = ParameterDirection.Input;

            SqlParameter EmailParameter = new SqlParameter();
            EmailParameter.ParameterName = "@Email";
            EmailParameter.SqlDbType = SqlDbType.VarChar;
            EmailParameter.Value = golfer.Email;
            EmailParameter.Direction = ParameterDirection.Input;

            SqlParameter DateOfBirthParameter = new SqlParameter();
            DateOfBirthParameter.ParameterName = "@DateOfBirth";
            DateOfBirthParameter.SqlDbType = SqlDbType.VarChar;
            DateOfBirthParameter.Value = golfer.DateOfBirth;
            DateOfBirthParameter.Direction = ParameterDirection.Input;

            SqlParameter OccupationParameter = new SqlParameter();
            OccupationParameter.ParameterName = "@Occupation";
            OccupationParameter.SqlDbType = SqlDbType.VarChar;
            OccupationParameter.Value = golfer.Occupation;
            OccupationParameter.Direction = ParameterDirection.Input;

            SqlParameter CompanyNameParameter = new SqlParameter();
            CompanyNameParameter.ParameterName = "@CompanyName";
            CompanyNameParameter.SqlDbType = SqlDbType.VarChar;
            CompanyNameParameter.Value = golfer.CompanyName;
            CompanyNameParameter.Direction = ParameterDirection.Input;

            SqlParameter CompanyAddressParameter = new SqlParameter();
            CompanyAddressParameter.ParameterName = "@CompanyAddress";
            CompanyAddressParameter.SqlDbType = SqlDbType.VarChar;
            CompanyAddressParameter.Value = golfer.CompanyAddress;
            CompanyAddressParameter.Direction = ParameterDirection.Input;

            SqlParameter CompanyPostalCodeParameter = new SqlParameter();
            CompanyPostalCodeParameter.ParameterName = "@CompanyPostalCode";
            CompanyPostalCodeParameter.SqlDbType = SqlDbType.VarChar;
            CompanyPostalCodeParameter.Value = golfer.CompanyPostalCode;
            CompanyPostalCodeParameter.Direction = ParameterDirection.Input;

            SqlParameter CompanyPhoneParameter = new SqlParameter();
            CompanyPhoneParameter.ParameterName = "@CompanyPhone";
            CompanyPhoneParameter.SqlDbType = SqlDbType.VarChar;
            CompanyPhoneParameter.Value = golfer.CompanyPhone;
            CompanyPhoneParameter.Direction = ParameterDirection.Input;

            SqlParameter Sponser1Parameter = new SqlParameter();
            Sponser1Parameter.ParameterName = "@Sponser1";
            Sponser1Parameter.SqlDbType = SqlDbType.VarChar;
            Sponser1Parameter.Value = golfer.Sponser1;
            Sponser1Parameter.Direction = ParameterDirection.Input;

            SqlParameter Sponser2Parameter = new SqlParameter();
            Sponser2Parameter.ParameterName = "@Sponser2";
            Sponser2Parameter.SqlDbType = SqlDbType.VarChar;
            Sponser2Parameter.Value = golfer.Sponser2;
            Sponser2Parameter.Direction = ParameterDirection.Input;

            SqlParameter ShareholderParameter = new SqlParameter();
            ShareholderParameter.ParameterName = "@Shareholder";
            ShareholderParameter.SqlDbType = SqlDbType.Bit;
            ShareholderParameter.Value = golfer.Shareholder;
            ShareholderParameter.Direction = ParameterDirection.Input;

            SqlParameter ReturnCodeParameter = new SqlParameter();
            ReturnCodeParameter.ParameterName = "@ReturnCode";
            ReturnCodeParameter.SqlDbType = SqlDbType.Int;
            ReturnCodeParameter.Direction = ParameterDirection.ReturnValue;

            RecordMembershipApplicationCommand.Parameters.Add(MembershipLevelParameter);
            RecordMembershipApplicationCommand.Parameters.Add(FirstNameParameter);
            RecordMembershipApplicationCommand.Parameters.Add(LastNameParameter);
            RecordMembershipApplicationCommand.Parameters.Add(PasswordParameter);
            RecordMembershipApplicationCommand.Parameters.Add(AddressParameter);
            RecordMembershipApplicationCommand.Parameters.Add(PostalCodeParameter);
            RecordMembershipApplicationCommand.Parameters.Add(PhoneParameter);
            RecordMembershipApplicationCommand.Parameters.Add(AltPhoneParameter);
            RecordMembershipApplicationCommand.Parameters.Add(EmailParameter);
            RecordMembershipApplicationCommand.Parameters.Add(DateOfBirthParameter);
            RecordMembershipApplicationCommand.Parameters.Add(OccupationParameter);
            RecordMembershipApplicationCommand.Parameters.Add(CompanyNameParameter);
            RecordMembershipApplicationCommand.Parameters.Add(CompanyAddressParameter);
            RecordMembershipApplicationCommand.Parameters.Add(CompanyPostalCodeParameter);
            RecordMembershipApplicationCommand.Parameters.Add(CompanyPhoneParameter);
            RecordMembershipApplicationCommand.Parameters.Add(Sponser1Parameter);
            RecordMembershipApplicationCommand.Parameters.Add(Sponser2Parameter);
            RecordMembershipApplicationCommand.Parameters.Add(ShareholderParameter);
            RecordMembershipApplicationCommand.Parameters.Add(ReturnCodeParameter);

            try
            {
                clubbaistConnection.Open();
                RecordMembershipApplicationCommand.ExecuteNonQuery();

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

        public bool ReviewMembershipApplication(Golfer golfer)
        {
            bool Success = false;
            SqlConnection clubbaistConnection = new SqlConnection();
            clubbaistConnection.ConnectionString = Startup.ConnectionString;

            SqlCommand ReviewMembershipApplicationCommand = new SqlCommand();
            ReviewMembershipApplicationCommand.CommandType = CommandType.StoredProcedure;
            ReviewMembershipApplicationCommand.Connection = clubbaistConnection;
            ReviewMembershipApplicationCommand.CommandText = "ReviewMembershipApplication";

            SqlParameter MemberNumberParameter = new SqlParameter();
            MemberNumberParameter.ParameterName = "@MemberNumber";
            MemberNumberParameter.SqlDbType = SqlDbType.Int;
            MemberNumberParameter.Value = golfer.MemberNumber;
            MemberNumberParameter.Direction = ParameterDirection.Input;

            SqlParameter ApprovedParameter = new SqlParameter();
            ApprovedParameter.ParameterName = "@Approved";
            ApprovedParameter.SqlDbType = SqlDbType.Char;
            ApprovedParameter.Value = golfer.Approved;
            ApprovedParameter.Direction = ParameterDirection.Input;

            SqlParameter MembershipStartDateParameter = new SqlParameter();
            MembershipStartDateParameter.ParameterName = "@MembershipStartDate";
            MembershipStartDateParameter.SqlDbType = SqlDbType.Date;
            MembershipStartDateParameter.Value = golfer.MembershipStartDate;
            MembershipStartDateParameter.Direction = ParameterDirection.Input;

            SqlParameter ReturnCodeParameter = new SqlParameter();
            ReturnCodeParameter.ParameterName = "@ReturnCode";
            ReturnCodeParameter.SqlDbType = SqlDbType.Int;
            ReturnCodeParameter.Direction = ParameterDirection.ReturnValue;

            ReviewMembershipApplicationCommand.Parameters.Add(MemberNumberParameter);
            ReviewMembershipApplicationCommand.Parameters.Add(ApprovedParameter);
            ReviewMembershipApplicationCommand.Parameters.Add(MembershipStartDateParameter);
            ReviewMembershipApplicationCommand.Parameters.Add(ReturnCodeParameter);

            try
            {
                clubbaistConnection.Open();
                ReviewMembershipApplicationCommand.ExecuteNonQuery();

                int returnValue = (int)ReturnCodeParameter.Value;
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
