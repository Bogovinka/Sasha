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
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        DatabaseEntities db = new DatabaseEntities();
        public Menu()
        {
            InitializeComponent();
            eDg.ItemsSource = db.WarehouseEquipment.ToList();
            sDg.ItemsSource = db.WarehouseEquipmentS.ToList();
            AddE.Visibility = Visibility.Hidden;
            EditE.Visibility = Visibility.Hidden;
            DelE.Visibility = Visibility.Hidden;
        }
        public Menu(bool c)
        {
            InitializeComponent();
            eDg.ItemsSource = db.WarehouseEquipment.ToList();
            sDg.ItemsSource = db.WarehouseEquipmentS.ToList();
        }

        private void AddE_Click(object sender, RoutedEventArgs e)
        {
            AddE add = new AddE();
            if(add.ShowDialog() == true)
            {
                Equipment equipment = new Equipment()
                {
                    Name = add.nameT.Text,
                    Serial_num = add.serT.Text,
                    TypeID = (int)add.TypeT.SelectedValue,
                    EquipmentType = db.EquipmentType.Where(x => x.ID == (int)add.TypeT.SelectedValue).FirstOrDefault()
                };
                db.Equipment.Add(equipment);
                WarehouseEquipment warehouseEquipment = new WarehouseEquipment()
                {
                    EquipmentID = equipment.ID,
                    Equipment = equipment
                };
                db.WarehouseEquipment.Add(warehouseEquipment);
                db.SaveChanges();
                eDg.ItemsSource = db.WarehouseEquipment.ToList();
            }
        }

        private void EditE_Click(object sender, RoutedEventArgs e)
        {
            if (eDg.SelectedItem != null)
            {
                WarehouseEquipment warehouseEquipment = (WarehouseEquipment)eDg.SelectedItem;
                AddE add = new AddE(warehouseEquipment);
                if (add.ShowDialog() == true)
                {
                    Equipment equipment = warehouseEquipment.Equipment;
                    equipment.Name = add.nameT.Text;
                    equipment.Serial_num = add.serT.Text;
                    equipment.TypeID = (int)add.TypeT.SelectedValue;
                    equipment.EquipmentType = db.EquipmentType.Where(x => x.ID == (int)add.TypeT.SelectedValue).FirstOrDefault();
                    db.SaveChanges();
                    eDg.ItemsSource = db.WarehouseEquipment.ToList();
                }
            }
        }

        private void DelE_Click(object sender, RoutedEventArgs e)
        {
            if (eDg.SelectedItem != null)
            {
                WarehouseEquipment warehouseEquipment = (WarehouseEquipment)eDg.SelectedItem;
                WarehouseEquipmentS warehouseEquipmentS = new WarehouseEquipmentS()
                {
                    Equipment = warehouseEquipment.Equipment,
                    EquipmentID = warehouseEquipment.EquipmentID
                };
                db.WarehouseEquipmentS.Add(warehouseEquipmentS);
                db.WarehouseEquipment.Remove(warehouseEquipment);
                db.SaveChanges();
                eDg.ItemsSource = db.WarehouseEquipment.ToList();
                sDg.ItemsSource = db.WarehouseEquipmentS.ToList();
            }
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            Close();
        }
    }
}
