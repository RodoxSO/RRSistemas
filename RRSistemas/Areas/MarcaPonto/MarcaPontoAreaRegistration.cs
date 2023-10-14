using System.Web.Mvc;

namespace RRSistemas.Areas.MarcaPonto
{
    public class MarcaPontoAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "MarcaPonto";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "MarcaPonto_default",
                "MarcaPonto/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                    new[] { "RRSistemas.Areas.MarcaPonto.Controllers" }
            );
        }
    }
}