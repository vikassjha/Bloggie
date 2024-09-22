﻿
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace Bloggie.Web.Repositories
{
    public class CloudinaryImageRepositories : IImageRepositories
    {
        private readonly IConfiguration configuration;
        private readonly Account account;
        public CloudinaryImageRepositories(IConfiguration configuration)
        {
            this.configuration = configuration;
            account = new Account(configuration.GetSection("Cloudinary")["CloudName"],
                                  configuration.GetSection("Cloudinary")["ApiKey"],
                                  configuration.GetSection("Cloudinary")["ApiSecret"]


                );
        }
        async Task<string>  IImageRepositories.UploadAsync(IFormFile file)
        {
            var client = new Cloudinary(account);
            var uploadParams = new ImageUploadParams()
            { 
              File= new FileDescription(file.FileName,file.OpenReadStream()),
              DisplayName=file.FileName
            
            };
            var uploadResult =  await client.UploadAsync(uploadParams);
            if(uploadResult != null && uploadResult.StatusCode==System.Net.HttpStatusCode.OK)
            {

                return uploadResult.SecureUri.ToString();
            }
            return null;

        }
    }
}
