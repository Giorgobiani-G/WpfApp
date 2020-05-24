using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.econtact;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>


    public partial class MainWindow : Window
    {

        ContactClass contact = new ContactClass();



        public MainWindow()
        {
            InitializeComponent();

            Select();

        }


        // Method to Select data from Database
        public void Select()
        {
            //1.Connect to database

            string myconstring = Properties.Settings.Default.constr;
            
            SqlConnection sqlConnection = new SqlConnection(myconstring);


            try
            {
                
                //2. create sql querry

                string sql = "Select * from Wpf_Contacts order by ContactId Asc";
                
               
                //3.Create Cmd using sqlConnection and sql
                SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
                
                //4.Create Sqldataadapter using sqlCommand
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                
                DataTable dt = new DataTable("Wpf_Contacts");
                sqlConnection.Open();
                adapter.Fill(dt);
                txtboxdtgrid.ItemsSource = dt.DefaultView;


            }








            catch (Exception ex)
            {



            }
            finally
            {
                sqlConnection.Close();
            }







        }

         //add data to database
        private void Addbutton_Click(object sender, RoutedEventArgs e)
        {
            //Get data from input fields
            contact.FirstName = txtboxFirstName.Text;
            contact.LastName = txtboxLastName.Text;
            contact.ContactNo = txtboxContactNo.Text;
            contact.Address = txtboxAddress.Text;
            contact.Gender = txtboxGender.SelectionBoxItem.ToString();



            string myconstring = Properties.Settings.Default.constr;
            SqlConnection sqlConnection = new SqlConnection(myconstring);
            sqlConnection.Open();

            //Inserting data into database via method we greared
            bool success = contact.Insert(contact);
            if (success == true)
            {
                MessageBox.Show("New Contact inserted succesfully");
                Clear();
            }
            else
            {
                MessageBox.Show("Failed to new Contact, Try Again");

            }


            Select();

        }

        //Get Data from Datagrid to textboxes by mouse click
        
        private void Txtboxdtgrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid grid = (DataGrid)sender;
            DataRowView rowselected = grid.SelectedItem as DataRowView;
            if (rowselected != null) { 
            txtboxcontactid.Text = rowselected[0].ToString();
            txtboxFirstName.Text = rowselected[1].ToString();
            txtboxLastName.Text = rowselected[2].ToString();
            txtboxContactNo.Text = rowselected[3].ToString();
            txtboxAddress.Text = rowselected[4].ToString();
            txtboxGender.Text = rowselected[5].ToString();
            }
           
        }
       
        //Update data to database
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            contact.ContactId = int.Parse(txtboxcontactid.Text);
            contact.FirstName = txtboxFirstName.Text;
            contact.LastName = txtboxLastName.Text;
            contact.ContactNo = txtboxContactNo.Text;
            contact.Address = txtboxAddress.Text;
            contact.Gender = txtboxGender.SelectionBoxItem.ToString();

            string myconstring = Properties.Settings.Default.constr;
            SqlConnection sqlConnection = new SqlConnection(myconstring);
            sqlConnection.Open();


            bool success = contact.Update(contact);
            if (success == true)
            {
                MessageBox.Show("New Contact updated succesfully");
                Clear();
            }
            else
            {
                MessageBox.Show("Failed to update, Try Again");

            }

            Select();
        }

        //create Clear method
         public void Clear() 

        {
            txtboxcontactid.Text = "";
            txtboxFirstName.Text = "";
            txtboxLastName.Text = "";
            txtboxContactNo.Text = "";
            txtboxAddress.Text = "";
            txtboxGender.Text = "";
        }
        
        //Clear textbox data
        private void Clearbutton_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }
        
        //Delete data from database
        private void Deletebutton_Click(object sender, RoutedEventArgs e)
        {
            string myconstring = Properties.Settings.Default.constr;
            SqlConnection sqlConnection = new SqlConnection(myconstring);
            sqlConnection.Open();



            contact.ContactId = int.Parse(txtboxcontactid.Text);
            bool success = contact.Delete(contact);
            if (success == true)
            {
                MessageBox.Show("Contact Sucessfully Deleted");
                Clear();
            }
            else
            {
                MessageBox.Show("Failed to delete Contact");
            }
            
            Select();
            

        }

        //make searchox usable

        string myconstring = Properties.Settings.Default.constr;

        private void Searchbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string myconstring = Properties.Settings.Default.constr;
            SqlConnection sqlConnection = new SqlConnection(myconstring);
            


            string keyword = searchbox.Text;
            

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Select * from Wpf_Contacts where FirstName Like + '%"+keyword+ "%' or LastName Like + '%" + keyword + "%' or ContactNo Like + '%" + keyword + "%' or Address Like + '%" + keyword + "%' or Gender Like + '%" + keyword + "%' ", sqlConnection);
            DataTable dt = new DataTable();
            sqlConnection.Open();
            sqlDataAdapter.Fill(dt);
            txtboxdtgrid.ItemsSource = dt.DefaultView;
        }

        
    }
    
}
