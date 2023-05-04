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
using System.Windows.Shapes;

namespace Stolnik
{
    /// <summary>
    /// Логика взаимодействия для AddE.xaml
    /// </summary>
    public partial class AddE : Window
    {
        DatabaseEntities db = new DatabaseEntities();
        public AddE()
        {
            InitializeComponent();
            TypeT.ItemsSource = db.EquipmentType.ToList();
        }
        public AddE(WarehouseEquipment equipment)
        {
            InitializeComponent();
            TypeT.ItemsSource = db.EquipmentType.ToList();
            nameT.Text = equipment.Equipment.Name;
            TypeT.Text = equipment.Equipment.EquipmentType.Name;
            serT.Text = equipment.Equipment.Serial_num;
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void createB_Click(object sender, RoutedEventArgs e)
        {
            if (nameT.Text != "" && TypeT.SelectedValue != null && serT.Text != "") DialogResult = true;
            else MessageBox.Show("Заполни все поля");
        }
    }
}
