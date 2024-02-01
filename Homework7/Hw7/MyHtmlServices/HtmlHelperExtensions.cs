using System.Reflection;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Hw7.MyHtmlServices;

public static class HtmlHelperExtensions
{
    public static IHtmlContent MyEditorForModel(this IHtmlHelper helper)
    {
        HtmlContentBuilder content = new HtmlContentBuilder();
        var modelExplorer = helper.ViewData.ModelExplorer;
        var modelProperties = modelExplorer.ModelType.GetProperties();
        
        foreach (var property in modelProperties)
            content.AppendHtml(GenerateField(property, modelExplorer.Model));

        return content;
    }

    private static IHtmlContent GenerateField(PropertyInfo property, object model)
    {
        TagBuilder html = new ("div"), input = new ("input");

        html.InnerHtml.AppendHtml(SetPropertyNameLabel(property));
        html.InnerHtml.AppendHtml("<br>");
        html.InnerHtml.AppendHtml(ValidateFields(property, model));
        html.InnerHtml.AppendHtml("<br>");
        if (property.PropertyType.IsEnum)
            html.InnerHtml.AppendHtml(GenerateEnumList(property));
        else
        {
            
            if (property.PropertyType == typeof(int))
                input.Attributes.Add("type", "number");
            else
                input.Attributes.Add("type", "text");
            
            input.Attributes.Add("name", property.Name);
            input.Attributes.Add("id", property.Name);
            html.InnerHtml.AppendHtml(input);
        }
            
        return html; 
    }

    private static IHtmlContent SetPropertyNameLabel(PropertyInfo property)
    {
        TagBuilder html = new("label");
        var displayAttribute = property.GetCustomAttribute<DisplayAttribute>();
        html.Attributes.Add("for", property.Name);
        if (displayAttribute == null)
            html.InnerHtml.AppendHtml(SeparateCamelCaseString(property.Name));
        else
            html.InnerHtml.AppendHtml(displayAttribute.Name);

        return html;
    }

    private static IHtmlContent GenerateEnumList(PropertyInfo property)
    {
        var html = new TagBuilder("select");
        html.Attributes.Add("id", property.Name);
        foreach(var value in property.PropertyType.GetEnumValues())
        {
            var listElement = new TagBuilder("option");
            listElement.InnerHtml.AppendHtml(value.ToString());
            html.InnerHtml.AppendHtml(listElement);
        }
        
        return html;
    }

    private static IHtmlContent ValidateFields(PropertyInfo property, object model)
    {
        if (model == null)
            return new HtmlString(String.Empty);
        
        var htmlContent = new TagBuilder("span");
        htmlContent.Attributes.Add("id", property.Name);
        var validationAttributes = property.GetCustomAttributes(typeof(ValidationAttribute), true);

        foreach (ValidationAttribute validationAttribute in validationAttributes)
        {
            if (!validationAttribute.IsValid(property.GetValue(model)))
                htmlContent.InnerHtml.AppendHtml(validationAttribute.ErrorMessage!);
        }

        return htmlContent;
    }
    
    private static string SeparateCamelCaseString(string input)
    {
        string parsedString = "";

        for (int i = 0; i < input.Length; i++)
        {
            if (char.IsUpper(input[i]) && i > 0)
                parsedString += " " + input[i];
            else
                parsedString += input[i];
        }

        return parsedString;
    }
} 