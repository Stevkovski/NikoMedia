﻿using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.XMLParser
{
    public interface IXMLParseService
    {
        List<ClientData> ParseXml(string xmlString);
    }
}
