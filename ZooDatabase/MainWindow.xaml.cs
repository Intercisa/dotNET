using System;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Data;

namespace ZooDatabase
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private SqlConnection sqlConnection;

        public MainWindow()
        {
            InitializeComponent();
         

            //ExampleDB1ConnectionString

            string connectionString = ConfigurationManager.ConnectionStrings["ZooDatabase.Properties.Settings.ExampleDB1ConnectionString"].ConnectionString;
            sqlConnection = new SqlConnection(connectionString);
            ShowZoos();
            ShowAnimals();
         

        }
        private void ShowZoos()
        {
            try
            {
                string query = "select * from Zoo";

            //the sqlDataAdapter can be seen as an Interface to make Table usable by C#-Objects
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlConnection);

            using (sqlDataAdapter) {
                DataTable zooTable = new DataTable();

                sqlDataAdapter.Fill(zooTable);

                //Which Information of the Table in DataTable should be shown in our ListBox?
                listZoos.DisplayMemberPath = "Location";
                //Which Value should be delivered, when an Item from our ListBox is selected?
                listZoos.SelectedValuePath = "Id";
                //The Reference to the Data the ListBox should populate
                listZoos.ItemsSource = zooTable.DefaultView;
            }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }



        private void ShowAssociatedAnimals()
        {
            try
            {
                string query = "select * from Animal a inner join ZooAnimal za on a.Id = za.AnimalId where za.ZooId = @ZooId";

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                //the sqlDataAdapter can be seen as an Interface to make Table usable by C#-Objects
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                using (sqlDataAdapter)
                {

                    sqlCommand.Parameters.AddWithValue("@ZooId", listZoos.SelectedValue);
                    DataTable animalTable = new DataTable();

                    sqlDataAdapter.Fill(animalTable);

                    //Which Information of the Table in DataTable should be shown in our ListBox?
                    listAssociatedAnimals.DisplayMemberPath = "Name";
                    //Which Value should be delivered, when an Item from our ListBox is selected?
                    listAssociatedAnimals.SelectedValuePath = "Id";
                    //The Reference to the Data the ListBox should populate
                    listAssociatedAnimals.ItemsSource = animalTable.DefaultView;
                }

            }
            catch (Exception e)
            {
                //MessageBox.Show(e.ToString());
            }
        }


        private void ShowAnimals()
        {
            try
            {
                string query = "select * from Animal";

                //the sqlDataAdapter can be seen as an Interface to make Table usable by C#-Objects
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlConnection);

                using (sqlDataAdapter)
                {
                    DataTable animalTable = new DataTable();

                    sqlDataAdapter.Fill(animalTable);

                    //Which Information of the Table in DataTable should be shown in our ListBox?
                    listAnimals.DisplayMemberPath = "Name";
                    //Which Value should be delivered, when an Item from our ListBox is selected?
                    listAnimals.SelectedValuePath = "Id";
                    //The Reference to the Data the ListBox should populate
                    listAnimals.ItemsSource = animalTable.DefaultView;
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void listZoos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowAssociatedAnimals();
            ShowSelectedZooInTextBox();
        }
        private void RemoveZoo_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                string query = "delete from Zoo where Id = @ZooId";

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlConnection.Open();
                sqlCommand.Parameters.AddWithValue("@ZooId", listZoos.SelectedValue);
                sqlCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally 
            {
                sqlConnection.Close();
                ShowZoos();
            }
        }

        private void AddZoo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "insert into Zoo values (@Location)";

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlConnection.Open();
                sqlCommand.Parameters.AddWithValue("@Location", tbTextBox.Text);
                sqlCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sqlConnection.Close();
                ShowZoos();
            }
        }

        private void AddAnimalZoo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "insert into ZooAnimal values (@ZooId, @AnimalId)";

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlConnection.Open();
                sqlCommand.Parameters.AddWithValue("@ZooId", listZoos.SelectedValue);
                sqlCommand.Parameters.AddWithValue("@AnimalId", listAnimals.SelectedValue);
                sqlCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sqlConnection.Close();
                ShowAssociatedAnimals();
                ShowZoos();
            }
        }

        private void RemoveAnimalZoo_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                string query = "delete from Animal where Id = @ZooAnimalId";

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlConnection.Open();
               
                sqlCommand.Parameters.AddWithValue("@ZooAnimalId", listAssociatedAnimals.SelectedValue);
                sqlCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sqlConnection.Close();
                ShowAssociatedAnimals();
            }


        }

        private void AddAssociatedAnimal_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "insert into Animal values (@Name)";

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlConnection.Open();
                sqlCommand.Parameters.AddWithValue("@Name", tbTextBox.Text);
                sqlCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sqlConnection.Close();
                ShowAnimals();
            }
        }

        private void DeleteAnimal_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "delete from Animal where Id = @AnimalId";

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlConnection.Open();
                sqlCommand.Parameters.AddWithValue("@AnimalId", listAnimals.SelectedValue);
                sqlCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sqlConnection.Close();
                ShowAnimals();
            }
        }


        private int ShowSelectedZooInTextBox() {
            int id = -1;
            try
            {
                string query = "select * from Zoo where id = @ZooId" ;

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                using (sqlDataAdapter)
                {

                    sqlCommand.Parameters.AddWithValue("@ZooId", listZoos.SelectedValue);
                    DataTable zooDataTable = new DataTable();

                    sqlDataAdapter.Fill(zooDataTable);

                    tbTextBox.Text = zooDataTable.Rows[0]["Location"].ToString();
                    id =  (int)zooDataTable.Rows[0]["Id"];
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.ToString());
            }

            return id;
        }




        private int ShowSelectedAnimalInTextBox()
        {
            int id = -1;
            try
            {
                string query = "select * from Animal where id = @AnimalId";

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                using (sqlDataAdapter)
                {

                    sqlCommand.Parameters.AddWithValue("@AnimalId", listAnimals.SelectedValue);
                    DataTable animalDataTable = new DataTable();

                    sqlDataAdapter.Fill(animalDataTable);

                    tbTextBox.Text = animalDataTable.Rows[0]["Name"].ToString();
                    id = (int)animalDataTable.Rows[0]["Id"];
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.ToString());
            }

            return id;
        }



        private void UpdateAnimal_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = $"update Animal set Name=@UpdatedName where id=@AnimalId";

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlConnection.Open();
                sqlCommand.Parameters.AddWithValue("@UpdatedName", tbTextBox.Text);
                sqlCommand.Parameters.AddWithValue("@AnimalId", ShowSelectedAnimalInTextBox());
                sqlCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sqlConnection.Close();
                ShowAnimals();
            }
        }


        private void UpdateZoo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = $"update Zoo set Location=@UpdatedLocation where id=@ZooId";

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlConnection.Open();
                sqlCommand.Parameters.AddWithValue("@UpdatedLocation", tbTextBox.Text);
                sqlCommand.Parameters.AddWithValue("@ZooId", ShowSelectedZooInTextBox());
                sqlCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sqlConnection.Close();
                ShowZoos();
            }
        }

        private void listAnimals_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowSelectedAnimalInTextBox();
        }
    }
}
