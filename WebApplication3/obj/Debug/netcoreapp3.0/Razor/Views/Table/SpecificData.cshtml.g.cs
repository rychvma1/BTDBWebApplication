#pragma checksum "C:\Users\matus\source\repos\WebApplication3\WebApplication3\Views\Table\SpecificData.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7dd1614ee04956ebc9dfb061192c8295ecac9bdf"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Table_SpecificData), @"mvc.1.0.view", @"/Views/Table/SpecificData.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7dd1614ee04956ebc9dfb061192c8295ecac9bdf", @"/Views/Table/SpecificData.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"463d1c12e8fc14b2589daa67feec3183fea97611", @"/Views/_ViewImports.cshtml")]
    public class Views_Table_SpecificData : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ModelObject>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\matus\source\repos\WebApplication3\WebApplication3\Views\Table\SpecificData.cshtml"
  
    ViewData["Title"] = "Detail";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>");
#nullable restore
#line 6 "C:\Users\matus\source\repos\WebApplication3\WebApplication3\Views\Table\SpecificData.cshtml"
Write(ViewBag.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral(" detail</h1>\r\n\r\n<table class=\"table table-bordered table-hover\">\r\n");
#nullable restore
#line 9 "C:\Users\matus\source\repos\WebApplication3\WebApplication3\Views\Table\SpecificData.cshtml"
     if (@ViewBag.Type.Equals("relation"))
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <thead>\r\n            <tr>\r\n                <th>Name</th>\r\n                <th>Type</th>\r\n                <th>Value</th>\r\n            </tr>\r\n        </thead>\r\n        <tbody>\r\n\r\n");
#nullable restore
#line 20 "C:\Users\matus\source\repos\WebApplication3\WebApplication3\Views\Table\SpecificData.cshtml"
         foreach (ModelObject obj in @Model.Value)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n");
#nullable restore
#line 33 "C:\Users\matus\source\repos\WebApplication3\WebApplication3\Views\Table\SpecificData.cshtml"
                 foreach (var o in @obj.Value)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <td>");
#nullable restore
#line 35 "C:\Users\matus\source\repos\WebApplication3\WebApplication3\Views\Table\SpecificData.cshtml"
                   Write(o.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 36 "C:\Users\matus\source\repos\WebApplication3\WebApplication3\Views\Table\SpecificData.cshtml"
                   Write(o.Type);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 37 "C:\Users\matus\source\repos\WebApplication3\WebApplication3\Views\Table\SpecificData.cshtml"
                   Write(o.LastValue);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n");
#nullable restore
#line 38 "C:\Users\matus\source\repos\WebApplication3\WebApplication3\Views\Table\SpecificData.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </tr>\r\n");
#nullable restore
#line 40 "C:\Users\matus\source\repos\WebApplication3\WebApplication3\Views\Table\SpecificData.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </tbody>\r\n");
#nullable restore
#line 43 "C:\Users\matus\source\repos\WebApplication3\WebApplication3\Views\Table\SpecificData.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</table>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ModelObject> Html { get; private set; }
    }
}
#pragma warning restore 1591
