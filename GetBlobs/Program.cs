using System;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage;

namespace GetBlobs
{
    class Program
    {
        static void Main(string[] args)
        {

            string containerurl = "";
            string downloadpath = "";
            string azuresastoken = "";
            containerurl = @"https://mystorage2022.blob.core.windows.net/container2022/"; //url of the contained to download blobs from
            downloadpath = @"C:\Downloads\"; //local folder where the blobs will be downloaded
            azuresastoken = "@sp = racwdli & st = 2022 - 01 - 24T08: 23:12Z & se = 2022 - 01 - 24T16: 23:12Z & spr = https & sv = 2020 - 08 - 04 & sr = c & sig = BDPaCQN6h % 2BWhq7ioc % 2FNMSGLU65cIRpDKB0XoTO8BHcc % 3D"; //the sas token generated to access the container.
            DownloadBlobsUsingSasToken(containerurl, downloadpath, azuresastoken);
        }

        private static void DownloadBlobsUsingSasToken(string azurecontainerurl, string downloaddirectory, string sastoken)

        {
            try
            {
                Azure.AzureSasCredential sascreds = new Azure.AzureSasCredential(sastoken);
                System.Uri containeruri = new Uri(azurecontainerurl);
                BlobContainerClient containerclient = new BlobContainerClient(containeruri, sascreds);
                BlobContainerClient bcc = new BlobContainerClient(containeruri, sascreds);

                foreach (BlobItem bi in bcc.GetBlobs())
                {
                    string fileName = downloaddirectory + bi.Name;
                    BlobClient blob = bcc.GetBlobClient(bi.Name);
                    blob.DownloadToAsync(fileName);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }
    }
}
