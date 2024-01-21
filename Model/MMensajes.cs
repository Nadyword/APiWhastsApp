using System.Net;

namespace TestAPIWhastasAppv2.Model;

public class MMensajes() : IMMensajes
{
    public object Text(string to, string preview_url, string body) => new
    {
        messaging_product = "whatsapp",
        recipient_type = "individual",
        to = to ?? "",
        type = "text",
        text = new
        {
            preview_url = preview_url ?? "false",
            body = body ?? ""
        }
    };

    public object Image(string To, string link) => new
    {
        messaging_product = "whatsapp",
        recipient_type = "individual",
        to = To,
        type = "image",
        text = new
        {
            link = link ?? ""
        }
    };


    public object Audio(string To, string link) => new
    {
        messaging_product = "whatsapp",
        recipient_type = "individual",
        to = To,
        type = "audio",
        audio = new
        {
            link = link ?? ""
        }
    };


    public object Video(string To, string link) => new
    {
        messaging_product = "whatsapp",
        recipient_type = "individual",
        to = To,
        type = "video",
        video = new
        {
            link = link ?? ""
        }
    };


    public object Document(string To, string link) => new
    {
        messaging_product = "whatsapp",
        recipient_type = "individual",
        to = To,
        type = "document",
        document = new
        {
            link = link ?? ""
        }
    };


    public object Location(string To, string Latitude, string Longitude, string Name, string Address) => new
    {
        messaging_product = "whatsapp",
        recipient_type = "individual",
        to = To,
        type = "location",
        location = new
        {
            latitude = Latitude ?? "",
            longitude = Longitude ?? "",
            name = Name ?? "",
            address = Address ?? ""
        }
    };


    public object Buttoms(string To, string titleButton) => new
    {
        messaging_product = "whatsapp",
        recipient_type = "individual",
        to = To,
        type = "interactive",
        interactive = new
        {
            type = "button",
            body = new
            {
                text = titleButton ?? ""
            },
            action = new
            {
                buttons = new List<object>
                {
                    new
                    {
                        type = "reply",
                        reply = new
                        {
                              id = "01",
                              title = "Comprar"
                        }
                    },
                    new
                    {
                        type = "reply",
                        reply = new
                        {
                            id = "02",
                            title = "Vender"
                        }
                    },
                    new
                    {
                        type = "reply",
                        reply = new
                        {
                            id = "03",
                            title = "No se"
                        }
                    }
                }
            }
        }
    };
}
