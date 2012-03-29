using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;
using System.Web.Routing;
using System.Collections;
using System.Globalization;

namespace System.Web.Mvc.Html
{
    public static class MvcHtmlExtensions
    {
        private static List<SelectListItem> TelefoneList()
        {
            SelectListItem li = new SelectListItem();
            li.Value = "1";
            li.Text = "Celular";

            SelectListItem li1 = new SelectListItem();
            li1.Value = "2";
            li1.Text = "Telefone";

            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(li);
            list.Add(li1);

            return list;
        }

        public static MvcHtmlString TelefoneDisplayFor(String codTipo)
        {

            String auxText = "";
            foreach (SelectListItem item in TelefoneList())
            {
                if (Int32.Parse(item.Value) == Int32.Parse(codTipo))
                {
                    auxText = item.Text;
                    break;
                }
            }

            return new MvcHtmlString(auxText);
        }

        public static MvcHtmlString TelefoneDropdown<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, bool editable)
        {

            List<SelectListItem> list = TelefoneList();

            if (editable)
            {
                return htmlHelper.DropDownListFor(
                    expression,
                    list.AsEnumerable()
                );
            }
            else
            {
                return htmlHelper.DropDownListFor(
                    expression,
                    list.AsEnumerable(),
                    new { @disabled=true }
                );
            }
        }

        public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            IEnumerable<TEnum> values = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();

            IEnumerable<SelectListItem> items =
                values.Select(value => new SelectListItem
                {
                    Text = value.ToString(),
                    Value = value.ToString(),
                    Selected = value.Equals(metadata.Model)
                });

            return htmlHelper.DropDownListFor(
                expression,
                items
                );
        }

        public static MvcHtmlString TextBoxFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression, bool editable)
        {
            MvcHtmlString html = default(MvcHtmlString);

            if (editable)
            {
                html = Html.InputExtensions.TextBoxFor(htmlHelper, expression);
            }
            else
            {
                html = Html.InputExtensions.TextBoxFor(htmlHelper, expression,
                    new { @class = "readOnly", @readonly = "read-only" });
            }
            return html;
        }

        public static MvcHtmlString DropDownListFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItem> selectList,
            object htmlAttributes, bool editable)
        {
            Func<TModel, TValue> deleg = expression.Compile();
            var result = deleg(htmlHelper.ViewData.Model);

            if (editable)
            {
                return Html.SelectExtensions.DropDownListFor(htmlHelper, expression, selectList, htmlAttributes);
            }
            else
            {
                string name = ExpressionHelper.GetExpressionText(expression);

                string selectedText = SelectInternal(htmlHelper, name, selectList);

                RouteValueDictionary routeValues = new RouteValueDictionary(htmlAttributes);
                routeValues.Add("class", "readOnly");
                routeValues.Add("readonly", "read-only");

                return Html.InputExtensions.TextBox(htmlHelper, name, selectedText, routeValues);
            }
        }

        private static string SelectInternal(HtmlHelper htmlHelper, string name, IEnumerable selectList)
        {
            ModelState state;

            string selectedText = string.Empty;

            string fullHtmlFieldName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);

            object obj2 = null;
            if (htmlHelper.ViewData.ModelState.TryGetValue(fullHtmlFieldName, out state) && (state.Value != null))
            {
                obj2 = state.Value.ConvertTo(typeof(string), null);
            }
            if (obj2 == null)
            {
                obj2 = htmlHelper.ViewData.Eval(fullHtmlFieldName);
            }

            if (obj2 != null)
            {
                IEnumerable source = ((IEnumerable)new object[] { obj2 });
                HashSet<string> set = new HashSet<string>(source.Cast<object>().Select<object, string>(delegate(object value)
                {
                    return Convert.ToString(value, CultureInfo.CurrentCulture);
                }), StringComparer.OrdinalIgnoreCase);
                foreach (SelectListItem item in selectList)
                {
                    if ((item.Value != null) ? set.Contains(item.Value) : set.Contains(item.Text))
                    {
                        selectedText = item.Text;
                        break;
                    }
                }
            }

            return selectedText;
        }

        public static MvcHtmlString TextBoxFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
           Expression<Func<TModel, TValue>> expression, object htmlAttributes, bool editable)
        {
            MvcHtmlString html = default(MvcHtmlString);

            if (editable)
            {
                html = Html.InputExtensions.TextBoxFor(htmlHelper, expression, htmlAttributes);
            }
            else
            {
                RouteValueDictionary routeValues = new RouteValueDictionary(htmlAttributes);
                routeValues.Add("class", "readOnly");
                routeValues.Add("readonly", "read-only");

                html = Html.InputExtensions.TextBoxFor(htmlHelper, expression, routeValues);
            }
            return html;
        }
    }




}