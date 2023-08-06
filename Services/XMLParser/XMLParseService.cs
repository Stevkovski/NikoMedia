using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Services.XMLParser
{
    public class XMLParseService : IXMLParseService
    {
        public List<ClientData> ParseXml(string xmlString)
        {
            List<ClientData> clientDataList = new List<ClientData>();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);

            XmlNodeList clientNodes = xmlDoc.GetElementsByTagName("Client");
            foreach (XmlNode clientNode in clientNodes)
            {
                XmlElement clientElement = (XmlElement)clientNode;
                ClientData clientData = new ClientData();
                clientData.ClientId = Convert.ToInt32(clientElement.GetAttribute("ID"));

                XmlNode templateNode = clientElement.SelectSingleNode(".//Template");
                if (templateNode != null)
                {
                    XmlElement templateElement = (XmlElement)templateNode;
                    clientData.TemplateId = Convert.ToInt32(templateElement.GetAttribute("Id"));

                    XmlNode nameNode = templateElement.SelectSingleNode(".//Name");
                    if (nameNode != null)
                    {
                        clientData.TemplateName = nameNode.InnerText.Trim();
                    }

                    XmlNode marketingDataNode = templateElement.SelectSingleNode(".//MarketingData");
                    if (marketingDataNode != null)
                    {
                        clientData.MarketingData = marketingDataNode.InnerText.Trim();
                    }
                }

                clientDataList.Add(clientData);
            }

            return clientDataList;
        }

    }
}
