using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Civionics.Controllers
{
    public class ImageController : Controller
    {
        //
        // GET: /Image/Get/green-orb
        /// <summary>
        /// Returns the contents of the specified image file. If no such
        /// file exists, returns 404
        /// </summary>
        /// <param name="id">The name of the image file to return</param>
        /// <returns>The contents of the selected image file</returns>
        public ActionResult Get(string id)
        {
            var path = "/Images/" + id + ".png";

            return base.File(path, "image/png");
        }
    }
}