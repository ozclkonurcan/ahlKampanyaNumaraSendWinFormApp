using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using Timer = System.Timers.Timer;
using Quartz;
using Quartz.Impl;
using Newtonsoft.Json;
using RestSharp;
using KarelSeviceFormApp.Model;
using KarelSeviceFormApp.VM_Model;
using KarelSeviceFormApp;
using System.Drawing;
using KarelSeviceFormApp.Func;

namespace KarelSeviceFormApp
{
    //[RunInstaller(true)]

    public partial class Form1 : Form 
    {
        private Timer timer;
        private string _path = "C:\\Program Files (x86)\\AHLKampanyaUygulama\\JsonDataDB\\ApiAddToCampaign.json";
        //private string _path = "C:\\Users\\Lenovo\\Desktop\\windowsFormAppAutoNumberSend-main - Kopya\\windows form app\\KarelSeviceFormApp\\KarelSeviceFormApp\\JsonDataDB\\ApiAddToCampaign.json";

        
        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;

           

            //anketDataList.ColumnCount = 5;

            //anketDataList.Columns[0].Name = "Merkez";
            //anketDataList.Columns[1].Name = "Müsteri Adı";
            //anketDataList.Columns[2].Name = "Telefon 1";
            //anketDataList.Columns[3].Name = "Bilgi 1";
            //anketDataList.Columns[4].Name = "Durum";


            ////forEq with

            //anketDataList.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //anketDataList.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //anketDataList.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //anketDataList.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //anketDataList.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            ApiGetCampaigns();
            ApiGetPollobControlArea();

            dtp_aramaTarih.Format = DateTimePickerFormat.Custom;
            dtp_aramaTarih.CustomFormat = "yyyy-MM-dd";




        }


        //private void AnketListDataGridViewShow()
        //{
        //    try
        //    {
        //        var jsonData = System.IO.File.ReadAllText(_path);
        //        var jsonDataDB = JsonConvert.DeserializeObject<List<ApiAddToCampJson>>(jsonData)
        //              ?? new List<ApiAddToCampJson>();

        //        if (jsonDataDB.Count > 0)
        //        {

        //            var data = (from a in jsonDataDB
        //                        select a).ToList();
        //            data = data.GroupBy(x => x.CustPhone).Select(x => x.First()).ToList();
        //            foreach (var item in data)
        //            {

        //                anketDataList.Rows.Add(cb_camp.Text, item.CustName, item.CustPhone, item.CustData1, item.DurumType);
        //            }
        //            //for (int i = 0; i <= j; i++)
        //            //{
        //            //    anketDataList.Rows[i].Cells[0].Value = data.FirstOrDefault().CampID;
        //            //    anketDataList.Rows[i].Cells[1].Value = data.FirstOrDefault().CustName;
        //            //    anketDataList.Rows[i].Cells[2].Value = data.FirstOrDefault().CustPhone;
        //            //    anketDataList.Rows[i].Cells[3].Value = data.FirstOrDefault().CustData1;
        //            //    anketDataList.Rows[i].Cells[4].Value = data.FirstOrDefault().DurumType;
        //            //}


        //            //anketDataList.DataSource = jsonDataDB;
        //            anketDataList.Refresh();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        throw;
        //    }
        //}


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {

            bool MousePointerNotOnTaskBar = Screen.GetWorkingArea(this).Contains(Cursor.Position);

            if (this.WindowState == FormWindowState.Minimized && MousePointerNotOnTaskBar)
            {
                notifyIcon1.Icon = SystemIcons.Application;
                notifyIcon1.BalloonTipText = "Program küçültüldü";
                notifyIcon1.ShowBalloonTip(1000);
                this.ShowInTaskbar = false;
                notifyIcon1.Visible = true;
            }
        }

        //private void addRow(string CampId,string CustName,string CustPhone,string CustData1, bool DurumType)
        //{
        //    //String[] row = [CampId, CustName, CustPhone, CustData1, DurumType];

        //    anketDataList.Rows.Add(CampId, CustName, CustPhone, CustData1, DurumType);
        //}



   

