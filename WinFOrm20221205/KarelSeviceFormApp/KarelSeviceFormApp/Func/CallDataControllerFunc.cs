using KarelSeviceFormApp.Model;
using KarelSeviceFormApp.VM_Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarelSeviceFormApp.Func
{
    public static class CallDataControllerFunc
    {
        
        public static void DataCont(string CampID, string CustName, string CustPhone, string CustData1)
        {
            string _path = "C:\\Program Files (x86)\\AHLKampanyaUygulama\\JsonDataDB\\ApiAddToCampaign.json";

            #region Zaman Ayar
            FullTime fullTime = new FullTime();
            DateTime startTime = Convert.ToDateTime(fullTime.startTime);
            DateTime endTime = Convert.ToDateTime(fullTime.endTime);
            DateTime startTime2 = Convert.ToDateTime(fullTime.startTime2);
            DateTime endTime2 = Convert.ToDateTime(fullTime.endTime2);
            #endregion
            var jsonData = System.IO.File.ReadAllText(_path);
            var errorApiDial = JsonConvert.DeserializeObject<List<ApiAddToCampJson>>(jsonData)
                  ?? new List<ApiAddToCampJson>();

            //var respDDD = errorApiDial.Find(x => x.CustPhone == CustPhone.Substring(2)).CustData.Select(x => x.CustData1).Contains(CustData1);

            if (CustPhone.StartsWith("+9"))
            {

                if (errorApiDial.Select(x => x.CustPhone).Contains(CustPhone.Substring(2)))
                {

                    if (errorApiDial.Find(x => x.CustPhone == CustPhone.Substring(2)).DurumType != true)
                    {
                        if ((DateTime.Now > startTime && DateTime.Now < endTime) || (DateTime.Now > startTime2 && DateTime.Now < endTime2))
                        {
                            //await System.Threading.Tasks.Task.Run(() => ApiAddToCampaign("10", "", item.MUSADSOYAD, item.CEPTEL.Substring(2), null, "POLLOB-5", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), endTime2.ToString("yyyy-MM-dd HH:mm:ss")));
                            ApiAddToCampaign.CallApiAddToCampaign(CampID, "", CustName, CustPhone.Substring(2), null, CustData1, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), endTime2.ToString("yyyy-MM-dd HH:mm:ss"));
                        }
                    }
                    else if (errorApiDial.Find(x => x.CustPhone == CustPhone.Substring(2)).DurumType == true && !errorApiDial.Find(x => x.CustPhone == CustPhone.Substring(2)).CustData.Select(x => x.CustData1).Contains(CustData1))
                    {
                        if ((DateTime.Now > startTime && DateTime.Now < endTime) || (DateTime.Now > startTime2 && DateTime.Now < endTime2))
                        {
                            //await System.Threading.Tasks.Task.Run(() => ApiAddToCampaign("10", "", item.MUSADSOYAD, item.CEPTEL, null, "POLLOB-5", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), endTime2.ToString("yyyy-MM-dd HH:mm:ss")));
                            ApiAddToCampaign.CallApiAddToCampaign(CampID, "", CustName, CustPhone.Substring(2), null, CustData1, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), endTime2.ToString("yyyy-MM-dd HH:mm:ss"));
                        }
                    }
                }
                else
                {
                    if ((DateTime.Now > startTime && DateTime.Now < endTime) || (DateTime.Now > startTime2 && DateTime.Now < endTime2))
                    {
                        //await System.Threading.Tasks.Task.Run(() => ApiAddToCampaign("10", "", item.MUSADSOYAD, item.CEPTEL.Substring(2), null, "POLLOB-5", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), endTime2.ToString("yyyy-MM-dd HH:mm:ss")));
                        ApiAddToCampaign.CallApiAddToCampaign(CampID, "", CustName, CustPhone.Substring(2), null, CustData1, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), endTime2.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                }
            }//Başında sıfır olmayanlara sıfır eklenecek
            else if (!CustPhone.StartsWith("0"))
            {
                if (errorApiDial.Select(x => x.CustPhone).Contains(CustPhone.Substring(2)))
                {
                    if (errorApiDial.Find(x => x.CustPhone == CustPhone.Substring(2)).DurumType != true)
                    {
                        if (DateTime.Now > startTime && DateTime.Now < endTime || DateTime.Now > startTime2 && DateTime.Now < endTime2)
                        {
                            ApiAddToCampaign.CallApiAddToCampaign(CampID, "", CustName, "0" + CustPhone, null, CustData1, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), endTime.ToString("yyyy-MM-dd HH:mm:ss"));
                        }

                    }
                    else if (errorApiDial.Find(x => x.CustPhone == "0"+CustPhone).DurumType == true && !errorApiDial.Find(x => x.CustPhone == "0"+CustPhone).CustData.Select(x => x.CustData1).Contains(CustData1))
                    {
                        if ((DateTime.Now > startTime && DateTime.Now < endTime) || (DateTime.Now > startTime2 && DateTime.Now < endTime2))
                        {
                            //await System.Threading.Tasks.Task.Run(() => ApiAddToCampaign("10", "", item.MUSADSOYAD, item.CEPTEL, null, "POLLOB-5", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), endTime2.ToString("yyyy-MM-dd HH:mm:ss")));
                            ApiAddToCampaign.CallApiAddToCampaign(CampID, "", CustName, "0"+CustPhone, null, CustData1, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), endTime2.ToString("yyyy-MM-dd HH:mm:ss"));
                        }
                    }
                }
                else
                {
                    if ((DateTime.Now > startTime && DateTime.Now < endTime))
                    {
                        ApiAddToCampaign.CallApiAddToCampaign(CampID, "", CustName, "0" + CustPhone, null, CustData1, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), endTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                    else if ((DateTime.Now > startTime2 && DateTime.Now < endTime2))
                    {
                        ApiAddToCampaign.CallApiAddToCampaign(CampID, "", CustName, "0" + CustPhone, null, CustData1, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), endTime2.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                }
            }
            else
            {

                if (errorApiDial.Select(x => x.CustPhone).Contains(CustPhone))
                {

                    if (errorApiDial.Find(x => x.CustPhone == CustPhone).DurumType != true)
                    {
                        if ((DateTime.Now > startTime && DateTime.Now < endTime) || (DateTime.Now > startTime2 && DateTime.Now < endTime2))
                        {
                            //await System.Threading.Tasks.Task.Run(() => ApiAddToCampaign("10", "", item.MUSADSOYAD, item.CEPTEL, null, "POLLOB-5", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), endTime2.ToString("yyyy-MM-dd HH:mm:ss")));
                            ApiAddToCampaign.CallApiAddToCampaign(CampID, "", CustName, CustPhone, null, CustData1, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), endTime2.ToString("yyyy-MM-dd HH:mm:ss"));
                        }
                    }else if(errorApiDial.Find(x => x.CustPhone == CustPhone).DurumType == true && !errorApiDial.Find(x => x.CustPhone == CustPhone).CustData.Select(x => x.CustData1).Contains(CustData1))
                    {
                        if ((DateTime.Now > startTime && DateTime.Now < endTime) || (DateTime.Now > startTime2 && DateTime.Now < endTime2))
                        {
                            //await System.Threading.Tasks.Task.Run(() => ApiAddToCampaign("10", "", item.MUSADSOYAD, item.CEPTEL, null, "POLLOB-5", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), endTime2.ToString("yyyy-MM-dd HH:mm:ss")));
                            ApiAddToCampaign.CallApiAddToCampaign(CampID, "", CustName, CustPhone, null, CustData1, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), endTime2.ToString("yyyy-MM-dd HH:mm:ss"));
                        }
                    }
                }
                else
                {
                    if ((DateTime.Now > startTime && DateTime.Now < endTime) || (DateTime.Now > startTime2 && DateTime.Now < endTime2))
                    {
                        //await System.Threading.Tasks.Task.Run(() => ApiAddToCampaign("10", "", item.MUSADSOYAD, item.CEPTEL, null, "POLLOB-5", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), endTime2.ToString("yyyy-MM-dd HH:mm:ss")));
                        ApiAddToCampaign.CallApiAddToCampaign(CampID, "", CustName, CustPhone, null, CustData1, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), endTime2.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                }
            }
        }

    }
}
