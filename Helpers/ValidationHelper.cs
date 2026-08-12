namespace AppPickingMinhasCompras.Helpers;

public static class ValidationHelper
{
    public static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    public static bool IsValidPrice(string priceText)
    {
        return decimal.TryParse(priceText, out var price) && price > 0;
    }

    public static bool IsValidQuantity(string quantityText)
    {
        return int.TryParse(quantityText, out var quantity) && quantity > 0;
    }

    public static bool IsNotNullOrEmpty(string text)
    {
        return !string.IsNullOrWhiteSpace(text);
    }

    public static string TruncateText(string text, int maxLength)
    {
        if (string.IsNullOrEmpty(text) || text.Length <= maxLength)
            return text;

        return text.Substring(0, maxLength) + "...";
    }
}
