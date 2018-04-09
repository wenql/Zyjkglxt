using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace TcmHMS.Web
{
    public static class RazorExtensions
    {
        /// <summary>
        /// ng输入框
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="helper"></param>
        /// <param name="expression">属性表达式</param>
        /// <param name="ngKey">ng属性</param>
        /// <returns></returns>
        public static MvcHtmlString NgEditorFor<TModel, TValue>(this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TValue>> expression, string ngKey)
        {
            return MvcHtmlString.Create(helper.TextBoxFor(expression,
                new
                {
                    @class = "form-control",
                    @ng_model = ngKey,
                    @placeholder = "请输入" + ModelMetadata.FromLambdaExpression(expression, helper.ViewData).DisplayName
                }).ToString());
        }

        /// <summary>
        /// ng输入框
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="helper"></param>
        /// <param name="expression">属性表达式</param>
        /// <param name="ngKey">ng属性</param>
        /// <returns></returns>
        public static MvcHtmlString NkTextAreaFor<TModel, TValue>(this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TValue>> expression, string ngKey)
        {
            return MvcHtmlString.Create(helper.TextAreaFor(expression,
                new
                {
                    @class = "form-control",
                    @ng_model = ngKey,
                    @placeholder = "请输入" + ModelMetadata.FromLambdaExpression(expression, helper.ViewData).DisplayName
                }).ToString());
        }
    }
}