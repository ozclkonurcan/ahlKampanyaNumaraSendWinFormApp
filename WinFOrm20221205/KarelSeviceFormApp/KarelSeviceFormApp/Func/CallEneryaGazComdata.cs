using KarelSeviceFormApp.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace KarelSeviceFormApp.Func
{
    public static class CallEneryaGazComdata
    {
        public static void EneryaGazComdata(string TARIH, string campName, string pollName)
        {

            try
            {

                var client = new RestClient("http://egpoq00.enr.local:50000/XISOAPAdapter/MessageServlet?senderParty=&senderService=COMDATA_IVR&receiverParty=&receiverService=&interface=SI_CCAnketArama_Sync_OB&interfaceNamespace=http://energaz.com/ComdataCallCenterAnketArama");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("SOAPAction", "http://sap.com/xi/WebService/soap1.1");
                request.AddHeader("Content-Type", "text/xml;charset=UTF-8");
                request.AddHeader("Authorization", "Basic UkZDVVNFUjphQTEyMzQ1Ng==");
                request.AddHeader("Cookie", "JSESSIONID=0K6u_4yjCDCZngpMxM-7V8_FMukThAESj4YA_SAPRhn5SsC-YaW0xTJMTNpYNpvH; JSESSIONMARKID=NMn5CQu3X5Apk_e955bHYrOL9NQRTLGwYzGhKPhgA; saplb_*=(J2EE8818420)8818450");

                var body = "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:urn=\"urn:sap-com:document:sap:rfc:functions\">" +
                    "<soapenv:Header/>" +
                    " <soapenv:Body>" +
                    "<urn:ZMD_WS_USER_PRS_BULK>" +
                    "<!--You may enter the following 4 items in any order-->" +
                    "<!--Optional:-->" +
                    "<BUKRS>3000</BUKRS>" +
                    "<!--Optional:-->" +
                    "<FLG></FLG>" +
                    "<!--Optional:-->" +
                    "<TARIH>" + TARIH + "</TARIH>" +
                    " <!--Optional:-->" +
                    "<GT_DATA>" +
                    "<!--Zero or more repetitions:-->" +
                    "<item>" +
                    "<!--Optional:-->" +
                    "<MANDT></MANDT>" +
                    "<!--Optional:-->" +
                    "<TARIH></TARIH>" +
                    "<!--Optional:-->" +
                    "<SOZHESAP></SOZHESAP>" +
                    "<!--Optional:-->" +
                    "<SURECADI></SURECADI>" +
                    "<!--Optional:-->" +
                    "<PERADSOYAD></PERADSOYAD>" +
                    "<!--Optional:-->" +
                    "<MUSADSOYAD></MUSADSOYAD>" +
                    "<!--Optional:-->" +
                    "<CEPTEL></CEPTEL>" +
                    "<!--Optional:-->" +
                    "<USER></USER>" +
                    "<!--Optional:-->" +
                    "<LOKASYON></LOKASYON>" +
                    "<!--Optional:-->" +
                    "<SEHIR></SEHIR>" +
                    "</item>" +
                    "</GT_DATA>" +
                    "</urn:ZMD_WS_USER_PRS_BULK>" +
                    "</soapenv:Body>" +
                    "</soapenv:Envelope>";


                request.AddParameter("text/xml;charset=UTF-8", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                List<Enerya_Anket_Arama> anketAramaObList = new List<Enerya_Anket_Arama>();
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(response.Content);
                XmlNodeList elemList = doc.GetElementsByTagName("item");



                foreach (XmlNode chldNode in elemList)
                {
                    anketAramaObList.Add(new Enerya_Anket_Arama
                    {
                        //Id = int.Parse(chldNode.InnerText),
                        MANDT = chldNode.ChildNodes[0].InnerText,
                        TARIH = DateTime.Parse(chldNode.ChildNodes[1].InnerText),
                        SOZHESAP = chldNode.ChildNodes[2].InnerText,
                        SURECADI = chldNode.ChildNodes[3].InnerText,
                        PERADSOYAD = chldNode.ChildNodes[4].InnerText,
                        MUSADSOYAD = chldNode.ChildNodes[5].InnerText,
                        CEPTEL = chldNode.ChildNodes[6].InnerText,
                        USER = chldNode.ChildNodes[7].InnerText,
                        LOKASYON = chldNode.ChildNodes[8].InnerText,
                        SEHIR = chldNode.ChildNodes[9].InnerText,


                    });
                }
                var anketListDinstinc = anketAramaObList.GroupBy(x => x.CEPTEL).Select(x => x.First()).ToList();

                var anketListDinstinc2 = new List<Enerya_Anket_Arama>()
                {
                    new Enerya_Anket_Arama(){MUSADSOYAD="Onur ÖZÇELİK",CEPTEL="+905531830397"},
                    //new Enerya_Anket_Arama(){MUSADSOYAD="Osman YÜKSEL",CEPTEL="+905070295892"},
                };
                foreach (var item in anketListDinstinc2)
                {
                    CallDataControllerFunc.DataCont(campName, item.MUSADSOYAD, item.CEPTEL, "POLLOB-" +pollName);
                    //DataController(cb_camp.Text, item.MUSADSOYAD, item.CEPTEL, "POLLOB-"+cb_pollob.Text);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Anket Arama Servis bağlantı problemi!!!!!");
            }
        }
    }
}
