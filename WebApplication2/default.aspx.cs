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
            BindDropDownListParti();


            DataTable dt = new DataTable(); 
            var connectionString = ConfigurationManager.ConnectionStrings["ConnCms"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT stemmenummer, stemmer.kommuneNr, stemmer.partiId, partier, partiforkortelse, KNavn " +
                                                "from stemmer, parti, valgkrets " +
                                                " where stemmer.partiId=parti.partiId and stemmer.kommuneNr=valgkrets.kommuneNr", conn);
                cmd.CommandType = CommandType.Text;

                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);

                GridView1.DataSource = dt;
                GridView1.DataBind();
                

                conn.Close();
            }

        }

        protected void ButtonSearchParti_Click1(object sender, EventArgs e)
        {
            SqlParameter param;
            DataTable dt = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["ConnCms"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT * from parti where partier= @parti OR partiforkortelse= @partiforkortelse", conn);
                cmd.CommandType = CommandType.Text;

                //param here
                param = new SqlParameter("@parti", SqlDbType.NChar);
                param.Value = TextBoxSearchParti.Text; //variabel som blir sendt inn til metodesjekk
                cmd.Parameters.Add(param);

                param = new SqlParameter("@partiforkortelse", SqlDbType.NChar);
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
            var connectionString = ConfigurationManager.ConnectionStrings["ConnCms"].ConnectionString;
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from parti", conn);//@ betyr at det er et parameter
                cmd.CommandType = CommandType.Text;
                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
                reader.Close();
                conn.Close();
            }

            //loope gjennom datatable for å hente ut partinavn. lage et dropdownitem og putte navnet i det
            foreach (DataRow row in dt.Rows)
            {
                ListItem item = new ListItem(row["partier"].ToString(), row["partiId"].ToString());//henter ut verdier fra parti tabellen
                DropDownListParti.Items.Add(item);//legge item i lista
            }

            //DropDownListParti.DataSource= dt;
            DropDownListParti.DataBind();
        }

        //protected void StemmeKnapp_Click(object sender, EventArgs e)
        //{
        //    SqlParameter param;
        //    DataTable dt = new DataTable();
        //    var connectionString = ConfigurationManager.ConnectionStrings["ConnCms"].ConnectionString;

        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        conn.Open();

        //        SqlCommand cmd = new SqlCommand("Insert into stemmenummer. ", conn);
        //        cmd.CommandType = CommandType.Text;

        //        //param here
        //        param = new SqlParameter("@parti", SqlDbType.NChar);
        //        param.Value = DropDownAvgiStemme.Text; //variabel som blir sendt inn til metodesjekk
        //        cmd.Parameters.Add(param);

        //        param = new SqlParameter("@partiforkortelse", SqlDbType.NChar);
        //        param.Value = DropDownAvgiStemme.Text; //variabel som blir sendt inn til metodesjekk
        //        cmd.Parameters.Add(param);

        //        SqlDataReader reader = cmd.ExecuteReader();
        //        dt.Load(reader);

        //        GridView1.DataSource = dt;
        //        GridView1.DataBind();


        //        conn.Close();


        //    }

        //}

        //protected void AddNewPerson_Click(object sender, EventArgs e)
        //{
        //    SqlParameter param;
        //    DataTable dt = new DataTable();
        //    var connectionString = ConfigurationManager.ConnectionStrings["ConnCms"].ConnectionString;

        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        conn.Open();
        //        SqlCommand cmd = new SqlCommand("Insert into Person (Fornavn, Etternavn, PersNr) values (@fornavn, @etternavn, @PersNr) ", conn);
        //        cmd.CommandType = CommandType.Text;

        //        //param here
        //        param = new SqlParameter("@fornavn", SqlDbType.NChar);
        //        param.Value = NewPersonFirstName.Text; //variabel som blir sendt inn til metodesjekk
        //        cmd.Parameters.Add(param);

        //        param = new SqlParameter("@etternavn", SqlDbType.NChar);
        //        param.Value = NewPersonLastName.Text; //variabel som blir sendt inn til metodesjekk
        //        cmd.Parameters.Add(param);

        //        param = new SqlParameter("@PersNr", SqlDbType.NChar);
        //        param.Value = NewPersonID.Text; //variabel som blir sendt inn til metodesjekk
        //        cmd.Parameters.Add(param);


        //        SqlDataReader reader = cmd.ExecuteReader();
        //        dt.Load(reader);

        //        GridView1.DataSource = dt;
        //        GridView1.DataBind();


        //        conn.Close();

        //    }
        //}
    }


}
