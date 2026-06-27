
namespace Overlay.Core.Contents.Chats;

internal sealed class ChatMessageEmote(
    string code,
    string url
)
{
    internal string Code { get; set; } = code;
    internal string Url  { get; set; } = url;
}