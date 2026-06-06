using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Itable_DSA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        // AVAILABLE ITEMS
        ObservableCollection<Shopping> shopping =
            new ObservableCollection<Shopping>();

        // SHOPPING CART
        ObservableCollection<Shopping> shoppings =
            new ObservableCollection<Shopping>();

        public MainWindow()
        {
            InitializeComponent();

            ShoppingGrid.ItemsSource = shopping;
            AddtoCart.ItemsSource = shoppings;
        }

        // SHOPPING CLASS
        public class Shopping
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Price { get; set; }
        }

        // ADD ITEM
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtId.Text) ||
                    string.IsNullOrWhiteSpace(txtName.Text) ||
                    string.IsNullOrWhiteSpace(txtDescription.Text) ||
                    string.IsNullOrWhiteSpace(txtPrice.Text))
                {
                    MessageBox.Show(
                        "Please fill in all fields.",
                        "Missing Input",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);

                    return;
                }

                MessageBoxResult result = MessageBox.Show(
                    $"Product Details\n\n" +
                    $"ID: {txtId.Text}\n" +
                    $"Name: {txtName.Text}\n" +
                    $"Description: {txtDescription.Text}\n" +
                    $"Price: {txtPrice.Text}\n\n" +
                    $"Do you want to add this product?",
                    "Confirm Add Product",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    shopping.Add(new Shopping
                    {
                        Id = txtId.Text.Trim(),
                        Name = txtName.Text.Trim(),
                        Description = txtDescription.Text.Trim(),
                        Price = txtPrice.Text.Trim()
                    });

                    MessageBox.Show(
                        "Product added successfully!",
                        "Success",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);

                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        // REMOVE ITEM
        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            Shopping selectedShopping =
                ShoppingGrid.SelectedItem as Shopping;

            if (selectedShopping != null)
            {
                MessageBoxResult result = MessageBox.Show(
                    $"ID: {selectedShopping.Id}\n" +
                    $"Name: {selectedShopping.Name}\n\n" +
                    $"Remove this product?",
                    "Confirm Remove",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    shopping.Remove(selectedShopping);

                    MessageBox.Show(
                        "Product removed successfully!",
                        "Removed",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);

                    ClearFields();
                }
            }
            else
            {
                MessageBox.Show(
                    "Please select an item first.",
                    "No Selection",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
        }

        // ADD TO CART
        private void btnaddtoCart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Shopping selectedShopping =
                    ShoppingGrid.SelectedItem as Shopping;

                if (selectedShopping != null)
                {
                    MessageBoxResult result = MessageBox.Show(
                        $"ID: {selectedShopping.Id}\n" +
                        $"Name: {selectedShopping.Name}\n\n" +
                        $"Add this item to cart?",
                        "Confirm Add To Cart",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        shoppings.Add(new Shopping
                        {
                            Id = selectedShopping.Id,
                            Name = selectedShopping.Name,
                            Description = selectedShopping.Description,
                            Price = selectedShopping.Price
                        });

                        MessageBox.Show(
                            "Item added to cart successfully!",
                            "Cart Updated",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show(
                        "Please select an item first.",
                        "No Selection",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        // REMOVE FROM CART
        private void btnRemoveCart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Shopping selectedCartItem =
                    AddtoCart.SelectedItem as Shopping;

                if (selectedCartItem != null)
                {
                    MessageBoxResult result = MessageBox.Show(
                        $"ID: {selectedCartItem.Id}\n" +
                        $"Name: {selectedCartItem.Name}\n\n" +
                        $"Remove this item from cart?",
                        "Confirm Remove",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        shoppings.Remove(selectedCartItem);

                        MessageBox.Show(
                            "Item removed from cart!",
                            "Cart Updated",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show(
                        "Please select an item from the cart.",
                        "No Selection",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        // SEARCH PRODUCT
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string searchText =
                    txtSearch.Text.Trim().ToLower();

                if (string.IsNullOrWhiteSpace(searchText))
                {
                    MessageBox.Show(
                        "Please enter a Product ID or Product Name.",
                        "Search",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);

                    return;
                }

                Shopping foundItem =
                    shopping.FirstOrDefault(x =>
                        x.Id.ToLower().Contains(searchText) ||
                        x.Name.ToLower().Contains(searchText));

                if (foundItem != null)
                {
                    ShoppingGrid.SelectedItem = foundItem;
                    ShoppingGrid.ScrollIntoView(foundItem);

                    MessageBox.Show(
                        $"Product Found!\n\n" +
                        $"ID: {foundItem.Id}\n" +
                        $"Name: {foundItem.Name}\n" +
                        $"Description: {foundItem.Description}\n" +
                        $"Price: {foundItem.Price}",
                        "Search Result",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show(
                        "Product not found.",
                        "Search Result",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = txtSearch.Text.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                ShoppingGrid.ItemsSource = shopping;
                return;
            }

            var filteredItems = shopping.Where(x =>
                x.Id.ToLower().Contains(searchText) ||
                x.Name.ToLower().Contains(searchText) ||
                x.Description.ToLower().Contains(searchText) ||
                x.Price.ToLower().Contains(searchText));

            ShoppingGrid.ItemsSource =
                new ObservableCollection<Shopping>(filteredItems);
        }

        private void cmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbSort.SelectedItem == null)
                return;

            string selected =
                ((ComboBoxItem)cmbSort.SelectedItem)
                .Content.ToString();

            switch (selected)
            {
                case "ID Ascending":
                    ShoppingGrid.ItemsSource =
                        new ObservableCollection<Shopping>(
                            shopping.OrderBy(x => x.Id));
                    break;

                case "ID Descending":
                    ShoppingGrid.ItemsSource =
                        new ObservableCollection<Shopping>(
                            shopping.OrderByDescending(x => x.Id));
                    break;

                case "Name A-Z":
                    ShoppingGrid.ItemsSource =
                        new ObservableCollection<Shopping>(
                            shopping.OrderBy(x => x.Name));
                    break;

                case "Name Z-A":
                    ShoppingGrid.ItemsSource =
                        new ObservableCollection<Shopping>(
                            shopping.OrderByDescending(x => x.Name));
                    break;

                case "Price Low-High":
                    ShoppingGrid.ItemsSource =
                        new ObservableCollection<Shopping>(
                            shopping.OrderBy(x =>
                            {
                                decimal.TryParse(x.Price, out decimal p);
                                return p;
                            }));
                    break;

                case "Price High-Low":
                    ShoppingGrid.ItemsSource =
                        new ObservableCollection<Shopping>(
                            shopping.OrderByDescending(x =>
                            {
                                decimal.TryParse(x.Price, out decimal p);
                                return p;
                            }));
                    break;
            }
        }

        // GRID SELECTION
        private void ShoppingGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ShoppingGrid.SelectedItem is Shopping selectedShopping)
            {
                txtId.Text = selectedShopping.Id;
                txtName.Text = selectedShopping.Name;
                txtDescription.Text = selectedShopping.Description;
                txtPrice.Text = selectedShopping.Price;
            }
        }

        // CLEAR FIELDS
        private void ClearFields()
        {
            txtId.Clear();
            txtName.Clear();
            txtDescription.Clear();
            txtPrice.Clear();

            if (txtSearch != null)
                txtSearch.Clear();

            ShoppingGrid.SelectedItem = null;
        }

        // CLEAR BUTTON
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();

            MessageBox.Show(
                "Fields cleared successfully!",
                "Cleared",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
    }
}
