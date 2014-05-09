using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace AIO_medier
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GetFile movies = new GetFile();
     
        string[] testarray = new string[] { "avi", "3gp", "mpeg", "mpg", "flv", "divx", "wmv", "mp4", "xvid", "mp4", "mkv" };
       
        public MainWindow()
        {
            InitializeComponent();

            //movies.readToList(ListMovies);
            
        }

        private void btmmovie_Click(object sender, RoutedEventArgs e)
        {
            movies.path(ListMovies, listfiletype, (bool)checkfiletype.IsChecked, "*" + combofiletype.SelectedItem);

           
            ListMovies.Items.Refresh();
        }

        
        //private string test()
        //{
        //    if (checkfiletype.IsChecked == true)
        //    {
                
        //    }
        //}

        private void ListMovies_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            movies.Open(this.ListMovies, this.listhistory,this.lblprogram);
        }

        private void BtnDeleteMovies_Click(object sender, RoutedEventArgs e)
        {
            //movies.delete(this.ListMovies);
        }

        private void ComboBox_DropDownOpened(object sender, EventArgs e)
        {
            combofiletype.ItemsSource = testarray;
        }

        private void btndeleteall_Click(object sender, RoutedEventArgs e)
        {
            //movies.fileNameList.Clear();
            //movies.filePathList.Clear();
            //listfiletype.Items.Clear();
            //listhistory.Items.Clear();
            //listhistory.Items.Refresh();
            //listfiletype.Items.Refresh();
            //ListMovies.Items.Refresh();            
        }

        private void btndeletehistory_Click(object sender, RoutedEventArgs e)
        {
            listhistory.Items.Clear();
        }

        private void ListMovies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //foreach (var item in movies.dict.Values)
            //{		 	
            //    if (item.Equals(ListMovies.SelectedItem))
            //    {
            //        string moviePath = item;
            //        FileInfo fi1 = new FileInfo(moviePath);

            //        lblmetacreationtime.Content = fi1.CreationTime;
            //        lblmetaname.Content = fi1.Name;
            //        lbltest.Content = fi1.LastWriteTime;

            //        break;
            //    }

            //}
        }

        private void btnprogram_Click(object sender, RoutedEventArgs e)
        {
                OpenFileDialog playerpath = new OpenFileDialog();
                playerpath.ShowDialog();
                lblprogram.Content = playerpath.FileName;
            
        }

        private void Tabmovies_Loaded(object sender, RoutedEventArgs e)
        {
            //movies.readToList(ListMovies);
        }
    }
}
    


