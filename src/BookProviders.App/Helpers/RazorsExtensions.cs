using Microsoft.AspNetCore.Mvc.Razor;
using System;

namespace BookProviders.App.Helpers
{
    public static class RazorsExtensions
    {
        public static string FormatDocument(this RazorPage page, int catererType, string document)
        {
            return catererType == 1 ? Convert.ToUInt64(document).ToString(@"000\.000\.000\-00") :
                Convert.ToUInt64(document).ToString(@"00\.000\.000\/0000\-00");
        }
    }
}
