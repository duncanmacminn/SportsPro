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

namespace SportsPro.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("TempMessage")]
    public class TempMessageTagHelper : TagHelper
    {
        public string Message
        {
            get; //{ return "Hello"; }
            set;
            //{
            //    //HttpContext.Session.GetString("message");
            //}
        }



        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "MessageTag";
            output.TagMode = TagMode.StartTagAndEndTag;

            var sb = new StringBuilder();
            sb.AppendFormat("<h3 class='bg-info text-center text-white p-2'>{0}</h3>", this.Message);

            output.PreContent.SetHtmlContent(sb.ToString());
        }
    }
}
