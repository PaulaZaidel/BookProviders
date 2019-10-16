using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;
using System;

namespace BookProviders.App.Helpers
{

    [HtmlTargetElement("a", Attributes = "supress-claim-name")]
    [HtmlTargetElement("a", Attributes = "supress-claim-value")]
    public class DeleteElementByClaimTagHelper : TagHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public DeleteElementByClaimTagHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        [HtmlAttributeName("supress-claim-name")]

        public string IdentityClaimName { get; set; }

        [HtmlAttributeName("supress-claim-value")]
        public string IdentityClaimValue { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            var hasAccess = CustomAuthorization.ValidateUserClaims(_contextAccessor.HttpContext, IdentityClaimName, IdentityClaimValue);

            if (hasAccess)
                return;

            output.Attributes.RemoveAll("href");
            output.Attributes.Add(new TagHelperAttribute("style", "cursor:not-alowed"));
            output.Attributes.Add(new TagHelperAttribute("title", "You does not have permission"));
        }
    }

    [HtmlTargetElement("*", Attributes = "supress-by-action")]
    public class DeleteElementByActionTagHelper : TagHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public DeleteElementByActionTagHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        [HtmlAttributeName("supress-by-action")]

        public string ActionName { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            var action = _contextAccessor.HttpContext.GetRouteData().Values["action"].ToString();

            if (ActionName.Contains(action))
                return;

            output.SuppressOutput();
        }
    }
}
