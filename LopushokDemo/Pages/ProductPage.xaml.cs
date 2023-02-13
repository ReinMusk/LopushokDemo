using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace LopushokDemo
{
    /// <summary>
    /// Interaction logic for ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {

        public Product Product { get; set; }

        public List<ProductType> ProductTypes { get; set; }
        public ProductPage(Product product, bool isNewProduct)
        {
            InitializeComponent();

            Product = product;
            ProductTypes = DBConnection.connection.ProductType.ToList();

            if (isNewProduct == true)
            {
                Title = "Новый продукт";
                btnDelete.Visibility = Visibility.Hidden;
            }
            else
                btnDelete.Visibility = Visibility.Visible;

            this.DataContext = this;
        }


        private void btnAddImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Filter = "*.png|*.png|*.jpeg|*.jpeg|*.jpg|*.jpg"
            };

            if (fileDialog.ShowDialog().Value)
            {
                var image = File.ReadAllBytes(fileDialog.FileName);
                Product.Image = image;

                imgProduct.Source = new BitmapImage(new Uri(fileDialog.FileName));
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!DBConnection.connection.Product.ToList().Any(x => x == Product))
                    DBConnection.connection.Product.Add(Product);
                DBConnection.connection.SaveChanges();
            }
            catch
            {
                MessageBox.Show("Введены некорректные значения", "Ошибка");
                return;
            }

            if (!DBConnection.connection.Product.ToList().Any(x => x == Product))
                DBConnection.connection.Product.Add(Product);
            DBConnection.connection.SaveChanges();
            NavigationService.GoBack();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы точно хотите удалить данный продукт?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.No)
                return;
            DBConnection.connection.Product.Remove(Product);
            DBConnection.connection.SaveChanges();
            NavigationService.GoBack();
        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
