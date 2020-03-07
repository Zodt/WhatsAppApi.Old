#if !NETSTANDARD || !NETSTANDARD2_0
namespace xNet
{
    /// <summary>
    /// Тип прокси-сервера.
    /// </summary>
    public enum ProxyType
    {
        Http,
        Socks4,
        Socks4a,
        Socks5,
        Chain
    }
}
#endif