using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
//добавление библиотек
namespace Kourse
{
    public partial class Form1 : Form
    {
    //первая попытка подключения к бд
    string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CompanyDatabase;Integrated Security=True";
    SqlConnection connection;
    SqlDataAdapter adapter;
    DataSet dataSet;
        public Form1()
        {
            InitializeComponent();
            connection = new SqlConnection(connectionString);
            adapter = new SqlDataAdapter();
            InitializeDataSet();
            LoadData();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        static void main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
