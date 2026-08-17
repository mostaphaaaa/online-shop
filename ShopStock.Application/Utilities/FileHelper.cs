using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Application.Utilities
{
    public class FileHelper
    {
        public static void DeleteFile(string path)
        {
            if (File.Exists(path))
                File.Delete(path); 
        }
    }
}
