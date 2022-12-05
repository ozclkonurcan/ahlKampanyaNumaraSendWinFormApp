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
    public static class CallPersonelBazliCagri
    {
        public static void PersonelBazliCagri(string TARIH, string campName, string pollName)
        {

            try
            {


                var client = new RestClient("http://egpoq00.enr.local:50000/XISOAPAdapter/MessageServlet?senderParty=&senderService=COMDATA&receiverParty=&receiverService=&interface=SI_PersonelBazliCagri_OB&interfaceNamespace=http://energaz.com/CRM/PersonelBazliCagri");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("SOAPAction", "http://sap.com/xi/WebService/soap1.1");
                request.AddHeader("Content-Type", "text/xml;charset=UTF-8");
                request.AddHeader("Authorization", "Basic UkZDVVNFUjphQTEyMzQ1Ng==");
                request.AddHeader("Cookie", "JSESSIONID=Ho6zU3lUfMEVkmNdPqCcQJbc5btVhAESj4YA_SAPSA-DTO07AwjnEQGUTxrBQ-No; JSESSIONMARKID=JROs9gednb9dv2CR50zdcRUOSYFMiK5kOXHBKPhgA; saplb_*=(J2EE8818420)8818450");
                var body =
                "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:urn=\"urn:sap-com:document:sap:rfc:functions\">" +
                "<soapenv:Header/>" +
                "<soapenv:Body>" +
                "<urn:ZCRM_PERSONEL_BAZLI_CAGRI_WS>" +
                "<!--You may enter the following 2 items in any order-->" +
                "<!--Optional:-->" +
                "<IV_KANAL></IV_KANAL>" +
                "<IV_TARIH>" + TARIH + "</IV_TARIH>" +
                "</urn:ZCRM_PERSONEL_BAZLI_CAGRI_WS>" +
                "</soapenv:Body>" +
                "</soapenv:Envelope>";

                request.AddParameter("text/xml;charset=UTF-8", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

                List<PersonelBazliCagriAnketArama> personelBazliobList = new List<PersonelBazliCagriAnketArama>();
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(response.Content);
                XmlNodeList elemList = doc.GetElementsByTagName("item");


                //string dosyaYolu = "C:\\Users\\Lenovo\\Desktop\\KarelCMservice windows service\\KarelWebAnketAuto\\KarelWebAnketAuto\\LogFile\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";


                foreach (XmlNode chldNode in elemList)
                {
                    personelBazliobList.Add(new PersonelBazliCagriAnketArama
                    {
                        //Id = int.Parse(chldNode.InnerText),
                        ISLEM_NO = chldNode.ChildNodes[0].InnerText,
                        TARIH = DateTime.Parse(chldNode.ChildNodes[1].InnerText),
                        SAAT = chldNode.ChildNodes[2].InnerText,
                        GRUP = chldNode.ChildNodes[3].InnerText,
                        AGRUP = chldNode.ChildNodes[4].InnerText,
                        ORGANIZASYON = chldNode.ChildNodes[5].InnerText,
                        KULLANICI = chldNode.ChildNodes[6].InnerText,
                        MUHATAP = chldNode.ChildNodes[7].InnerText,
                        AD_SOYAD = chldNode.ChildNodes[8].InnerText,
                        TELEFON = chldNode.ChildNodes[9].InnerText,
                        CAGRI_TEL = chldNode.ChildNodes[10].InnerText,


                    });
                }

                var personelBazListDinstinc = personelBazliobList.GroupBy(x => x.TELEFON).Select(x => x.First()).ToList();

                var personelBazListDinstinc2 = new List<PersonelBazliCagriAnketArama>()
                {
                    new PersonelBazliCagriAnketArama(){AD_SOYAD="Onur ÖZÇELİK",TELEFON="+905531830397"},
                    //new Enerya_Anket_Arama(){MUSADSOYAD="Osman YÜKSEL",CEPTEL="+905070295892"},
                };


                foreach (var item in personelBazListDinstinc2)
                {
                    CallDataControllerFunc.DataCont(campName, item.AD_SOYAD, item.TELEFON, "POLLOB-" +pollName);
                    //DataController(cb_camp.Text, item.AD_SOYAD, item.TELEFON, "POLLOB-"+cb_pollob.Text);
                }


            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message + "Personel Bazlı Çağrı Servis bağlantı problemi!!!!!");
            }
        }
    }
}
