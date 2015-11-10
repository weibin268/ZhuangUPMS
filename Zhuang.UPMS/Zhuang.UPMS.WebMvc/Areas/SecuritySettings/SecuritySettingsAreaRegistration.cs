﻿using System.Web.Mvc;

namespace Zhuang.UPMS.WebMvc.Areas.SecuritySettings
{
    public class SecuritySettingsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SecuritySettings";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SecuritySettings_default",
                "SecuritySettings/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}