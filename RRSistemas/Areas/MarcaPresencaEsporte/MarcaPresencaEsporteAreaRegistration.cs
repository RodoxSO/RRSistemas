using System.Web.Mvc;

namespace RRSistemas.Areas.MarcaPresencaEsporte
{
    public class MarcaPresencaEsporteAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "MarcaPresencaEsporte";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "MarcaPresencaEsporte_default",
                "MarcaPresencaEsporte/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                    new[] { "RRSistemas.Areas.MarcaPresencaEsporte.Controllers" }
            );
        }
    }
}