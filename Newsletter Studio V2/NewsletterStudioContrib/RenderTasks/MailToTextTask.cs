using NewsletterStudio.Services.RenderTasks;
using NewsletterStudioContrib.RenderTasks.Utils.Peche33Task;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace NewsletterStudioContrib.RenderTasks
{
    class MailToTextTask : RenderTask
    {
        public override void ProcessPreRender(RenderResult renderResult, RenderTaskParameters parameters)
        {
            renderResult.MessageBody = renderResult.MessageBody.Replace("unexpectedtoken", "");
        }


        public override void ProcessUniqueItem(RenderResult renderResult, RenderTaskUniqueItemParameters parameters)
        {
            HtmlToText converter = new HtmlToText();
            string originalHtml = renderResult.MessageBody;
            string textVersion = converter.ConvertHtml(originalHtml);

            AlternateView textView = AlternateView.CreateAlternateViewFromString(textVersion, Encoding.UTF8, MediaTypeNames.Text.Plain);
            parameters.MailMessage.AlternateViews.Add(textView);

        }
    }
}
