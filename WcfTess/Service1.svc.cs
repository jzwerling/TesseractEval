using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;
using Tesseract;

namespace WcfTess
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        String TessDataPath = System.Web.Hosting.HostingEnvironment.MapPath("~/tessdata/");
        String TiffPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Sample.tif");

        
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }
  
        public string OCRTiff(string path)
        {
            String PageText; String Result = String.Empty;
            try
            {

                using (TesseractEngine TE = new TesseractEngine(TessDataPath, "eng", EngineMode.Default))
                {
                    using (Pix Image = Pix.LoadFromFile(TiffPath))
                    {
                        PageText = TE.Process(Image).GetText();
                    }
                }
                String TextFilePath = string.Format("{0}{1}", TiffPath.Substring(0, TiffPath.Length - 4), ".txt");

                File.WriteAllText(TextFilePath, PageText);

                Result = "Sucess!";

            }
            catch (Exception Exception)
            {
                Result = String.Format("Error: {0}", Exception.Message);
            }

            return Result;
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
