using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;
using System.Web.Routing;
using System.Collections;
using System.Globalization;
using System.Web.Security;

namespace System.Web.Mvc.Html
{
    public static class MvcHtmlExtensions
    {
        #region Telefone(Funcionário/Paciente) 
        private static List<SelectListItem> TelefoneList()
        {

            Array enums = Enum.GetValues(typeof(ClinicaMVC3.Controllers.TelefoneController.tipoTelefone));

            List<SelectListItem> list = new List<SelectListItem>();
            for (int i = 0; i < enums.Length; i++)
			{
                SelectListItem li = new SelectListItem();
                li.Value = (i + 1).ToString();
                li.Text = enums.GetValue(i).ToString();
                list.Add(li);
			}

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
                    new { @disabled = true }
                );
            }
        }

        #endregion

        #region Função(Funcionário)
        private static List<SelectListItem> FuncaoList()
        {
            Array enums = Enum.GetValues(typeof(ClinicaMVC3.Controllers.FuncionarioController.funcao));

            List<SelectListItem> list = new List<SelectListItem>();
            for (int i = 0; i < enums.Length; i++)
            {
                SelectListItem li = new SelectListItem();
                li.Value = (i + 1).ToString();
                li.Text = enums.GetValue(i).ToString();
                list.Add(li);
            }

            return list;
            
        }

        public static MvcHtmlString FuncaoDisplayFor(String codTipo)
        {

            String auxText = "";
            foreach (SelectListItem item in FuncaoList())
            {
                if (Int32.Parse(item.Value) == Int32.Parse(codTipo))
                {
                    auxText = item.Text;
                    break;
                }
            }

            return new MvcHtmlString(auxText);
        }

        public static MvcHtmlString FuncaoDropdown<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression)
        {

            List<SelectListItem> list = FuncaoList();

            return htmlHelper.DropDownListFor(
                expression,
                list.AsEnumerable()
            );

        }


        public static MvcHtmlString FuncaoDropdown<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, object htmlAttributes)
        {

            List<SelectListItem> list = FuncaoList();

            return htmlHelper.DropDownListFor(
                expression,
                list.AsEnumerable(),
                htmlAttributes
            );

        }

        #endregion

        #region Status(Consulta)
        private static List<SelectListItem> StatusList()
        {
            Array enums = Enum.GetValues(typeof(ClinicaMVC3.Controllers.ConsultaController.Status));

            List<SelectListItem> list = new List<SelectListItem>();
            for (int i = 0; i < enums.Length; i++)
            {
                SelectListItem li = new SelectListItem();
                li.Value = (i + 1).ToString();
                li.Text = enums.GetValue(i).ToString().Replace('_',' ').Replace("Nao","Não");
                list.Add(li);
            }

            return list;

        }

        public static MvcHtmlString StatusDisplayFor(String codTipo)
        {

            String auxText = "";
            foreach (SelectListItem item in StatusList())
            {
                if (Int32.Parse(item.Value) == Int32.Parse(codTipo))
                {
                    auxText = item.Text;
                    break;
                }
            }

            return new MvcHtmlString(auxText);
        }

        public static MvcHtmlString StatusDropdown<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression)
        {

            List<SelectListItem> list = StatusList();

            return htmlHelper.DropDownListFor(
                expression,
                list.AsEnumerable()
            );

            
        }


        public static MvcHtmlString StatusDropdown<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, object htmlAttributes)
        {

            List<SelectListItem> list = StatusList();

            return htmlHelper.DropDownListFor(
                expression,
                list.AsEnumerable(),
                htmlAttributes
            );

        }

        #endregion

        #region Outras extensões
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
            Expression<Func<TModel, TValue>> expression, String regex)
        {
            MvcHtmlString html = default(MvcHtmlString);

            String aux = Html.DisplayExtensions.DisplayFor(htmlHelper, expression).ToString();



            html = Html.InputExtensions.TextBoxFor(htmlHelper, expression);


              

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
        #endregion

        public static MvcHtmlString Warning(this HtmlHelper htmlHelper, String text)
        {
            String result = "<div class=\"ui-state-highlight ui-corner-all\">" +
                            "<span class=\"ui-icon ui-icon-info\" style=\"float: left; margin-right: .3em;\"></span>" + 
                            text + "</div>";
            return MvcHtmlString.Create(result);
        }

        public static MvcHtmlString Error(this HtmlHelper htmlHelper, dynamic text)
        {
            String result = "<div class=\"ui-state-error ui-corner-all\" style=\"padding: 0 .7em;\"><p>" +
                            "<span class=\"ui-icon ui-icon-alert\" style=\"float: left; margin-right: .3em;\"></span>" +
                            text +
                            "</p></div>";
            return MvcHtmlString.Create(result);
        }
    }

    public static class LinkExtensions
    {
        public static MvcHtmlString If(this MvcHtmlString value, bool evaluation)
        {
            return evaluation ? value : MvcHtmlString.Empty;
        }


        public static IHtmlString ActionLinkRoles(
            this HtmlHelper htmlHelper,
            string linkText,
            string actionName,
            string controllerName
        )
        {
            /*
            if (!Roles.IsUserInRole(roles))
            {
                return MvcHtmlString.Empty;
            }
             */
            return htmlHelper.ActionLink(linkText, actionName,controllerName);
        }
    }




}