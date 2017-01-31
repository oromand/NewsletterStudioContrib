using NewsletterStudio.Services.RenderTasks;
using NewsletterStudioContrib.RenderTasks.Utils.Peche33Task;
using System;

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
            Guid boundaryGuid = new Guid();

            HtmlToText converter = new HtmlToText();
            string originalHtml = renderResult.MessageBody;
            string textVersion = converter.ConvertHtml(originalHtml);

            parameters.MailMessage.Headers["Content-Type"] = "multipart/alternative;boundary=" + boundaryGuid;

            renderResult.MessageBody = "\r\n\r\n" + boundaryGuid + "\r\n\r\n";

            renderResult.MessageBody += "Content-Type: text/plain; charset=utf-8\r\n\r\n";
            //renderResult.MessageBody += "Content-Transfer-Encoding: base64\r\n";
            renderResult.MessageBody += "\r\n" + textVersion + "\r\n";
            renderResult.MessageBody += "\r\n\r\n--" + boundaryGuid + "\r\n";

            renderResult.MessageBody += "Content-Type: text/html; charset=utf-8\r\n\r\n";
            //renderResult.MessageBody += "Content-Transfer-Encoding: base64\r\n";
            renderResult.MessageBody += "\r\n" + originalHtml + "\r\n";
            renderResult.MessageBody += "\r\n\r\n--" + boundaryGuid + "\r\n";
        }
    }
}
