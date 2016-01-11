using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarcodeLib;
using DAL.Models;

namespace Domain.Contracts
{

  
    public class Barcode
    {
       
      
        public static string Encode(string barcode, string path)
        {
            var barcodeLib = new BarcodeLib.Barcode(barcode);
            barcodeLib.Encode(TYPE.CODE128, barcode);
            string filename = path + barcode + ".jpg";
            barcodeLib.SaveImage(filename, SaveTypes.JPG);
            barcodeLib.IncludeLabel = true;
            return filename;
        }


        public static string GenerateBarCodeId()
        {
            string chars = "0123456789";
            var random = new Random();
            var package = new Package();
            string result;
            PackagesContracts packageContracts = new PackagesContracts();
            do
            {
                result = new string(
                    Enumerable.Repeat(chars, 10)
                        .Select(s => s[random.Next(s.Length)])
                        .ToArray());
            } while (packageContracts.GetAll().Where(x => x.BarcodeId == Convert.ToString(result)) == null);


            return result;
        }
    }
}
