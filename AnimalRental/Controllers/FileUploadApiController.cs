using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Umbraco.Web.WebApi;

namespace AnimalRental.Controllers
{
    /// <summary>
    /// Class for managing file upload in HTML forms
    /// </summary>
    public class FileUploadApiController : UmbracoApiController
    {
        /// <summary>
        /// Manages immage upload
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [MemberAuthorize]
        public async Task<HttpResponseMessage> Upload()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
            }

            var provider = GetMultipartProvider();
            var result = await Request.Content.ReadAsMultipartAsync(provider);

            
            // uploadedFileInfo object will give you some additional stuff like file length,
            // creation time, directory name, a few filesystem methods etc..
            var uploadedFileInfo = new FileInfo(result.FileData.First().LocalFileName);

            // Work-around for adding the extension (IIS doesn't let to load image without extension)
            File.Move(uploadedFileInfo.FullName, uploadedFileInfo.FullName + ".png");

            // Through the request response you can return an object to the Angular controller
            // You will be able to access this in the .success callback through its data attribute
            // If you want to send something to the .error callback, use the HttpStatusCode.BadRequest instead
            var fileUrl = uploadedFileInfo.Name + ".png";
            return Request.CreateResponse(HttpStatusCode.OK, new { fileUrl });
        }

        // You could extract these two private methods to a separate utility class since
        // they do not really belong to a controller class but that is up to you
        private MultipartFormDataStreamProvider GetMultipartProvider()
        {
            // IMPORTANT: replace "(tilde)" with the real tilde character
            // (our editor doesn't allow it, so I just wrote "(tilde)" instead)
            const string uploadFolder = "~/AnimalPhotos"; // you could put this to web.config
            var root = HttpContext.Current.Server.MapPath(uploadFolder);
            Directory.CreateDirectory(root);
            return new MultipartFormDataStreamProvider(root);
        }
        
    }
}
