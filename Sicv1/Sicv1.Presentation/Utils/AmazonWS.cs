using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Sicv1.Domain.Entities;
using Sicv1.Presentation.Models;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Sicv1.Presentation.Utils
{
    public static class AmazonWS
    {
        public static async Task<JsonResult> UploadImage(CouponViewModel model, Category category, byte[] vimage, int vresultado, JsonResult vOnSuccess, JsonResult vOnError)
        {
            IAmazonS3 client;
            using (client = new AmazonS3Client(RegionEndpoint.SAEast1))
            {
                var listResponse = client.ListObjectsV2(new ListObjectsV2Request { BucketName = "imagesoncobenefits", Prefix = "images/coupons" });
                var existsDirectory = listResponse.KeyCount;
                if (existsDirectory > 0)
                {
                    var req = new PutObjectRequest();
                    req.BucketName = ConfigurationManager.AppSettings["AWSBucketPath"].ToString();
                    req.CannedACL = S3CannedACL.PublicReadWrite;
                    req.Key = model.hdFileName;
                    using (var ms = new MemoryStream(vimage))
                    {
                        req.InputStream = ms;
                        await client.PutObjectAsync(req);
                    }
                    int resultado = vresultado;
                    return vOnSuccess;
                }
                else
                {
                    return vOnError;
                }
            }
        }

        public static async Task<JsonResult> UploadImage(NewsnessViewModel model, NewsNess newsness, byte[] vimage, int vresultado, JsonResult vOnSuccess, JsonResult vOnError)
        {
            IAmazonS3 client;
            using (client = new AmazonS3Client(RegionEndpoint.SAEast1))
            {
                var listResponse = client.ListObjectsV2(new ListObjectsV2Request { BucketName = "imagesoncobenefits", Prefix = "images/newness" });
                var existsDirectory = listResponse.KeyCount;
                if (existsDirectory > 0)
                {
                    var req = new PutObjectRequest();
                    req.BucketName = ConfigurationManager.AppSettings["AWSBucketPathNewsNess"].ToString();
                    req.CannedACL = S3CannedACL.PublicReadWrite;
                    req.Key = model.HDFILENAME;
                    using (var ms = new MemoryStream(vimage))
                    {
                        req.InputStream = ms;
                        await client.PutObjectAsync(req);
                    }
                    int resultado = vresultado;
                    return vOnSuccess;
                }
                else
                {
                    return vOnError;
                }
            }
        }

        public static async Task<JsonResult> UploadImage(CompanyViewModel model, Company company, byte[] vimage, int vresultado, JsonResult vOnSuccess, JsonResult vOnError)
        {
            IAmazonS3 client;
            using (client = new AmazonS3Client(RegionEndpoint.SAEast1))
            {
                var listResponse = client.ListObjectsV2(new ListObjectsV2Request { BucketName = "imagesoncobenefits", Prefix = "images/aliance" });
                var existsDirectory = listResponse.KeyCount;
                if (existsDirectory > 0)
                {
                    var req = new PutObjectRequest();
                    req.BucketName = ConfigurationManager.AppSettings["AWSBucketPathAlliances"].ToString();
                    req.CannedACL = S3CannedACL.PublicReadWrite;
                    req.Key = model.Logo;
                    using (var ms = new MemoryStream(vimage))
                    {
                        req.InputStream = ms;
                        await client.PutObjectAsync(req);
                    }
                    int resultado = vresultado;
                    return vOnSuccess;
                }
                else
                {
                    return vOnError;
                }
            }
        }
    }
}