using AspNetCore.Reporting;
using Microsoft.AspNetCore.Mvc;
using RRHH.Data;

namespace RRHH.Controllers
{
    public class ReportController : Controller
    {
        private readonly RRHHContext _context;
        public ReportController(RRHHContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult Pdf(string ReportID, string NombreDescarga, ReportArgs args = null)
        {
            LocalReport report = new LocalReport("Reports/Empleados.rdl");
            report.AddDataSource("DSEmpleado", _context.Empleado.ToList());
            byte[] data = report.Execute(RenderType.Pdf).MainStream;
            Response.Headers.Add("Content-Disposition", "inline; filename=" + NombreDescarga.Replace(" 	", "_") + ".pdf");
            return new FileContentResult(data, System.Net.Mime.MediaTypeNames.Application.Pdf);
        }
    }
    public class ReportArgs
    {
    }
}
