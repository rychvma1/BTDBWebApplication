#pragma checksum "C:\Users\matus\source\repos\WebApplication3\WebApplication3\Views\Visitor\SpecificData.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "54824b0f5bea0ee3a45ab7f01077279542747549"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Visitor_SpecificData), @"mvc.1.0.view", @"/Views/Visitor/SpecificData.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\matus\source\repos\WebApplication3\WebApplication3\Views\_ViewImports.cshtml"
using WebApplication3;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\matus\source\repos\WebApplication3\WebApplication3\Views\_ViewImports.cshtml"
using WebApplication3.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"54824b0f5bea0ee3a45ab7f01077279542747549", @"/Views/Visitor/SpecificData.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"463d1c12e8fc14b2589daa67feec3183fea97611", @"/Views/_ViewImports.cshtml")]
    public class Views_Visitor_SpecificData : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<WebApplication3.Models.v4.ModelObjectv4>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\matus\source\repos\WebApplication3\WebApplication3\Views\Visitor\SpecificData.cshtml"
  
    ViewData["Title"] = "Detail";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>");
#nullable restore
#line 6 "C:\Users\matus\source\repos\WebApplication3\WebApplication3\Views\Visitor\SpecificData.cshtml"
Write(ViewBag.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral(" detail</h1>\r\n\r\n<table class=\"table table-bordered table-hover\">\r\n    <thead>\r\n    <tr>\r\n        <th>Name</th>\r\n        <th>Type</th>\r\n        <th>Value</th>\r\n    </tr>\r\n    </thead>\r\n    <tbody>\r\n\r\n");
#nullable restore
#line 18 "C:\Users\matus\source\repos\WebApplication3\WebApplication3\Views\Visitor\SpecificData.cshtml"
     foreach (var obj in @Model)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\r\n            <td>");
#nullable restore
#line 21 "C:\Users\matus\source\repos\WebApplication3\WebApplication3\Views\Visitor\SpecificData.cshtml"
           Write(obj.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 22 "C:\Users\matus\source\repos\WebApplication3\WebApplication3\Views\Visitor\SpecificData.cshtml"
           Write(obj.Type);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 23 "C:\Users\matus\source\repos\WebApplication3\WebApplication3\Views\Visitor\SpecificData.cshtml"
           Write(obj.Value);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        </tr>\r\n");
#nullable restore
#line 25 "C:\Users\matus\source\repos\WebApplication3\WebApplication3\Views\Visitor\SpecificData.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </tbody>\r\n</table>\r\n\r\n");
#nullable restore
#line 30 "C:\Users\matus\source\repos\WebApplication3\WebApplication3\Views\Visitor\SpecificData.cshtml"
 if (ViewBag.Pages != null)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <ul class=\"pagination\">\r\n");
#nullable restore
#line 33 "C:\Users\matus\source\repos\WebApplication3\WebApplication3\Views\Visitor\SpecificData.cshtml"
         for (int i = 1; i <= ViewBag.Pages; i++)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <li");
            BeginWriteAttribute("class", " class=\"", 654, "\"", 705, 1);
#nullable restore
#line 35 "C:\Users\matus\source\repos\WebApplication3\WebApplication3\Views\Visitor\SpecificData.cshtml"
WriteAttributeValue("", 662, ViewBag.CurrentPage == i ? "active" : "", 662, 43, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                <a");
            BeginWriteAttribute("href", " href=\"", 727, "\"", 788, 1);
#nullable restore
#line 36 "C:\Users\matus\source\repos\WebApplication3\WebApplication3\Views\Visitor\SpecificData.cshtml"
WriteAttributeValue("", 734, Url.Action("SpecificData", "Visitor", new {page = i}), 734, 54, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 36 "C:\Users\matus\source\repos\WebApplication3\WebApplication3\Views\Visitor\SpecificData.cshtml"
                                                                            Write(i);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a>\r\n            </li>\r\n");
#nullable restore
#line 38 "C:\Users\matus\source\repos\WebApplication3\WebApplication3\Views\Visitor\SpecificData.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </ul>\r\n");
#nullable restore
#line 40 "C:\Users\matus\source\repos\WebApplication3\WebApplication3\Views\Visitor\SpecificData.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<p>");
#nullable restore
#line 42 "C:\Users\matus\source\repos\WebApplication3\WebApplication3\Views\Visitor\SpecificData.cshtml"
Write(Html.ActionLink("Back to table", "Index"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<WebApplication3.Models.v4.ModelObjectv4>> Html { get; private set; }
    }
}
#pragma warning restore 1591