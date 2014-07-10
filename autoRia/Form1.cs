using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace autoRia
{
    public partial class autoRiaForm : Form
    {
        Thread searchThread;
        List<string> listUrls = new List<string>();
        List<string> listIdForComment = new List<string>();
        public autoRiaForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            bool state = true;
            //waitMht(2);
            while (state)
            {
                if (webBrowser1.ReadyState != WebBrowserReadyState.Complete)
                {
                    Application.DoEvents();
                    if (webBrowser1.ReadyState == WebBrowserReadyState.Complete)
                    {
                        if (webBrowser1.Url.AbsolutePath == "/mymenu/")
                        {
                            panel1.Enabled = true;
                            loginTextBox.Enabled = false;
                            passTextBox.Enabled = false;
                            submitButt.Enabled = false;
                        }
                        state = false;
                    }
                }
            }
        }

        private void radioButton1_DragDrop(object sender, DragEventArgs e)
        {
           // MessageBox.Show("1");
        }

        private void radioButton2_DragDrop(object sender, DragEventArgs e)
        {
          //  MessageBox.Show("2");
        }

        private void radioButton1_EnabledChanged(object sender, EventArgs e)
        {
            
            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                searchAndExchange.Visible = false;
                searchAndComment.Visible = true;
                this.Size = new Size(900,540);
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                searchAndComment.Visible = false;
                searchAndExchange.Visible = true;
                this.Size = new Size(420, 540);
            }
        }

        private void submitButt_Click(object sender, EventArgs e)
        {
            webBrowser1.Document.GetElementById("email").SetAttribute("value", loginTextBox.Text);
            webBrowser1.Document.GetElementById("password").SetAttribute("value", passTextBox.Text);
            
            foreach (HtmlElement el in webBrowser1.Document.GetElementsByTagName("input"))
            {
                if (el.GetAttribute("type").Equals("submit"))
                {
                    el.InvokeMember("click");
                    break;
                }
            }
            waitMht(2);

            if (webBrowser1.Url.AbsolutePath == "/mymenu/")
            {
                pictureBox1.Visible = true;
                webBrowser1.Navigate("http://auto.ria.com/search");
                waitMht(1);
                pictureBox1.Visible = false;
                panel1.Enabled = true;
            }
            else
            {
                notCorrect_label.Visible = true;
                waitMht(2);
                notCorrect_label.Visible = false;
            }

            

        }

        public static List<String> SearchAndInput(string str, string start, string end)
        {
            try
            {
                Regex rq = new Regex(start.Replace("[", "\\[").Replace("]", "\\]").Replace(".", "\\.").Replace("?", "\\?"));
                Regex rq1 = new Regex(end);
                List<string> ls = new List<string>();
                int p1 = 0;
                int p2 = 0;
                while (p1 < str.Length)
                {
                    Match m = rq.Match(str, p1);
                    if (m.Success)
                    {
                        p1 = m.Index + start.ToString().Length;
                        Match m1 = rq1.Match(str, p1);
                        if (m1.Success)
                        {
                            p2 = m1.Index;
                            if (str.Substring(p1, p2 - p1) == "")
                            {
                                ls.Add(str.Substring(p1, p2 - p1));
                            }
                            else ls.Add(str.Substring(p1, p2 - p1));
                        }
                    }
                    else break;
                }
                return ls;
            }
            catch
            {
                return null;
            }
        }


        public void waitMht(int sec) //wait for complite load page
        {
            DateTime Tthen = DateTime.Now;
            do
            {
                Application.DoEvents();
            }
            while (Tthen.AddSeconds(sec) > DateTime.Now);
        }

        private void searchAndExchange_Click(object sender, EventArgs e)
        {

        }

        private void searchAndComment_Click(object sender, EventArgs e)
        {
           /* searchThread = new Thread(search);
            searchThread.IsBackground = true;
            searchThread.SetApartmentState(ApartmentState.STA);
            searchThread.Start();
            webBrowser1.BeginInvoke((Action)delegate
            {
                search();
            });*/
            if (textAreaMess.Text != "") //отправка сообщения, если поле не пустое
            {
                textAreaMess.Enabled = false;
                string category_id = "0";
                string mm_country = "0";
                string marka = "0";
                string model = "0";
                string s_yers = "2010";
                string po_yers = "2014";
                string state = "11";
                string city = "0";
                string price_ot = "";
                string price_do = "";
                string currency = "1";
                string gearbox = "0";
                string fuelType = "0";
                string door = "";
                string seatsFrom = "";
                string seatsTo = "";
                string color = "0";
                string engineVolumeFrom = "";
                string engineVolumeTo = "";
                string raceFrom = "";
                string raceTo = "";
                webBrowser1.Navigate("http://auto.ria.com/blocks_search_ajax/search/?mm_country[0]=" + mm_country + "&countpage=100&category_id=" + category_id + "&view_type_id=0&page=0&marka=" + marka + "&model=" + model + "&s_yers=" + s_yers + "&po_yers=" + po_yers + "&state=" + state + "&city=" + city + "&price_ot=" + price_ot + "&price_do=" + price_do + "&currency=" + currency + "&gearbox=" + gearbox + "&type=" + fuelType + "&drive_type=0&door=" + door + "&color=" + color + "&metallic=0&engineVolumeFrom=" + engineVolumeFrom + "&engineVolumeTo=" + engineVolumeTo + "&raceFrom=" + raceFrom + "&raceTo=" + raceTo + "&powerFrom=&powerTo=&power_name=1&fuelRateFrom=&fuelRateTo=&fuelRatesType=city&custom=0&damage=0&saledParam=0&under_credit=0&confiscated_car=0&auto_repairs=0&with_exchange=0&with_real_exchange=0&exchangeTypeId=0&with_photo=0&with_video=0&is_hot=0&vip=0&checked_auto_ria=0&top=0&order_by=0&hide_black_list=0&auto_id=&auth=0&deletedAutoSearch=0&user_id=0&scroll_to_auto_id=0&expand_search=0&can_be_checked=0&last_auto_id=0&matched_country=-1&seatsFrom=" + seatsFrom + "&seatsTo=" + seatsTo + "&wheelFormulaId=0&axleId=0&carryingTo=&carryingFrom=&search_near_states=0&company_id=0&company_type=0");

                waitMht(5);


                var file = webBrowser1.DocumentText;
                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(file);

                int countAuto = Convert.ToInt32(SearchAndInput(doc.DocumentNode.InnerHtml, "],\"count\":", ",\"last_id")[0]);
                double pages = Math.Ceiling(countAuto / (double)100);

                if (countAuto != 0 && countAuto != null)
                    if (pages > 1)
                    {
                        for (int j = 0; j < pages; j++) //проходимся по всем страницам и собираем ответ json с id's, если страниц больше одной
                        {
                            List<string> getAutoIds = new List<string>();
                            webBrowser1.Navigate("http://auto.ria.com/blocks_search_ajax/search/?mm_country[0]=" + mm_country + "&countpage=100&category_id=" + category_id + "&view_type_id=0&page=" + j + "&marka=" + marka + "&model=0&s_yers=" + s_yers + "&po_yers=" + po_yers + "&state=" + state + "&city=" + city + "&price_ot=" + price_ot + "&price_do=" + price_do + "&currency=" + currency + "&gearbox=" + gearbox + "&type=" + fuelType + "&drive_type=0&door=" + door + "&color=" + color + "&metallic=0&engineVolumeFrom=" + engineVolumeFrom + "&engineVolumeTo=" + engineVolumeTo + "&raceFrom=" + raceFrom + "&raceTo=" + raceTo + "&powerFrom=&powerTo=&power_name=1&fuelRateFrom=&fuelRateTo=&fuelRatesType=city&custom=0&damage=0&saledParam=0&under_credit=0&confiscated_car=0&auto_repairs=0&with_exchange=0&with_real_exchange=0&exchangeTypeId=0&with_photo=0&with_video=0&is_hot=0&vip=0&checked_auto_ria=0&top=0&order_by=0&hide_black_list=0&auto_id=&auth=0&deletedAutoSearch=0&user_id=0&scroll_to_auto_id=0&expand_search=0&can_be_checked=0&last_auto_id=0&matched_country=-1&seatsFrom=" + seatsFrom + "&seatsTo=" + seatsTo + "&wheelFormulaId=0&axleId=0&carryingTo=&carryingFrom=&search_near_states=0&company_id=0&company_type=0");

                            waitMht(4);

                            var filePages = webBrowser1.DocumentText;
                            var docPages = new HtmlAgilityPack.HtmlDocument();
                            docPages.LoadHtml(filePages);

                            string autoIds = (SearchAndInput(docPages.DocumentNode.InnerHtml, "{\"ids\":[", "],")[0]).ToString();
                            List<string> temp = new List<string>(autoIds.Split(','));

                            for (int i = 0; i < temp.Count; i++) //чистим id от левых символов
                            {
                                var source = temp[i];
                                var old = new[] { '\\', '"' };
                                string sIds = Remove(source, old);
                                getAutoIds.Add(sIds);
                                listIdForComment.Add(sIds);
                            }

                            urlsToList(getAutoIds); //set auto url's to list

                        }

                        MessageBox.Show("1 " + listUrls.Count.ToString());
                    }
                    else
                    {
                        List<string> getAutoIds = new List<string>();
                        string autoIds = (SearchAndInput(doc.DocumentNode.InnerHtml, "{\"ids\":[", "],")[0]).ToString();
                        List<string> temp = new List<string>(autoIds.Split(','));

                        for (int i = 0; i < temp.Count; i++) //чистим id от левых символов
                        {
                            var source = temp[i];
                            var old = new[] { '\\', '"' };
                            string sIds = Remove(source, old);
                            getAutoIds.Add(sIds);
                            listIdForComment.Add(sIds);
                        }

                        urlsToList(getAutoIds); //set auto url's to list

                        MessageBox.Show("2 " + listUrls.Count.ToString());
                    }
                else
                    MessageBox.Show("По вашему запросу ничего не было найдено");



                webBrowser1.Invoke(new Action(() =>
                {
                    for (int i = 0; i < listIdForComment.Count; i++)
                    {
                        /*
                        webBrowser1.Navigate("http://auto.ria.com" + listUrls[i]);
                        waitMht(2);

                        foreach (HtmlElement el in webBrowser1.Document.GetElementsByTagName("a"))
                        {
                            if (el.GetAttribute("id").Equals("final_page__add_comment"))
                            {
                                el.InvokeMember("click");
                                break;
                            }
                        }*/

                        waitMht(1);

                        webBrowser1.Navigate("http://auto.ria.com/ajax.php?target=blocks&event=addcomment&autoId=" + listIdForComment[i] + "&subcommId=&field_name=stKr&ucomment=" + System.Web.HttpUtility.UrlEncode(textAreaMess.Text) + "");
  
                        waitMht(4);

                        count_label.BeginInvoke((Action)delegate
                        {
                            count_label.Text = i.ToString();
                        });
                    }
                    MessageBox.Show("Все комментарии были опубликованны");
                    textAreaMess.Enabled = true;
                }
                ));

                
            }
            else
                MessageBox.Show("Поле комментария пустое...");
            
        }

        public void urlsToList(List<string> autoId) //set auto url's to list
        {
            string comaIds = string.Join(",", autoId);

            webBrowser1.Navigate("http://auto.ria.com/blocks_search/view/auto_items_list/" + comaIds + "/?lang_id=2&view_type_id=0&strategy=default&domain_zone=com");

            waitMht(2);

            var fileUrl = webBrowser1.DocumentText;
            var docUrl = new HtmlAgilityPack.HtmlDocument();
            docUrl.LoadHtml(fileUrl);

            for (int i = 0; i < autoId.Count; i++)
            {
                var urls = docUrl.DocumentNode.SelectSingleNode("//a[contains(@nav_mem,'" + autoId[i] + "')]").Attributes[0].Value;
                listUrls.Add(urls);
            }
        }

        public static string Remove(string source, char[] oldChar)
        {
            return String.Join("", source.ToCharArray().Where(a => !oldChar.Contains(a)).ToArray());
        }

        public void search()
        {
           // WebBrowser webBr = new WebBrowser();
            var file = webBrowser1.DocumentText;
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(file);

            string category_id = "1";
            string mm_country = "276";
            string marka = "9";
            string model = "0";
            string s_yers = "1980";
            string po_yers = "2013";
            string state = "0";
            string city = "0";
            string price_ot = "1000";
            string price_do = "3000";
            string currency = "1";
            string gearbox = "0";
            string fuelType = "0";
            string door = "4";
            string seatsFrom = "0";
            string seatsTo = "6";
            string color = "0";
            string engineVolumeFrom = "0";
            string engineVolumeTo = "300";
            string raceFrom = "0";
            string raceTo = "500";
            webBrowser1.Navigate("http://auto.ria.com/search/#mm_country[0]=" + mm_country + "&countpage=100&category_id=" + category_id + "&view_type_id=0&page=0&marka=" + marka + "&model=" + model + "&s_yers=" + s_yers + "&po_yers=" + po_yers + "&state=" + state + "&city=" + city + "&price_ot=" + price_ot + "&price_do=" + price_do + "&currency=" + currency + "&gearbox=" + gearbox + "&type=" + fuelType + "&drive_type=0&door=" + door + "&color=" + color + "&metallic=0&engineVolumeFrom=" + engineVolumeFrom + "&engineVolumeTo=" + engineVolumeTo + "&raceFrom=" + raceFrom + "&raceTo=" + raceTo + "&powerFrom=&powerTo=&power_name=1&fuelRateFrom=&fuelRateTo=&fuelRatesType=city&custom=0&damage=0&saledParam=0&under_credit=0&confiscated_car=0&auto_repairs=0&with_exchange=0&with_real_exchange=0&exchangeTypeId=0&with_photo=0&with_video=0&is_hot=0&vip=0&checked_auto_ria=0&top=0&order_by=0&hide_black_list=0&auto_id=&auth=0&deletedAutoSearch=0&user_id=0&scroll_to_auto_id=0&expand_search=0&can_be_checked=0&last_auto_id=0&matched_country=-1&seatsFrom=" + seatsFrom + "&seatsTo=" + seatsTo + "&wheelFormulaId=0&axleId=0&carryingTo=&carryingFrom=&search_near_states=0&company_id=0&company_type=0");

            

            bool bstate = true;

            while (bstate)
             {
                 if (webBrowser1.ReadyState != WebBrowserReadyState.Complete)
                 {
                     Application.DoEvents();
                     if (webBrowser1.ReadyState == WebBrowserReadyState.Complete)
                     {
                         waitMht(10);
                         var file1 = webBrowser1.DocumentText;
                         var doc1 = new HtmlAgilityPack.HtmlDocument();
                         doc1.LoadHtml(file1);

                         var countAuto = doc1.DocumentNode.SelectSingleNode("//strong[contains(@id,'search_results_count')]").InnerText;
                         MessageBox.Show("Найдено авто: " + countAuto);
                         bstate = false;
                     }
                 }
             }

           
        }


        
    }
}
