
namespace Overlay.Core.Contents.Chats;

internal readonly struct ChatMessageEmote(
    string code,
    string url
)
{
    internal string Code { get; } = _ = code;
    internal string Url  { get; } = _ = url;
}