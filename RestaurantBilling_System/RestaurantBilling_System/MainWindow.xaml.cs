using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RestaurantBilling_System
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int numItems = 0;
        int numBills = 0;
        double billTotal = 0.0;

        List<Bill> allBills = new List<Bill>();
        List<Item> allItems = new List<Item>();

        List<Item> currBillItem = new List<Item>();

        List<string> billItems = new List<string>();
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonActive(Button button)
        {
            btnHome.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#000"));
            btnBills.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#000"));
            btnAbout.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#000"));
            btnEmployee.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#000"));
            button.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#2C3E50"));
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            buttonActive((Button)sender);

            Button btn = sender as Button;
            string btnName = btn.Name;
            if (btnName == "btnHome")
            {
                tbEmpName.Text = String.Empty;
                tbTabNum.Text = String.Empty;
                btnClear_Click(sender, e);
                mainTitle.Content = "Home";
                tbTotal.Text = "Total: ₹0.00";
            }
            else if(btnName == "btnBills")
            {
                PrintAllBills();
            }
            else if(btnName == "btnEmployee")
            {
                if(tbEmpName.Text != null)
                {
                    MessageBox.Show("Employee Name:- " + tbEmpName.Text +"","Employee");
                }
            }
            else if(btnName == "btnAbout")
            {
                //About button clicked
                MessageBox.Show("Ashish's WPF project","About");
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            empWel.Content = "Welcome";
            empName.Content = tbEmpName.Text;
            tbEmpName.Visibility = Visibility.Collapsed;
            tbTabNum.Visibility = Visibility.Collapsed;
            lbEmpName.Visibility = Visibility.Collapsed;
            lbTabNum.Visibility = Visibility.Collapsed;
        }

        

        public void InitMenu()
        {
            tbTotal.Text = "Total: ₹0.00";
            Item item1 = new Item("bev1", "Soda", 20, 0);
            Item item2 = new Item("bev2", "Coffee", 25, 0);
            Item item3 = new Item("bev3", "Mineral Water", 15, 0);
            Item item4 = new Item("bev4", "Juice", 60, 0);
            Item item5 = new Item("bev5", "Soup", 60, 0);
            Item item6 = new Item("bev6", "Tea", 20, 0);
            Item item7 = new Item("bev7", "Milk", 20, 0);

            Item item8 = new Item("app1", "Samosa", 40, 0);
            Item item9 = new Item("app2", "Chaat", 55, 0);
            Item item10 = new Item("app3", "Kachori", 25, 0);
            Item item11 = new Item("app4", "Crispy Corn", 70, 0);
            Item item12 = new Item("app5", "Finger Chips", 55, 0);
            Item item13 = new Item("app6", "Onion Pakoda", 40, 0);
            Item item14 = new Item("app7", "Manchurian", 50, 0);

            //Item item15 = new Item("main1", "Soda", 20, 0);
            Item item16 = new Item("main1", "Butter Chicken", 180, 0);
            Item item17 = new Item("main2", "Chicken Tikka", 180, 0);
            Item item18 = new Item("main3", "Chicken Korma", 190, 0);
            Item item19 = new Item("main4", "Jalfrezi", 185, 0);
            Item item20 = new Item("main5", "Dal Makhani", 170, 0);
            Item item21 = new Item("main6", "Paneer Makhani", 190, 0);
            Item item22 = new Item("main7", "Rogan Josh", 250, 0);
            Item item23 = new Item("main8", "Biriyani", 250, 0);

            Item item24 = new Item("des1", "Rasagola", 20, 0);
            Item item25 = new Item("des2", "Kalajam", 20, 0);
            Item item26 = new Item("des3", "Jalebi", 40, 0);
            Item item27 = new Item("des4", "Mysore Pak", 40, 0);
            Item item28 = new Item("des5", "Rasmalai", 50, 0);
            Item item29 = new Item("des6", "Gulab Jamun", 30, 0);
            Item item30 = new Item("des7", "Sandesh", 50, 0);



            allItems.Add(item1);
            allItems.Add(item2);
            allItems.Add(item3);
            allItems.Add(item4);
            allItems.Add(item5);
            allItems.Add(item6);
            allItems.Add(item7);
            allItems.Add(item8);
            allItems.Add(item9);
            allItems.Add(item10);
            allItems.Add(item11);
            allItems.Add(item12);
            allItems.Add(item13);
            allItems.Add(item14);
            allItems.Add(item16);
            allItems.Add(item17);
            allItems.Add(item18);
            allItems.Add(item19);
            allItems.Add(item20);
            allItems.Add(item23);
            allItems.Add(item21);
            allItems.Add(item22);
            allItems.Add(item24);
            allItems.Add(item25);
            allItems.Add(item26);
            allItems.Add(item27);
            allItems.Add(item28);
            allItems.Add(item29);
            allItems.Add(item30);
        }

        public void AddToBill(Item item)
        {
            numItems++;
            billTotal = billTotal + item.iPrice;
            string text = item.iName + " : ₹ " + item.iPrice;
            if (!billItems.Contains(text))
            {
                billItems.Add(text);
            }
            tbPreview.Text = string.Join(Environment.NewLine, billItems);
            tbTotal.Text = "Total: ₹ " + Convert.ToString(billTotal);
            currBillItem.Add(item);

        }

        public void RemoveFromBill(Item item)
        {
            numItems--;
            billTotal = billTotal - item.iPrice;
            //tbPreview.Text
            string text = item.iName + " : ₹ " + item.iPrice;
            billItems.Remove(text);
            tbPreview.Text = string.Join(Environment.NewLine, billItems);
            tbTotal.Text = "Total: ₹ " + Convert.ToString(billTotal);
            currBillItem.Remove(item);
        }

        public void PrintAllBills()
        {
            tbPreview.Clear();
            foreach(Bill bill in allBills)
            {
                tbPreview.AppendText("\n");
                tbPreview.AppendText("****** Last Bill ******"+Environment.NewLine);
                tbPreview.AppendText(String.Format("Employee Name: {0} {1}", tbEmpName.Text, Environment.NewLine));
                tbPreview.AppendText(String.Format("Date: {0} {1}", DateTime.Now, Environment.NewLine));
                tbPreview.AppendText(String.Format("Total Bill: ₹ {0} {1}", bill.grandTotal, Environment.NewLine));
                tbPreview.AppendText("****** END ******" + Environment.NewLine);

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mainTitle.Content = "Welcome";
            InitMenu();
        }


        private void item_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            string itemId = cb.Name;
            Item? item = allItems.Find(x => x.iId == itemId);
            if (item != null)
            {
                AddToBill(item);
            }
        }

        private void item_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            string itemId = cb.Name;
            Item? item = allItems.Find(x => x.iId == itemId);
            if (item != null)
            {
                RemoveFromBill(item);
            }
        }

        public void GenerateBillAndDisplay(Bill bill)
        {

            if(bill != null)
            {
                List<Item> items = bill.currBillItems;

                tbPreview.AppendText("\n");
                tbPreview.AppendText("*************************" + Environment.NewLine);
                
                tbPreview.AppendText(String.Format("Date: {0} {1}", DateTime.Now, Environment.NewLine));
                tbPreview.AppendText(String.Format("Total Bill: ₹ {0} {1}", bill.grandTotal, Environment.NewLine));
                tbPreview.AppendText("****** VISIT AGAIN ******" + Environment.NewLine);
            }
        }

        private void btnGenBill_Click(object sender, RoutedEventArgs e)
        {
            Bill bill = new Bill(++numBills,currBillItem,billTotal);
            if (bill.grandTotal > 0)
            {
                allBills.Add(bill);
            }
            else
            {
                MessageBox.Show("Add Items to Generate Bill", "Bill Info");
                return;
            }
            //btnClear_Click(sender, e);
            GenerateBillAndDisplay(bill);
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            //Uncheck all checkboxes inside container1
            foreach(var cb in FindVisualChildren<CheckBox>(container1))
            {
                cb.IsChecked = false;
            }

            //Uncheck all checkboxes inside container2
            foreach(var cb in FindVisualChildren<CheckBox>(container2))
            {
                cb.IsChecked = false;
            }
            tbTotal.Text = string.Empty;
        }

        //Helper function to uncheck all check-boxes atonce
        private IEnumerable<T> FindVisualChildren<T>(DependencyObject parent) where T : DependencyObject
        {
            for(int i=0;i<VisualTreeHelper.GetChildrenCount(parent);i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if(child is T)
                {
                    yield return (T)child;
                }

                foreach(var desendant in FindVisualChildren<T>(child))
                {
                    yield return desendant;
                }
            }
        }
    }


    public class Item
    {
        public string iId {  get; set; }

        public string iName { get; set; }

        public double iPrice { get; set; }

        private int iQty { get; set; }

        public double iTotal { get; set; }

        public Item(string _iId, string _iName,double _iPrice,int _iQty)
        {
            this.iId = _iId;
            this.iName = _iName;
            this.iPrice = _iPrice;
            this.iQty = _iQty;
            this.iTotal = iQty * iPrice;
        }

        public int getQty()
        {
            return iQty;
        }

        public void setQty()
        {
            this.iQty = iQty;
        }

        public double getTotal()
        {
            return iQty * iPrice;
        }
    }

    public class Bill
    {
        public int bId { get; set; }
        public List<Item> currBillItems { get; set; }
        public double grandTotal { get; set; }

        public Bill(int bId, List<Item> currBillItems, double grandTotal)
        {
            this.bId = bId;
            this.currBillItems = currBillItems;
            this.grandTotal = grandTotal;
        }
    }

}