﻿using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Web;
using System.Net;
using System.Timers;
using System.Net.Http;
using System.Collections.Specialized;
using RestSharp;
using System.Xml;
// using Telesign;

namespace SMS_Sender
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string keyName = @"HKEY_CURRENT_USER\SOFTWARE\SMS Sender";
            string valueName = "registered";

            if ((string)Registry.GetValue(keyName, valueName, "0") == "0" || Registry.GetValue(keyName, valueName, "0") == null)
            {
                //code if key Not Exist
                Form5 dlg = new Form5();
                dlg.ShowDialog();
            }

            //int counter = 0;
            string line;

            // Read the file and display it line by line.
            System.IO.StreamReader file = new System.IO.StreamReader("senders.txt");
            while ((line = file.ReadLine()) != null)
            {
                comboBox1.Items.Add(line);
                //counter++;
            }

            comboBox1.SelectedIndex = 0;

            file.Close();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            count = 0;
            Tick();
        }

        private int count;
        private string[] allLines;
        private string phone_numbers;

        void Tick()
        {
            allLines = textBox2.Text.Split('\n');
            if (count == allLines.Length)
            {
                MessageBox.Show("Sent All");
                return;
            }
            
            count++;
            WorkThreadFunction(allLines[count - 1]);
            
            /*
            phone_numbers = textBox2.Text;
            phone_numbers = phone_numbers.Replace("\r\n", ",");
            WorkThreadFunction(phone_numbers);
            */
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
        
        public void WorkThreadFunction(string phone_number)
        {
            try
            {
                /* Mocean API */

                /*
                string mocean_api_key = "dadcf426";
                string mocean_api_secret = "d712f7f4";
                string senderid = comboBox1.GetItemText(comboBox1.SelectedItem);
                string message = textBox1.Text;
                string path = "https://rest-api.moceansms.com/rest/1/sms?mocean-api-key=" + mocean_api_key + "&mocean-api-secret=" + mocean_api_secret +
                    "&mocean-to=" + phone_number + "&mocean-from=" + WebUtility.UrlEncode(senderid) + "&mocean-text=" + WebUtility.UrlEncode(message);
                */

                /* Textbelt API */

                /*
                string message = textBox1.Text;
                string key = "bf2d7b712722c605b36bf47262396014ad12c0d3ofgXWorFXWdY97vaADtUfwZRP";
                string post_data = $"phone={phone_number}&message={message}&key={key}";
                string data1 = System.Net.WebUtility.UrlEncode(post_data);
                data1 = data1.Replace("%3D", "=").Replace("+", "%20").Replace("%26", "&");
                byte[] data = Encoding.ASCII.GetBytes(data1);

                WebRequest request = WebRequest.Create("https://textbelt.com/text");
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                string responseContent = null;

                using (WebResponse response = request.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader sr99 = new StreamReader(stream))
                        {
                            responseContent = sr99.ReadToEnd();
                        }
                    }
                }
                // MessageBox.Show(responseContent);
                */

                /* CheapSMS API */

                /*
                string senderid = comboBox1.GetItemText(comboBox1.SelectedItem);
                string message = textBox1.Text;
                string path = "http://198.24.149.4/API/pushsms.aspx?loginID=narinmoor3&password=123456&mobile=" + phone_numbers + "&text=" + WebUtility.UrlEncode(message) + "&senderid=" + senderid + "&route_id=17&Unicode=0";
                                
                WebRequest wrGETURL;
                wrGETURL = WebRequest.Create(path);

                string responseContent = null;

                using (WebResponse response = wrGETURL.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader sr99 = new StreamReader(stream))
                        {
                            responseContent = sr99.ReadToEnd();
                        }
                    }
                }
                MessageBox.Show(responseContent);
                */

                /* voodooSMS API */

                /*
                string senderid = comboBox1.GetItemText(comboBox1.SelectedItem);
                string message = textBox1.Text;
                string path = "https://www.voodooSMS.com/vapi/server/sendSMS?dest=" + phone_numbers + "&msg=" + WebUtility.UrlEncode(message) + "&orig=" + senderid + "&uid=narinmoor3_api&pass=dj6oz52&validity=1&format=json";

                WebRequest wrGETURL;
                wrGETURL = WebRequest.Create(path);

                string responseContent = null;

                using (WebResponse response = wrGETURL.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader sr99 = new StreamReader(stream))
                        {
                            responseContent = sr99.ReadToEnd();
                        }
                    }
                }
                MessageBox.Show("All sent.");
                */

                /* clockworksms API */

                /*
                string senderid = comboBox1.GetItemText(comboBox1.SelectedItem);
                string message = textBox1.Text;
                string path = "https://api.clockworksms.com/http/send.aspx?key=d8202b58237aec8eb08df9705720f60404faa9b9&to=" + phone_number + "&content=" + WebUtility.UrlEncode(message) + "&from=" + senderid;
                                
                WebRequest wrGETURL;
                wrGETURL = WebRequest.Create(path);

                WebResponse response = wrGETURL.GetResponse();
                response.Close();
                Tick();
                */

                /* messagebird API */

                /*
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                string senderid = comboBox1.GetItemText(comboBox1.SelectedItem);
                string message = textBox1.Text;
                string post_data = "{\"recipients\":\"" + phone_number + "\",\"originator\":\"" + senderid + "\",\"body\":\"" + message + "\"}";
                WebRequest request = WebRequest.Create("https://rest.messagebird.com/messages");

                var httpWebRequest = (HttpWebRequest)request;
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                httpWebRequest.PreAuthenticate = true;
                request.Headers.Add("Authorization", "AccessKey otUeLaxdY1IqZtSRfo8vV7ski");
                */

                /* telesign API */

                /*
                string customerId = "F9B9393D-B41F-4CAB-8244-7FB1AFBD02B1";
                string apiKey = "ocpD0JpavouG5dkzNrUZjnGDdQN29JtpeEpwEhfu4STsiBfda+uNk5QOxg498VGaCIMNe8Ljf5y6XsjRZ7WaYg==";

                string phoneNumber = "447516923944";

                string message = "Hello";
                string messageType = "ARN";

                try
                {
                    MessagingClient messagingClient = new MessagingClient(customerId, apiKey, "https://rest-api.telesign.com");
                    RestClient.TelesignResponse telesignResponse = messagingClient.Message(phoneNumber, message, messageType);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                */

                /* clxcommunications API */

                /*
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                string senderid = comboBox1.GetItemText(comboBox1.SelectedItem);
                string message = textBox1.Text;
                // string post_data = "{\"from\":\"demo\",\r\n\"to\":[\"447936973937\",\"447542897252\"],\r\n\"body\":\"hi, this is\"}";
                string post_data = "{\"from\":\"" + senderid + "\",\r\n\"to\":[\"" + phone_number.Replace(",", "\",\"") + "\"],\r\n\"body\":\"" + message + "\"}";
                WebRequest request = WebRequest.Create("https://api.clxcommunications.com/xms/v1/cheesmanbr12/batches");

                var httpWebRequest = (HttpWebRequest)request;
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                httpWebRequest.PreAuthenticate = true;
                request.Headers.Add("Authorization", "Bearer cf1e26c916e44171b6a4a880021962f8");
                */

                /* B-S-G API (Multi) */

                /*
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                string senderid = comboBox1.GetItemText(comboBox1.SelectedItem);
                string message = textBox1.Text;
                string[] phone_numbers = phone_number.Split(',');
                string last_phone = phone_numbers.Last();
                string phones = "";
                foreach (string number in phone_numbers)
                {
                    phones += "{\r\n\"msisdn\":\"" + number + "\",\r\n\"reference\":\"" + RandomString(10) + "\"\r\n}";
                    if (number != last_phone)
                    {
                        phones += ",\r\n";
                    }
                }
                
                string post_data = "{\"originator\":\"" + senderid + "\",\r\n\"body\":\"" + message + "\",\r\n\"destination\":\"phones\",\r\n\"phones\":[" + phones + "]}";
                WebRequest request = WebRequest.Create("https://app.bsg.hk/rest/sms/create");

                var httpWebRequest = (HttpWebRequest)request;
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "PUT";
                httpWebRequest.PreAuthenticate = true;
                request.Headers.Add("X-API-KEY", "live_ul4B1qFXA1ml285MVvrv");
                */

                /* B-S-G API (Single) */


                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                string senderid = comboBox1.GetItemText(comboBox1.SelectedItem);
                string message = textBox1.Text;
                
                string post_data = "{\"destination\":\"phone\",\r\n\"originator\":\"" + senderid + "\",\r\n\"body\":\"" + message + "\",\r\n\"msisdn\":\"" + phone_number + "\",\r\n\"reference\":\"" + RandomString(16) + "\"\r\n}";
                WebRequest request = WebRequest.Create("https://app.bsg.hk/rest/sms/create");

                var httpWebRequest = (HttpWebRequest)request;
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "PUT";
                httpWebRequest.PreAuthenticate = true;
                request.Headers.Add("X-API-KEY", "live_ul4B1qFXA1ml285MVvrv");


                /* Gateway API */

                /*
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                string senderid = comboBox1.GetItemText(comboBox1.SelectedItem);
                string message = textBox1.Text;
                var apiKey = "OCBnh8QnPgpJGIsGkoQObkU_";
                var apiSecret = "9NzpaCQ2Eq&Dk09oA*uJ!uftaJgJm7vDW#5ZfiEw";
                var client = new RestSharp.RestClient("https://gatewayapi.com/rest/");
                string[] phone_numbers = phone_number.Split(',');

                client.Authenticator = RestSharp.Authenticators.OAuth1Authenticator.ForRequestToken(apiKey, apiSecret);
                var request = new RestSharp.RestRequest("mtsms", RestSharp.Method.POST);

                request.AddJsonBody(new {
                    sender = senderid,
                    message = message,
                    recipients = phone_numbers.Select(number => new { msisdn = number })
                });
                var response = client.Execute(request);

                if ((int) response.StatusCode == 200)
                {
                    var res = Newtonsoft.Json.Linq.JObject.Parse(response.Content);
                    MessageBox.Show("All sent.");
                } else if (response.ResponseStatus == RestSharp.ResponseStatus.Completed) {
                    MessageBox.Show(response.Content);
                } else {
                    MessageBox.Show(response.ErrorMessage);
                }
                */

                /* Gateway API without using RestSharp */

                /*
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                string senderid = comboBox1.GetItemText(comboBox1.SelectedItem);
                string message = textBox1.Text;
                string[] phone_numbers = phone_number.Split(',');
                string last_phone = phone_numbers.Last();
                string phones = "";
                foreach (string number in phone_numbers)
                {
                    phones += "{ \"msisdn\":" + number + " }";
                    if (number != last_phone)
                    {
                        phones += ",";
                    }
                }
                
                string post_data = "{\"sender\":\"" + senderid + "\",\r\n\"message\":\"" + message + "\",\r\n\"recipients\":[" + phones + "]}";
                WebRequest request = WebRequest.Create("https://gatewayapi.com/rest/mtsms");

                var httpWebRequest = (HttpWebRequest)request;
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                httpWebRequest.PreAuthenticate = true;
                request.Headers.Add("Authorization", "Basic Zm9XNW1qZUhST0tCMUtHb2VUZzN6eUFJVjVJenNPRnlSNUlfT1JpY2pOYlJnNWljbUJ0RE5QLW1KUFFyeTVWajo=");
                */

                /* cmtelecom API */

                /*
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                string senderid = comboBox1.GetItemText(comboBox1.SelectedItem);
                string message = textBox1.Text;
                // string post_data = "{"messages": {"authentication": {"producttoken": "7f626247-3366-492e-b010-41ee4d8f36c3"}, "msg": [ { "from": "SenderName1", "to": [{ "number": "00447799783289" }, {number": "00447516923946"}], "body": {"content": "Test message1" }}] }}";
                
                string[] phone_numbers = phone_number.Split(',');
                string[] numbers = new string[phone_numbers.Length];

                for (int i = 0; i < phone_numbers.Length; i++)
                {
                    numbers[i] = "{ \"number\": \"" + phone_numbers[i] + "\" }";
                }

                string str_numbers = string.Join(",", numbers);
                
                string post_data = "{\"messages\": {\"authentication\": {\"producttoken\": \"7f626247-3366-492e-b010-41ee4d8f36c3\"}, \"msg\":[ { \"from\": \"" + senderid + "\", \"to\":[" + str_numbers + "], \"body\": {\"content\":\"" + message + "\" } } ] } }";
                WebRequest request = WebRequest.Create("https://gw.cmtelecom.com/v1.0/message");
                var httpWebRequest = (HttpWebRequest)request;
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                */

                /* clickSMS API */

                /*
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                string senderid = comboBox1.GetItemText(comboBox1.SelectedItem);
                string message = textBox1.Text;
                // string post_data = "{\"from\":\"demo\",\r\n\"to\":[\"447936973937\",\"447542897252\"],\r\n\"body\":\"hi, this is\"}";
                string[] phone_numbers = phone_number.Split(',');

                XmlDocument doc = new XmlDocument();
                XmlElement msg = (XmlElement)doc.AppendChild(doc.CreateElement("Msg"));
                msg.AppendChild(doc.CreateElement("Txn")).InnerText = "submitsms";
                msg.AppendChild(doc.CreateElement("AccountID")).InnerText = "narinmoor3";
                msg.AppendChild(doc.CreateElement("Password")).InnerText = "YWtqMq93";
                msg.AppendChild(doc.CreateElement("SenderID")).InnerText = senderid;
                msg.AppendChild(doc.CreateElement("Message")).InnerText = message;
                msg.AppendChild(doc.CreateElement("RateCode")).InnerText = "1";
                XmlElement mobiles = (XmlElement)msg.AppendChild(doc.CreateElement("Mobiles"));
                foreach (string number in phone_numbers)
                {
                    mobiles.AppendChild(doc.CreateElement("MobileNo")).InnerText = number;
                }
                
                string post_data = doc.OuterXml;
                WebRequest request = WebRequest.Create("http://service.clicksms.co.uk");

                var httpWebRequest = (HttpWebRequest)request;
                httpWebRequest.ContentType = "text/xml";
                httpWebRequest.Method = "POST";
                */

                
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = post_data;
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                string responseContent = null;

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    responseContent = streamReader.ReadToEnd();
                }

                Tick();

                /*
                MessageBox.Show("All sent.");
                */
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form6 dlg = new Form6();
            dlg.ShowDialog();
        }
    }
}
