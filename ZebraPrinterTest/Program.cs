using System;
using System.Text;
using Zebra.Sdk.Comm;

namespace ZebraPrinterTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            const float labelWithInches = 3.34f;
            const float labelHeighInches = 1.8f;
            const int dotsPerInch = 203;

            const float labelWidth = labelWithInches * dotsPerInch;
            const float labelHeight = labelHeighInches * dotsPerInch;
            const float margin = 0.1f * dotsPerInch;
            const float letterHeightU = 0.290f * dotsPerInch;
            const float letterWidthU = 0.261f * dotsPerInch;
            const float letterHeightR = 0.172f * dotsPerInch;
            const float letterWidthR = 0.153f * dotsPerInch;
            const float letterHeightQ = 0.138f * dotsPerInch;
            const float letterWidthQ = 0.118f * dotsPerInch;

            string name = "Bo Ib";
            string dateOfVisit = $"{DateTime.Now:d MMMM, yyyy}";
            string host = "Henning Hansen Larsen";

            var nameStart = (int)(labelWidth - name.Length * letterWidthU) / 2;

            if (nameStart < 0)
            {
                nameStart = 0;
            }

            const string visitorHeader = "VISITOR";
            const string dateHeader = "DATE OF VISIT";
            const string hostHeader = "HOST";

            var labelString = $@"^XA^CI28
                        ^FO{(int)(labelHeight - margin - letterHeightU)},{margin}^AUR,{(int)letterHeightU},{(int)letterWidthU}^FD{visitorHeader}^FS
                        ^FO190,{nameStart}^AUR,{(int)letterHeightU},{(int)letterWidthU}^FD{name}^FS
                        ^FO80,{(int)margin}^ARR,{(int)letterHeightR},{(int)letterWidthR}^FD{dateHeader}^FS
                        ^FO80,{(int)(labelWidth - margin - (hostHeader.Length * letterWidthR))}^ARR,{(int)letterHeightR},{(int)letterWidthR}^FD{hostHeader}^FS
                        ^FO{(int)margin},{(int)margin}^AQR,{(int)letterHeightQ},{(int)letterWidthQ}^FD{dateOfVisit}^FS
                        ^FO{(int)margin},{(int)(labelWidth - margin - (host.Length * letterWidthQ))}^AQR,{(int)letterHeightQ},{(int)letterWidthQ}^FD{host}^FS
                        ^XZ";

            var printerConnection = new DriverPrinterConnection("ZDesigner ZD420-203dpi ZPL");
            printerConnection.Open();
            printerConnection.Write(Encoding.UTF8.GetBytes(labelString));
            printerConnection.Close();
        }
    }
}
