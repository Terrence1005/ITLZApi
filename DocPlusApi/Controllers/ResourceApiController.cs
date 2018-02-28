using DocPlusApi.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using DocPlusApi.Core;

namespace DocPlusApi.Controllers
{
    [RoutePrefix("Res")]
    public class ResourceApiController : ApiController
    {
        // DB Operations
        private WebResourceContextEntities db = new WebResourceContextEntities();

        //Maximum Storage
        private static readonly long MEMORY_SIZE = 64 * 1024 * 1024;

        //Default Storage Location
        private static readonly string ROOT_PATH = HttpContext.Current.Server.MapPath("~/App_Data/");

        //ContentType Dictionary
        private static Dictionary<string, string> CONTENT_TYPE = null;
        
        /// <summary>
        /// Init Dictionary 
        /// </summary>
        static ResourceApiController()
        {
            CONTENT_TYPE = new Dictionary<string, string>();
            CONTENT_TYPE.Add("ex", "application/andrew-inset");
            CONTENT_TYPE.Add("hqx", "application/mac-binhex40");
            CONTENT_TYPE.Add("cpt", "application/mac-compactpro");
            CONTENT_TYPE.Add("doc", "application/msword");
            CONTENT_TYPE.Add("docx", "application/msword");
            CONTENT_TYPE.Add("bin", "application/octet-stream");
            CONTENT_TYPE.Add("dms", "application/octet-stream");
            CONTENT_TYPE.Add("lha", "application/octet-stream");
            CONTENT_TYPE.Add("lzh", "application/octet-stream");
            CONTENT_TYPE.Add("exe", "application/octet-stream");
            CONTENT_TYPE.Add("class", "application/octet-stream");
            CONTENT_TYPE.Add("so", "application/octet-stream");
            CONTENT_TYPE.Add("dll", "application/octet-stream");
            CONTENT_TYPE.Add("oda", "application/oda");
            CONTENT_TYPE.Add("pdf", "application/pdf");
            CONTENT_TYPE.Add("ai", "application/postscript");
            CONTENT_TYPE.Add("eps", "application/postscript");
            CONTENT_TYPE.Add("ps", "application/postscript");
            CONTENT_TYPE.Add("smi", "application/smil");
            CONTENT_TYPE.Add("smil", "application/smil");
            CONTENT_TYPE.Add("mif", "application/vnd.mif");
            CONTENT_TYPE.Add("xls", "application/vnd.ms-excel");
            CONTENT_TYPE.Add("xlsx", "application/vnd.ms-excel");
            CONTENT_TYPE.Add("ppt", "application/vnd.ms-powerpoint");
            CONTENT_TYPE.Add("wbxml", "application/vnd.wap.wbxml");
            CONTENT_TYPE.Add("wmlc", "application/vnd.wap.wmlc");
            CONTENT_TYPE.Add("wmlsc", "application/vnd.wap.wmlscriptc");
            CONTENT_TYPE.Add("bcpio", "application/x-bcpio");
            CONTENT_TYPE.Add("vcd", "application/x-cdlink");
            CONTENT_TYPE.Add("pgn", "application/x-chess-pgn");
            CONTENT_TYPE.Add("cpio", "application/x-cpio");
            CONTENT_TYPE.Add("csh", "application/x-csh");
            CONTENT_TYPE.Add("dcr", "application/x-director");
            CONTENT_TYPE.Add("dir", "application/x-director");
            CONTENT_TYPE.Add("dxr", "application/x-director");
            CONTENT_TYPE.Add("dvi", "application/x-dvi");
            CONTENT_TYPE.Add("spl", "application/x-futuresplash");
            CONTENT_TYPE.Add("gtar", "application/x-gtar");
            CONTENT_TYPE.Add("hdf", "application/x-hdf");
            CONTENT_TYPE.Add("js", "application/x-javascript");
            CONTENT_TYPE.Add("skp", "application/x-koan");
            CONTENT_TYPE.Add("skd", "application/x-koan");
            CONTENT_TYPE.Add("skt", "application/x-koan");
            CONTENT_TYPE.Add("skm", "application/x-koan");
            CONTENT_TYPE.Add("latex", "application/x-latex");
            CONTENT_TYPE.Add("nc", "application/x-netcdf");
            CONTENT_TYPE.Add("cdf", "application/x-netcdf");
            CONTENT_TYPE.Add("sh", "application/x-sh");
            CONTENT_TYPE.Add("shar", "application/x-shar");
            CONTENT_TYPE.Add("swf", "application/x-shockwave-flash");
            CONTENT_TYPE.Add("flv", "application/x-shockwave-flash");
            CONTENT_TYPE.Add("sit", "application/x-stuffit");
            CONTENT_TYPE.Add("sv4cpio", "application/x-sv4cpio");
            CONTENT_TYPE.Add("sv4crc", "application/x-sv4crc");
            CONTENT_TYPE.Add("tar", "application/x-tar");
            CONTENT_TYPE.Add("tcl", "application/x-tcl");
            CONTENT_TYPE.Add("tex", "application/x-tex");
            CONTENT_TYPE.Add("texinfo", "application/x-texinfo");
            CONTENT_TYPE.Add("texi", "application/x-texinfo");
            CONTENT_TYPE.Add("t", "application/x-troff");
            CONTENT_TYPE.Add("tr", "application/x-troff");
            CONTENT_TYPE.Add("roff", "application/x-troff");
            CONTENT_TYPE.Add("man", "application/x-troff-man");
            CONTENT_TYPE.Add("me", "application/x-troff-me");
            CONTENT_TYPE.Add("ms", "application/x-troff-ms");
            CONTENT_TYPE.Add("ustar", "application/x-ustar");
            CONTENT_TYPE.Add("src", "application/x-wais-source");
            CONTENT_TYPE.Add("xhtml", "application/xhtml+xml");
            CONTENT_TYPE.Add("xht", "application/xhtml+xml");
            CONTENT_TYPE.Add("zip", "application/zip");
            CONTENT_TYPE.Add("rar", "application/zip");
            CONTENT_TYPE.Add("gz", "application/x-gzip");
            CONTENT_TYPE.Add("bz2", "application/x-bzip2");
            CONTENT_TYPE.Add("au", "audio/basic");
            CONTENT_TYPE.Add("snd", "audio/basic");
            CONTENT_TYPE.Add("mid", "audio/midi");
            CONTENT_TYPE.Add("midi", "audio/midi");
            CONTENT_TYPE.Add("kar", "audio/midi");
            CONTENT_TYPE.Add("mpga", "audio/mpeg");
            CONTENT_TYPE.Add("mp2", "audio/mpeg");
            CONTENT_TYPE.Add("mp3", "audio/mpeg");
            CONTENT_TYPE.Add("aif", "audio/x-aiff");
            CONTENT_TYPE.Add("aiff", "audio/x-aiff");
            CONTENT_TYPE.Add("aifc", "audio/x-aiff");
            CONTENT_TYPE.Add("m3u", "audio/x-mpegurl");
            CONTENT_TYPE.Add("rmm", "audio/x-pn-realaudio");
            CONTENT_TYPE.Add("rmvb", "audio/x-pn-realaudio");
            CONTENT_TYPE.Add("ram", "audio/x-pn-realaudio");
            CONTENT_TYPE.Add("rm", "audio/x-pn-realaudio");
            CONTENT_TYPE.Add("rpm", "audio/x-pn-realaudio-plugin");
            CONTENT_TYPE.Add("ra", "audio/x-realaudio");
            CONTENT_TYPE.Add("wav", "audio/x-wav");
            CONTENT_TYPE.Add("wma", "audio/x-wma");
            CONTENT_TYPE.Add("pdb", "chemical/x-pdb");
            CONTENT_TYPE.Add("xyz", "chemical/x-xyz");
            CONTENT_TYPE.Add("bmp", "image/bmp");
            CONTENT_TYPE.Add("gif", "image/gif");
            CONTENT_TYPE.Add("ief", "image/ief");
            CONTENT_TYPE.Add("jpeg", "image/jpeg");
            CONTENT_TYPE.Add("jpg", "image/jpeg");
            CONTENT_TYPE.Add("jpe", "image/jpeg");
            CONTENT_TYPE.Add("png", "image/png");
            CONTENT_TYPE.Add("tiff", "image/tiff");
            CONTENT_TYPE.Add("tif", "image/tiff");
            CONTENT_TYPE.Add("djvu", "image/vnd.djvu");
            CONTENT_TYPE.Add("djv", "image/vnd.djvu");
            CONTENT_TYPE.Add("wbmp", "image/vnd.wap.wbmp");
            CONTENT_TYPE.Add("ras", "image/x-cmu-raster");
            CONTENT_TYPE.Add("pnm", "image/x-portable-anymap");
            CONTENT_TYPE.Add("pbm", "image/x-portable-bitmap");
            CONTENT_TYPE.Add("pgm", "image/x-portable-graymap");
            CONTENT_TYPE.Add("ppm", "image/x-portable-pixmap");
            CONTENT_TYPE.Add("rgb", "image/x-rgb");
            CONTENT_TYPE.Add("xbm", "image/x-xbitmap");
            CONTENT_TYPE.Add("xpm", "image/x-xpixmap");
            CONTENT_TYPE.Add("xwd", "image/x-xwindowdump");
            CONTENT_TYPE.Add("igs", "model/iges");
            CONTENT_TYPE.Add("iges", "model/iges");
            CONTENT_TYPE.Add("msh", "model/mesh");
            CONTENT_TYPE.Add("mesh", "model/mesh");
            CONTENT_TYPE.Add("silo", "model/mesh");
            CONTENT_TYPE.Add("wrl", "model/vrml");
            CONTENT_TYPE.Add("vrml", "model/vrml");
            CONTENT_TYPE.Add("css", "text/css");
            CONTENT_TYPE.Add("html", "text/html");
            CONTENT_TYPE.Add("htm", "text/html");
            CONTENT_TYPE.Add("asc", "text/plain");
            CONTENT_TYPE.Add("txt", "text/plain");
            CONTENT_TYPE.Add("rtx", "text/richtext");
            CONTENT_TYPE.Add("rtf", "text/rtf");
            CONTENT_TYPE.Add("sgml", "text/sgml");
            CONTENT_TYPE.Add("sgm", "text/sgml");
            CONTENT_TYPE.Add("tsv", "text/tab-separated-values");
            CONTENT_TYPE.Add("wml", "text/vnd.wap.wml");
            CONTENT_TYPE.Add("wmls", "text/vnd.wap.wmlscript");
            CONTENT_TYPE.Add("etx", "text/x-setext");
            CONTENT_TYPE.Add("xsl", "text/xml");
            CONTENT_TYPE.Add("xml", "text/xml");
            CONTENT_TYPE.Add("mpeg", "video/mpeg");
            CONTENT_TYPE.Add("mpg", "video/mpeg");
            CONTENT_TYPE.Add("mpe", "video/mpeg");
            CONTENT_TYPE.Add("qt", "video/quicktime");
            CONTENT_TYPE.Add("mov", "video/quicktime");
            CONTENT_TYPE.Add("mxu", "video/vnd.mpegurl");
            CONTENT_TYPE.Add("avi", "video/x-msvideo");
            CONTENT_TYPE.Add("movie", "video/x-sgi-movie");
            CONTENT_TYPE.Add("wmv", "video/x-ms-wmv");
            CONTENT_TYPE.Add("asf", "video/x-ms-asf");
            CONTENT_TYPE.Add("ice", "x-conference/x-cooltalk");
        }

