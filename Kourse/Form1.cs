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
        private void InitializeDataSet()
{
    dataSet = new DataSet();
//добавление параметров для таблицы Firm
    DataTable firmTable = new DataTable("Firm");
    firmTable.Columns.Add("ID", typeof(int));
    firmTable.Columns.Add("Name", typeof(string));
    firmTable.Columns.Add("Phone", typeof(string));
    firmTable.Columns.Add("Address", typeof(string));

    //добавление параметров для таблицы Store
    DataTable storeTable = new DataTable("Store");
    storeTable.Columns.Add("ID", typeof(int));
    storeTable.Columns.Add("Name", typeof(string));
    storeTable.Columns.Add("Phone", typeof(string));
    storeTable.Columns.Add("Address", typeof(string));

//добавление параметров для таблицы Product
    DataTable productTable = new DataTable("Product");
    productTable.Columns.Add("ID", typeof(int));
    productTable.Columns.Add("Name", typeof(string));
    productTable.Columns.Add("Category", typeof(string));
    productTable.Columns.Add("Description", typeof(string));
    productTable.Columns.Add("Price", typeof(decimal));
    productTable.Columns.Add("Quantity", typeof(int));
    productTable.Columns.Add("Unit", typeof(string));
    productTable.Columns.Add("ArrivalDate", typeof(DateTime));
    productTable.Columns.Add("StoreID", typeof(int));
    productTable.Columns.Add("Discount", typeof(decimal));
    productTable.Columns.Add("VAT", typeof(decimal));

    dataSet.Tables.Add(firmTable);
    dataSet.Tables.Add(storeTable);
    dataSet.Tables.Add(productTable);
//Добавление DateRelation для связи двух таблиц и их объектов друг с другом
    DataRelation storeFirmRelation = new DataRelation("FK_Store_Firm",dataSet.Tables["Firm"].Columns["ID"],dataSet.Tables["Store"].Columns["ID"]);
    DataRelation productStoreRelation = new DataRelation("FK_Product_Store",dataSet.Tables["Store"].Columns["ID"], dataSet.Tables["Product"].Columns["StoreID"]);


    
}
    }
}
