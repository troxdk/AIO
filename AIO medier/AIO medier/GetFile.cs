using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Controls;
using System.Diagnostics;
using Microsoft.Win32;
using System.Windows;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Specialized;
//using System.Windows.Forms;

namespace AIO_medier
{

    public class GetFile
    {
        public List<string> filePathList { get; set; }
        public List<string> fileNameList { get; set; }

        //private SortedDictionary<string, string> dict;

        //public SortedDictionary<string, string> dict {get; set;}

        //http://www.dotnetperls.com/dictionary-binary

        public SortedDictionary<string, string> dict = new SortedDictionary<string, string>();

        public string[] path(ListBox listfiles, ListBox listfiletype, bool allfiles, string filetype = "")
        {
            SortedDictionary<string, string> dict2 = new SortedDictionary<string, string>();
            
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            fbd.ShowDialog();
            string path3 = fbd.SelectedPath;

            string[] pathhhh = Directory.GetFileSystemEntries(path3, filetype, SearchOption.AllDirectories);

            if (allfiles == true)
            {
                    pathhhh = (string[]) Directory.GetFileSystemEntries(path3, "*.*", SearchOption.AllDirectories) 
                                                                                .Where(s => s.EndsWith(".avi") ||
                                                                                s.EndsWith(".3gp") || s.EndsWith(".mpeg") ||
                                                                                s.EndsWith(".mpg") || s.EndsWith(".flv") ||
                                                                                s.EndsWith(".divx") || s.EndsWith(".wmv") ||
                                                                                s.EndsWith(".mp4") || s.EndsWith(".xvid") ||
                                                                                s.EndsWith(".mkv") || s.EndsWith(".jpg")).ToArray();
            }

            if (listfiletype.Items.Contains(path3))
            {
                MessageBox.Show("det virker!");
            }
            else if (!listfiletype.Items.Contains(path3))
            {
                listfiletype.Items.Add(path3);
            }

            dict2 = Read(@"C:\Users\TroxDK\Documents\Visual Studio 2012\Projects\AIO medier\AIO medier\bin\Debug\dictionary.bin");

            

            foreach (string item in pathhhh)
            {
                string filePath = Path.GetFullPath(item);
                int getLastBackSlash = filePath.LastIndexOf(@"\");
                string fileName = filePath.Remove(0, getLastBackSlash+1);
                int getLastDot = fileName.LastIndexOf(".");
                fileName = fileName.Remove(getLastDot);

                if (dict2.ContainsKey(item))
                {
                    MessageBox.Show("Stien er allerede tilføjet");
                    break;
                }
                else
                {
                    dict2.Add(filePath, fileName);
                }
            }

            fileNameList = new List<string>();
            filePathList = new List<string>();

            var items = from pair in dict2
                        orderby pair.Value ascending
                        select pair;

            foreach (KeyValuePair<string, string> pair in items)
            {
                fileNameList.Add(pair.Value);
                filePathList.Add(pair.Key);
            }

            for (int i = 0; i < fileNameList.Count; i++)
            {
                dict.Add(filePathList[i], fileNameList[i]);
            }


            Write(dict, @"C:\Users\TroxDK\Documents\Visual Studio 2012\Projects\AIO medier\AIO medier\bin\Debug\dictionary.bin");

            SortedDictionary<string, string> dict3 = new SortedDictionary<string, string>();
            dict3 = Read(@"C:\Users\TroxDK\Documents\Visual Studio 2012\Projects\AIO medier\AIO medier\bin\Debug\dictionary.bin");

            listfiles.ItemsSource = dict3.Values;
            return pathhhh;
        }

        //public void delete(ListBox ListMovies)
        //{
        //    for (int i = 0; i < fileNameList.Count; i++)
        //    {
        //        if (fileNameList[i].Equals(ListMovies.SelectedItem))
        //        {
        //            filePathList.Remove(filePathList[i]);
        //            fileNameList.Remove(fileNameList[i]);
        //            ListMovies.Items.Refresh();
        //        }
        //    }
        //}

        public void Open(ListBox ListMovies, ListBox listhistory, Label label)
        {
            List<string> fileNameList = new List<string>();

            foreach (var item in dict.Keys)
	        {
		    fileNameList.Add(item);
	        }

            int i = 0;
            string path3 = ListMovies.SelectedItem.ToString();
            try
            {
            foreach (var item in dict.Values)
	        {
                
                if (item.Equals(ListMovies.SelectedItem))
                {

                    var moviePath = fileNameList[i];

                    Process.Start(label.Content.ToString(), "/play " + "\"" + moviePath + "\"");
                    break;
                }
                i++;
            }
            }
            catch (Exception)
            {
                MessageBox.Show("Du har ikke valgt en afspiller, eller valgt en afspiller der ikke er kompatibel med filtypen");
            }

            if (listhistory.Items.Contains(ListMovies.SelectedItem)) { }
            else
            {
                listhistory.Items.Add(ListMovies.SelectedItem);
            }
        }

        static void Write(SortedDictionary<string, string> dictionary, string file)
        {
            using (FileStream fs = File.OpenWrite(file))
            using (BinaryWriter writer = new BinaryWriter(fs))
            {
                // Put count.
                writer.Write(dictionary.Count);
                // Write pairs.
                foreach (var pair in dictionary)
                {
                    writer.Write(pair.Key);
                    writer.Write(pair.Value);
                }
            }
        }

        static SortedDictionary<string, string> Read(string file)
        {
            var result = new SortedDictionary<string, string>();
            using (FileStream fs = File.OpenRead(file))
            using (BinaryReader reader = new BinaryReader(fs))
            {
                // Get count.
                int count = reader.ReadInt32();
                // Read in all pairs.
                for (int i = 0; i < count; i++)
                {
                    string key = reader.ReadString();
                    string value = reader.ReadString();
                    result[key] = value;
                }
            }
            return result;
        }

        public void readToList(ListBox listmovies)
        {
            var dictionary = Read(@"C:\Users\TroxDK\Documents\Visual Studio 2012\Projects\AIO medier\AIO medier\bin\Debug\dictionary.bin");

            dict = new SortedDictionary<string, string>();

            if (listmovies.Items.IsEmpty == true)
            {
                foreach (var pair in dictionary)
                {
                    dict.Add(pair.Key, pair.Value);
                }
            }
            listmovies.ItemsSource = dict.Values;
        }
    }
}
