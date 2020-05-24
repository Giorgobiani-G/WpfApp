using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.econtact
{
    //Getter Setter Properties
    //Acts as Data Carrier in  our Application
    class ContactClass
    {
        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }

        static readonly string myconstring = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        
        

        

        //insert data into dababase

        public bool Insert (ContactClass c)
        {
            //creating default value typa and set its value to false
            bool issucess = false;

            //Connect to database
            SqlConnection sqlConnection = new SqlConnection(myconstring);
            try
            {
                //create querry to insert data 
                string sql = "INSERT INTO Wpf_Contacts (FirstName, LastName,ContactNo,Address,Gender) VALUES(@FirstName, @LastName, @ContactNo, @Address, @Gender)";
                //Create sqlcommand using sqlconnection and sql
                SqlCommand cmd = new SqlCommand(sql, sqlConnection);

                //Create Parameters to add data
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);

                //open connection
                sqlConnection.Open();
                int rows = cmd.ExecuteNonQuery();
                //if the query runs succesfully rows will be greater than zero
                if (rows > 0)
                {
                    issucess = true;
                }
                else
                {
                    issucess = false;
                }
            }

            catch (Exception ex)
            {

            }

            finally
            {
                sqlConnection.Close();
            }

            return issucess;




        }

        //Method to update data into database
        public bool Update(ContactClass c)
        {
            //creating default value typa and set its value to false
            bool issucess = false;
            SqlConnection con = new SqlConnection(myconstring);
            try
            {
                
                //create querry
                string str = "Update Wpf_Contacts Set FirstName = @FirstName,LastName= @LastName, ContactNo= @ContactNo,Address= @Address,Gender=@Gender Where ContactId= @ContactId";
                //Create comand
                SqlCommand sqlCommand = new SqlCommand(str, con);
                //Crete Parameters to Add value
                sqlCommand.Parameters.AddWithValue("@ContactId", c.ContactId);
                sqlCommand.Parameters.AddWithValue("@FirstName", c.FirstName);
                sqlCommand.Parameters.AddWithValue("@LastName", c.LastName);
                sqlCommand.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                sqlCommand.Parameters.AddWithValue("@Address", c.Address);
                sqlCommand.Parameters.AddWithValue("@Gender", c.Gender);
                //open connection
                con.Open();
                int rows = sqlCommand.ExecuteNonQuery();
                //if the rows querry runs succesfully rows number should be greager than zero

                if (rows > 0)
                {
                    issucess = true;
                }
                else
                {
                    issucess = false;
                }


            }
            catch (Exception ex)
            {

            }

            finally
            {
                con.Close();
             }

            return issucess;
        }

        //Method to delete data into database 

        public bool Delete(ContactClass c)
        {
            bool issucess = false;
            SqlConnection sqlConnection = new SqlConnection(myconstring);

            try
            {
                string del = "Delete From Wpf_Contacts Where ContactId = @ContactId";

                SqlCommand cmd = new SqlCommand(del, sqlConnection);
                cmd.Parameters.AddWithValue("@ContactId", c.ContactId);
                sqlConnection.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    issucess = true;
                }
                else
                {
                    issucess = false;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                sqlConnection.Close();
            }
            return issucess;

            
        }

        
        
    }
    
}
