using Sicv1.Presentation.Models;
using System;
using System.IO;

namespace Sicv1.Presentation.Utils
{
    public static class Images
    {
        public static string GetImageB64(CouponViewModel model, string imageExtension)
        {
            if (model.imageBase64 == "" || model.imageBase64 == null)
            {
                return "";
            }
            return model.imageBase64.Replace(imageExtension, "").Replace(";base64,", "");
        }

        public static string GetImageB64NewsNess(NewsnessViewModel model, string imageExtension)
        {
            if (model.IMAGEBASE64 == "" || model.IMAGEBASE64 == null)
            {
                return "";
            }
            return model.IMAGEBASE64.Replace(imageExtension, "").Replace(";base64,", "");
        }

        public static string GetImageB64(CompanyViewModel model, string imageExtension)
        {
            if (model.ImageBase64 == "" || model.ImageBase64 == null)
            {
                return "";
            }
            return model.ImageBase64.Replace(imageExtension, "").Replace(";base64,", "");
        }


        public static byte[] FromB64ToStr(string imagebase64)
        {
            return Convert.FromBase64String(imagebase64);
        }

        public static string GetExtension(string str)
        {
            var extension = (str.Split(';')[0]).Replace("data:image/","");
            return extension;
        }

        public static string GetDataImageExtension(CouponViewModel model)
        {
            if (model.imageBase64 == "" || model.imageBase64==null)
            {
                return "";
            }

            var extension = model.imageBase64.Split(';')[0];
            return extension;
        }

        public static string GetDataImageExtensionNewsNess(NewsnessViewModel model)
        {
            if (model.IMAGEBASE64 == "" || model.IMAGEBASE64 == null)
            {
                return "";
            }

            var extension = model.IMAGEBASE64.Split(';')[0];
            return extension;
        }

        public static string GetDataImageExtension(CompanyViewModel model)
        {
            if (model.ImageBase64 == "" || model.ImageBase64 == null)
            {
                return "";
            }

            var extension = model.ImageBase64.Split(';')[0];
            return extension;
        }

        public static bool DirectoryExists(string urlPath)
        {
            return Directory.Exists(urlPath);
        }

        public static string GetUrlPath(string server, string folder)
        {
            return server + folder;
        }

        public static string PathToSave(CouponViewModel model, string imageExtension, string urlPath)
        {
            var result = urlPath + model.hdFileName + "." + imageExtension;
            return result.Replace("data:image/", "");
        }
    }
}