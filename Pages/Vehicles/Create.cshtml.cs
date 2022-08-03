using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace VehicleProject.Pages.Vehicles
{
    public class CreateModel : PageModel
    {
        public VehicleInfo vehicleInfo = new VehicleInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            vehicleInfo.VehicleNumber = Request.Form["VehicleNumber"];
            vehicleInfo.VehicleName = Request.Form["VehicleName"];
            vehicleInfo.VehicleColor = Request.Form["VehicleColor"];
            vehicleInfo.VehicleWheel = Request.Form["VehicleWheel"];
            vehicleInfo.VehicleHeadlight = Request.Form["VehicleHeadlight"];

            if (vehicleInfo.VehicleNumber.Length == 0 || vehicleInfo.VehicleName.Length == 0 || vehicleInfo.VehicleColor.Length == 0 || vehicleInfo.VehicleWheel.Length == 0 || vehicleInfo.VehicleHeadlight.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            try
            {
                String connectionString = "Data Source=DESKTOP-C3RSTJQ\\SQLEXPRESS;Initial Catalog=VehicleProjectDb;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO Vehicle " +
                        "(VehicleNumber, VehicleName, VehicleColor, VehicleWheel, VehicleHeadlight) VALUES " +
                        "(@VehicleNumber, @VehicleName, @VehicleColor, @VehicleWheel, @VehicleHeadlight);";
                    using (SqlCommand command = new SqlCommand(sql,connection))
                    {
                        command.Parameters.AddWithValue("@VehicleNumber", vehicleInfo.VehicleNumber);
                        command.Parameters.AddWithValue("@VehicleName", vehicleInfo.VehicleName);
                        command.Parameters.AddWithValue("@VehicleColor", vehicleInfo.VehicleColor);
                        command.Parameters.AddWithValue("@VehicleWheel", vehicleInfo.VehicleWheel);
                        command.Parameters.AddWithValue("@VehicleHeadlight", vehicleInfo.VehicleHeadlight);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage=ex.Message;
                return;
            }
                //save new vehicle db
            vehicleInfo.VehicleNumber = "";
            vehicleInfo.VehicleName = "";
            vehicleInfo.VehicleColor = "";
            vehicleInfo.VehicleWheel = "";
            vehicleInfo.VehicleHeadlight = "";
            successMessage = "New Vehicle Added Correctly";

            Response.Redirect("/Vehicles/Index");
        }
    }
}
