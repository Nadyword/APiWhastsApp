using Microsoft.AspNetCore.Mvc;
using TestAPIWhastasAppv2.Interfases;
using TestWhatsApp.Models;

namespace TestAPIWhastasAppv2.Controllers;

[ApiController]
[Route("WhatsApp")]
public class WhatsAppController : Controller
{
    private readonly IEnviarMensaje _enviarMensaje;
    public WhatsAppController(IEnviarMensaje enviarMensaje)
    {
        _enviarMensaje = enviarMensaje;   
    }
    [HttpGet("Sample")]
    public async Task<ActionResult> Sample()
    {
        var data = new
        {
            messaging_product = "whatsapp",
            recipient_type = "individual",
            to = "584241325210",
            type = "text",
            text = new
            {
                preview_url = false,
                body = "Esto es un test"
            }
        };

        var resul = await _enviarMensaje.Execute(data);
        return Ok();
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
        catch
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
