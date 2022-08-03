using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace VehicleProject.Pages.Vehicles
{
    public class EditModel : PageModel
    {
        public VehicleInfo vehicleInfo = new VehicleInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String Id = Request.Query["Id"];
            try
            {
                String connectionString = "Data Source=DESKTOP-C3RSTJQ\\SQLEXPRESS;Initial Catalog=VehicleProjectDb;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Vehicle WHERE Id=@Id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", Id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                vehicleInfo.Id = "" + reader.GetInt32(0);
                                vehicleInfo.VehicleNumber = reader.GetString(1);
                                vehicleInfo.VehicleName = reader.GetString(2);
                                vehicleInfo.VehicleColor = reader.GetString(3);
                                vehicleInfo.VehicleWheel = reader.GetString(4);
                                vehicleInfo.VehicleHeadlight = reader.GetString(5);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            vehicleInfo.Id = Request.Form["Id"];
            vehicleInfo.VehicleNumber = Request.Form["VehicleNumber"];
            vehicleInfo.VehicleName = Request.Form["VehicleName"];
            vehicleInfo.VehicleColor = Request.Form["VehicleColor"];
            vehicleInfo.VehicleWheel = Request.Form["VehicleWheel"];
            vehicleInfo.VehicleHeadlight = Request.Form["VehicleHeadlight"];

            if (vehicleInfo.Id.Length == 0 || vehicleInfo.VehicleNumber.Length == 0 || vehicleInfo.VehicleName.Length == 0 || vehicleInfo.VehicleColor.Length == 0  || vehicleInfo.VehicleWheel.Length == 0 || vehicleInfo.VehicleHeadlight.Length == 0)
            {
                errorMessage = "All fields are required..";
                return;
            }

            try
            {
                String connectionString = "Data Source=DESKTOP-C3RSTJQ\\SQLEXPRESS;Initial Catalog=VehicleProjectDb;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE Vehicle " +
                        "SET VehicleNumber=@VehicleNumber, VehicleName=@VehicleName,VehicleColor=@VehicleColor, VehicleWheel=@VehicleWheel, VehicleHeadlight=@VehicleHeadlight"   + "WHERE Id=@Id";
                    using (SqlCommand command = new SqlCommand(sql,connection))
                    {
                        command.Parameters.AddWithValue("@VehicleNumber", vehicleInfo.VehicleNumber);
                        command.Parameters.AddWithValue("@VehicleName", vehicleInfo.VehicleName);
                        command.Parameters.AddWithValue("@VehicleColor", vehicleInfo.VehicleColor);
                        command.Parameters.AddWithValue("@VehicleWheel", vehicleInfo.VehicleWheel);
                        command.Parameters.AddWithValue("@VehicleHeadlight", vehicleInfo.VehicleHeadlight);
                        command.Parameters.AddWithValue("@Id", vehicleInfo.Id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            Response.Redirect("/Vehicles/Index");
        }
    }
}
