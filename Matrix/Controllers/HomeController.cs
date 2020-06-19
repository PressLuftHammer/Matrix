using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Matrix.Models;

namespace Matrix.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            using (my_testContext db = new my_testContext())
            {
                DateTime Start_Date = db.ObjectTrack
                                          .Min(a => a.CrtDate),
                         Finish_Date = db.ObjectTrack
                                        .Max(a => a.CrtDate)
                                        .AddMinutes(1);//DateTime.Now;

                return View(new Init_params { start_dt = Start_Date, finish_dt = Finish_Date });
                                    
            }
        } 

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
        public JsonResult GetReport(DateTime Start,DateTime Finish)
        {
            using (my_testContext db = new my_testContext())
            {
                DateTime start = DateTime.Now;

                var md = Config.instance
                                .zones
                                .Select((z, idx) => new ZoneReport
                                {
                                    Num = idx + 1,
                                    zone_name = z.zone_name,
                                    BriketCount = db.ObjectTrack
                                                    .Where(r => r.X >= z.x1 && r.X <= z.x2 &&
                                                            r.CrtDate>=Start  && r.CrtDate<=Finish)
                                                    .Select(a => a.ObjectId)
                                                    .Distinct()
                                                    .Count()

                                })
                                .ToArray();

                DateTime finish = DateTime.Now;

                return Json(new
                {
                    zones = md,
                    query_time = (finish - start).Milliseconds
                });
            }
            
        }
    }
}