        /// <summary>
        /// Get file Content Type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static string GetContentType(string type)
        {
            try
            {
                string contentType = CONTENT_TYPE[type];
                if (contentType != null)
                {
                    return contentType;
                }
            }
            catch { }

            return "application/octet-stream" + type;
        }

        /// <summary>
        /// get Content Disposition
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static string GetDisposition(string type)
        {
            //check use brower to open or annex
            if (GetContentType(type).StartsWith("image"))
            {
                return "inline";
            }

            return "attachment";
        }

        [HttpGet]
        [Route("{Id}")]
        public async Task<HttpResponseMessage> Get(string Id)
        {
            //304 check if contain tag
            var tag = Request.Headers.IfNoneMatch.FirstOrDefault();
            if (Request.Headers.IfModifiedSince.HasValue && tag != null && tag.Tag.Length > 0)
                return new HttpResponseMessage(HttpStatusCode.NotModified);

            //search for file
            Resource model = await db.Resources.FindAsync(Id);

            //Not found
            if (model == null)
                return new HttpResponseMessage(HttpStatusCode.BadRequest);

            // load file
            FileInfo info = new FileInfo(Path.Combine(ROOT_PATH, model.Folder, model.Id));

            //not found file
            if (!info.Exists)
                return new HttpResponseMessage(HttpStatusCode.BadRequest);

            FileStream file = null;

            try
            {
                //open file
                file = new FileStream(info.FullName, FileMode.Open, FileAccess.Read, FileShare.Read);
                //New Http Response
                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);

                // get header info
                // GetDisposition() to check open or download, load img, down file
                ContentDispositionHeaderValue disposition = new ContentDispositionHeaderValue(GetDisposition(model.Type));
                // set file name and extension
                disposition.FileName = string.Format("{0}.{1}", model.Name, model.Type);
                //file name
                disposition.Name = model.Name;
                // file size
                disposition.Size = file.Length;

                if (file.Length < MEMORY_SIZE)
                {
                    // size less than 64 MB copy to memory stream
                    byte[] bytes = new byte[file.Length];
                    await file.ReadAsync(bytes, 0, (int)file.Length);
                    file.Close();
                    MemoryStream ms = new MemoryStream(bytes);

                    result.Content = new ByteArrayContent(ms.ToArray());
                }
                else
                {
                    //larger than pack and return
                    result.Content = new StreamContent(file);
                }

                //GetContentType() search by extension 
                result.Content.Headers.ContentType = new MediaTypeHeaderValue(GetContentType(model.Type));
                result.Content.Headers.ContentDisposition = disposition;

                //set browser buffer
                //buffer expires
                result.Content.Headers.Expires = new DateTimeOffset(DateTime.Now).AddHours(1);
                //buffer last modified time
                result.Content.Headers.LastModified = new DateTimeOffset(model.Published);
                //buffer control
                result.Headers.CacheControl = new CacheControlHeaderValue()
                {
                    Public = true,
                    MaxAge = TimeSpan.FromHours(1)
                };
                //set ETag to directly use MD5
                result.Headers.ETag = new EntityTagHeaderValue(string.Format("\"{0}\"", model.Id));

                return result;
            }
            catch
            {
                if (file != null)
                {
                    file.Close();
                }
            }

            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Upload/{Id?}")]
        [ResponseType(typeof(Resource))]
        public async Task<IHttpActionResult> Post(string Id = null)
        {
            List<Resource> resources = new List<Resource>();

            //multipart form data
            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);

