using System.Net;
using System.IO;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Xml;
using System.Xml.Linq;

namespace JSON_XML_Parser
{
    public class Address1
    {
        public string streetAddress { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string postalCode { get; set; }

        public List<Address1> addresses { get; set; }
    }

    public class PhoneNumber
    {
        public string type { get; set; }
        public string number { get; set; }
        public bool CanContact { get; set; }
    }

    public class Root
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public bool isEnrolled { get; set; }
        public int YearsEnrolled { get; set; }
        public Address1 address1 { get; set; }
        public object address2 { get; set; }
        public List<PhoneNumber> phoneNumbers { get; set; }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            var data = "";
            string link = "C:\\Users\\Gamer\\source\\repos\\ParseData_XML-JSON\\ParseData_XML-JSON\\Files\\SampleJSON.json";
            string Pathed = ($"{Directory.GetCurrentDirectory()}\\Files");
            string linkJ = $"{Pathed}\\SampleJSON.json";
            string linkX = $"{Pathed}\\SampleXML.xml";
            string linkO = $"{Pathed}\\OutDatat";

            WebRequest request = WebRequest.Create(linkJ);
            WebResponse response = request.GetResponse();

            using (Stream datastream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(datastream);
                string responseFromServer = reader.ReadToEnd();

                Root root = JsonConvert.DeserializeObject<Root>(responseFromServer);


                
                foreach (PhoneNumber p in root.phoneNumbers)
                {
                    data = p.number + '\n' +  root.firstName + '\n' + root.lastName + '\n' + root.isEnrolled + '\n' + root.YearsEnrolled + '\n' + p.type 
                        + '\n' + root.address1.streetAddress + '\n' + root.address1.city + '\n' + root.address1.state + '\n' + root.address1.postalCode;
                }
            }

          
            XmlDocument doc = new XmlDocument();
            var xml = XDocument.Load(linkX);

            var query = from c in xml.Root.Descendants("menu")
                        where (int)c.Attribute("name") < 4
                        select c.Element("price").Value + " " +
                               c.Element("uom").Value;

            using (StreamWriter outputFile = new StreamWriter(Path.Combine(linkX, "C:\\Users\\Gamer\\source\\repos\\JSON-XML_Parser\\JSON-XML_Parser\\bin\\Debug\\net6.0\\Files\\OutData.txt"), true))
            {
                outputFile.WriteLine(data + xml.Root.Value);
            }

        }
    }
}