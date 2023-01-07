using Connector.Interfaces;

namespace Connector.Services;

public class GuidProvider:IGuidProvider
{
    public Guid NewGuid()
    {
        return Guid.NewGuid();
    }
}