            foreach(var item in provider.Contents)
            {
                if (item.Headers.ContentDisposition.FileName != null)
                {
                    //stream
                    var ms = item.ReadAsStreamAsync().Result;
                    using (var br = new BinaryReader(ms))
                    {
                        if (ms.Length <= 0)
                            break;
                        var data = br.ReadBytes((int)ms.Length);

                        //MD5
                        string id = HashUtils.GetMD5Hash(data);

                        Resource temp = await db.Resources.FindAsync(id);
                        if (temp == null)
                        {
                            //Create
                            Resource resource = new Resource();
                            resource.Id = id;
                            resource.Size = ms.Length;
                            resource.Cursor = resource.Size;
                            resource.Published = DateTime.Now;
                            //Info
                            FileInfo info = new FileInfo(item.Headers.ContentDisposition.FileName.Replace("\"",""));
                            resource.Type = info.Extension.Substring(1).ToLower();
                            resource.Name = info.Name.Substring(0, info.Name.LastIndexOf("."));
                            //Relative
                            resource.Folder = DateTime.Now.ToString("yyyy/MM/dd/",DateTimeFormatInfo.InvariantInfo);

                            //Write
                            try
                            {
                                string dirPath = Path.Combine(ROOT_PATH, resource.Folder);
                                if (!Directory.Exists(dirPath))
                                {
                                    Directory.CreateDirectory(dirPath);
                                }

                                File.WriteAllBytes(Path.Combine(dirPath, resource.Id), data);
                                //Save to Database
                                db.Resources.Add(resource);
                                await db.SaveChangesAsync();
                                temp = await db.Resources.FindAsync(resource.Id);

                            }
                            catch { }
                        }
                        if (temp != null)
                            resources.Add(temp);
                    }
                }
            }

            if (resources.Count == 0)
                return BadRequest();
            else if (resources.Count == 1)
                return Ok(resources.FirstOrDefault());
            else
                return Ok(resources);


        }

    }
}
