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
// Создание команд для адаптера
            SqlCommand selectFirmCommand = new SqlCommand("SELECT * FROM Firm", connection);
            SqlCommand selectStoreCommand = new SqlCommand("SELECT * FROM Store", connection);
            SqlCommand selectProductCommand = new SqlCommand("SELECT * FROM Product", connection);

            adapter.SelectCommand = selectFirmCommand;
            adapter.Fill(dataSet, "Firm");

            adapter.SelectCommand = selectStoreCommand;
            adapter.Fill(dataSet, "Store");

            adapter.SelectCommand = selectProductCommand;
            adapter.Fill(dataSet, "Product");
// Создание команд для обновления данных
            SqlCommandBuilder firmCommandBuilder = new SqlCommandBuilder(adapter);
            SqlCommandBuilder storeCommandBuilder = new SqlCommandBuilder(adapter);
            SqlCommandBuilder productCommandBuilder = new SqlCommandBuilder(adapter);

            adapter.UpdateCommand = firmCommandBuilder.GetUpdateCommand();
            adapter.Update(dataSet, "Firm");

            adapter.UpdateCommand = storeCommandBuilder.GetUpdateCommand();
            adapter.Update(dataSet, "Store");

            adapter.UpdateCommand = productCommandBuilder.GetUpdateCommand();
            adapter.Update(dataSet, "Product");
        }
private void LoadData()
        {
            // Загрузка данных в DataGridView
            dgvFirms.DataSource = dataSet.Tables["Firm"];
            dgvStores.DataSource = dataSet.Tables["Store"];
            dgvProducts.DataSource = dataSet.Tables["Product"];
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
{
    int storeID = Convert.ToInt32(txtStoreID.Text);
    int firmID = Convert.ToInt32(txtFirmID.Text);

    // Получение отчета на основе информации из БД
    DataTable resultTable = new DataTable("Report");
    resultTable.Columns.Add("ProductName", typeof(string));
    resultTable.Columns.Add("Quantity", typeof(int));
    resultTable.Columns.Add("Price", typeof(decimal));
    resultTable.Columns.Add("TotalPrice", typeof(decimal));

    DataRow[] productRows = dataSet.Tables["Product"].Select($"StoreID = {storeID}");
    foreach (DataRow productRow in productRows)
    {
        DataRow[] firmRows = productRow.GetParentRows("FK_Product_Store");
        if (firmRows.Length > 0 && Convert.ToInt32(firmRows[0]["ID"]) == firmID)
        {
            string productName = productRow["Name"].ToString();
            int quantity = Convert.ToInt32(productRow["Quantity"]);
            decimal price = Convert.ToDecimal(productRow["Price"]);
            decimal totalPrice = quantity * price;

            resultTable.Rows.Add(productName, quantity, price, totalPrice);
        }
    }
}
    
}
    }
}
