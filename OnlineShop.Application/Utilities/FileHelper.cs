using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Application.Utilities
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
