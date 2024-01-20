using Microsoft.AspNetCore.Mvc;
using TestWhatsApp.Models;

namespace TestAPIWhastasAppv2.Controllers;

[ApiController]
[Route("WhatsApp")]
public class WhatsAppController : Controller
{
    [HttpGet("Sample")]
    public ActionResult Sample()
    {
        return Ok("Todo bien");
    }

    [HttpGet("Tokken")]
    public IActionResult VerifyToken()
    {
        string AccessToker = "Tokken1234";
        var token = Request.Query["hup.verify_token"].ToString();
        var challenge = Request.Query["hup.challenge"].ToString();

        if (challenge != null && token != null && token == AccessToker)
        {
            return Ok(challenge);
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpPost("Resivir")]
    public async Task<IActionResult> ResicirMensaje([FromBody] WhatsAppCloud body)
    {
        try
        {
            var mensaje = body.Entry[0]?.Changes[0]?.Value.Messages[0];
            if (mensaje != null)
            {
                var NumeroUsu = mensaje.From;
                var TextUser = GetUserText(mensaje);
            }
            return Ok("EVENT_RECEIVE");
        }
        catch (Exception ex)
        {
            return Ok("EVENT_RECEIVE");
        }
    }

    private string GetUserText(Message message)
    {
        string TypeMessage = message.Type;
        if (TypeMessage.ToUpper() == "TEXT")
        {
            return message.Text.Body;
        }
        else if (TypeMessage.ToUpper() == "INTERACTIVE")
        {
            string interacticeType = message.Interactive.Type;

            if (interacticeType.ToUpper() == "LIST_REPLY")
            {
                return message.Interactive.List_Reply.Title;
            }
            else if (interacticeType.ToUpper() == "BUTTON_REPLY")
            {
                return message.Interactive.List_Reply.Title;
            }
            else
            {
                return string.Empty;
            }
        }
        else
        {
            return string.Empty;
        }
    }
}
