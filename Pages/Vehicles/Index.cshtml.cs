using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace VehicleProject.Pages.Vehicles
{
    public class IndexModel : PageModel
    {
        public List<VehicleInfo> listVehicles = new List<VehicleInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-C3RSTJQ\\SQLEXPRESS;Initial Catalog=VehicleProjectDb;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Vehicle";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                VehicleInfo vehicleinfo = new VehicleInfo();
                                vehicleinfo.Id = "" + reader.GetInt32(0);
                                vehicleinfo.VehicleNumber =reader.GetString(1);
                                vehicleinfo.VehicleName =reader.GetString(2);
                                vehicleinfo.VehicleColor =reader.GetString(3);
                                vehicleinfo.VehicleWheel =reader.GetString(4);
                                vehicleinfo.VehicleHeadlight =reader.GetString(5);
                                vehicleinfo.created_at =reader.GetDateTime(6).ToString();

                                listVehicles.Add(vehicleinfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex.ToString());
            }
        }
    }
    public class VehicleInfo
    {
        public String Id;
        public String VehicleNumber;
        public String VehicleName;
        public String VehicleColor;
        public String VehicleWheel;
        public String VehicleHeadlight;
        public String created_at;
    }
}
