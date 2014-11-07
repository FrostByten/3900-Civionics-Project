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
        public ActionResult Get(string id)
        {
            var path = "/Images/" + id + ".png";

            return base.File(path, "image/png");
        }
    }
}