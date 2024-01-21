using System.Net;

namespace TestAPIWhastasAppv2.Model;

public interface IMMensajes
{
    public object Text(string to, string preview_url, string body);

    public object Image(string To, string link);

    public object Audio(string To, string link);

    public object Video(string To, string link);

    public object Document(string To, string link);

    public object Location(string To, string Latitude, string Longitude, string Name, string Address);

    public object Buttoms(string To, string titleButton);
}