        private object ApiGetCampaigns()
        {
            try
            {

                var client = new RestClient("http://10.19.12.12/ccm/wsdl/ccmapi.php");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "text/xml;charset=UTF-8");
                var body = @"<soapenv:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:urn=""urn:ccmapiwsdl"">
" + "\n" +
                @"   <soapenv:Header/>
" + "\n" +
                @"   <soapenv:Body>
" + "\n" +
                @"      <urn:ApiGetCampaigns soapenv:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/"">
" + "\n" +
                @"         <Status xsi:type=""xsd:string"">1</Status>
" + "\n" +
                @"      </urn:ApiGetCampaigns>
" + "\n" +
                @"   </soapenv:Body>
" + "\n" +
                @"</soapenv:Envelope>";
                request.AddParameter("text/xml;charset=UTF-8", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

                List<VM_ApiGetCampaigns> activeCamp = new List<VM_ApiGetCampaigns>();
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(response.Content);
                XmlNodeList elemList = doc.GetElementsByTagName("item");

                foreach (XmlNode chldNode in elemList)
                {
                    activeCamp.Add(new VM_ApiGetCampaigns
                    {
                        CampID = int.Parse(chldNode.ChildNodes[0].InnerText),
                        campName = chldNode.ChildNodes[1].InnerText,
                        startDate = chldNode.ChildNodes[2].InnerText,
                        endDate = chldNode.ChildNodes[3].InnerText,
                        status = chldNode.ChildNodes[4].InnerText,

                    });
                }

                var activeCampID = (from a in activeCamp
                                    select new
                                    {
                                        a.CampID,
                                        a.campName
                                    }).ToList();

                var comboCamp = cb_camp.DataSource = activeCamp.Select(a => a.campName).ToList();

                return comboCamp;

            }
            catch (Exception ex)
            {
                timer.Stop();
                MessageBox.Show(ex.Message);
                throw;
            }
        }

 
        private object ApiGetPollobControlArea()
        {
            try
            {

                var client = new RestClient("http://10.19.12.12/ccm/wsdl/ccmapi.php");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "text/xml;charset=UTF-8");
                var body = @"<soapenv:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:urn=""urn:ccmapiwsdl"">
" + "\n" +
                @"   <soapenv:Header/>
" + "\n" +
                @"   <soapenv:Body>
" + "\n" +
                @"      <urn:ApiGetPoll soapenv:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/"">
" + "\n" +
                @"         <PollId xsi:type=""xsd:integer""></PollId>
" + "\n" +
                @"      </urn:ApiGetPoll>
" + "\n" +
                @"   </soapenv:Body>
" + "\n" +
                @"</soapenv:Envelope>";
                request.AddParameter("text/xml;charset=UTF-8", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

                List<VM_ApiGetPoll> activePoll = new List<VM_ApiGetPoll>();
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(response.Content);
                XmlNodeList elemList = doc.GetElementsByTagName("item");

                foreach (XmlNode chldNode in elemList)
                {
                    activePoll.Add(new VM_ApiGetPoll
                    {
                        pollID = int.Parse(chldNode.ChildNodes[0].InnerText),
                        pollName = chldNode.ChildNodes[1].InnerText,
                        pollStartDate = chldNode.ChildNodes[2].InnerText,
                        pollEndDate = chldNode.ChildNodes[3].InnerText,
                        pollLang = chldNode.ChildNodes[4].InnerText,
                        pollStatus = int.Parse(chldNode.ChildNodes[5].InnerText),

                    });
                }

                var activeCampID = (from a in activePoll
                                    where a.pollStatus == 1
                                    select new
                                    {
                                        a.pollID
                                    }).ToList();

                var comboPoll = cb_pollob.DataSource = activeCampID.Select(a => a.pollID).ToList();


                return comboPoll;

            }
            catch (Exception ex)
            {
                timer.Stop();
                MessageBox.Show(ex.Message);
                throw;
            }
        }








        private void main()
        {
            DateTime nowTime = DateTime.Now;

            FullTime fullTime = new FullTime();

            DateTime startTime = Convert.ToDateTime(fullTime.startTime);
            DateTime endTime = Convert.ToDateTime(fullTime.endTime);
            DateTime startTime2 = Convert.ToDateTime(fullTime.startTime2);
            DateTime endTime2 = Convert.ToDateTime(fullTime.endTime2);




            Thread.Sleep(1000);
            try
            {
                if ((DateTime.Now > startTime && DateTime.Now < endTime))
                {
                    var TARIH = DateTime.Now.ToString("yyyy-MM-dd");
                    var tarihDeneme = dtp_aramaTarih.Text;
                    //"2021-09-09"
                    CallEneryaGazComdata.EneryaGazComdata(dtp_aramaTarih.Text,cb_camp.Text,cb_pollob.Text);
                    CallPersonelBazliCagri.PersonelBazliCagri(dtp_aramaTarih.Text, cb_camp.Text, cb_pollob.Text);
                    //EneryaGazComdata(dtp_aramaTarih.Text);
                    //"2021-08-01"
                    //PersonelBazliCagri(dtp_aramaTarih.Text);
                }
                else if ((DateTime.Now > startTime2 && DateTime.Now < endTime2))
                {
                    var TARIH = DateTime.Now.ToString("yyyy-MM-dd");
                    CallEneryaGazComdata.EneryaGazComdata(dtp_aramaTarih.Text, cb_camp.Text, cb_pollob.Text);
                    CallPersonelBazliCagri.PersonelBazliCagri(dtp_aramaTarih.Text, cb_camp.Text, cb_pollob.Text);
                    //"2021-09-09"
                    //EneryaGazComdata(dtp_aramaTarih.Text);
                    //"2021-08-01"
                    //PersonelBazliCagri(dtp_aramaTarih.Text);

                }
            }
            catch (Exception ex)
            {
                timer.Stop();
                MessageBox.Show(ex.Message);
            }

        }


