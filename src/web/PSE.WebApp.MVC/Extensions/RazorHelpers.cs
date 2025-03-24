using Microsoft.AspNetCore.Mvc.Razor;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace PSE.WebApp.MVC.Extensions;

public static class RazorHelpers
{
    public static string HashEmailForGravatar(this RazorPage page, string email)
    {
        var md5Hasher = MD5.Create();
        var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(email));
        var sBuilder = new StringBuilder();
        foreach (var t in data)
        {
            sBuilder.Append(t.ToString("x2"));
        }
        return sBuilder.ToString();
    }

    public static string FormatCurrency(this RazorPage page, decimal value)
    {
        return value > 0 ? string.Format(Thread.CurrentThread.CurrentCulture, "{0:C}", value) : "Free";
    }

    public static string StockMessage(this RazorPage page, int quantity)
    {
        return quantity > 0 ? $"Only {quantity} in stock!" : "Out of stock!";
    }

    public static string UnitsByProduct(this RazorPage page, int units)
    {
        return units > 1 ? $"{units} units" : $"{units} unit";
    }

    public static string SelectOptionsByQuantity(this RazorPage page, int quantity, int valueSelect = 0)
    {
        var sb = new StringBuilder();
        for (var i = 1; i <= quantity; i++)
        {
            var selected = "";
            if (i == valueSelect) selected = "selected";
            sb.Append($"<option {selected} value='{i}'>{i}</option>");
        }

        return sb.ToString();
    }

    public static string UnitsBtProductTotalValue(this RazorPage page, int units, decimal value)
    {
        return $"{units}x {FormatCurrency(page, value)} = Total: {FormatCurrency(page, value * units)}";
    }

    public static string DisplayStatus(this RazorPage page, int status)
    {
        var statusMessage = "";
        var statusClasse = "";

        switch (status)
        {
            case 1:
                statusClasse = "info";
                statusMessage = "In approval";
                break;
            case 2:
                statusClasse = "primary";
                statusMessage = "Approved";
                break;
            case 3:
                statusClasse = "danger";
                statusMessage = "Declined";
                break;
            case 4:
                statusClasse = "success";
                statusMessage = "Delivered";
                break;
            case 5:
                statusClasse = "warning";
                statusMessage = "Canceled";
                break;
        }

        return $"<span class='badge badge-{statusClasse}'>{statusMessage}</span>";
    }
}
