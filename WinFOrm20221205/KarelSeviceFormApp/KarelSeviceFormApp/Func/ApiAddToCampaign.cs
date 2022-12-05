using KarelSeviceFormApp.VM_Model;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace KarelSeviceFormApp.Func
{
    public static class ApiAddToCampaign
    {

        public static void CallApiAddToCampaign(string CampId, string CustId, string CustName, string CustPhone, string CustGsm, string CustData1, string DialStart, string DialEnd) 
        {

            try
            {
                string _path = "C:\\Program Files (x86)\\AHLKampanyaUygulama\\JsonDataDB\\ApiAddToCampaign.json";
                CampId = ApiGetCampaignsControlArea(CampId).ToString();


                List<ApiAddToCampJson> errorApiDial = new List<ApiAddToCampJson>();
                var jsonData = System.IO.File.ReadAllText(_path);
                errorApiDial = JsonConvert.DeserializeObject<List<ApiAddToCampJson>>(jsonData)
                      ?? new List<ApiAddToCampJson>();





                if ((errorApiDial.Count == 0) || (errorApiDial.Count > 0 && !errorApiDial.Select(x => x.CustPhone).Contains(CustPhone)) || (errorApiDial.Find(x => x.CustPhone == CustPhone).DurumType == false) || errorApiDial.Find(x => x.CustPhone == CustPhone).DurumType != false && !errorApiDial.Find(x => x.CustPhone == CustPhone).CustData.Select(x => x.CustData1).Contains(CustData1))
                {


                    var client = new RestClient("http://10.19.12.12/ccm/wsdl/ccmapi.php");
                    client.Timeout = -1;
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "text/xml;charset=UTF-8");

                    var body =
                         "<soapenv:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:urn=\"urn:ccmapiwsdl\">" +
                         " <soapenv:Header/>" +
                         "<soapenv:Body>" +
                         " <urn:ApiAddToCampaign soapenv:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\">" +
                         "<CampId xsi:type=\"xsd:string\">" + CampId + "</CampId>" +
                         "<CustId xsi:type=\"xsd:string\">" + CustId + "</CustId>" +
                         " <CustName xsi:type=\"xsd:string\">" + CustName + "</CustName>" +
                         "<CustPhone xsi:type=\"xsd:string\">" + CustPhone + "</CustPhone>" +
                         " <CustPhone2 xsi:type=\"xsd:string\">?</CustPhone2>" +
                         "<CustGsm xsi:type=\"xsd:string\">" + CustGsm + "</CustGsm>" +
                         " <CustFax xsi:type=\"xsd:string\">?</CustFax>" +
                         "<CustPerson xsi:type=\"xsd:string\">?</CustPerson>" +
                         "<CustData1 xsi:type=\"xsd:string\">" + CustData1 + "</CustData1>" +
                         "<CustData2 xsi:type=\"xsd:string\">?</CustData2>" +
                         "<CustData3 xsi:type=\"xsd:string\">?</CustData3>" +
                         "<CustData4 xsi:type=\"xsd:string\">?</CustData4>" +
                         " <DialStart xsi:type=\"xsd:string\">" + DialStart + "</DialStart>" +
                         "<DialEnd xsi:type=\"xsd:string\">" + DialEnd + "</DialEnd>" +
                         "<CheckNumberExist xsi:type=\"xsd:string\">?</CheckNumberExist>" +
                         "</urn:ApiAddToCampaign>" +
                         "</soapenv:Body>" +
                         "</soapenv:Envelope>";

                    request.AddParameter("text/xml;charset=UTF-8", body, ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);

                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(response.Content);
                    XmlNodeList elemList = doc.GetElementsByTagName("return");


                    foreach (XmlNode chldNode in elemList)
                    {

                        var serializeObject = Newtonsoft.Json.JsonConvert.SerializeObject(errorApiDial);

                        if (chldNode.ChildNodes[0].InnerText.StartsWith("ERR"))
                        {


                            //string text = File.ReadAllText(_path);
                            //var persons = JsonSerializer.Deserialize<ErrorApiDial>(text);


                            if (!errorApiDial.Select(x => x.CustPhone).Contains(CustPhone))
                            {
                                errorApiDial.GroupBy(x => x.CustPhone).Select(x => x.First()).ToList();


                                errorApiDial.Add(new ApiAddToCampJson
                                {
                                    CampID = CampId,
                                    CustName = CustName,
                                    CustPhone = CustPhone,
                                    CustData = new List<CustDataList>()
                                    {
                                        new CustDataList()
                                        {
                                            CustData1 = CustData1
                                        }
                                    },
                                    DurumType = false
                                });




                                if (!File.Exists(_path))
                                {
                                    // Create a file to write to.
                                    File.WriteAllText(_path, JsonConvert.SerializeObject(errorApiDial));
                                }
                                File.WriteAllText(_path, JsonConvert.SerializeObject(errorApiDial));
                            }
                            else if (errorApiDial.Count > 0 && errorApiDial.Select(x => x.CustPhone).Contains(CustPhone))
                            {
                                errorApiDial.Find(x => x.CustPhone == CustPhone).DurumType = false;

                                File.WriteAllText(_path, JsonConvert.SerializeObject(errorApiDial));
                            }




                        }
                        else if (chldNode.ChildNodes[0].InnerText.StartsWith("OK"))
                        {

                            if (errorApiDial.Count > 0 && errorApiDial.Select(x => x.CustPhone).Contains(CustPhone))
                            {
                                errorApiDial.Find(x => x.CustPhone == CustPhone).DurumType = true;

                                if (!errorApiDial.Find(x => x.CustPhone == CustPhone).CustData.Select(x => x.CustData1).Contains(CustData1))
                                {
                                    errorApiDial.Find(x => x.CustPhone == CustPhone).CustData.Add(new CustDataList
                                    {
                                        CustData1 = CustData1
                                    });
                                }


                                File.WriteAllText(_path, JsonConvert.SerializeObject(errorApiDial));
                                //anketDataList.Update();

                            }
                            else if (errorApiDial.Count > 0 && !errorApiDial.Select(x => x.CustPhone).Contains(CustPhone))
                            {


                                errorApiDial.Add(new ApiAddToCampJson
                                {
                                    CampID = CampId,
                                    CustName = CustName,
                                    CustPhone = CustPhone,
                                    CustData = new List<CustDataList>()
                                    {
                                        new CustDataList()
                                        {
                                            CustData1 = CustData1
                                        }
                                    },
                                    DurumType = true
                                });


                                if (!File.Exists(_path))
                                {
                                    // Create a file to write to.
                                    File.WriteAllText(_path, JsonConvert.SerializeObject(errorApiDial));
                                }
                                File.WriteAllText(_path, JsonConvert.SerializeObject(errorApiDial));
                            }
                            else if (errorApiDial.Count == 0)
                            {
                                errorApiDial.Add(new ApiAddToCampJson
                                {
                                    CampID = CampId,
                                    CustName = CustName,
                                    CustPhone = CustPhone,
                                    CustData = new List<CustDataList>()
                                    {
                                        new CustDataList()
                                        {
                                            CustData1 = CustData1
                                        }
                                    },
                                    DurumType = true
                                });


                                if (!File.Exists(_path))
                                {
                                    // Create a file to write to.
                                    File.WriteAllText(_path, JsonConvert.SerializeObject(errorApiDial));
                                }
                                File.WriteAllText(_path, JsonConvert.SerializeObject(errorApiDial));

                            }


                            //AnketListDataGridViewShow();

                        }


                    }

                }

                //anketDataList.Update();
                //anketDataList.Refresh();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private static int ApiGetCampaignsControlArea(string Data)
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
                                    where a.campName == Data
                                    select new
                                    {
                                        a.CampID
                                    }).FirstOrDefault();


                return activeCampID.CampID;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }
    }
}
