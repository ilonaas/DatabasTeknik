using Microsoft.EntityFrameworkCore;
using ProductApp.Data;
using ProductApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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

namespace ProductApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<ProductEntity> products = new ObservableCollection<ProductEntity>();
        private readonly DataContexts _context;


        public MainWindow(DataContexts context)
        {
            InitializeComponent();
            _context = context;
            PopulateProduct().ConfigureAwait(false);
            PopulateCustomer().ConfigureAwait(false);  
            //hej hans
        }


        public async Task PopulateCustomer()
        {

            
        }
        public async Task PopulateProduct()
        {
            var c_collection = new ObservableCollection<KeyValuePair<string, int>>();

            foreach (var customer in await _context.Customers.ToListAsync())
                c_collection.Add(new KeyValuePair<string, int>(customer.Email, customer.Id));

            foreach (var c in c_collection)
            {
                cb_customer.Items.Add(c);
            }

            var collection = new ObservableCollection<KeyValuePair<string, int>>();

            foreach (var product in await _context.Products.ToListAsync())
                collection.Add(new KeyValuePair<string, int>(product.Name, product.Id));

            foreach (var p in collection)
            {
                cb_product.Items.Add(p);
            }
        }



        private async void btn_Add_Product_To_List_Click(object sender, RoutedEventArgs e)
        {
            // hämta produkten från combobox och spara in value in i id - lektion 7
            var selected_product = (KeyValuePair<string, int>)cb_product.SelectedItem;
            var id = selected_product.Value;
            var product = await _context.Products.FindAsync(id);
            products.Add(product);
    

        }
    

        private async void btn_Save_Order_Click(object sender, RoutedEventArgs e)
        {
            var customer = (KeyValuePair<string, int>)cb_customer.SelectedItem;

            var orderEntity = new OrderEntity()
            {
                // lägg till det som som behövs för en order entity
                // CustomerId hämtar du från customer combobox value
                Date = DateTime.Now,
                CustomerId = customer.Value
            };
            _context.Add(orderEntity);
            await _context.SaveChangesAsync();

            foreach (var product in products)
            {
                _context.Add(new OrderRowEntity
                {
                    OrderId = orderEntity.Id,
                    ProductId = product.Id
                });
                await _context.SaveChangesAsync();
            }



        }
    }
}
