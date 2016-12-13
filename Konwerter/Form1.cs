using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Configuration;

namespace Konwerter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

       
        FolderBrowserDialog folder = new FolderBrowserDialog();
        FolderBrowserDialog folder1 = new FolderBrowserDialog();
        FolderBrowserDialog folder2 = new FolderBrowserDialog();
        FolderBrowserDialog folder3 = new FolderBrowserDialog();

        string sciezka1;
        string sciezka2;
        string sciezka3;
        string sciezka4;



        private void Form1_Load(object sender, EventArgs e)
        {


            textBox1.Text = Properties.Settings.Default.files1Path;
            textBox2.Text = Properties.Settings.Default.files2Path;
            textBox3.Text = Properties.Settings.Default.files3Path;
            textBox4.Text = Properties.Settings.Default.files4Path;
     
            button3.BackColor = Color.Yellow;

 
           
        }







        private void button1_Click(object sender, EventArgs e)
        {
         
          
            folder.ShowDialog();
            
            textBox1.Text = folder.SelectedPath.ToString();
            sciezka1 = textBox1.Text;
         Properties.Settings.Default.files1Path = folder.SelectedPath.ToString();
       
         Properties.Settings.Default.Save();
        
        }

        private void button2_Click(object sender, EventArgs e)
        {
      
            folder1.ShowDialog();
          //  sciezka2 = folder1.SelectedPath.ToString();
            textBox2.Text = folder1.SelectedPath.ToString();
            sciezka2 = textBox2.Text;

            Properties.Settings.Default.files2Path = folder1.SelectedPath.ToString();
            Properties.Settings.Default.Save();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.BackColor = Color.Red;
            sciezka1 = textBox1.Text;
            sciezka2 = textBox2.Text;
            sciezka3 = textBox3.Text;
            sciezka4 = textBox4.Text;


            if(checkBox1.Checked){
            DirectoryInfo d = new DirectoryInfo(sciezka1);//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.txt"); //Getting Text files
            foreach (FileInfo file in Files)
            {
                StreamReader streamReader = new StreamReader(file.FullName.ToString(), Encoding.Default, false);
             
                string text = streamReader.ReadToEnd();



                streamReader.Close();


                string nowy_plik = bank_bgz(text);


                string filepath = textBox3.Text+"\\"+file.Name.ToString();


                StreamWriter streamWriter = new StreamWriter(filepath, false, Encoding.Default);


                string s = Regex.Replace(nowy_plik, "\n", "\r\n");

                string end = s.Replace("?", "");


                streamWriter.Write(end.ToString());
                streamWriter.Close();
            }
            }
                if(checkBox2.Checked){
            DirectoryInfo ep = new DirectoryInfo(sciezka2);//Assuming Test is your Folder
            FileInfo[] Filese = ep.GetFiles("*.txt"); //Getting Text files
            foreach (FileInfo files in Filese)
            {
                StreamReader streamReadere = new StreamReader(files.FullName.ToString(), Encoding.GetEncoding(28592), false);
                string text = streamReadere.ReadToEnd();

                streamReadere.Close();


                
                




                string s = Regex.Replace(text, "\n", ""); // tu bylo \r\n

                string end = s.Replace("?", "");


                Regex schemat_liczbaspecjalna = new Regex("\\:[8][6]\\:[a-z0-9A-ZąęćźżśółńĄĘĆŻŹŚÓŁŃ\\/\\s\\,\\.\\-\\(\\)]*\\:[6]");
                foreach (Match match in schemat_liczbaspecjalna.Matches(end)) { 
                
                    string linia =match.ToString(); // mamy linię
                    string linia_oryg = linia;
                  //  string linka = linia.Replace("\r\n","");
                    linia = linia.Replace("\r", "");
                    string numer_konta = "";
                    string tytul = "";
                    string nazwisko = "";
                    string num_konta = "";
                    string num_kontaa = "";
                    string num_kontaaa ="";
                    string num_konta1 = "";
                    string num_konta2 = "";

                    Regex schemat_liczbaspecjalna1 = new Regex("[B][I][/][0-9\r\n]*[/][B][N]");
                    foreach (Match match1 in schemat_liczbaspecjalna1.Matches(linia)) {
                        numer_konta = match1.ToString();
                                            }

                    if (numer_konta != "") {
                        num_konta = numer_konta.Remove(0, 3);
                        num_kontaa = num_konta.Remove(num_konta.Length - 3, 3);
                        num_kontaaa = num_kontaa.Replace("\r", "").Replace("\r\n", "").Replace("\n", "");
                        num_konta1 = num_kontaaa.Substring(2, 8);
                        num_konta2 = num_kontaaa.Substring(10, 16);
                    }
                    else if (numer_konta == "")
                    {

                        Regex schemat_liczbaspecjalnax = new Regex("[0-9]{26}");
                        foreach (Match matchx in schemat_liczbaspecjalnax.Matches(linia))
                        {
                            numer_konta = matchx.ToString();
                        }

                        if (numer_konta == "")
                        {
                            Regex schemat_liczbaspecjalnax1 = new Regex("([0-9\r]{27})||([0-9\r]{28})");
                            foreach (Match matchx1 in schemat_liczbaspecjalnax1.Matches(linia))
                            {
                                numer_konta = matchx1.ToString();
                            }
                            if (numer_konta == "")
                            {
                                num_kontaaa = "";
                                num_konta1 = "";
                                num_konta1 = "";
                            }
                            else
                            {
                                num_kontaaa = numer_konta.Replace("\r", "").Replace("\r\n", "").Replace("\n", "");
                                num_konta1 = num_kontaaa.Substring(2, 8);
                                num_konta2 = num_kontaaa.Substring(10, 16);
                            }

                        }
                        else
                        {
                            num_kontaaa = numer_konta.Replace("\r", "").Replace("\r\n", "").Replace("\n", "");
                            num_konta1 = num_kontaaa.Substring(2, 8);
                            num_konta2 = num_kontaaa.Substring(10, 16);
                        }
                    }
               

                 

                    Regex schemat_liczbaspecjalna2 = new Regex("[P][Y]\\/[a-z0-9A-ZąęćźżśółńĄĘĆŻŹŚÓŁŃ\\/\\s\\,\\.\\-]*\\/[B][I]\\/");
                    foreach (Match match2 in schemat_liczbaspecjalna2.Matches(linia))
                    {
                        tytul = match2.ToString(); 
                    }
                    if (tytul == "")
                    { 
                    Regex schemat_liczbaspecjalna2t = new Regex("[P][Y]\\/[a-z0-9A-ZąęćźżśółńĄĘĆŻŹŚÓŁŃ\\/\\s\\,\\.\\-]*");
                    foreach (Match match2t in schemat_liczbaspecjalna2t.Matches(linia))
                    {
                        tytul = match2t.ToString();
                    }
                    }

                    Regex schemat_liczbaspecjalna3 = new Regex("[B][N]\\/[a-z0-9A-ZąęćźżśółńĄĘĆŻŹŚÓŁŃ\\/\\s\\,\\.\\-]*\\/[A][B]\\/");
                    foreach (Match match3 in schemat_liczbaspecjalna3.Matches(linia))
                    {
                        nazwisko = match3.ToString(); 
                    }

                    string nowa_linia = ":86:<00X<101<20" + tytul.Replace("PY/", "").Replace("/BI/", "").Replace("\r\n", " ").Replace("\r", " ") + "<27" + nazwisko.Replace("BN/", "").Replace("/AB/", "") + "<30" + num_konta1 + "<31" + num_konta2 + "<32" + nazwisko.Replace("BN/", "").Replace("/AB/", "") + "\r\n:6";

                    
                    end = end.Replace(linia_oryg, nowa_linia);

                }

                //uzupelniamy wiersze bez linii 86  (tylko 61)

             

                string filepath = textBox4.Text + "\\" + files.Name.ToString();
                StreamWriter streamWritere = new StreamWriter(filepath, false, Encoding.GetEncoding(28592));
              end =  Regex.Replace(end, "\r\n", "\r");
              end = Regex.Replace(end, "\r", "\r\n");

              Regex schemat_linia61 = new Regex("\\/CTC\\/[a-zA-Z0-9\\s\\/]*\\r\\n\\:61\\:");
              foreach (Match match_linia61 in schemat_linia61.Matches(end))
              {
                  string linia_oryginalna = match_linia61.ToString();
                  string linia_skrocona = match_linia61.ToString().Substring(0, linia_oryginalna.Length - 4);
                  string linia_nowa = linia_skrocona + ":86:<00X<101<20Transakcja bez szczegolow\r\n:61:";

                  end = end.Replace(linia_oryginalna, linia_nowa);
              }

                streamWritere.Write(end.ToString());
                streamWritere.Close();
            }
                }




            button3.BackColor = Color.Green;
            
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            folder2.ShowDialog();
            textBox3.Text = folder2.SelectedPath.ToString();
            sciezka3 = textBox3.Text;

            Properties.Settings.Default.files3Path = folder2.SelectedPath.ToString();
            Properties.Settings.Default.Save();

        }

        private string bank_bgz(string tresc_pliku) { 
        
            // szukamy linii
            string nowa_tresc = "";

            tresc_pliku = tresc_pliku.Replace("_", "");
            tresc_pliku = tresc_pliku.Replace("NIP:", "NIP ").Replace("REGON:","REGON ").Replace("DEKL.:","DEKL. ");
            
             Regex schemat_liczbaspecjalna = new Regex("\\:[8][6]\\:[a-z0-9A-ZąęćźżśółńĄĘĆŻŹŚÓŁŃ\\/\\s\\[\\]\\,\\.\\;\\t\"\\'\\-\\(\\)\\?\\r\\n]*\\:[6]");
             foreach (Match match in schemat_liczbaspecjalna.Matches(tresc_pliku))
             {

                 string nowa_linia = "";

                 string linia_do_podmiany = match.ToString();
                 string linia = match.ToString();

                 linia = linia.Substring(13, linia.Length - 13);

                 int rodzaj_przelewu = 0;

                 Regex schemat_p_obciazeniowy = new Regex("PRZELEW OBCIĄŻENIOWY\\?");
                 foreach (Match match_obciazeniowy in schemat_p_obciazeniowy.Matches(linia))
                 {
                     rodzaj_przelewu = 1;
                 }

                 Regex schemat_p_obciazeniowy2 = new Regex("PRZELEW OBCIĄŻENIOWY[a-zA-Z0-9ąęćźżśółńĄĘĆŻŹŚÓŁŃ\\s]*");
                 foreach (Match match_obciazeniowy2  in schemat_p_obciazeniowy2.Matches(linia)){
                 rodzaj_przelewu =6;
                 }
                 
                


                 Regex schemat_p_uznaniowy = new Regex("PRZELEW UZNANIOWY");
                 foreach (Match match_uznaniowy in schemat_p_uznaniowy.Matches(linia))
                 {
                     rodzaj_przelewu = 2;
                 }


                 Regex schemat_p_wewnetrzny = new Regex("PRZELEW WEWNĘTRZNY");
                 foreach (Match match_wewenetrzny in schemat_p_wewnetrzny.Matches(linia))
                 {

                     rodzaj_przelewu = 3;

                 }

                 Regex schemat_p_z_rachunku = new Regex("PRZELEW Z RACHUNKU");
                 foreach (Match match_z_rachunku in schemat_p_z_rachunku.Matches(linia))
                 {
                     rodzaj_przelewu = 3;
                 }

                 Regex schemat_pobranie_prowizji = new Regex("POBRANIE PROWIZJI");
                 foreach (Match match_pobranie_prowizji in schemat_pobranie_prowizji.Matches(linia))
                 {

                     rodzaj_przelewu = 4;
                 }

                 Regex schemat_wplata_zamknieta = new Regex("WPŁATA ZAMKNIĘTA");
                 foreach (Match match_wplata_zamknieta in schemat_wplata_zamknieta.Matches(linia))
                 {
                     rodzaj_przelewu = 4;

                 }

                 if (rodzaj_przelewu == 0)
                 {
                     rodzaj_przelewu = 5;
                 }

                 switch (rodzaj_przelewu)
                 {



                     case 1:

                         nowa_linia = linia_obciazeniowy(linia);

                         break;

                     case 2:

                         nowa_linia = linia_uznaniowy(linia);

                         break;

                     case 3:

                         nowa_linia = linia_wewnetrzny(linia);

                         break;

                     case 4:

                         nowa_linia = linia_pobr_prowizji(linia);

                         break;

                     case 5: // opcja, że nie znalazło nam jednak nic..

                         nowa_linia = linia_pobr_prowizji(linia);

                         break;

                     case 6:
                         nowa_linia = linia_obciazeniowy2(linia);
                         break;


                 }

                 tresc_pliku = tresc_pliku.Replace(linia_do_podmiany, nowa_linia);
                 rodzaj_przelewu = 0;
             }

             nowa_tresc = tresc_pliku;


             return nowa_tresc;
        
        }

        private string linia_obciazeniowy(string linia)
        {
            string tytul_przelewu ="";
            string kontrahent_nazwa = "";
            string kontrahent_adres = "";
            string kontrahent_ulica = "";
            string kontrahent_miasto = "";
            string numer_konta = "";
            string numer_konta_1 = "";
            string numer_konta_2 = "";
            string linia_nowa = "";


            int licznik = 0;
            Regex schemat = new Regex("[a-z0-9A-ZąęćźżśółńĄĘĆŻŹŚÓŁŃ\\/\\s\\,\\.\\-\\(\\)]*\\?");
            foreach (Match match in schemat.Matches(linia))
            {

                if (licznik == 0)
                {
                   
                }
                else if (licznik == 1)
                {
                    tytul_przelewu = match.ToString().Replace("?", "");
                }

                else if (licznik == 2)
                {
                    kontrahent_nazwa = match.ToString().Replace("?", "");

                   

                
                }
                else if (licznik == 3) {
                    kontrahent_adres = match.ToString().Replace("?", "");

                    Regex schemat_ulica = new Regex("UL\\.\\s[A-Za-z0-9\\s\\.\\-\\/ńŃęĘóÓśŚąAńŃćĆżŻźŹ]*[0-9]{2}\\-[0-9]{3}");
                    foreach (Match match_ulica in schemat_ulica.Matches(kontrahent_adres))
                    {
                        kontrahent_ulica = match_ulica.ToString().Substring(0, match_ulica.Length - 6);
                    }
                    Regex schemat_miasto = new Regex("[0-9]{2}\\-[0-9]{3}\\s[A-Za-z0-9\\s\\.\\-\\/ńŃęĘóÓśŚąAńŃćĆżŻźŹ]*\\?");
                    foreach (Match match_miasto in schemat_miasto.Matches(match.ToString()))
                    {
                        kontrahent_miasto = match_miasto.ToString().Substring(6, match_miasto.Length - 6).Replace("?", "");
                    }


                }

         


                licznik++;
                // czwórka numer konta

            }


            if (kontrahent_adres == "")
            {
                Regex schemat_ulica = new Regex("UL\\.\\s[A-Za-z0-9\\s\\.\\-\\/ńŃęĘóÓśŚąAńŃćĆżŻźŹ]*[0-9]{2}\\-[0-9]{3}");
                foreach (Match match_ulica in schemat_ulica.Matches(kontrahent_nazwa))
                {
                    kontrahent_ulica = match_ulica.ToString().Substring(0, match_ulica.Length - 6);
                }
                Regex schemat_miasto = new Regex("[0-9]{2}\\-[0-9]{3}\\s[A-Za-z0-9\\s\\.\\-\\/ńŃęĘóÓśŚąAńŃćĆżŻźŹ]*");
                foreach (Match match_miasto in schemat_miasto.Matches(kontrahent_nazwa))
                {
                    kontrahent_miasto = match_miasto.ToString().Substring(6, match_miasto.Length - 6).Replace("?", "");
                }
            }

            Regex schmemat_nr_banku = new Regex("[0-9]{2}\\s[0-9]{4}\\s[0-9]{4}\\s[0-9]{4}\\s[0-9]{4}\\s[0-9]{4}\\s[0-9]{4}");
            foreach (Match match_nr_banku in schmemat_nr_banku.Matches(linia))
            {
                
                numer_konta = match_nr_banku.ToString().Replace("?", "").Replace(" ", "");
         
            }

            if (numer_konta != "")
            {
                numer_konta_1 = numer_konta.Substring(2, 8);
                numer_konta_2 = numer_konta.Substring(10, 16);
            }

            if (tytul_przelewu == "" || tytul_przelewu == " ") {
                tytul_przelewu = "Przelew";
            }

            linia_nowa = ":86:<00X<101<20" + tytul_przelewu + "<27" + kontrahent_nazwa + "<29" + kontrahent_ulica + "<30" + numer_konta_1 + "<31" + numer_konta_2 + "<32" + kontrahent_nazwa + "<60" + kontrahent_miasto + "\r\n:6";
            
            return linia_nowa; 
        
        }

        private string linia_obciazeniowy2(string linia)
        {
            string tytul_przelewu = "";
            string kontrahent_nazwa = "";
            string kontrahent_adres = "";
            string kontrahent_ulica = "";
            string kontrahent_miasto = "";
            string numer_konta = "";
            string numer_konta_1 = "";
            string numer_konta_2 = "";
            string linia_nowa = "";


            int licznik = 0;
            Regex schemat = new Regex("[a-z0-9A-ZąęćźżśółńĄĘĆŻŹŚÓŁŃ\\/\\s\\,\\.\\-\\(\\)]*\\?");
            foreach (Match match in schemat.Matches(linia))
            {

                if (licznik == 0)
                {

                }
                else if (licznik == 1)
                {
                    kontrahent_nazwa = match.ToString().Replace("?", "");
                }

                else if (licznik == 2)
                {

                    tytul_przelewu = match.ToString().Replace("?", "");
                    




                }
                else if (licznik == 3)
                {
                    kontrahent_adres = match.ToString().Replace("?", "");

                    Regex schemat_ulica = new Regex("UL\\.\\s[A-Za-z0-9\\s\\.\\-\\/ńŃęĘóÓśŚąAńŃćĆżŻźŹ]*[0-9]{2}\\-[0-9]{3}");
                    foreach (Match match_ulica in schemat_ulica.Matches(kontrahent_adres))
                    {
                        kontrahent_ulica = match_ulica.ToString().Substring(0, match_ulica.Length - 6);
                    }
                    Regex schemat_miasto = new Regex("[0-9]{2}\\-[0-9]{3}\\s[A-Za-z0-9\\s\\.\\-\\/ńŃęĘóÓśŚąAńŃćĆżŻźŹ]*\\?");
                    foreach (Match match_miasto in schemat_miasto.Matches(match.ToString()))
                    {
                        kontrahent_miasto = match_miasto.ToString().Substring(6, match_miasto.Length - 6).Replace("?", "");
                    }


                }




                licznik++;
                // czwórka numer konta

            }


            if (kontrahent_adres == "")
            {
                Regex schemat_ulica = new Regex("UL\\.\\s[A-Za-z0-9\\s\\.\\-\\/ńŃęĘóÓśŚąAńŃćĆżŻźŹ]*[0-9]{2}\\-[0-9]{3}");
                foreach (Match match_ulica in schemat_ulica.Matches(kontrahent_nazwa))
                {
                    kontrahent_ulica = match_ulica.ToString().Substring(0, match_ulica.Length - 6);
                }
                Regex schemat_miasto = new Regex("[0-9]{2}\\-[0-9]{3}\\s[A-Za-z0-9\\s\\.\\-\\/ńŃęĘóÓśŚąAńŃćĆżŻźŹ]*");
                foreach (Match match_miasto in schemat_miasto.Matches(kontrahent_nazwa))
                {
                    kontrahent_miasto = match_miasto.ToString().Substring(6, match_miasto.Length - 6).Replace("?", "");
                }
            }

            Regex schmemat_nr_banku = new Regex("[0-9]{2}\\s[0-9]{4}\\s[0-9]{4}\\s[0-9]{4}\\s[0-9]{4}\\s[0-9]{4}\\s[0-9]{4}");
            foreach (Match match_nr_banku in schmemat_nr_banku.Matches(linia))
            {

                numer_konta = match_nr_banku.ToString().Replace("?", "").Replace(" ", "");

            }

            if (numer_konta != "")
            {
                numer_konta_1 = numer_konta.Substring(2, 8);
                numer_konta_2 = numer_konta.Substring(10, 16);
            }


            if (tytul_przelewu == "" || tytul_przelewu == " ")
            {
                tytul_przelewu = "Przelew";
            }

            linia_nowa = ":86:<00X<101<20" + tytul_przelewu + "<27" + kontrahent_nazwa + "<29" + kontrahent_ulica + "<30" + numer_konta_1 + "<31" + numer_konta_2 + "<32" + kontrahent_nazwa +  "<60" + kontrahent_miasto + "\r\n:6";

            return linia_nowa;

        }


        private string linia_wewnetrzny(string linia) {

            string tytul_przelewu = "";
            string kontrahent_nazwa = "";
            string kontrahent_adres = "";
            string numer_konta = "";
            string numer_konta_1 = "";
            string numer_konta_2 = "";
            string linia_nowa = "";
            string kontrahent_ulica = "";
            string kontrahent_miasto = "";

            int licznik = 0;
            Regex schemat = new Regex("[a-z0-9A-ZąęćźżśółńĄĘĆŻŹŚÓŁŃ\\/\\s\\,\\.\\-\\(\\)]*\\?");
            foreach (Match match in schemat.Matches(linia))
            {
                if (licznik == 0)
                {
                    //tu mamy przelew wewnętrzny i numer rachunku
                }
                else if (licznik == 1)
                {
                    kontrahent_nazwa = match.ToString().Replace("?", "");
                }

                else if (licznik == 2)
                {
                    kontrahent_adres = match.ToString().Replace("?", "");

                    Regex schemat_ulica = new Regex("UL\\.\\s[A-Za-z0-9\\s\\.\\-\\/ńŃęĘóÓśŚąAńŃćĆżŻźŹ]*[0-9]{2}\\-[0-9]{3}");
                    foreach (Match match_ulica in schemat_ulica.Matches(kontrahent_adres))
                    {
                        kontrahent_ulica = match_ulica.ToString().Substring(0, match_ulica.Length - 6);
                    }
                    Regex schemat_miasto = new Regex("[0-9]{2}\\-[0-9]{3}\\s[A-Za-z0-9\\s\\.\\-\\/ńŃęĘóÓśŚąAńŃćĆżŻźŹ]*\\?");
                    foreach (Match match_miasto in schemat_miasto.Matches(match.ToString()))
                    {
                        kontrahent_miasto = match_miasto.ToString().Substring(6, match_miasto.Length - 6).Replace("?", "");
                    }
                    
                }
                else if (licznik == 3)
                {
                    tytul_przelewu = match.ToString().Replace("?", "");
                    
                }

                if (kontrahent_adres == "")
                {
                    Regex schemat_ulica = new Regex("UL\\.\\s[A-Za-z0-9\\s\\.\\-\\/ńŃęĘóÓśŚąAńŃćĆżŻźŹ]*[0-9]{2}\\-[0-9]{3}");
                    foreach (Match match_ulica in schemat_ulica.Matches(kontrahent_nazwa))
                    {
                        kontrahent_ulica = match_ulica.ToString().Substring(0, match_ulica.Length - 6);
                    }
                    Regex schemat_miasto = new Regex("[0-9]{2}\\-[0-9]{3}\\s[A-Za-z0-9\\s\\.\\-\\/ńŃęĘóÓśŚąAńŃćĆżŻźŹ]*");
                    foreach (Match match_miasto in schemat_miasto.Matches(kontrahent_nazwa))
                    {
                        kontrahent_miasto = match_miasto.ToString().Substring(6, match_miasto.Length - 6).Replace("?", "");
                    }
                }

                licznik++;

            }

            Regex schmemat_nr_banku = new Regex("[0-9]{2}\\s[0-9]{4}\\s[0-9]{4}\\s[0-9]{4}\\s[0-9]{4}\\s[0-9]{4}\\s[0-9]{4}");
            foreach (Match match_nr_banku in schmemat_nr_banku.Matches(linia))
            {

                numer_konta = match_nr_banku.ToString().Replace("?", "").Replace(" ", "");

            }

            if (numer_konta != "")
            {
                numer_konta_1 = numer_konta.Substring(2, 8);
                numer_konta_2 = numer_konta.Substring(10, 16);
            }



            if (tytul_przelewu == "" || tytul_przelewu == " ")
            {
                tytul_przelewu = "Przelew";
            }

            linia_nowa = ":86:<00X<101<20" + tytul_przelewu + "<27" + kontrahent_nazwa + "<29" + kontrahent_ulica + "<30" + numer_konta_1 + "<31" + numer_konta_2 + "<32" + kontrahent_nazwa + "<60" + kontrahent_miasto + "\r\n:6";
            



            return linia_nowa; 
        }

        private string linia_uznaniowy(string linia) {

            string tytul_przelewu = "";
            string kontrahent_nazwa = "";
            string kontrahent_adres = "";
            string numer_konta = "";
            string numer_konta_1 = "";
            string numer_konta_2 = "";
            string linia_nowa = "";
            string kontrahent_miasto = "";
            string kontrahent_ulica = "";


            int licznik = 0;
            Regex schemat = new Regex("[a-z0-9A-ZąęćźżśółńĄĘĆŻŹŚÓŁŃ\\/\\s\\,\\.\\-\\(\\)]*\\?");
            foreach (Match match in schemat.Matches(linia))
            {
                if (licznik == 0)
                {
                    //tu mamy przelew wewnętrzny i numer rachunku
                }
                else if (licznik == 1)
                {
                    tytul_przelewu = match.ToString().Replace("?", "");
                   
                }

                else if (licznik == 2)
                {
                    kontrahent_nazwa = match.ToString().Replace("?", "");
                    

                }
                else if (licznik == 3)
                {
                    kontrahent_adres = match.ToString().Replace("?", "");
                    Regex schemat_ulica = new Regex("UL\\.\\s[A-Za-z0-9\\s\\.\\-\\\\/ńŃęĘóÓśŚąAńŃćĆżŻźŹ]*[0-9]{2}\\-[0-9]{3}");
                    foreach (Match match_ulica in schemat_ulica.Matches(kontrahent_adres))
                    {
                        kontrahent_ulica = match_ulica.ToString().Substring(0, match_ulica.Length - 6);
                    }
                    Regex schemat_miasto = new Regex("[0-9]{2}\\-[0-9]{3}\\s[A-Za-z0-9\\s\\.\\-\\/ńŃęĘóÓśŚąAńŃćĆżŻźŹ]*\\?");
                    foreach (Match match_miasto in schemat_miasto.Matches(match.ToString()))
                    {
                        kontrahent_miasto = match_miasto.ToString().Substring(6, match_miasto.Length - 6).Replace("?", "");
                    }

                }

                //czwórka numer konta

                licznik++;

            }



            if (kontrahent_adres == "")
            {
                Regex schemat_ulica = new Regex("UL\\.\\s[A-Za-z0-9\\s\\.\\-\\\\/ńŃęĘóÓśŚąAńŃćĆżŻźŹ]*[0-9]{2}\\-[0-9]{3}");
                foreach (Match match_ulica in schemat_ulica.Matches(kontrahent_nazwa))
                {
                    kontrahent_ulica = match_ulica.ToString().Substring(0, match_ulica.Length - 6);
                }
                Regex schemat_miasto = new Regex("[0-9]{2}\\-[0-9]{3}\\s[A-Za-z0-9\\s\\.\\-\\/ńŃęĘóÓśŚąAńŃćĆżŻźŹ]*");
                foreach (Match match_miasto in schemat_miasto.Matches(kontrahent_nazwa))
                {
                    kontrahent_miasto = match_miasto.ToString().Substring(6, match_miasto.Length - 6).Replace("?", "");
                }
            }


            Regex schmemat_nr_banku = new Regex("[0-9]{2}\\s[0-9]{4}\\s[0-9]{4}\\s[0-9]{4}\\s[0-9]{4}\\s[0-9]{4}\\s[0-9]{4}");
            foreach (Match match_nr_banku in schmemat_nr_banku.Matches(linia))
            {

                numer_konta = match_nr_banku.ToString().Replace("?", "").Replace(" ", "");

            }

            if (numer_konta != "")
            {
                numer_konta_1 = numer_konta.Substring(2, 8);
                numer_konta_2 = numer_konta.Substring(10, 16);
            }


            if (tytul_przelewu == "" || tytul_przelewu == " ")
            {
                tytul_przelewu = "Przelew";
            }

            linia_nowa = ":86:<00X<101<20" + tytul_przelewu + "<27" + kontrahent_nazwa + "<29" + kontrahent_ulica + "<30" + numer_konta_1 + "<31" + numer_konta_2 + "<32" + kontrahent_nazwa +  "<60" + kontrahent_miasto + "\r\n:6";
            

            return linia_nowa; 
        
        }

        private string linia_pobr_prowizji(string linia) {

            string tytul_przelewu = "";
            string linia_nowa = "";

            tytul_przelewu = linia.Substring(0, linia.Length - 2);
            tytul_przelewu = tytul_przelewu.Replace("?", "").Replace("\n", "").Replace("\r","");

            if (tytul_przelewu == "" || tytul_przelewu == " ")
            {
                tytul_przelewu = "Przelew";
            }
            linia_nowa = ":86:<00X<101<20" + tytul_przelewu + "\r\n:6";
            return linia_nowa; 
        
        }


        private void button5_Click(object sender, EventArgs e)
        {
            folder3.ShowDialog();
            textBox4.Text = folder3.SelectedPath.ToString();
            sciezka4 = textBox4.Text;

            Properties.Settings.Default.files4Path = folder3.SelectedPath.ToString();
            Properties.Settings.Default.Save();

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
