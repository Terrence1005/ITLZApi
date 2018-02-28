//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Web;

//namespace DocPlusApi.Models
//{
//    //folder store info
//    public class Resource
//    {
        
//        /// <summary>
//        /// Id for unique symbol
//        ///record Doc MD5
//        ///store file MD5 value of name and size 
//        /// </summary>
//        [Key]
//        public string Id { get; set; }

//        [Required]
//        public string Name { get; set; }

//        [Required]
//        public long Size { get; set; }

//        /// <summary>
//        /// check if file finished upload through http
//        /// </summary>
//        [Required]
//        public long Cursor { get; set; }

//        [Required]
//        public string Type { get; set; }

//        [Required]
//        public string Folder { get; set; }

//        [Required]
//        public int Clicks { get; set; }

//        /// <summary>
//        /// store time
//        /// </summary>
//        [Required]
//        public DateTime Published { get; set; }
//    }
//}