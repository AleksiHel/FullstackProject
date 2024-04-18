using HtmlAgilityPack;
using System.Linq;
using System.Text;

public class HtmlTruncator
{
    public static string TruncateHtml(string html, int maxLength, string continuation = "...")
    {
        var doc = new HtmlDocument();
        doc.LoadHtml(html);
        StringBuilder truncatedHtml = new StringBuilder();
        int currentLength = 0;

        foreach (var node in doc.DocumentNode.ChildNodes)
        {
            string text = node.InnerText;
            if (currentLength + text.Length > maxLength)
            {
                int lengthNeeded = maxLength - currentLength;
                if (lengthNeeded > 0)
                {
                    if (node.Name == "#text")
                    {
                        truncatedHtml.Append(node.InnerText.Substring(0, lengthNeeded) + continuation);
                    }
                    else
                    {
                        truncatedHtml.Append($"<{node.Name}>{node.InnerText.Substring(0, lengthNeeded)}{continuation}</{node.Name}>");
                    }
                    break;
                }
            }
            else
            {
                if (node.Name == "#text")
                {
                    truncatedHtml.Append(node.InnerText);
                }
                else
                {
                    truncatedHtml.Append($"<{node.Name}>{node.InnerText}</{node.Name}>");
                }
                currentLength += text.Length;
            }
        }

        return truncatedHtml.ToString();
    }
}
