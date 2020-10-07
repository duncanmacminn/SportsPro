using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;
using SportsPro.DataLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;


namespace SportsPro.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("TempMessage")]
    public class TempMessageTagHelper : TagHelper
    {
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewCtx { get; set; }

        //public string Message
        //{
        //    get; //{ return "Hello"; }
        //    set;
        //    //{
        //    //    //HttpContext.Session.GetString("message");
        //    //}
        //}



        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var td = ViewCtx.TempData;
            if (td.Keys.Contains("message"))
            {
                output.TagName = "TempMessage";
                output.TagMode = TagMode.StartTagAndEndTag;
                var sb = new StringBuilder();
                sb.AppendFormat("<h3 class='bg-info text-center text-white p-2'>{0}</h3>", td["message"].ToString());
                output.PreContent.SetHtmlContent(sb.ToString());
            }
            else
            {
                output.SuppressOutput();
            }
        }
    }
}
