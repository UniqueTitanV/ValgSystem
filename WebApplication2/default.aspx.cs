using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) { BindDropDownListParti(); }



            ////DataTable dt = new DataTable(); 
            //var connectionString = ConfigurationManager.ConnectionStrings["ConnCms"].ConnectionString;

            //using (SqlConnection conn = new SqlConnection(connectionString))
            //{
            //    conn.Open();

            //    SqlCommand cmd = new SqlCommand(
            //        "SELECT COUNT(stemmenummer) as stemmer, stemmer.kommuneNr as [Kommune nummer], KNavn as [Kommune navn], stemmer.partiId as [Parti id], partier, partiforkortelse " +
            //        "FROM stemmer, parti, valgkrets " +
            //        "WHERE stemmer.partiId=parti.partiId AND stemmer.kommuneNr=valgkrets.kommuneNr " +
            //        "GROUP BY stemmer.kommuneNr, KNavn, stemmer.partiId, partier, partiforkortelse " +
            //        "ORDER BY stemmer.partiId", conn);
            //    cmd.CommandType = CommandType.Text;

            //    SqlDataReader reader = cmd.ExecuteReader();
            //    //dt.Load(reader);

            //    //GridView1.DataSource = dt;
            //    //GridView1.DataBind();


            //    conn.Close();
            //}

        }

        protected void ButtonSearchParti_Click1(object sender, EventArgs e)
        {
            SqlParameter param;
            DataTable dt = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["ConnCms"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    "Select parti.partiId as [Parti id], partier, partiForkortelse as [Parti forkortelse], Count(stemmenummer) as stemmer " +
                    "from parti, stemmer " +
                    "where parti.partiId=stemmer.partiId " +
                    "AND (parti.partier = @parti OR parti.partiForkortelse = @partiForkortelse)" +
                    "group by parti.partiId, partier, partiForkortelse", conn);
                 cmd.CommandType = CommandType.Text;

                //param here
                param = new SqlParameter("@parti", SqlDbType.NChar);
                param.Value = TextBoxSearchParti.Text; //variabel som blir sendt inn til metodesjekk
                cmd.Parameters.Add(param);

                param = new SqlParameter("@partiForkortelse", SqlDbType.NChar);
                param.Value = TextBoxSearchParti.Text; //variabel som blir sendt inn til metodesjekk
                cmd.Parameters.Add(param);

                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);

                GridView1.DataSource = dt;
                GridView1.DataBind();


                conn.Close();

            }
        }

        private void BindDropDownListParti()
        {
            if (!IsPostBack)
            {
                var connectionString = ConfigurationManager.ConnectionStrings["ConnCms"].ConnectionString;

                // Define your SQL query to retrieve data from the database
                string query = "SELECT partier, partiId FROM parti";

                // Create a SqlConnection to connect to the database
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Create a SqlDataAdapter to fetch the data
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        // Create a DataTable to hold the data
                        DataTable dataTable = new DataTable();

                        // Fill the DataTable with the data from the database
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the DropDownList
                        DropDownListWithData.DataTextField = "partier";
                        DropDownListWithData.DataValueField = "partiId";
                        DropDownListWithData.DataSource = dataTable;
                        DropDownListWithData.DataBind();

                    }
                }
            }
        }

        protected void StemmeKnapp_Click(object sender, EventArgs e)
        {

                DataTable dt = new DataTable();

                string selectedItem = DropDownListWithData.SelectedItem.Value;
                string enteredValue = GetKommuneNrByKommuneNavn(kommuneboks.Text);
                //kan ikke bruke kommunenavn, vi må ha nr
                //så da må vi hente nr by navn

            
                var connectionString = ConfigurationManager.ConnectionStrings["ConnCms"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sq1 = "INSERT INTO stemmer VALUES (@kommuneNr, @partiId)";

                    using (SqlCommand command = new SqlCommand(sq1, conn))
                    {
                        command.Parameters.AddWithValue("@kommuneNr", enteredValue);
                        command.Parameters.AddWithValue("@partiId", selectedItem);

                        command.ExecuteNonQuery();
                    }

                    string selectQuery = 
                    "SELECT  valgkrets.KNavn as [Kommune Navn], partier as [Parti], COUNT(stemmenummer) as stemmer " +
                    "FROM stemmer, parti, valgkrets " +
                    "WHERE stemmer.partiId=parti.partiId " +
                    "AND stemmer.kommuneNr=valgkrets.kommuneNr  " +
                    "AND stemmenummer > 1 " +
                    "GROUP BY stemmer.partiId, partier, KNavn " +
                    "ORDER BY partier";
                    using (SqlCommand selcmd = new SqlCommand(selectQuery, conn))
                    {
                        using (SqlDataReader reader = selcmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }

                    GridView1.DataSource = dt;
                    GridView1.DataBind();

                    conn.Close();

                }
        }

        private string GetKommuneNrByKommuneNavn(string kommune)
        {
            string enteredValue = kommuneboks.Text;
            string kommunenr="";
            var connectionString = ConfigurationManager.ConnectionStrings["ConnCms"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sq1 = "select kommuneNr from valgkrets where KNavn=@kommunenavn";

                using (SqlCommand command = new SqlCommand(sq1, conn))
                {
                    command.Parameters.AddWithValue("@kommunenavn", kommune);

                    SqlDataReader reader = command.ExecuteReader();
                    while(reader.Read())
                    {
                        kommunenr=reader.GetString(0);//hente ut verdi fra den 1. kolonna
                    }
                }
                conn.Close();
            }
            return kommunenr;
        }

        protected void ButtonKommuneSok_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["ConnCms"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    "SELECT COUNT(stemmenummer) as stemmer, partier, valgkrets.KNavn as [Kommune navn]" +
                    "FROM stemmer, parti, valgkrets " +
                    "WHERE stemmer.partiId=parti.partiId AND stemmer.kommuneNr=valgkrets.kommuneNr " +
                    "AND valgkrets.KNavn = @valgkrets " +
                    "GROUP BY stemmer.partiId, partier, partiforkortelse, KNavn " +
                    "ORDER BY partier", conn);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@valgkrets", kommuneboks.Text);

                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);

                GridView1.DataSource = dt;
                GridView1.DataBind();

                

                conn.Close();
            }
        }
        //Not in use possible for future like percentage

        //protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DataTable dt2 = new DataTable();
        //    var connectionString = ConfigurationManager.ConnectionStrings["ConnCms"].ConnectionString;

        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        conn.Open();

        //        SqlCommand cmd = new SqlCommand("", conn);
        //        cmd.CommandType = CommandType.Text;

        //        SqlDataReader reader = cmd.ExecuteReader();
        //        dt2.Load(reader);

        //        GridView2.DataSource = dt2;
        //        GridView2.DataBind();


        //        conn.Close();
        //    }
        //}
    }


}
