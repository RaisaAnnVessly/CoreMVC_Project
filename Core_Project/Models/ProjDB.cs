using Core_Project.Models;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
namespace Core_Project.Models
{
    public class ProjDB
    {
        SqlConnection con = new SqlConnection(@"server=RAISA\SQLEXPRESS;database=Core_Proj;Integrated Security=true");

        public int GetMaxId()
        {
            int id = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("sp_loginMaxId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                object result = cmd.ExecuteScalar();
                con.Close();

                if (result != DBNull.Value && result != null)
                {
                    id = Convert.ToInt32(result)+1;
                }
                else
                {
                    id = 1;
                }
            }
            catch (Exception)
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return id;
        }
        public string InsertUsr(InsertCls clsobj)
        {
            try
            {
                int i = GetMaxId();
                clsobj.uid = i;
             
                SqlCommand cmd = new SqlCommand("sp_insert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@uid", clsobj.uid);
                cmd.Parameters.AddWithValue("@na", clsobj.uname);
                cmd.Parameters.AddWithValue("@email", clsobj.uemail);
                cmd.Parameters.AddWithValue("@phone", clsobj.uphone);
                cmd.Parameters.AddWithValue("@addr", clsobj.uaddr);
                SqlCommand cmd1 = new SqlCommand("sp_loginIns", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@regid", clsobj.uid);
                cmd1.Parameters.AddWithValue("@uname", clsobj.username);
                cmd1.Parameters.AddWithValue("@pass", clsobj.password);
                cmd1.Parameters.AddWithValue("@logtype", "user");
                con.Open();
                cmd.ExecuteNonQuery();
                cmd1.ExecuteNonQuery();
                con.Close();
                return "Inserted Successfully";
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return ex.Message.ToString();
            }
        }
        public string InsertAdmin(AdminCls clsobj)
        {
            try
            {
                int i = GetMaxId();
                clsobj.id = i;
                SqlCommand cmd = new SqlCommand("sp_AdminInsert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", clsobj.id);
                cmd.Parameters.AddWithValue("@name", clsobj.name);
                cmd.Parameters.AddWithValue("@email", clsobj.email);
                cmd.Parameters.AddWithValue("@phone", clsobj.phone);
                SqlCommand cmd1 = new SqlCommand("sp_loginIns", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@regid", clsobj.id);
                cmd1.Parameters.AddWithValue("@uname", clsobj.username);
                cmd1.Parameters.AddWithValue("@pass", clsobj.password);
                cmd1.Parameters.AddWithValue("@logtype","admin");
                con.Open();
                cmd.ExecuteNonQuery();
                cmd1.ExecuteNonQuery();
                con.Close();
                return "Inserted Successfully";
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return ex.Message.ToString();
            }
        }


        public int GetId(LoginCls clsobj)
        {
            try
            {
                SqlCommand cmd1 = new SqlCommand("sp_getLogId", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@una", clsobj.uname);
                cmd1.Parameters.AddWithValue("@pwd", clsobj.pass);
                con.Open();
                int regid = Convert.ToInt32(cmd1.ExecuteScalar());
                con.Close();
                return regid;
            }
            catch(Exception)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                throw;
            }

        }
        public string GetLogType(LoginCls clsobj)
        {
            try
            {
                SqlCommand cmd2 = new SqlCommand("sp_loginType", con);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@uname", clsobj.uname);
                cmd2.Parameters.AddWithValue("@pass", clsobj.pass);
                con.Open();
                string log_type = cmd2.ExecuteScalar()?.ToString() ?? "";
                con.Close();
                return log_type;
            }
            catch(Exception)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                throw;
            }
        }
        public int Login(LoginCls clsobj)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_loginCountId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@una", clsobj.uname);
                cmd.Parameters.AddWithValue("@pwd", clsobj.pass);
                con.Open();
                int cid = Convert.ToInt32(cmd.ExecuteScalar()); 
                con.Close();
                if (cid == 1)
                {
                    return cid;
                }
                else
                {
                    return -1;
                }
            }
            catch(Exception)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                throw;
            }    
        }

        public int InsertVehicle(ServiceCls clsobj)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_serviceInsert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@uid", clsobj.uid);
                cmd.Parameters.AddWithValue("@b_date", clsobj.bookingdate.ToDateTime(TimeOnly.MinValue));
                cmd.Parameters.AddWithValue("@vehicleno", clsobj.vehicleno);
                cmd.Parameters.AddWithValue("@vehiclemodel", clsobj.vehiclemodel);
                cmd.Parameters.AddWithValue("@s_type", clsobj.servicetype);
                cmd.Parameters.AddWithValue("@preferdate", clsobj.preferredDate);
                cmd.Parameters.AddWithValue("@docs", clsobj.documents);
                con.Open();
                int i=cmd.ExecuteNonQuery();
                con.Close();
                return i;
            }
            catch(Exception)
            {
                if(con.State==ConnectionState.Open)
                {
                    con.Close();
                }
                throw;
                
            }
            
        }
        public List<ServiceCls> ListServicesDB(int uid)
        {
            try
            {
                List<ServiceCls> services = new List<ServiceCls>();

                SqlCommand cmd = new SqlCommand("sp_ListService", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@u_id", uid);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    ServiceCls s = new ServiceCls
                    {
                        bookingdate = DateOnly.FromDateTime(Convert.ToDateTime(dr["booking_date"])),
                        vehicleno = dr["vehicle_no"].ToString(),
                        vehiclemodel = dr["vehicle_model"].ToString(),
                        servicetype = dr["service_type"].ToString(),
                        preferredDate = Convert.ToDateTime(dr["preferred_date"]),
                        documents = dr["vehicle_doc"].ToString(),
                        status = dr["service_status"].ToString(),
                        mechanicname = dr["MechanicName"].ToString()
                    };

                    services.Add(s);
                }

                con.Close();
                return services;
            }
            catch(Exception)
            {
                if(con.State==ConnectionState.Open)
                {
                    con.Close();
                }
                throw;
            }
          
        }

        public int InsertMechanic(MechanicCls clsobj)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_mechInsert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@na", clsobj.name);
                cmd.Parameters.AddWithValue("@ph", clsobj.phone);
                cmd.Parameters.AddWithValue("@spec", clsobj.spec);
                cmd.Parameters.AddWithValue("@exp", clsobj.exp);
                con.Open();
                int i=cmd.ExecuteNonQuery();
                con.Close();
                return i;
            }
            catch (Exception)
            {
                if(con.State==ConnectionState.Open)
                {
                    con.Close(); 
                }
                throw;
            }
        }

        public List<MechanicCls> ListMechanic()
        {
            try
            {
                List<MechanicCls> mechanic = new List<MechanicCls>();
                SqlCommand cmd = new SqlCommand("sp_MechanicList",con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    MechanicCls m=new MechanicCls
                    {
                        id = Convert.ToInt32(dr["m_id"]),
                        name = dr["m_name"].ToString(),
                        phone = dr["m_phone"].ToString(),
                        spec = dr["specialisation"].ToString(),
                        exp = dr["experience"].ToString()
                    };
                    mechanic.Add(m);
                }
                con.Close();
                return mechanic;
            }
            catch(Exception)
            {
                if(con.State==ConnectionState.Open)
                {
                    con.Close();
                }
                throw;
            }
        }
        public List<ServiceCls> ListAdmin()
        {
            try
            {
                List<ServiceCls> services = new List<ServiceCls>();
                SqlCommand cmd = new SqlCommand("sp_AdminListService", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ServiceCls s = new ServiceCls
                    {
                        bid = Convert.ToInt32(dr["booking_id"]),
                        uname = dr["name"].ToString(),
                        mid = dr["MechId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["MechId"]),
                        mechanicname = dr["MechanicName"].ToString(),
                        bookingdate = DateOnly.FromDateTime(Convert.ToDateTime(dr["booking_date"])),
                        vehicleno = dr["vehicle_no"].ToString(),
                        vehiclemodel = dr["vehicle_model"].ToString(),
                        servicetype = dr["service_type"].ToString(),
                        preferredDate = Convert.ToDateTime(dr["preferred_date"]),
                        documents = dr["vehicle_doc"].ToString(),
                        status = dr["service_status"].ToString()
                    };
                    services.Add(s);
                }
                con.Close();
                return services;
            }
            catch(Exception)
            {
                if(con.State==ConnectionState.Open)
                {
                    con.Close();
                }
                throw;
            }
        }
        public int AssignMechanicToService(int bid, int mid)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_AssignMechanic", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@bookid", bid);
                cmd.Parameters.AddWithValue("@mid", mid);

                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();

                return i;
            }
            catch (Exception)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                throw;
            }

        }
        public void UpdateServiceStatus(int bookid)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ServiceStatus", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@bookid", bookid);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                throw;
            }
        }
    }
}

  


