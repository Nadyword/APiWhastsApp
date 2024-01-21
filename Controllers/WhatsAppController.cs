using Microsoft.AspNetCore.Mvc;
using TestAPIWhastasAppv2.Interfases;
using TestAPIWhastasAppv2.Model;
using TestWhatsApp.Models;

namespace TestAPIWhastasAppv2.Controllers;

[ApiController]
[Route("WhatsApp")]
public class WhatsAppController : Controller
{
    private readonly IEnviarMensaje _enviarMensaje;
    private readonly IMMensajes _mMensajes;

    public WhatsAppController(IEnviarMensaje enviarMensaje, IMMensajes mMensajes)
    {
        _mMensajes = mMensajes;
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
                if (NumeroUsu != null)
                {
                    var TextUser = GetUserText(mensaje);

                    object objetcMessage;

                    switch (TextUser.ToUpper())
                    {
                        case "TEXT":
                            objetcMessage = _mMensajes.Text(NumeroUsu, "false", "Enviaste un texto");
                            break;
                        case "IMAGE":
                            objetcMessage = _mMensajes.Image(NumeroUsu, "https://economipedia.com/wp-content/uploads/test-de-estr%C3%A9s.png");
                            break;
                        case "AUDIO":
                            objetcMessage = _mMensajes.Audio(NumeroUsu, "C:\\Users\\samue\\Downloads\\gospel-choir-heavenly-transition-3-186880.mp3");
                            break;
                        case "VIDEO":
                            objetcMessage = _mMensajes.Video(NumeroUsu, "C:\\Users\\samue\\Videos\\Captures\\WhatsApp 2023-12-19 12-23-36.mp4");
                            break;
                        case "DOCUMET":
                            objetcMessage = _mMensajes.Document(NumeroUsu, "https://editorial.fxstreet.com/miscelaneous/Patrones%20Fibonacci%20y%20de%20Andrews%20Pitchfork-637151234849184990.pdf");
                            break;
                        case "LOCATION":
                            objetcMessage = _mMensajes.Location(NumeroUsu, "39.72935980207274", "-104.98567798472007", "Summit Strong", "800 Lincoln St, Denver, CO 80203, Estados Unidos");
                            break;

                        default:
                            objetcMessage = _mMensajes.Text(NumeroUsu, "true", "Mensaje no compatible ver documentacion https://waapi.app/?gclid=Cj0KCQiA-62tBhDSARIsAO7twbaP244i_dOzMNe7wBPs2IsnOjlJQw7vPL4ntITj6x3Ab9n9unfsMyUaAksJEALw_wcB");
                            break;
                    }
                    await _enviarMensaje.Execute(objetcMessage);
                }
                return Ok("EVENT_RECEIVE");
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
