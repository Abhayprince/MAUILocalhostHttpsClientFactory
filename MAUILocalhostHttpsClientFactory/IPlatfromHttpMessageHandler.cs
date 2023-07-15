using System;
namespace MAUILocalhostHttpsClientFactory
{
    public interface IPlatformHttpMessageHandler
    {
        HttpMessageHandler GetHttpMessageHandler();
    }
}

