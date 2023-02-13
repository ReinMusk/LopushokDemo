using System;
using System.Collections.Generic;
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
    /// Interaction logic for ProductsListPage.xaml
    /// </summary>
    public partial class ProductsListPage : Page
    {
        public static List<Product> ProductList { get; set; }
        public static List<Product> FilteredProducts { get; set; }
        public static List<ProductType> ProductTypesList { get; set; }
        public static Dictionary<string, int> Sorting { get; set; }


        public static int page;

        public ProductsListPage()
        {
            InitializeComponent();

            page = 0;
            

            ProductList = DBConnection.connection.Product.ToList();
            FilteredProducts = ProductList;

            ProductTypesList = DBConnection.connection.ProductType.ToList();
            ProductTypesList.Insert(0, new ProductType() { Name = "Все типы" });

            Sorting = new Dictionary<string, int>
            {
                { "По умолчанию", 0},
                { "По имени", 1},
                { "По имени по убыванию", 2},
                { "По цеху", 3},
                { "По цеху по убыванию", 4},
                { "По цене", 5},
                { "По цене по убыванию", 6}
            };

            this.DataContext = this;
        }

        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFiltres();
        }

        private void SortingCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFiltres();
        }

        private void TypesCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFiltres();
        }

        public void ApplyFiltres()
        {
            var search = SearchTB.Text.ToLower();
            var sorting = Sorting[SortingCB.SelectedItem as string];
            var prodType = TypesCB.SelectedItem as ProductType;

            FilteredProducts = FilteredProducts.FindAll(x => x.Name.ToLower().Contains(search)).ToList();

            if (sorting == null || prodType == null)
                return;

            if (prodType.Id != 0)
                FilteredProducts = FilteredProducts.FindAll(x => x.ProductType == prodType);

            switch (sorting)
            {
                case 0:
                    FilteredProducts.OrderBy(x => x.Id).ToList();
                    break;
                case 1:
                    FilteredProducts.OrderBy(x => x.Name).ToList();
                    break;
                case 2:
                    FilteredProducts.OrderByDescending(x => x.Name).ToList();
                    break;
                case 3:
                    FilteredProducts.OrderBy(x => x.Workshop).ToList();
                    break;
                case 4:
                    FilteredProducts.OrderByDescending(x => x.Workshop).ToList();
                    break;
                case 5:
                    FilteredProducts.OrderBy(x => x.MinPrice).ToList();
                    break;
                case 6:
                    FilteredProducts.OrderByDescending(x => x.MinPrice).ToList();
                    break;
            }

            ProductsLV.ItemsSource = FilteredProducts;
        }

        private void ButtonDown_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonUp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ProductsLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NavigationService.Navigate(new ProductPage(ProductsLV.SelectedItem as Product, true));
        }
    }
}
