
namespace Overlay.Core.Contents.Chats;

internal sealed class ChatMessageEmote(
    string code,
    string url
)
{
    internal string Code { get; set; } = _ = code;
    internal string Url  { get; set; } = _ = url;
}