        static public void clearFileJson()
        {
            DateTime nowTime = DateTime.Now;

            FullTime fullTime = new FullTime();


            DateTime startClearTime = Convert.ToDateTime(fullTime.startClearTime);
            DateTime endClearTime = Convert.ToDateTime(fullTime.endClearTime);



            Thread.Sleep(1000);
            try
            {


                if (DateTime.Now > startClearTime && DateTime.Now < endClearTime)
                {

                    string _path = "C:\\Program Files (x86)\\AHLKampanyaUygulama\\JsonDataDB\\ApiAddToCampaign.json";
                    //string _path = "C:\\Users\\Lenovo\\Desktop\\windowsFormAppAutoNumberSend-main - Kopya\\windows form app\\KarelSeviceFormApp\\KarelSeviceFormApp\\JsonDataDB\\ApiAddToCampaign.json";
                    var jsonData = System.IO.File.ReadAllText(_path);
                    var errorApiDial = JsonConvert.DeserializeObject<List<ApiAddToCampJson>>(jsonData)
                          ?? new List<ApiAddToCampJson>();

                    File.Delete(_path);
                    Thread.Sleep(2000);
                    File.Create(_path);
                }



            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message);
            }



        }


        private IScheduler Baslat()
        {
            ISchedulerFactory schedFact = new StdSchedulerFactory();
            IScheduler sched = schedFact.GetScheduler().Result;
            if (!sched.IsStarted)
            {
                sched.Start();
            }
            return sched;
        }


        private async void startWithQuartz(object sender, ElapsedEventArgs e)
        {
            timer.Stop();
            IScheduler sched = Baslat();
            //IJobDetail OgledenOnce = JobBuilder.Create<OgledenOnce>().WithIdentity("OgledenOnce", null).Build();
            //IJobDetail OgledenSonra = JobBuilder.Create<OgledenSonra>().WithIdentity("OgledenSonra", null).Build();
            //IJobDetail ClearJsonFile = JobBuilder.Create<ClearJsonFile>().WithIdentity("ClearJsonFile", null).Build();

            FullTime fullTime = new FullTime();

            DateTime startTime = Convert.ToDateTime(fullTime.startTime);
            DateTime endTime = Convert.ToDateTime(fullTime.endTime);
            DateTime startTime2 = Convert.ToDateTime(fullTime.startTime2);
            DateTime endTime2 = Convert.ToDateTime(fullTime.endTime2);


            DateTime startClearTime = Convert.ToDateTime(fullTime.startClearTime);
            DateTime endClearTime = Convert.ToDateTime(fullTime.endClearTime);
            try
            {
                if (DateTime.Now > startTime && DateTime.Now < endTime)
                {
                    //ISimpleTrigger TriggerGorev = (ISimpleTrigger)TriggerBuilder.Create().WithIdentity("OgledenOnce").WithSimpleSchedule(x => x.WithIntervalInMinutes(30).RepeatForever()).EndAt(endTime).Build();
                    //await sched.ScheduleJob(OgledenOnce, TriggerGorev);
                    main();

                }
                else if (DateTime.Now > startTime2 && DateTime.Now < endTime2)
                {
                    //ISimpleTrigger TriggerGorev2 = (ISimpleTrigger)TriggerBuilder.Create().WithIdentity("OgledenSonra").WithSimpleSchedule(x => x.WithIntervalInMinutes(30).RepeatForever()).EndAt(endTime2).Build();
                    //await sched.ScheduleJob(OgledenSonra, TriggerGorev2);
                    main();

                }
                //else if(DateTime.Now > endTime && DateTime.Now < startTime2)
                //{
                //    //Öğlee Arası
                //    notificatonSettings("Uygulama çalışma saati'nin dışında !!!  \n Uygulama çalışma saatleri [ 08:30 / 12:00 ] && [ 13:00 / 17:00 ]");


                //    DateTime girisZamani = Convert.ToDateTime(startTime2);
                //    DateTime cikisZamani = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
                //    TimeSpan sonuc = girisZamani - cikisZamani;
                //    string calismaSuresi = sonuc.TotalMinutes.ToString();

                //    if (calismaSuresi.StartsWith("-"))
                //    {
                //        int toplamSure = 1440 + Convert.ToInt32(calismaSuresi);
                //        int toplamMiliSaniye = toplamSure * 60000;
                //        Thread.Sleep(toplamMiliSaniye);
                //    }
                //    else
                //    {
                //        int toplamSure = Convert.ToInt32(calismaSuresi);
                //        int toplamMiliSaniye = toplamSure * 60000;
                //        Thread.Sleep(toplamMiliSaniye);

                //    }
                //}
                else if (DateTime.Now > endTime2 || DateTime.Now < startTime)
                {
                    //Uygulama bitiş ve çalışma saati başlangıç arası
                    notificatonSettings("Uygulama çalışma saati'nin dışında !!!  \n Uygulama çalışma saatleri [ 08:30 / 12:00 ] && [ 13:00 / 17:00 ]");


                    DateTime girisZamani = Convert.ToDateTime("08:30");
                    DateTime cikisZamani = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
                    TimeSpan sonuc = girisZamani - cikisZamani;
                    string calismaSuresi = sonuc.TotalMinutes.ToString();

                    if (calismaSuresi.StartsWith("-"))
                    {
                    int toplamSure = 1440 + Convert.ToInt32(calismaSuresi);
                    int toplamMiliSaniye = toplamSure * 60000;
                        //Thread.Sleep(toplamMiliSaniye);
                        await Task.Delay(toplamMiliSaniye);
                    }
                    else
                    {
                        int toplamSure = Convert.ToInt32(calismaSuresi);
                        int toplamMiliSaniye = toplamSure * 60000;
                        //Thread.Sleep(toplamMiliSaniye);
                        await Task.Delay(toplamMiliSaniye);


                    }
                }
                //else if(DateTime.Now > endClearTime || DateTime.Now < startTime)
                //{
                //    var beklemesuresi = (24 - DateTime.Now.Hour + startTime.Hour) * 60 * 60 * 1000;
                //    Thread.Sleep(beklemesuresi);
                //}
                //Eğer iptal etmeseydim bugün içinde aranan eleman yarın bir daha aranacaktı çünkü program çalışma saatini bitirdikten sonra dosyayı
                //sıfırlıyor ve güne sıfırlanımış dosya ile başlıyoruz böylelikle dünki aranan numarayı gün içinde bulamıyor ve tekrar arıyor
                //else if (DateTime.Now > startClearTime && DateTime.Now < endClearTime)
                //{
                //    //ISimpleTrigger TriggerGorev3 = (ISimpleTrigger)TriggerBuilder.Create().WithIdentity("ClearJsonFile").StartAt(startClearTime).WithSimpleSchedule(x => x.WithIntervalInMinutes(30).RepeatForever()).EndAt(endClearTime).Build();
                //    //await sched.ScheduleJob(ClearJsonFile, TriggerGorev3);
                //    clearFileJson();
                //}
            }
            catch (Exception ex)
            {
                timer.Stop();
                MessageBox.Show(ex.Message);
            }
            timer.Start();
        }

        private void btn_run_Click(object sender, EventArgs e)
        {
            notificatonSettings("Uygulama başlatıldı");
            btn_run.Enabled = false;
            cb_camp.Enabled = false;
            cb_pollob.Enabled = false;
            dtp_aramaTarih.Enabled = false;
            çalıştırToolStripMenuItem.Enabled = false;
            timer = new Timer
            {
                Interval = 3000,
                AutoReset = true,

            };
            timer.Elapsed += startWithQuartz;
            timer.Start();

        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            try
            {
                notificatonSettings("Uygulama Durduruldu");
                btn_run.Enabled = true;
                cb_camp.Enabled = true;
                cb_pollob.Enabled = true;
                dtp_aramaTarih.Enabled = true;
                çalıştırToolStripMenuItem.Enabled = true;
                timer.Stop();
            }
            catch (Exception)
            {
                timer.Stop();
                MessageBox.Show("Henüz programı başlatmadan direkt durduramazsın !!!!!!");
            }

        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Uygulamayı sonlandırmak istiyormusun ?", "Exit", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                notificatonSettings("Uygulama kapatıldı");
                Application.Exit();

            }

        }

        private void anketDataList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btn_reflesh_Click(object sender, EventArgs e)
        {
            //AnketListDataGridViewShow();
            //anketDataList.Update();
            //anketDataList.Refresh();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            if (this.WindowState == FormWindowState.Normal)
            {
                this.ShowInTaskbar = true;
                notifyIcon1.Visible = false;
            }
        }

        private void btn_minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            bool MousePointerNotOnTaskBar = Screen.GetWorkingArea(this).Contains(Cursor.Position);

            if (this.WindowState == FormWindowState.Minimized && MousePointerNotOnTaskBar)
            {
                notificatonSettings("Program küçültüldü");
            }
        }


        private void notificatonSettings(string text)
        {
            notifyIcon1.BalloonTipText = text;
            notifyIcon1.ShowBalloonTip(1000);
            this.ShowInTaskbar = false;
            notifyIcon1.Visible = true;
        }

        private void çalıştırToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notificatonSettings("Uygulama başlatıldı");
            btn_run.Enabled = false;
            cb_camp.Enabled = false;
            cb_pollob.Enabled = false;
            dtp_aramaTarih.Enabled = false;
            çalıştırToolStripMenuItem.Enabled = false;
            timer = new Timer
            {
                Interval = 3000,
                AutoReset = true,

            };
            timer.Elapsed += startWithQuartz;
            timer.Start();
        }

        private void durdurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                notificatonSettings("Uygulama Durduruldu");
                btn_run.Enabled = true;
                cb_camp.Enabled = true;
                cb_pollob.Enabled = true;
                dtp_aramaTarih.Enabled = true;
                çalıştırToolStripMenuItem.Enabled = true;
                timer.Stop();
            }
            catch (Exception)
            {
                MessageBox.Show("Henüz programı başlatmadan direkt durduramazsın !!!!!!");
            }
        }

        private void kapatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Uygulamayı sonlandırmak istiyormusun ?", "Exit", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                notificatonSettings("Uygulama kapatıldı");
                Application.Exit();

            }
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            try
            {

            string _path = "C:\\Program Files (x86)\\AHLKampanyaUygulama\\JsonDataDB\\ApiAddToCampaign.json";


            List<ApiAddToCampJson> errorApiDial = new List<ApiAddToCampJson>();
            var jsonData = System.IO.File.ReadAllText(_path);
            errorApiDial = JsonConvert.DeserializeObject<List<ApiAddToCampJson>>(jsonData)
                  ?? new List<ApiAddToCampJson>();


            if(errorApiDial.Count > 0)
            {

            foreach (var item in errorApiDial)
            {
                        notificatonSettings("Sıfırlama işlemi gerçekleşti.");
                        //btn_run.Enabled = true;
                        //cb_camp.Enabled = true;
                        //cb_pollob.Enabled = true;
                        //dtp_aramaTarih.Enabled = true;
                        //çalıştırToolStripMenuItem.Enabled = true;
                        item.CustData.Clear();
                        //timer.Stop();
                item.DurumType = false;
                File.WriteAllText(_path, JsonConvert.SerializeObject(errorApiDial));
            }

            }
            else
            {
                MessageBox.Show("Değişiklik yapılabilmesi için daha önce aranmış en az bir kayıt bulunması gerekiyor!");
            }

            }
            catch (Exception ex)
            {
                timer.Stop();
                MessageBox.Show(ex.Message);
            }

            //anketDataList.Update();

        

        }
    }
}






//class OgledenOnce : IJob
//{
//    public Task Execute(IJobExecutionContext context)
//    {
//        try
//        {
//            Form1.main();
//        }
//        catch (Exception)
//        {

//        }
//        return Task.CompletedTask;
//    }
//}

//class OgledenSonra : IJob
//{
//    public Task Execute(IJobExecutionContext context)
//    {
//        try
//        {
//            Form1.main();
//        }
//        catch (Exception)
//        {

//        }
//        return Task.CompletedTask;
//    }
//}

//class ClearJsonFile : IJob
//{
//    public Task Execute(IJobExecutionContext context)
//    {
//        try
//        {
//            Form1.clearFileJson();
//        }
//        catch (Exception)
//        {

//        }
//        return Task.CompletedTask;
//    }
//}
