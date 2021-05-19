using Kentico.Web.Mvc;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Kentico.PageBuilder.Web.Mvc
{
    /// <summary>
    /// A static class implementing operations for working with inline editable areas.
    /// </summary>
    public static class InlineEditableAreas
    {
        /// <summary>
        /// Resolves the editable areas in the given HTML string and writes the result to the view context.
        /// </summary>
        /// <param name="htmlHelper">An <see cref="IHtmlHelper"/> object containing the view context.</param>
        /// <param name="html">The HTML string.</param>
        /// <returns>The return value is not used and will always be null.</returns>
        public static async Task<object> ResolveEditableAreasAsync(
            this IHtmlHelper htmlHelper,
            string html)
        {
            var components = Regex.Split(html, @"<editable-area id=""(.*?)""><\/editable-area>");
            for (var i = 0; i < components.Length; i++)
            {
                var isEditableArea = i % 2 == 1;
                if (isEditableArea)
                {
                    await RenderEditableAreaAsync(components[i]);
                }
                else
                {
                    RenderHtml(components[i]);
                }
            }

            return null;

            async Task RenderEditableAreaAsync(string identifier)
            {
                var kentico = htmlHelper.Kentico();
                RenderContent(await kentico.EditableAreaAsync(identifier));
            }

            void RenderHtml(string value) =>
                RenderContent(content: htmlHelper.Raw(value));

            void RenderContent(IHtmlContent content) =>
                htmlHelper.ViewContext.Writer.Write(content);
        }
    }
}
